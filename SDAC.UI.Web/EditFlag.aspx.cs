using System;
using System.Data;
using System.Collections.Generic;
using SDAC.DomainModel;
using System.Web.UI.WebControls;
using SDAC.UI.Web.Enums;
using SDAC.Core;
using Newtonsoft.Json.Linq;

namespace SDAC.UI.Web
{
	public partial class Edit : System.Web.UI.Page
	{
		protected SLC_SDACEntities _sDACEntities = new SLC_SDACEntities();
		protected User _user = null;
		protected SqlHelper _sqlHelper = null;
		protected int _conditionId;
		protected int _flagId;
		protected int _flagIdForPage;
		protected inBloomApi _inBloomApi = null;

		protected void Page_Load(object sender, EventArgs e)
		{
			_sqlHelper = new SqlHelper();

			_inBloomApi = new inBloomApi(Session[SessionEnum.AccessToken.ToString()].ToString());
			
			if (!IsPostBack)
			{
				_sqlHelper = new SqlHelper();
				LoadDataDomainList();
				LoadGridWithAttribute();
				lstCondition.DataSource = _sDACEntities.Conditions;
				lstCondition.DataBind();

				_user = (User)Session["UserDetail"];

				if (!_user.IsAdminUser)
					PanelFlagType.Visible = false;

				if (Session["EditFlag"] != null)
				{
					try
					{
						int FlagId = Convert.ToInt16(Session["EditFlag"]);
						Session.Add("FlagIdForEditPage", FlagId);
						_flagIdForPage = FlagId;

						IList<Flag> _Flag = _sqlHelper.GetFlag(FlagId);

						txtFlagName.Text = _Flag[0].FlagName;
						txtDescription.Text = _Flag[0].FlagDescription;
						txtFlag.Text = _Flag[0].Keyword;

						String IsPublic = _Flag[0].IsPublic.ToString();

						if (IsPublic == "True")
						{
							radioFlagType.SelectedIndex = 0;
						}
						else
						{
							radioFlagType.SelectedIndex = 1;
						}

						lstCondition.SelectedValue = _Flag[0].ConditionId.ToString();

						if (_Flag[0].ConditionId.ToString() != null && _Flag[0].ConditionId.ToString() != "")
						{
							condition.Style.Add("display", "block");
							condition.Text = lstCondition.SelectedItem.Text;
						}

						txtSetVal.Text = _Flag[0].ValueSet1;
						txtSetVal2.Text = _Flag[0].ValueSet2;

						if (_Flag[0].ValueSet1 != null && _Flag[0].ValueSet1 != "")
						{
							val1.Style.Add("display", "block");
							val1.Text = _Flag[0].ValueSet1;
						}

						if (_Flag[0].ValueSet2 != null && _Flag[0].ValueSet2 != "")
						{
							val2.Style.Add("display", "block");
							val2.Text = " & " + _Flag[0].ValueSet2;
						}

						_conditionId = _Flag[0].ConditionId;
						if (condition.Text == "Is between" || condition.Text == "Is not between")
						{
							txtSetVal2.Style.Add("display", "block");
						}

						hdnDataElementId.Value = _Flag[0].DataElementId.ToString();

						int DataElementId = _Flag[0].DataElementId;
						hdnDataType.Value = _sqlHelper.GetDataTypeByDataElementId(DataElementId);

						String EntityName = _sqlHelper.GetDataDomainNameByDataElementId(DataElementId);

						// LoadEntityList();
						LoadDataDomainList();
						DropDownListEntity.SelectedItem.Text = EntityName;
						LoadGridWithAttribute();

						SelectRow(_Flag[0].DataElementId);


						// process to call run flag

						String Entity = _sqlHelper.GetEntityNameByDataElementId(DataElementId);
						String FieldName = _sqlHelper.GetFieldNameByDataElementId(DataElementId);
						String DataType = _sqlHelper.GetDataTypeByDataElementId(DataElementId);
						bool ResponseType = false;
						_user = (User)Session["UserDetail"];
						String UserId = _user.ExternalId;
						String SchoolId = Session[SessionEnum.SchoolId.ToString()].ToString();
						String CourseId = Session[SessionEnum.CourseId.ToString()].ToString();
						String SectionId = Session[SessionEnum.SectionId.ToString()].ToString();
						String Value1 = txtSetVal.Text;
						String Value2 = txtSetVal2.Text;

					   // String ExternalField = "";
						bool IsPreview = true;

						DataTable dt = _inBloomApi.RunFlag(FieldName, DataType, ResponseType, UserId, SchoolId, CourseId, SectionId, _conditionId, Value1, Value2, Entity, IsPreview);

						gridDisplayResult.DataSource = dt;
						gridDisplayResult.DataBind();
						gridDisplayResult.HeaderRow.Cells[1].Text =_inBloomApi.GetWellFormattedString(char.ToUpper(Entity[0]) + Entity.Substring(1))+"."+_inBloomApi.GetWellFormattedString(FieldName);
						entity.Text = _inBloomApi.GetWellFormattedString(char.ToUpper(Entity[0]) + Entity.Substring(1)) + "." + _inBloomApi.GetWellFormattedString(FieldName); ;

					}
					catch (Exception ex)
					{
					}
				}
				else
				{
					Response.Redirect("Search.aspx");
				}
			}
		}

		public void SelectRow(int dataElementId)
		{
			try
			{
                foreach (GridViewRow row in GridResult.Rows)
				{
					if (Convert.ToInt16(row.Cells[0].Text) == dataElementId)
					{
						//row.Style.Add("background-color", "#C4E667");
					   // row.Attributes.Add("class", "selectedBgColor");
                        GridResult.Rows[row.RowIndex].CssClass = "selectedBgColor";
						autoComplete.Text = row.Cells[1].Text;
						hdDatatpe.Value = row.Cells[3].Text;
						entity.Style.Add("display", "block");
						//entity.Text = row.Cells[1].Text;
						gridDisplayResult.HeaderRow.Cells[1].Text = row.Cells[1].Text;
						break;
					}
				}
			}
			catch (Exception Ex)
			{
			}
		}

		protected void btnSave_Click(object sender, EventArgs e)
		{
			bool FlagValue = false;

				SqlHelper _sqlHelper = new SqlHelper();

				if (Session["UserDetail"] != null)
				{
					_user = (User)Session["UserDetail"];

					if (_user.IsAdminUser)
					{
						if (radioFlagType.SelectedItem.Text == "Public")
						{
							FlagValue = true;
						}
						else
						{
							FlagValue = false;
						}
					}

					bool IsFlagAdded = false;
					int FlagId = Convert.ToInt16(Session["EditFlag"]);
					if (Request.Form["ctl00$ContentPlaceHolder1$hdnDataElementId"] != null)
					{
						//int FlagId = Convert.ToInt16(Session["EditFlag"]);
						IsFlagAdded = _sqlHelper.UpdateFlag(FlagId, txtFlagName.Text.Trim(), txtDescription.Text.Trim(), txtFlag.Text.Trim(), Convert.ToInt16(Request.Form["ctl00$ContentPlaceHolder1$hdnDataElementId"]),
						Convert.ToInt16(lstCondition.SelectedItem.Value), txtSetVal.Text.Trim(), txtSetVal2.Text.Trim(), _user.ExternalId, _user.FullName, FlagValue
						);

					}
					if (IsFlagAdded)
					{
						//successfully update
					   
						JArray _links = (JArray)Session["HomeLinks"];

						String UserCustomLink = _inBloomApi.GetCustomLink(_links);
						String EducationOrganizationId = Session["EducationOrganizationId"].ToString();

						String FlagUserId = Session["CustomUserId"].ToString();
						bool IsAdminUser = (bool)Session["CustomIsAdminUser"];
						bool IsPublic = (bool)Session["CustomIsPublic"];

						IList<Flag> _flag = (IList<Flag>)_sqlHelper.GetFlag(FlagId);

						FlagCls[] _flagCls = new FlagCls[1];
						_flagCls[0] = new FlagCls();
						_flagCls[0].FlagId = _flag[0].FlagId;
						_flagCls[0].FlagName = _flag[0].FlagName;
						_flagCls[0].FlagDescription = _flag[0].FlagDescription;
						_flagCls[0].FlagKeyword = _flag[0].Keyword;
						_flagCls[0].IsPublic = _flag[0].IsPublic;
						_flagCls[0].IsFavorite = _flag[0].IsFavorite;
						//_flagCls[0].IsDeleted =(bool) _flag[0].IsDeleted;
						_flagCls[0].ConditionId = _flag[0].ConditionId;
						_flagCls[0].DataElementId = _flag[0].DataElementId;
						_flagCls[0].ValueSet1 = _flag[0].ValueSet1;
						_flagCls[0].ValueSet2 = _flag[0].ValueSet2;
						_flagCls[0].UserId = _flag[0].UserId;
						_flagCls[0].CreatedBy = _flag[0].CreatedBy;
						_flagCls[0].CreatedDate = _flag[0].CreatedDate;
						_flagCls[0].ModifiedBy = _user.ExternalId;
						_flagCls[0].ModifiedDate = DateTime.Now;

						_inBloomApi.UpateFlag(FlagId, UserCustomLink, EducationOrganizationId, IsAdminUser, IsPublic, FlagUserId, _flagCls, _user);

						if (Session["EditFlag"] != null)
						{
							// flag is updated 
							Session["EditFlag"] = null;
						}
					 
						Session.Add("Success", "Flag edited successfully.");
						Session["FlagIdForEditPage"] = null;
						Response.Redirect("Search.aspx");
					}
					else
					{
						Session["Success"] = "A flag with the name " + txtFlagName.Text.ToString() + " already exists.  Please enter a different name.";
						Session["EditFlag"] = Session["FlagIdForEditPage"].ToString();
						Response.Redirect("EditFlag.aspx");
					}
				}
				else
				{
					Response.Redirect("Search.aspx");
				}
		}

        protected void GridResult_RowCreated(object sender, GridViewRowEventArgs e)
		{
			_sqlHelper = new SqlHelper();
			string RowID = String.Empty;
			if (e.Row.RowType == DataControlRowType.DataRow)
			{
				GridViewRow rd = e.Row;
				int RowIndex = e.Row.RowIndex;
				String FieldName = rd.Cells[1].Text;
				int count = 1;
				String Res = "";
				String Res1 = "";
				String ExternalEntity = rd.Cells[4].Text;
				ExternalEntity = ExternalEntity.Replace("&nbsp;", "");

                int dataElementId = (int)GridResult.DataKeys[RowIndex].Value;
				String entity = _sqlHelper.GetEntityNameByDataElementId(dataElementId);

				if (!FieldName.Equals(""))
				{
					if (!ExternalEntity.Equals(""))
						e.Row.Cells[1].Text = _inBloomApi.GetWellFormattedString(char.ToUpper(entity[0]) + entity.Substring(1)) + ". " + _inBloomApi.GetWellFormattedString(ExternalEntity) + ". " + _inBloomApi.GetWellFormattedString(FieldName);
					else
						e.Row.Cells[1].Text = _inBloomApi.GetWellFormattedString(char.ToUpper(entity[0]) + entity.Substring(1)) + ". " + _inBloomApi.GetWellFormattedString(FieldName);
				}

				RowID = "row" + e.Row.RowIndex;
				e.Row.Attributes.Add("id", "row" + e.Row.RowIndex);
				e.Row.Attributes.Add("onclick", "ChangeRowColor(" + "'" + RowID + "'" + ")");
			}
		}


        protected void DropDownListEntity_SelectedIndexChanged(object sender, EventArgs e)
        {
            autoComplete.Text = "";
            hdnDataElementId.Value = "";
            hdnDataType.Value = "";

            DataTable dt = new DataTable();
            dt.Columns.Add("student_Name");
            dt.Columns.Add("GPA");

            for (int i = 0; i < 5; i++)
            {

                dt.Rows.Add((new object[] { "", "" }));
            }

            gridDisplayResult.DataSource = dt;
            gridDisplayResult.DataBind();

            LoadGridWithAttribute();
        }

    

		public void LoadDataDomainList()
		{
			DropDownListEntity.Items.Clear();
			IList<String> _dataElement = _sqlHelper.GetAllDataDomainFromDataElement();
			if (_dataElement.Count > 0)
			{
				for (int i = 0; i < _dataElement.Count; i++)
				{
					DropDownListEntity.Items.Add(_dataElement[i]);
				}
			}
		}

		public void LoadGridWithAttribute()
		{
			String dataDomain = DropDownListEntity.SelectedItem.Text;
			if (!string.IsNullOrEmpty(dataDomain))
			{
                GridResult.DataSource = _sqlHelper.GetAllDataElementByDataDomain(dataDomain);
                GridResult.DataBind();
			}
		}

		protected void btnPreview_Click(object sender, EventArgs e)
		{
			try
			{
				int DataElementId = Convert.ToInt16(Request.Form["ctl00$ContentPlaceHolder1$hdnDataElementId"]);
				int ConditionId = Convert.ToInt16(lstCondition.SelectedItem.Value);
			   
				String Entity = _sqlHelper.GetEntityNameByDataElementId(DataElementId);
				String FieldName = _sqlHelper.GetFieldNameByDataElementId(DataElementId);
				String DataType = _sqlHelper.GetDataTypeByDataElementId(DataElementId);
				bool ResponseType = false;
				_user = (User)Session["UserDetail"];
				String UserId = _user.ExternalId;
				String SchoolId = Session[SessionEnum.SchoolId.ToString()].ToString();
				String CourseId = Session[SessionEnum.CourseId.ToString()].ToString();
				String SectionId = Session[SessionEnum.SectionId.ToString()].ToString();
				String Value1 = txtSetVal.Text;
				String Value2 = txtSetVal2.Text;

				//String ExternalField = "";
				bool IsPreview = true;
				condition.Text = lstCondition.SelectedItem.Text;
				val1.Text = txtSetVal.Text;
				val2.Text = txtSetVal2.Text;

				DataTable dt = _inBloomApi.RunFlag(FieldName, DataType, ResponseType, UserId, SchoolId, CourseId, SectionId, ConditionId, Value1, Value2, Entity, IsPreview);
				
				gridDisplayResult.DataSource = dt;
				gridDisplayResult.DataBind();
				gridDisplayResult.HeaderRow.Cells[1].Text = FieldName;
			}
			catch (Exception ex)
			{
			}
		}
	}
}