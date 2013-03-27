using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using SDAC.DomainModel;
using System.IO;
using SDAC.UI.Web.Enums;
using SDAC.Core;
using Newtonsoft.Json.Linq;

namespace SDAC.UI.Web
{
	public partial class Result : Page
	{
		protected inBloomApi _inBloomApi = null;
		protected SqlHelper _sqlHelper = null;
		protected IList<Flag> _flag = null;

		protected int _flagId;
		protected String _flagType = "";
		protected String _fieldName = "";
		protected String _dataType = "";
		protected bool _responseType = false;
		protected User _user = null;
		protected String _schoolId = "";
		protected String _courseId = "";
		protected String _sectionId = "";
		protected int _conditionId;
		protected String _value1, _value2;
		protected String _entityName = "";
		protected bool _isPreview = false;
		protected String _flagUser = "";
		protected String _flagName = "";
		protected String _flagDescription = "";


		protected void Page_Load(object sender, EventArgs e)
		{
			_inBloomApi = new inBloomApi();
			_sqlHelper = new SqlHelper();
			_user = (User) Session["UserDetail"];

			try
			{
				_inBloomApi.AccessToken = Session[SessionEnum.AccessToken.ToString()].ToString();
			}
			catch (Exception ex)
			{
				Response.Redirect("Search.aspx");
			}

			if (!IsPostBack)
			{
				try
				{
					if (!IsPostBack)
					{
						if (Session[SessionEnum.SchoolId.ToString()] == null)
						{
						}
						else
						{
							LoadFieldNames();

							_schoolId = Session[SessionEnum.SchoolId.ToString()].ToString();
							_courseId = Session[SessionEnum.CourseId.ToString()].ToString();
							_sectionId = Session[SessionEnum.SectionId.ToString()].ToString();

							_user = (User) Session["UserDetail"];

							if (Session["FlagId"] == null || Session["FlagType"] == null)
							{
								// show result for school and courses only.
								Response.Redirect("Search.aspx");
							}
							else
							{
								_flagId = Convert.ToInt16(Session["FlagId"].ToString());
								_flagType = Session["FlagType"].ToString();

								if (_flagType.Equals("Flag"))
								{
									// get all the values for flag
									IList<Flag> _flag = _sqlHelper.GetFlag(_flagId);

									lblFlagName.Text = _flag[0].FlagName;
									lblFlagDescription.Text = _flag[0].FlagDescription;

									if (_flag.Count > 0)
									{
										int DataElementId = _flag[0].DataElementId;
										int ConditionId = _flag[0].ConditionId;
										_value1 = _flag[0].ValueSet1;
										_value2 = _flag[0].ValueSet2;
										_flagUser = _flag[0].UserId;

										_dataType = _sqlHelper.GetDataTypeByDataElementId(DataElementId);
										_fieldName = _sqlHelper.GetFieldNameByDataElementId(DataElementId);
										_entityName = _sqlHelper.GetEntityNameByDataElementId(DataElementId);

										DataTable dt = _inBloomApi.RunFlag(_fieldName, _dataType, _responseType, _flagUser, _schoolId, _courseId, _sectionId, ConditionId, _value1, _value2, _entityName, _isPreview);

										Session.Add("FieldName", _fieldName);
										gvResult.DataSource = dt;
										gvResult.DataBind();

										//gvResult.HeaderRow.Cells[3].Text = _inBloomApi.GetWellFormattedString(FieldName);
										for (int i = 0; i < gvResult.HeaderRow.Cells.Count; i++)
										{
											String HeaderText = gvResult.HeaderRow.Cells[i].Text;
											if (HeaderText == "FieldName")
											{

												gvResult.HeaderRow.Cells[i].Text = _inBloomApi.GetWellFormattedString(char.ToUpper(_entityName[0]) + _entityName.Substring(1)) + "." + _inBloomApi.GetWellFormattedString(_fieldName);

												break;
											}
										}

										HideColumnInitial();

										Session.Add("ResultGrid", dt);

										if (_sqlHelper.IsFav(_flagId) || _sqlHelper.IsInPublicFavorite(_flagId, _user.ExternalId))
										{
											btnlnkfavorite.Text = "Remove Favorite";
											btnlnkfavorite.CssClass = "";
										}
										else
										{
											btnlnkfavorite.Text = "Add to Favorite";
											btnlnkfavorite.CssClass = "unfavorite_result";
										}
									}
								}
								else if (_flagType.Equals("AggregateFlag"))
								{
									try
									{
										IList<AggregateFlag> _aggregateFlag = _sqlHelper.GetAggregateFlag(_flagId);

										lblFlagName.Text = _aggregateFlag[0].AggregateFlagName;
										lblFlagDescription.Text = _aggregateFlag[0].AggregateFlagDescription;

										int[] FlagArrayId = _sqlHelper.GetAllFlagIdOfAggregateFlagByAggregateFlagId(_flagId);

										DataTable dt = _inBloomApi.RunAggregateFlag(FlagArrayId, _schoolId, _courseId, _sectionId);

										GridViewAggregateFlag.DataSource = dt;
										Session.Add("ResultGrid", dt);
										GridViewAggregateFlag.DataBind();
										for (int i = 0; i < GridViewAggregateFlag.HeaderRow.Cells.Count; i++)
										{
											//GridViewAggregateFlag.HeaderRow.Cells[i].Text = _inBloomApi.GetWellFormattedString(GridViewAggregateFlag.HeaderRow.Cells[i].Text);
										}

										HideColumnInitialForAggregate();

										//GridViewAggregateFlag.HeaderRow.Cells[0].Visible = false;

										if (_sqlHelper.IsAggregateIsFav(_flagId) || _sqlHelper.IsAggregateInPublicFavorite(_flagId, _user.ExternalId))
										{
											btnlnkfavorite.Text = "Remove Favorite";
											btnlnkfavorite.CssClass = "";
											btnlnkfavorite.Attributes.Add("onclick", "showunfavmsg();");

										}
										else
										{
											btnlnkfavorite.Text = "Add to Favorite";
											btnlnkfavorite.CssClass = "unfavorite_result";
											btnlnkfavorite.Attributes.Add("onclick", "showfavmsg();");

										}
									}
									catch (Exception ex)
									{
									}

								}
							}

							//LoadStudentGrid();
						}
					}
				}
				catch (Exception ex)
				{
					Response.Redirect("Search.aspx");
				}
			}
		}

		public void LoadStudentGrid()
		{
			DataTable _dtStudentList = null;
			if (Session["Result"] == null)
			{
				_dtStudentList = _inBloomApi.GetStudentListBySchool(Session[SessionEnum.SchoolId.ToString()].ToString());
				Session.Add("Result", _dtStudentList);
			}
			else
			{
				_dtStudentList = (DataTable) Session["Result"];
			}
			if (_dtStudentList.Rows.Count == 0)
			{

			}
			else
			{
				DataView dv = _dtStudentList.DefaultView;

				gvResult.DataSource = dv;
				gvResult.DataBind();


			}
		}

		public override void VerifyRenderingInServerForm(Control control)
		{
			/* Verifies that the control is rendered */
		}


		protected void gvrecords_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			if (e.Row.RowType == DataControlRowType.DataRow)
			{
				e.Row.Attributes["onmouseover"] = "javascript:SetMouseOver(this)";
				e.Row.Attributes["onmouseout"] = "javascript:SetMouseOut(this)";


			}
		}

		protected void btnlnkexport_Click(object sender, EventArgs e)
		{


			try
			{
				if (Session["ResultGrid"] == null)
				{
				}
				else
				{
					if (Session["FlagType"].ToString() == "Flag")
					{
						int FlagId = Convert.ToInt16(Session["FlagId"].ToString());
						Flag _flag = _sqlHelper.GetFlag(FlagId)[0];
						String FlagName = _flag.FlagName;
						int DataElementId = _flag.DataElementId;
						String Entity = _sqlHelper.GetEntityNameByDataElementId(DataElementId);
						FlagName = FlagName.Replace(' ', '_');
						gvResult.DataSource = (DataTable) Session["ResultGrid"];
						gvResult.DataBind();
						for (int i = 0; i < gvResult.HeaderRow.Cells.Count; i++)
						{
							try
							{
								if (gvResult.HeaderRow.Cells[i].Text == "FieldName")
								{
									gvResult.HeaderRow.Cells[i].Text = _inBloomApi.GetWellFormattedString(char.ToUpper(Entity[0]) + Entity.Substring(1)) + "." + _inBloomApi.GetWellFormattedString(Session["FieldName"].ToString());
									break;
								}
							}
							catch (Exception ex)
							{
							}
						}
						//gvResult.HeaderRow.Cells[3].Text = _inBloomApi.GetWellFormattedString(Session["FieldName"].ToString());
						Response.ClearContent();
						Response.Buffer = true;

						Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", FlagName + ".xls"));
						Response.ContentType = "application/ms-excel";
						StringWriter sw = new StringWriter();
						HtmlTextWriter htw = new HtmlTextWriter(sw);
						gvResult.AllowPaging = false;

						gvResult.HeaderRow.Style.Add("background-color", "#FFFFFF");
						for (int i = 0; i < gvResult.HeaderRow.Cells.Count; i++)
						{
							gvResult.HeaderRow.Cells[i].Style.Add("background-color", "#BFDBFF");
						}
						int j = 1;
						foreach (GridViewRow gvrow in gvResult.Rows)
						{
							if (j <= gvResult.Rows.Count)
							{
								if (j%2 != 0)
								{
									for (int k = 0; k < gvrow.Cells.Count; k++)
									{
										gvrow.Cells[k].Style.Add("background-color", "#EFF3FB");
									}
								}
							}
							j++;
						}
						gvResult.RenderControl(htw);
						Response.Write(sw.ToString());
						Response.End();
					}
					else
					{

						//GridViewAggregateFlag.DataSource = (DataTable)Session["ResultGrid"];

						int FlagId = Convert.ToInt16(Session["FlagId"].ToString());
						AggregateFlag _aggregateFlag = _sqlHelper.GetAggregateFlag(FlagId)[0];
						String AggregateFlagName = _aggregateFlag.AggregateFlagName;
						AggregateFlagName = AggregateFlagName.Replace(' ', '_');
						//GridViewAggregateFlag.DataBind();
						for (int i = 0; i < GridViewAggregateFlag.HeaderRow.Cells.Count; i++)
						{
							//GridViewAggregateFlag.HeaderRow.Cells[i].Text = _inBloomApi.GetWellFormattedString(GridViewAggregateFlag.HeaderRow.Cells[i].Text);
						}

						Response.ClearContent();
						Response.Buffer = true;
						Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", AggregateFlagName + ".xls"));
						Response.ContentType = "application/ms-excel";
						StringWriter sw = new StringWriter();
						HtmlTextWriter htw = new HtmlTextWriter(sw);
						GridViewAggregateFlag.AllowPaging = false;

						GridViewAggregateFlag.HeaderRow.Style.Add("background-color", "#FFFFFF");
						for (int i = 0; i < GridViewAggregateFlag.HeaderRow.Cells.Count; i++)
						{
							GridViewAggregateFlag.HeaderRow.Cells[i].Style.Add("background-color", "#BFDBFF");
						}
						int j = 1;
						foreach (GridViewRow gvrow in GridViewAggregateFlag.Rows)
						{
							if (j <= gvResult.Rows.Count)
							{
								if (j%2 != 0)
								{
									for (int k = 0; k < gvrow.Cells.Count; k++)
									{
										gvrow.Cells[k].Style.Add("background-color", "#EFF3FB");
									}
								}
							}
							j++;
						}
						GridViewAggregateFlag.RenderControl(htw);
						Response.Write(sw.ToString());
						Response.End();
					}
				}
			}
			catch (Exception ex)
			{

			}
		}

		protected void GridResult_PageIndexChanging(object sender, GridViewPageEventArgs e)
		{
			gvResult.PageIndex = e.NewPageIndex;
			LoadStudentGrid();
		}

		protected void GridResult_Sorting(object sender, GridViewSortEventArgs e)
		{
		}

		protected void btnlnkfavorite_Click(object sender, EventArgs e)
		{
			int FlagId = Convert.ToInt16(Session["FlagId"].ToString());
			String FlagType = Session["FlagType"].ToString();
			if (FlagType == "Flag")
			{
				// check for flag
				_sqlHelper.FavoriteFlag(FlagId, _user.ExternalId);

				JArray _links = (JArray) Session["HomeLinks"];

				String UserCustomLink = _inBloomApi.GetCustomLink(_links);
				String EducationOrganizationId = Session["EducationOrganizationId"].ToString();

				String FlagUserId = Session["CustomUserId"].ToString();
				bool IsAdminUser = (bool) Session["CustomIsAdminUser"];
				bool IsPublic = (bool) Session["CustomIsPublic"];

				IList<Flag> _flag = (IList<Flag>) _sqlHelper.GetFlag(FlagId);

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





				if (_sqlHelper.IsFav(FlagId) || _sqlHelper.IsInPublicFavorite(FlagId, _user.ExternalId))
				{
					btnlnkfavorite.Text = "Remove Favorite";
					btnlnkfavorite.CssClass = "";
					btnlnkfavorite.Attributes.Add("onclick", "showunfavmsg();");

				}
				else
				{
					btnlnkfavorite.Text = "Add to Favorite";
					btnlnkfavorite.CssClass = "unfavorite_result";
					btnlnkfavorite.Attributes.Add("onclick", "showfavmsg();");

				}

			}
			else if (FlagType == "AggregateFlag")
			{
				try
				{
					_sqlHelper.AggregatetFavoriteFlag(FlagId, _user.ExternalId);


					_user = (User) Session["UserDetail"];


					JArray _links = (JArray) Session["HomeLinks"];

					String GetCustomLink = _inBloomApi.GetCustomLink(_links);
					String EducationOrganizationId = Session["EducationOrganizationId"].ToString();


					String FlagUserId = Session["CustomUserId"].ToString();
					bool IsAdminUser = (bool) Session["CustomIsAdminUser"];
					bool IsPublic = (bool) Session["CustomIsPublic"];

					String UserId = _user.ExternalId;

					AggregateCls[] _aggregateCls = new AggregateCls[1];

					IList<AggregateFlag> _aggregateFlag = _sqlHelper.GetAggregateFlag(FlagId);

					_aggregateCls[0] = new AggregateCls();
					_aggregateCls[0].AggregateFlagId = _aggregateFlag[0].AggregateFlagId;
					_aggregateCls[0].AggregateFlagDescription = _aggregateFlag[0].AggregateFlagDescription;
					_aggregateCls[0].AggregateFlagName = _aggregateFlag[0].AggregateFlagName;
					_aggregateCls[0].Keyword = _aggregateFlag[0].Keyword;

					_aggregateCls[0].IsPublic = _aggregateFlag[0].IsPublic;
					_aggregateCls[0].IsFavorite = _aggregateFlag[0].IsFavorite;
					_aggregateCls[0].UserId = _aggregateFlag[0].UserId;
					_aggregateCls[0].CreatedBy = _aggregateFlag[0].CreatedBy;
					_aggregateCls[0].ModifiedBy = _user.ExternalId;
					_aggregateCls[0].CreatedDate = _aggregateFlag[0].CreatedDate;
					_aggregateCls[0].ModifiedDate = DateTime.Now;


					int[] flagId = _sqlHelper.GetAllFlagIdOfAggregateFlagByAggregateFlagId(FlagId);
					FlagForAggregate[] _flagForAggregateList = new FlagForAggregate[flagId.Count()];
					for (int j = 0; j < flagId.Count(); j++)
					{
						_flagForAggregateList[j] = new FlagForAggregate();

						_flagForAggregateList[j].AggregateFlagId = _aggregateFlag[0].AggregateFlagId;
						_flagForAggregateList[j].FlagId = flagId[j];
						_flagForAggregateList[j].CreatedBy = _aggregateFlag[0].CreatedBy;
						_flagForAggregateList[j].CreatedDate = _aggregateFlag[0].CreatedDate;
						_flagForAggregateList[j].ModifiedBy = _aggregateFlag[0].ModifiedBy;
						_flagForAggregateList[j].ModifiedDate = _aggregateFlag[0].ModifiedDate;

					}

					_aggregateCls[0].FlagForAggregate = _flagForAggregateList;

					_inBloomApi.UpateAggregateFlag(FlagId, GetCustomLink, EducationOrganizationId, IsAdminUser, IsPublic, UserId, _aggregateCls, _user);





					if (_sqlHelper.IsAggregateIsFav(FlagId) || _sqlHelper.IsAggregateInPublicFavorite(FlagId, _user.ExternalId))
					{
						btnlnkfavorite.Text = "Remove Favorite";
						btnlnkfavorite.CssClass = "";
						btnlnkfavorite.Attributes.Add("onclick", "showunfavmsg();");

					}
					else
					{
						btnlnkfavorite.Text = "Add to Favorite";
						btnlnkfavorite.CssClass = "unfavorite_result";
						btnlnkfavorite.Attributes.Add("onclick", "showfavmsg();");

					}

				}
				catch (Exception ex)
				{
					throw ex;
				}
			}

			UpdateMasterFlagList();
		}

		protected void btnlnkcopy_Click(object sender, EventArgs e)
		{
			int FlagId = Convert.ToInt16(Session["FlagId"].ToString());
			String FlagType = Session["FlagType"].ToString();
			if (FlagType == "Flag")
			{
				// call flag
				Session.Add("CopyFlag", FlagId);
				Session["FlagId"] = null;
				Session["FlagType"] = null;
				Response.Redirect("CopyFlag.aspx");
			}
			else if (FlagType == "AggregateFlag")
			{
				// call aggregate flag

				Session.Add("CopyAggregateFlag", FlagId);
				Session["FlagId"] = null;
				Session["FlagType"] = null;
				Response.Redirect("CopyAggregateFlag.aspx");
			}
		}

		protected void btnlnkeditflag_Click(object sender, EventArgs e)
		{
			int FlagId = Convert.ToInt16(Session["FlagId"].ToString());
			String FlagType = Session["FlagType"].ToString();
			if (FlagType == "Flag")
			{
				// call flag
				Session.Add("EditFlag", FlagId);
				Session["FlagId"] = null;
				Session["FlagType"] = null;
				Response.Redirect("EditFlag.aspx");
			}
			else if (FlagType == "AggregateFlag")
			{
				// call aggregate flag               
				Session.Add("EditAggregateFlag", FlagId);
				Session["FlagId"] = null;
				Session["FlagType"] = null;
				Response.Redirect("EditAggregateFlag.aspx");
			}
		}

		public void HideColumnInitial()
		{
			for (int i = 0; i < cblList.Items.Count; i++)
			{
				if (i >= 4)
				{
					cblList.Items[i].Selected = false;

					try
					{
						String ss = cblList.Items[i].Text;

						var dataControlField = gvResult.Columns.Cast<DataControlField>().SingleOrDefault(fld => (fld.HeaderText == ss));

						if (dataControlField != null)
							dataControlField.Visible = false;
					}
					catch (Exception ex)
					{
					}
				}
				else
				{
					cblList.Items[i].Selected = true;
				}
			}
		}

		public void HideColumnInitialForAggregate()
		{
			for (int i = 0; i < GridViewAggregateFlag.HeaderRow.Cells.Count; i++)
			{
				if (i > 4 && i < 40)
				{

					GridViewAggregateFlag.HeaderRow.Cells[i].Visible = false;
					cblList.Items[i].Selected = false;
				}
				else
				{
					if (i == 4)
						continue;
					else
						cblList.Items[i].Selected = true;
				}
			}
			//cblList.Items[4].Selected = false;// for city
		}

		public void LoadFieldNames()
		{
			cblList.Items.Clear();
			cblList.Items.Add("Student.Student Unique State Id");
			cblList.Items.Add("Student.First Name");
			cblList.Items.Add("Student.Middle Name");
			cblList.Items.Add("Student.Last Surname");
			cblList.Items.Add("Student.City");
			cblList.Items.Add("Student.Name Of Country");
			cblList.Items.Add("Student.Apartment Room Suite Number");
			cblList.Items.Add("Student.Street Number Name");
			cblList.Items.Add("Student.Postal Code");
			cblList.Items.Add("Student.State Abbreviation");
			cblList.Items.Add("Student.Address Type");
			cblList.Items.Add("Student.Email Address");
			cblList.Items.Add("Student.Email Address Type");
			cblList.Items.Add("Student.Telephone Number");
			cblList.Items.Add("Student.Telephone Number Type");
			cblList.Items.Add("Student.Sex");
			cblList.Items.Add("Student.Hispanic Latino Ethnicity");
			cblList.Items.Add("Student.Birth Date");
			cblList.Items.Add("Student.Id");
			cblList.Items.Add("Student.Limited English Proficiency");
			cblList.Items.Add("Student.Entity Type");

			cblList.Items.Add("Parent.First Name");
			cblList.Items.Add("Parent.Middle Name");
			cblList.Items.Add("Parent.Last Surname");
			cblList.Items.Add("Parent.City");
			cblList.Items.Add("Parent.Name Of Country");
			cblList.Items.Add("Parent.Apartment Room Suite Number");
			cblList.Items.Add("Parent.Street Number Name");
			cblList.Items.Add("Parent.Postal Code");
			cblList.Items.Add("Parent.State Abbreviation");
			cblList.Items.Add("Parent.Address Type");
			cblList.Items.Add("Parent.Email Address");
			cblList.Items.Add("Parent.Email Address Type");
			cblList.Items.Add("Parent.Telephone Number");
			cblList.Items.Add("Parent.Telephone Number Type");
			cblList.Items.Add("Parent.Sex");
			cblList.Items.Add("Parent.Parent Unique State Id");
			cblList.Items.Add("Parent.Id");
			cblList.Items.Add("Parent.Entity Type");
		}

		protected void btnUpdate_Click(object sender, EventArgs e)
		{
			String hdnFieldsVal = hdnField.Value.ToString();

			// show all
			String FlagType = Session["FlagType"].ToString();
			if (FlagType == "Flag")
			{
				for (int i = 0; i < cblList.Items.Count; i++)
				{
					try
					{
						String ss = cblList.Items[i].Text;
						var dataControlField = gvResult.Columns.Cast<DataControlField>().SingleOrDefault(fld => (fld.HeaderText == ss));

						if (dataControlField != null)
							dataControlField.Visible = true;
					}
					catch (Exception ex)
					{
					}
				}

				// hide selected
				for (int i = 0; i < cblList.Items.Count; i++)
				{
					if (cblList.Items[i].Selected == false)
					{
						try
						{
							String ss = cblList.Items[i].Text;

							var dataControlField = gvResult.Columns.Cast<DataControlField>().SingleOrDefault(fld => (fld.HeaderText == ss));
							if (dataControlField != null)
								dataControlField.Visible = false;
						}
						catch (Exception ex)
						{
						}
					}
				}
			}
			else if (FlagType == "AggregateFlag")
			{
				for (int i = 0; i < GridViewAggregateFlag.HeaderRow.Cells.Count; i++)
				{
					GridViewAggregateFlag.HeaderRow.Cells[i].Visible = true;
					foreach (GridViewRow Row in GridViewAggregateFlag.Rows)
					{
						Row.Cells[i].Visible = true;
					}
				}

				for (int i = 0; i < cblList.Items.Count; i++)
				{
					if (cblList.Items[i].Selected == false)
					{
						String Name = cblList.Items[i].Text;
						for (int index = 0; index < GridViewAggregateFlag.HeaderRow.Cells.Count; index++)
						{
							if (Name == GridViewAggregateFlag.HeaderRow.Cells[index].Text)
							{
								GridViewAggregateFlag.HeaderRow.Cells[index].Visible = false;
								foreach (GridViewRow Row in GridViewAggregateFlag.Rows)
								{
									Row.Cells[index].Visible = false;
								}
								break;
							}
						}
					}
				}

			}
		}

		public void UpdateMasterFlagList()
		{
			DropDownList FlagList = (DropDownList) Master.FindControl("DropDownListFlag");
			FlagList.Items.Clear();


			_user = (User) Session["UserDetail"];
			ListItem[] _FlagList = _sqlHelper.GetFlagListByCategory(_user.ExternalId);

			String EducationOrganizationId = Session["EducationOrganizationId"].ToString();
			String[] AdminUserId = _inBloomApi.GetAdminIdByEducationOrganizationId(EducationOrganizationId);



			IList<Flag> _flagListTemp = null;
			IList<Flag> _flagListPublic = null;

			IList<AggregateFlag> _aggregateFlagListTemp = null;
			IList<AggregateFlag> _aggregateFlagListPublic = null;

			if (AdminUserId != null)
			{
				for (int i = 0; i < AdminUserId.Count(); i++)
				{
					_flagListTemp = _sqlHelper.GetPublicFlagListForAdmin(AdminUserId[i]);
					if (_flagListPublic == null)
					{
						_flagListPublic = _flagListTemp;
					}
					else
					{
						for (int j = 0; j < _flagListTemp.Count; j++)
						{
							_flagListPublic.Add(_flagListTemp[j]);
						}

					}
				}

				for (int i = 0; i < AdminUserId.Count(); i++)
				{
					_aggregateFlagListTemp = _sqlHelper.GetAllAggregatePublicFlagByUserId(AdminUserId[i]);
					if (_aggregateFlagListPublic == null)
					{
						_aggregateFlagListPublic = _aggregateFlagListTemp;
					}
					else
					{
						for (int j = 0; j < _aggregateFlagListTemp.Count; j++)
						{
							_aggregateFlagListPublic.Add(_aggregateFlagListTemp[j]);
						}
					}

				}
			}

			if (_FlagList != null)
			{
				bool FavStart = false;
				for (int Index = 0; Index < _FlagList.Length; Index++)
				{
					if (_FlagList[Index] == null)
					{
					}
					else
					{
						if (_FlagList[Index].Text == "Public")
						{

							_FlagList[Index].Attributes.Add("class", "abc");
							FlagList.Items.Add(_FlagList[Index]);
							try
							{
								for (int j = 0; j < _flagListPublic.Count(); j++)
								{
									FlagList.Items.Add(new ListItem(_flagListPublic[j].FlagName, _flagListPublic[j].FlagId + "Flag"));

								}
							}
							catch (Exception ex)
							{
							}

							try
							{
								for (int j = 0; j < _aggregateFlagListPublic.Count(); j++)
								{
									FlagList.Items.Add(new ListItem(_aggregateFlagListPublic[j].AggregateFlagName, _aggregateFlagListPublic[j].AggregateFlagId + "_AggregateFlag"));

								}
							}
							catch (Exception ex)
							{
							}
						}
						else
						{
							if (_FlagList[Index].Text == "Favorite" || _FlagList[Index].Text == "Recent Flag")
							{

								_FlagList[Index].Attributes.Add("class", "abc");
								//_FlagList[Index].Attributes.CssStyle.Add("color", "black");
								//_FlagList[Index].Attributes.CssStyle.Add("font-weight", "bold");
								FavStart = true;
							}
							if (FavStart)
								FlagList.Items.Add(_FlagList[Index]);
						}
					}
				}
				//DropDownListFlag.Items.AddRange(_FlagList);
			}
		}

		protected void GridViewAggregateFlag_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			if (e.Row.RowType == DataControlRowType.DataRow)
			{
				for (int i = 0; i < GridViewAggregateFlag.HeaderRow.Cells.Count; i++)
				{
					if (i > 4 && i < 40)
					{
						e.Row.Cells[i].Visible = false;
					}
				}
			}
		}
	}
}