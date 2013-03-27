using System;
using System.Data;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using SDAC.DomainModel;
using SDAC.UI.Web.Enums;
using SDAC.Core;
using Newtonsoft.Json.Linq;

namespace SDAC.UI.Web
{
	public partial class CopyFlag : System.Web.UI.Page
	{
		protected SLC_SDACEntities _sDACEntities = new SLC_SDACEntities();
		protected SqlHelper _sqlHelper = null;
		protected User _user = null;
		protected int _flagIdForCopyPage;
		protected inBloomApi _inBloomApi = null;
		
		protected void Page_Load(object sender, EventArgs e)
		{
			_sqlHelper = new SqlHelper();
			_inBloomApi = new inBloomApi(Session[SessionEnum.AccessToken.ToString()].ToString());
			
			if (!IsPostBack)
			{
				try
				{
					LoadDataDomainList();
					LoadGridWithAttribute();

					lstCondition.DataSource = _sDACEntities.Conditions;
					lstCondition.DataBind();

					_user = (User)Session["UserDetail"];

					if (!_user.IsAdminUser)
						PanelFlagType.Visible = false;

					#region Retrieving The Information For Copy Flag

					if (Session["CopyFlag"] != null)
					{
						try
						{

							_sqlHelper = new SqlHelper();
							int FlagId = Convert.ToInt16(Session["CopyFlag"].ToString());
							Session.Add("FlagIdForCopyPage", FlagId);

							Session["CopyFlag"] = null;

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
								val2.Text = _Flag[0].ValueSet2;
							}



							int ConditionId = _Flag[0].ConditionId;
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

							// int DataElementId = _Flag[0].DataElementId;                 
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

							DataTable dt = _inBloomApi.RunFlag(FieldName, DataType, ResponseType, UserId, SchoolId, CourseId, SectionId, ConditionId, Value1, Value2, Entity, IsPreview);

							gridDisplayResult.DataSource = dt;
							gridDisplayResult.DataBind();
							gridDisplayResult.HeaderRow.Cells[1].Text = _inBloomApi.GetWellFormattedString(char.ToUpper(Entity[0]) + Entity.Substring(1)) + "." + _inBloomApi.GetWellFormattedString(FieldName);
							entity.Text = _inBloomApi.GetWellFormattedString(char.ToUpper(Entity[0]) + Entity.Substring(1)) + "." + _inBloomApi.GetWellFormattedString(FieldName); ;
							_inBloomApi = new inBloomApi(Session[SessionEnum.AccessToken.ToString()].ToString());

						}
						catch (Exception Ex)
						{
						   
						}
						}
						else
						{
							// Response.Redirect("Search.aspx");
						}

					#endregion
				}
				catch (Exception ex)
				{
				}
			}
		}
		protected void btnSave_Click(object sender, EventArgs e)
		{
			try
			{
				bool flagValue = false;

				SqlHelper _sqlHelper = new SqlHelper();

				if (Session["UserDetail"] != null)
				{
					_user = (User)Session["UserDetail"];


					if (_user.IsAdminUser)
						flagValue = radioFlagType.SelectedItem.Text == "Public";

					bool IsFlagAdded = false;

					if (Request.Form["ctl00$ContentPlaceHolder1$hdnDataElementId"] != null)
					{
						IsFlagAdded = _sqlHelper.AddFlag(txtFlagName.Text.Trim(), txtDescription.Text.Trim(), txtFlag.Text.Trim(), Convert.ToInt16(Request.Form["ctl00$ContentPlaceHolder1$hdnDataElementId"]),
						Convert.ToInt16(lstCondition.SelectedItem.Value), txtSetVal.Text.Trim(), txtSetVal2.Text.Trim(), _user.ExternalId, _user.FullName, flagValue
					   );
					}

					if (IsFlagAdded)
					{
						JArray _links = (JArray)Session["HomeLinks"];
						int FlagId = _sqlHelper.GetFlagId(txtFlagName.Text.Trim());

						String GetCustomLink = _inBloomApi.GetCustomLink(_links);
						String EducationOrganizationId = Session["EducationOrganizationId"].ToString();


						FlagCls[] _flagCls = new FlagCls[1];
						_flagCls[0] = new FlagCls();
						_flagCls[0].FlagId = FlagId;
						_flagCls[0].FlagName = txtFlagName.Text.Trim();
						_flagCls[0].FlagDescription = txtDescription.Text.Trim();
						_flagCls[0].FlagKeyword = txtFlag.Text.Trim();
						_flagCls[0].IsPublic = false;
						_flagCls[0].IsFavorite = false;
						_flagCls[0].IsDeleted = false;
						_flagCls[0].ConditionId = Convert.ToInt16(lstCondition.SelectedItem.Value);
						_flagCls[0].DataElementId = Convert.ToInt16(Request.Form["ctl00$ContentPlaceHolder1$hdnDataElementId"]);
						_flagCls[0].ValueSet1 = txtSetVal.Text.Trim();
						_flagCls[0].ValueSet2 = txtSetVal2.Text.Trim();
						_flagCls[0].UserId = _user.ExternalId;
						_flagCls[0].CreatedBy = _user.ExternalId;
						_flagCls[0].CreatedDate = DateTime.Now;
						_flagCls[0].ModifiedBy = _user.ExternalId;
						_flagCls[0].ModifiedDate = DateTime.Now;

						if (_user.IsAdminUser && flagValue == true)
						{
							// process to store the public flag into organization
							_flagCls[0].IsPublic = true;
							_inBloomApi.AddFlagIntoEducationOrganization(_user, EducationOrganizationId, _flagCls);
						}
						else
						{
							// process to add flag 
							_inBloomApi.AddFlagsIntoCustom(GetCustomLink, _user, _flagCls);
						}

						//successfully added
						Session["CopyFlag"] = null;
						Session["Success"]="Flag copied Successfully";
						Session["FlagIdForCopyPage"] = null;
						Response.Redirect("Search.aspx");
					}
					else
					{
						Session["Success"] = "A flag with the name " + txtFlagName.Text.ToString() + " already exists.  Please enter a different name.";
						Session["CopyFlag"] = Session["FlagIdForCopyPage"].ToString();
						Response.Redirect("CopyFlag.aspx");
					}
				}
			}
			catch (Exception ex)
			{
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
                        GridResult.Rows[row.RowIndex].CssClass = "selectedBgColor";
						//row.Attributes.Add("class", "selectedBgColor");
						autoComplete.Text = row.Cells[1].Text;
						hdndttype.Value = row.Cells[3].Text;
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
					{
						e.Row.Cells[1].Text = _inBloomApi.GetWellFormattedString(char.ToUpper(entity[0]) + entity.Substring(1)) + ". " + _inBloomApi.GetWellFormattedString(ExternalEntity) + ". " + _inBloomApi.GetWellFormattedString(FieldName);
					}
					else
					{
						e.Row.Cells[1].Text = _inBloomApi.GetWellFormattedString(char.ToUpper(entity[0]) + entity.Substring(1)) + ". " + _inBloomApi.GetWellFormattedString(FieldName);
					}
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

		protected void btnPreview_Click(object sender, EventArgs e)
		{
			try
			{
				_user = (User)Session["UserDetail"];

				int DataElementId = Convert.ToInt16(Request.Form["ctl00$ContentPlaceHolder1$hdnDataElementId"]);
				int ConditionId = Convert.ToInt16(lstCondition.SelectedItem.Value);
				string Entity = _sqlHelper.GetEntityNameByDataElementId(DataElementId);
				string FieldName = _sqlHelper.GetFieldNameByDataElementId(DataElementId);
				string DataType = _sqlHelper.GetDataTypeByDataElementId(DataElementId);
				bool ResponseType = false;
				string UserId = _user.ExternalId;
				string SchoolId = Session[SessionEnum.SchoolId.ToString()].ToString();
				string CourseId = Session[SessionEnum.CourseId.ToString()].ToString();
				string SectionId = Session[SessionEnum.SectionId.ToString()].ToString();
				string Value1 = txtSetVal.Text;
				string Value2 = txtSetVal2.Text;

				string ExternalField = "";
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

		public void LoadDataDomainList()
		{
			DropDownListEntity.Items.Clear();
			IList<String> dataElements = _sqlHelper.GetAllDataDomainFromDataElement();
			
			if (dataElements.Count > 0)
				foreach (string elem in dataElements)
					DropDownListEntity.Items.Add(elem);
		}

		public void LoadGridWithAttribute()
		{
			string dataDomain = DropDownListEntity.SelectedItem.Text;
			if (!string.IsNullOrEmpty(dataDomain))
			{
                GridResult.DataSource = _sqlHelper.GetAllDataElementByDataDomain(dataDomain);
                GridResult.DataBind();
			}
		}
	}
}
