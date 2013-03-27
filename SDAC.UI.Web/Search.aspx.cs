using System;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Web.Services;
using SDAC.DomainModel;
using SDAC.UI.Web.Enums;
using SDAC.UI.Web.Classes;
using SDAC.Core;
using Newtonsoft.Json.Linq;

namespace SDAC.UI.Web
{
	public partial class Search : System.Web.UI.Page
	{
		private AuthenticateUser _authenticateUser = new AuthenticateUser();
		private SLC_SDACEntities _sDACEntities = new SLC_SDACEntities();
		private SqlHelper _sqlHelper = null;
		private User _user = null;
		private JArray _homeLinks = null;
		private inBloomApi _slcApi = null;

		protected void Page_Load(object sender, EventArgs e)
		{
			_sqlHelper = new SqlHelper();

			try
			{
				if (!IsPostBack)
				{
					if (Session[SessionEnum.AccessToken.ToString()] == null)
					{
						if (Request.QueryString[QueryStringTokenEnum.Code.ToString()] == null)
						{
							_authenticateUser.AuthorizeUser();
						}
						else
						{
							Session.Add(SessionEnum.AccessToken.ToString(), _authenticateUser.GetAccessToken());
						}
					}

					_slcApi = new inBloomApi(Session[SessionEnum.AccessToken.ToString()].ToString());

					Session["ResultPage"] = null;
					_user = _slcApi.UserDetails();

					if (_user == null)
					{
						Session[SessionEnum.AccessToken.ToString()] = null;
						Response.Redirect("Search.aspx");
					}
					else if (Session["UserDetail"] == null)
					{
						Session.Add("UserDetail", _user);
					}

					_homeLinks = _slcApi.GetHomeLinks();
					if (_homeLinks != null)
					{
						Session.Add("HomeLinks", _homeLinks);
					}

					_homeLinks = (JArray) Session["HomeLinks"];
					String EducationOrganizationId = _slcApi.GetEducationOrganizationId(_homeLinks);
					
					if (EducationOrganizationId != null || EducationOrganizationId != "")
					{
						// not state level admin
						Session.Add("EducationOrganizationId", EducationOrganizationId);
						//_slcApi.GetFlagListForAdminUser(EducationOrganizationId, _user.ExternalId);
					}

					LoadFlagGrid();
				}
			}
			catch (Exception Ex)
			{
				//if (Ex.ToString().Contains("The remote server returned an error: (403) Forbidden."))
				//{
				//    Session.Add("ErrorStack", Ex.ToString());
				//    Session[SessionEnum.AccessToken.ToString()] = null;
				//    Response.Redirect("ErrorPage.aspx");

				//}
				//Response.Redirect("Search.aspx");
			}
		}

		protected void btnAddFlag_Click(object sender, EventArgs e)
		{
			Response.Redirect("AddFlag.aspx");
		}

		protected void btnAggregateFlag_Click(object sender, EventArgs e)
		{
			Response.Redirect("Aggregate.aspx");
		}

		protected void gridViewFlag_RowCommand(object sender, GridViewCommandEventArgs e)
		{
			#region Grid Operation Edit, Delete, Update, Favorite


			_slcApi = new inBloomApi(Session[SessionEnum.AccessToken.ToString()].ToString());

			if (e.CommandName == "CmdDelete")
			{
				try
				{
					int RowIndex = Int32.Parse(e.CommandArgument.ToString());
					int FlagID = Convert.ToInt16(gridViewFlag.DataKeys[RowIndex].Value.ToString());

					_user = (User) Session["UserDetail"];
					IList<Flag> _flag = (IList<Flag>) _sqlHelper.GetFlag(FlagID);


					if (_user.ExternalId == _flag[0].UserId)
					{

						_sqlHelper.DeleteFlag(FlagID);

						JArray _links = (JArray) Session["HomeLinks"];

						String UserCustomLink = _slcApi.GetCustomLink(_links);
						String EducationOrganizationId = Session["EducationOrganizationId"].ToString();

						String UserId = gridViewFlag.Rows[RowIndex].Cells[10].Text;
						String AdminUser = gridViewFlag.Rows[RowIndex].Cells[9].Text;

						bool IsAdminUser = (AdminUser == "True");

						String IsPublicFag = gridViewFlag.Rows[RowIndex].Cells[4].Text;
						bool IsPublic = (IsPublicFag == "True");

						FlagCls[] _flagCls = new FlagCls[1];
						_flagCls[0] = new FlagCls();
						_flagCls[0].FlagId = _flag[0].FlagId;
						_flagCls[0].FlagName = _flag[0].FlagName;
						_flagCls[0].FlagDescription = _flag[0].FlagDescription;
						_flagCls[0].FlagKeyword = _flag[0].Keyword;
						_flagCls[0].IsPublic = _flag[0].IsPublic;
						_flagCls[0].IsFavorite = _flag[0].IsFavorite;
						_flagCls[0].IsDeleted = true;
						_flagCls[0].ConditionId = _flag[0].ConditionId;
						_flagCls[0].DataElementId = _flag[0].DataElementId;
						_flagCls[0].ValueSet1 = _flag[0].ValueSet1;
						_flagCls[0].ValueSet2 = _flag[0].ValueSet2;
						_flagCls[0].UserId = _flag[0].UserId;
						_flagCls[0].CreatedBy = _flag[0].CreatedBy;
						_flagCls[0].CreatedDate = _flag[0].CreatedDate;
						_flagCls[0].ModifiedBy = _user.ExternalId;
						_flagCls[0].ModifiedDate = DateTime.Now;

						_slcApi.UpateFlag(FlagID, UserCustomLink, EducationOrganizationId, IsAdminUser, IsPublic, UserId, _flagCls, _user);

						LoadFlagGrid();
					}
				}
				catch (Exception ex)
				{
				}
			}
			else if (e.CommandName == "CmdFavorite")
			{
				try
				{
					_user = (User) Session["UserDetail"];

					int RowIndex = Int32.Parse(e.CommandArgument.ToString());
					int FlagID = Convert.ToInt16(gridViewFlag.DataKeys[RowIndex].Value.ToString());

					_sqlHelper.FavoriteFlag(FlagID, _user.ExternalId);
					_user = (User) Session["UserDetail"];

					JArray _links = (JArray) Session["HomeLinks"];

					String UserCustomLink = _slcApi.GetCustomLink(_links);
					String EducationOrganizationId = Session["EducationOrganizationId"].ToString();

					String UserId = gridViewFlag.Rows[RowIndex].Cells[10].Text;
					String AdminUser = gridViewFlag.Rows[RowIndex].Cells[9].Text;

					bool IsAdminUser = AdminUser == "True";

					String IsPublicFag = gridViewFlag.Rows[RowIndex].Cells[4].Text;
					bool IsPublic = IsPublicFag == "True";

					IList<Flag> _flag = _sqlHelper.GetFlag(FlagID);

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

					_slcApi.UpateFlag(FlagID, UserCustomLink, EducationOrganizationId, IsAdminUser, IsPublic, UserId, _flagCls, _user);

					UpdateMasterFlagList();

					LoadFlagGrid();

				}
				catch (Exception ex)
				{
				}
			}
			else if (e.CommandName == "CmdCopyFlag")
			{
				try
				{
					int FlagID = Int32.Parse(e.CommandArgument.ToString());
					Session.Add("CopyFlag", FlagID);
					Response.Redirect("CopyFlag.aspx");
				}
				catch (Exception ex)
				{
				}
			}
			else if (e.CommandName == "CmdEditFlag")
			{
				try
				{
					int RowIndex = Int32.Parse(e.CommandArgument.ToString());
					int FlagId = Convert.ToInt16(gridViewFlag.DataKeys[RowIndex].Value.ToString());
					String UserId = gridViewFlag.Rows[RowIndex].Cells[10].Text;
					String AdminUser = gridViewFlag.Rows[RowIndex].Cells[9].Text;

					bool IsAdminUser = (AdminUser == "True");

					String IsPublicFag = gridViewFlag.Rows[RowIndex].Cells[4].Text;
					bool IsPublic = (IsPublicFag == "True");

					String FlagType = gridViewFlag.Rows[RowIndex].Cells[7].Text;

					Session.Add("CustomUserId", UserId);
					Session.Add("CustomIsPublic", IsPublic);
					Session.Add("CustomFlagType", FlagType);
					Session.Add("CustomIsAdminUser", IsAdminUser);
					Session.Add("EditFlag", FlagId);
					Response.Redirect("EditFlag.aspx");
				}
				catch (Exception Ex)
				{
				}
			}
			else if (e.CommandName == "CmdCopyAggregateFlag")
			{
				try
				{
					int AggregateFlagID = Int32.Parse(e.CommandArgument.ToString());
					Session.Add("CopyAggregateFlag", AggregateFlagID);
					Response.Redirect("CopyAggregateFlag.aspx");
				}
				catch (Exception ex)
				{
				}
			}
			else if (e.CommandName == "CmdEditAggregateFlag")
			{
				try
				{
					int RowIndex = Int32.Parse(e.CommandArgument.ToString());
					int AggregateFlagID = Convert.ToInt16(gridViewFlag.DataKeys[RowIndex].Value.ToString());
					String UserId = gridViewFlag.Rows[RowIndex].Cells[10].Text;
					String AdminUser = gridViewFlag.Rows[RowIndex].Cells[9].Text;

					bool IsAdminUser = (AdminUser == "True");

					String IsPublicFag = gridViewFlag.Rows[RowIndex].Cells[4].Text;
					bool IsPublic = (IsPublicFag == "True");

					String FlagType = gridViewFlag.Rows[RowIndex].Cells[7].Text;

					Session.Add("CustomUserId", UserId);
					Session.Add("CustomIsPublic", IsPublic);
					Session.Add("CustomFlagType", FlagType);
					Session.Add("CustomIsAdminUser", IsAdminUser);
					Session.Add("EditAggregateFlag", AggregateFlagID);

					Response.Redirect("EditAggregateFlag.aspx");
				}
				catch (Exception Ex)
				{
				}
			}
			else if (e.CommandName == "CmdFavoriteAggregateFlag")
			{
				try
				{
					int RowIndex = Int32.Parse(e.CommandArgument.ToString());
					int AggregateFlagID = Convert.ToInt16(gridViewFlag.DataKeys[RowIndex].Value.ToString());

					_user = (User) Session["UserDetail"];
					_sqlHelper.AggregatetFavoriteFlag(AggregateFlagID, _user.ExternalId);

					JArray _links = (JArray) Session["HomeLinks"];

					String UserCustomLink = _slcApi.GetCustomLink(_links);
					String EducationOrganizationId = Session["EducationOrganizationId"].ToString();

					String UserId = gridViewFlag.Rows[RowIndex].Cells[10].Text;
					String AdminUser = gridViewFlag.Rows[RowIndex].Cells[9].Text;

					bool IsAdminUser = (AdminUser == "True");

					String IsPublicFag = gridViewFlag.Rows[RowIndex].Cells[4].Text;
					bool IsPublic = (IsPublicFag == "True");

					AggregateCls[] _aggregateCls = new AggregateCls[1];

					IList<AggregateFlag> _aggregateFlag = _sqlHelper.GetAggregateFlag(AggregateFlagID);

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

					int[] flagId = _sqlHelper.GetAllFlagIdOfAggregateFlagByAggregateFlagId(AggregateFlagID);
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

					_slcApi.UpateAggregateFlag(AggregateFlagID, UserCustomLink, EducationOrganizationId, IsAdminUser, IsPublic, UserId, _aggregateCls, _user);

					UpdateMasterFlagList();

					LoadFlagGrid();
				}
				catch (Exception ex)
				{
					throw ex;
				}
			}
			else if (e.CommandName == "CmdDeleteAggregateFlag")
			{
				try
				{
					int RowIndex = Int32.Parse(e.CommandArgument.ToString());
					int AggregateFlagID = Convert.ToInt16(gridViewFlag.DataKeys[RowIndex].Value.ToString());

					_user = (User) Session["UserDetail"];

					AggregateCls[] _aggregateCls = new AggregateCls[1];

					IList<AggregateFlag> _aggregateFlag = _sqlHelper.GetAggregateFlag(AggregateFlagID);

					if (_user.ExternalId == _aggregateFlag[0].UserId)
					{
						if (_sqlHelper.DeleteAggregateFlag(AggregateFlagID))
						{
							// deleted successfully                                                     

							_user = (User) Session["UserDetail"];

							JArray _links = (JArray) Session["HomeLinks"];

							String UserCustomLink = _slcApi.GetCustomLink(_links);
							String EducationOrganizationId = Session["EducationOrganizationId"].ToString();

							String UserId = gridViewFlag.Rows[RowIndex].Cells[10].Text;
							String AdminUser = gridViewFlag.Rows[RowIndex].Cells[9].Text;

							bool IsAdminUser = (AdminUser == "True");

							String IsPublicFag = gridViewFlag.Rows[RowIndex].Cells[4].Text;
							bool IsPublic = (IsPublicFag == "True");

							_aggregateCls[0] = new AggregateCls();
							_aggregateCls[0].AggregateFlagId = _aggregateFlag[0].AggregateFlagId;
							_aggregateCls[0].AggregateFlagDescription = _aggregateFlag[0].AggregateFlagDescription;
							_aggregateCls[0].AggregateFlagName = _aggregateFlag[0].AggregateFlagName;
							_aggregateCls[0].Keyword = _aggregateFlag[0].Keyword;
							_aggregateCls[0].IsDeleted = true;
							_aggregateCls[0].IsPublic = _aggregateFlag[0].IsPublic;
							_aggregateCls[0].IsFavorite = _aggregateFlag[0].IsFavorite;
							_aggregateCls[0].UserId = _aggregateFlag[0].UserId;
							_aggregateCls[0].CreatedBy = _aggregateFlag[0].CreatedBy;
							_aggregateCls[0].ModifiedBy = _user.ExternalId;
							_aggregateCls[0].CreatedDate = _aggregateFlag[0].CreatedDate;
							_aggregateCls[0].ModifiedDate = DateTime.Now;

							int[] flagId = _sqlHelper.GetAllFlagIdOfAggregateFlagByAggregateFlagId(AggregateFlagID);
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

							_slcApi.UpateAggregateFlag(AggregateFlagID, UserCustomLink, EducationOrganizationId, IsAdminUser, IsPublic, UserId, _aggregateCls, _user);

							LoadFlagGrid();
						}
					}
				}
				catch (Exception ex)
				{
				}
			}
			else if (e.CommandName == "CmdRunFlag")
			{
				//Session.Add("ResultPage", true);
				int RowIndex = Convert.ToInt16(e.CommandArgument);
				int FlagId = Convert.ToInt16(gridViewFlag.Rows[RowIndex].Cells[8].Text);
				String FlagType = gridViewFlag.Rows[RowIndex].Cells[7].Text;
				Session.Add("FlagId", FlagId);
				Session.Add("FlagType", FlagType);

				String UserId = gridViewFlag.Rows[RowIndex].Cells[10].Text;
				String AdminUser = gridViewFlag.Rows[RowIndex].Cells[9].Text;

				bool IsAdminUser = (AdminUser == "True");

				String IsPublicFag = gridViewFlag.Rows[RowIndex].Cells[4].Text;
				bool IsPublic = (IsPublicFag == "True");

				FlagType = gridViewFlag.Rows[RowIndex].Cells[7].Text;

				Session.Add("CustomUserId", UserId);
				Session.Add("CustomIsPublic", IsPublic);
				Session.Add("CustomFlagType", FlagType);
				Session.Add("CustomIsAdminUser", IsAdminUser);
				Session.Add("EditFlag", FlagId);

				Response.Redirect("Result.aspx");
			}

			#endregion
		}

		public void LoadFlagGrid()
		{
			try
			{
				DataView dv = GetFlagForGrid();

				gridViewFlag.DataSource = dv;
				gridViewFlag.DataBind();

				ShowHidePublicFlag();

			}
			catch (Exception Ex)
			{
				Session.Add("ErrorStack", Ex.ToString());
				Response.Redirect("ErrorPage.aspx");
			}
		}

		public DataView GetFlagForGrid()
		{
			try
			{
				_slcApi = new inBloomApi(Session[SessionEnum.AccessToken.ToString()].ToString());

				_user = (User) Session["UserDetail"];

				Temp[] _temp = null;
				Temp[] _tempForStaff = null;
				JArray _homeLinks = (JArray) Session["HomeLinks"];
				String UserCustomLink = _slcApi.GetCustomLink(_homeLinks);

				String EducationOrganizationId = Session["EducationOrganizationId"].ToString();

				if (_user.IsAdminUser)
				{
					// get all the flags from organization by admin user specific 
					_temp = _slcApi.GetFlagListByEducationOrganization(_user, EducationOrganizationId);
					ListItem[] _schoolList = null;

					_schoolList = GetSchoolList();

					_tempForStaff = _slcApi.GetFlagListOfStaffForAdminUser(_user, EducationOrganizationId, _schoolList);

					if (_tempForStaff.Any())
					{
						if (_temp != null)
						{
							Temp[] _tempNew = _temp;
							_temp = new Temp[_tempNew.Count() + _tempForStaff.Count()];

							for (int i = 0; i < _tempNew.Count(); i++)
							{
								_temp[i] = new Temp();
								_temp[i] = _tempNew[i];
							}
							int j = 0;
							for (int i = _tempNew.Count(); i < _tempNew.Count() + _tempForStaff.Count(); i++)
							{
								_temp[i] = new Temp();
								_temp[i] = _tempForStaff[j];
								j++;
							}
						}
						else
							_temp = _tempForStaff;
					}
				}
				else
				{
					// get all the flags from custom and organization for normal users i.e public flag
					_temp = _slcApi.GetFlagListByEducationOrganization(null, EducationOrganizationId);
				}

				Temp[] _newTemp = null;
				Temp _ownTemp = null;
				
				if (_temp != null)
				{
					_newTemp = new Temp[_temp.Count() + 1];
					for (int i = 0; i < _temp.Count(); i++)
					{
						_newTemp[i] = new Temp();
						_newTemp[i] = _temp[i];
					}

					_ownTemp = _slcApi.GetCustomForStaff(UserCustomLink);
					_newTemp[_temp.Count()] = new Temp();
					_newTemp[_temp.Count()] = _ownTemp;
				}
				else
				{
					// to get the flags from user custom, private for admin users
					_ownTemp = _slcApi.GetCustomForStaff(UserCustomLink);
					_newTemp = new Temp[1];
					_newTemp[0] = new Temp();
					_newTemp[0] = _ownTemp;
				}

				DataTable dt = new DataTable("blablaTable");
				dt.Columns.Add("FlagId", typeof (int));
				dt.Columns.Add("FlagName", typeof (string));
				dt.Columns.Add("FlagDescription", typeof (string));
				dt.Columns.Add("Keyword", typeof (string));
				dt.Columns.Add("IsPublic", typeof (bool));
				dt.Columns.Add("IsFavorite", typeof (bool));
				dt.Columns.Add("CreatedDate", typeof (DateTime));
				dt.Columns.Add("Type", typeof (string));

				dt.Columns.Add("IsAdmin", typeof (string));
				dt.Columns.Add("UserId", typeof (string));

				for (int i = 0; i < _newTemp.Count(); i++)
				{
					if (_newTemp[i] != null)
					{
						FlagCls[] _flagCls = _newTemp[i].FlagList;
						AggregateCls[] _aggregateFlagCls = _newTemp[i].AggregateFlagList;

						if (_flagCls != null)
						{
							for (int j = 0; j < _flagCls.Count(); j++)
							{
								if (_flagCls[j].IsDeleted == false)
									dt.Rows.Add(new Object[] {_flagCls[j].FlagId, _flagCls[j].FlagName, _flagCls[j].FlagDescription, _flagCls[j].FlagKeyword, _flagCls[j].IsPublic, _flagCls[j].IsFavorite, _flagCls[j].CreatedDate, "Flag", _newTemp[i].IsAdmin, _newTemp[i].UserId});
							}
						}

						if (_aggregateFlagCls != null)
						{
							for (int j = 0; j < _aggregateFlagCls.Count(); j++)
							{
								if (_aggregateFlagCls[j].IsDeleted == false)
									dt.Rows.Add(new Object[] {_aggregateFlagCls[j].AggregateFlagId, _aggregateFlagCls[j].AggregateFlagName, _aggregateFlagCls[j].AggregateFlagDescription, _aggregateFlagCls[j].Keyword, _aggregateFlagCls[j].IsPublic, _aggregateFlagCls[j].IsFavorite, _aggregateFlagCls[j].CreatedDate, "AggregateFlag", _newTemp[i].IsAdmin, _newTemp[i].UserId});
							}
						}

					}
				}

				return dt.DefaultView;
			}
			catch (Exception ex)
			{
				return null;
			}
		}

		protected void gridViewFlag_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			if (e.Row.RowType == DataControlRowType.DataRow)
			{
				GridViewRow d = e.Row;

				// check the flag is public or private and the button of edit and delete
				String IsPublic = d.Cells[4].Text;

				_user = (User) Session["UserDetail"];

				if (IsPublic == "True")
				{
					// hide the controls
					ImageButton btnDelete = (ImageButton) d.FindControl("ImgBtndelete");
					btnDelete.Visible = false;

					ImageButton btnEdit = (ImageButton) d.FindControl("ImgBtnEdit");
					btnEdit.Visible = false;
				}

				// admin can edit and delete his flag
				if (_user.IsAdminUser)
				{
					ImageButton btnDelete = (ImageButton) d.FindControl("ImgBtndelete");
					btnDelete.Visible = true;

					ImageButton btnEdit = (ImageButton) d.FindControl("ImgBtnEdit");
					btnEdit.Visible = true;
				}

				// check flag is favorite or not

				String IsFavorite = d.Cells[5].Text;

				int FlagId = Convert.ToInt16(gridViewFlag.DataKeys[e.Row.RowIndex].Value.ToString());

				String FlagType = d.Cells[7].Text;

				ImageButton imgBtnFav = null;

				if (FlagType == "Flag")
				{
					if (_sqlHelper.AddedByUser(_user.ExternalId, FlagId))
					{
						if (IsFavorite == "True")
						{
							imgBtnFav = (ImageButton) d.FindControl("ImgBtnFav");
							imgBtnFav.CssClass = "fav";
							imgBtnFav.ToolTip = "Remove from Favorite";
							//imgBtnFav.OnClientClick = "return confirm('Are you sure you want to set flag as un favorite ?');";
							imgBtnFav.OnClientClick = "javascript:showunfavmsg();";

						}
						else
						{
							imgBtnFav = (ImageButton) d.FindControl("ImgBtnFav");
							imgBtnFav.CssClass = "unfav";
							//imgBtnFav.ImageUrl = "~/Images/fav.png";
							imgBtnFav.ToolTip = "Add to Favorite";
							//imgBtnFav.OnClientClick = "return confirm('Are you sure you want to set flag as favorite ?');";
							imgBtnFav.OnClientClick = "javascript:showfavmsg();";
						}
					}
					else if (_sqlHelper.IsInPublicFavorite(FlagId, _user.ExternalId))
					{
						imgBtnFav = (ImageButton) d.FindControl("ImgBtnFav");
						imgBtnFav.CssClass = "fav";
						imgBtnFav.ToolTip = "Remove from Favorite";
						//imgBtnFav.OnClientClick = "return confirm('Are you sure you want to set flag as un favorite ?');";
						imgBtnFav.OnClientClick = "javascript:showunfavmsg();";
					}
					else
					{
						imgBtnFav = (ImageButton) d.FindControl("ImgBtnFav");
						imgBtnFav.CssClass = "unfav";
						imgBtnFav.ToolTip = "Add to Favorite";
						//imgBtnFav.OnClientClick = "return confirm('Are you sure you want to set flag as favorite ?');";
						imgBtnFav.OnClientClick = "javascript:showfavmsg();";
					}
				}
				else if (FlagType == "AggregateFlag")
				{
					// we need to change the command name and other things

					if (_sqlHelper.AggregateAddedByUser(_user.ExternalId, FlagId))
					{
						if (IsFavorite == "True")
						{
							imgBtnFav = (ImageButton) d.FindControl("ImgBtnFav");
							imgBtnFav.CssClass = "fav";
							imgBtnFav.ToolTip = "Remove from Favorite";
							imgBtnFav.OnClientClick = "javascript:showunfavmsg();";
						}
						else
						{
							imgBtnFav = (ImageButton) d.FindControl("ImgBtnFav");
							imgBtnFav.CssClass = "unfav";
							//imgBtnFav.ImageUrl = "~/Images/fav.png";
							imgBtnFav.ToolTip = "Add to Favorite";
							imgBtnFav.OnClientClick = "javascript:showfavmsg();";
						}
					}

					else if (_sqlHelper.IsAggregateInPublicFavorite(FlagId, _user.ExternalId))
					{
						imgBtnFav = (ImageButton) d.FindControl("ImgBtnFav");
						imgBtnFav.CssClass = "fav";
						imgBtnFav.ToolTip = "Remove from Favorite";
						imgBtnFav.OnClientClick = "javascript:showunfavmsg();";
					}
					else
					{
						imgBtnFav = (ImageButton) d.FindControl("ImgBtnFav");
						imgBtnFav.CssClass = "unfav";
						imgBtnFav.ToolTip = "Add to Favorite";
						imgBtnFav.OnClientClick = "javascript:showfavmsg();";
					}

					ImageButton btnCopy = (ImageButton) d.FindControl("ImgBtnCopy");
					btnCopy.CommandName = "CmdCopyAggregateFlag";

					ImageButton btnEdit = (ImageButton) d.FindControl("ImgBtnEdit");
					btnEdit.CommandName = "CmdEditAggregateFlag";

					ImageButton btnFav = (ImageButton) d.FindControl("ImgBtnFav");
					btnFav.CommandName = "CmdFavoriteAggregateFlag";

					ImageButton btnDel = (ImageButton) d.FindControl("ImgBtndelete");
					btnDel.CommandName = "CmdDeleteAggregateFlag";

					d.Cells[0].CssClass = "agimage";
				}
			}
		}

		protected void CheckBoxPublicFlag_CheckedChanged(object sender, EventArgs e)
		{
			ShowHidePublicFlag();
		}

		public void ShowHidePublicFlag()
		{
			String PublicFlag = String.Empty;

			if (CheckBoxPublicFlag.Checked == true)
			{
				// code to show the with public flag
				foreach (GridViewRow row in gridViewFlag.Rows)
				{
					PublicFlag = row.Cells[4].Text;
					if (PublicFlag == "True")
					{
						row.Visible = true;
					}
				}
			}
			else
			{
				// code to show only private flag 
				foreach (GridViewRow row in gridViewFlag.Rows)
				{
					PublicFlag = row.Cells[4].Text;
					if (PublicFlag == "True")
					{
						row.Visible = false;
					}
				}
			}
		}

		[WebMethod]
		public static List<string> GetFlagNames(string Name)
		{
			try
			{
				//SqlConnection con = new SqlConnection(Connectionstring);
				//con.Open();
				//string strQuery = "SELECT Name  FROM [dbo].[tblNames] WHERE Name like '%" + Name + "%' order by Name desc";
				//SqlCommand cmd = new SqlCommand(strQuery, con);
				//SqlDataAdapter da = new SqlDataAdapter(cmd);
				//DataSet dsNames = new DataSet();
				//da.Fill(dsNames);
				//if (dsNames != null && dsNames.Tables[0].Rows.Count > 0)
				//{
				//    List<string> NameData = new List<string>();
				//    foreach (DataRow dRow in dsNames.Tables[0].Rows)
				//    {
				//        NameData.Add((string)dRow["Name"]);
				//    }
				//    return NameData;
				//}

				return new List<string> {"test", "test1", "test2", "test3"};
			}
			catch (Exception ex)
			{
				return null;
			}
		}

		protected void gvrecords_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			if (e.Row.RowType == DataControlRowType.DataRow)
			{
				e.Row.Attributes["onmouseover"] = "javascript:SetMouseOver(this)";
				e.Row.Attributes["onmouseout"] = "javascript:SetMouseOut(this)";
			}
		}

		protected void gvResult_Sorting(object sender, GridViewSortEventArgs e)
		{
		}

		public ListItem[] GetSchoolList()
		{
			ListItem[] _schoolList = null;
			try
			{
				ListItem[] _listDistrict = null;
				ListItem[] _schoolListTemp = null;
				_listDistrict = _slcApi.GetDistrictForAdmin();
				for (int Index = 1; Index < _listDistrict.Length; Index++)
				{
					if (_listDistrict[Index] != null)
					{
						String CategoryName = _listDistrict[0].Text;
						if (CategoryName == "School")
						{
							_schoolListTemp = new ListItem[1];
							_schoolListTemp[0] = new ListItem();
							_schoolListTemp[0].Text = _listDistrict[Index].Text;
							_schoolListTemp[0].Value = _listDistrict[Index].Value;
						}
						else
						{
							String DistrictId = _listDistrict[Index].Value.ToString();
							_schoolListTemp = _slcApi.GetSchoolForAdmin(DistrictId);

						}

						if (_schoolList == null)
						{
							_schoolList = _schoolListTemp;
						}
						else
						{
							ListItem[] _forTemp = _schoolList;

							_schoolList = new ListItem[_forTemp.Count() + _schoolListTemp.Count()];
							for (int i = 0; i < _forTemp.Count(); i++)
							{
								_schoolList[i] = new ListItem();
								_schoolList[i] = _forTemp[i];
							}
							int k = 0;
							for (int i = _forTemp.Count(); i < _forTemp.Count() + _schoolListTemp.Count(); i++)
							{
								_schoolList[i] = new ListItem();
								_schoolList[i] = _schoolListTemp[k];
								k = k + 1;
							}
						}
					}
				}
				return _schoolList;
			}
			catch (Exception Ex)
			{
				return null;
			}
		}

		public void UpdateMasterFlagList()
		{
			DropDownList FlagList = (DropDownList) Master.FindControl("dropDownListFlag");
			FlagList.Items.Clear();

			_user = (User) Session["UserDetail"];
			ListItem[] _FlagList = _sqlHelper.GetFlagListByCategory(_user.ExternalId);

			String EducationOrganizationId = Session["EducationOrganizationId"].ToString();
			String[] AdminUserId = _slcApi.GetAdminIdByEducationOrganizationId(EducationOrganizationId);

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
							catch (Exception Ex)
							{
							}

							try
							{
								for (int j = 0; j < _aggregateFlagListPublic.Count(); j++)
								{
									FlagList.Items.Add(new ListItem(_aggregateFlagListPublic[j].AggregateFlagName, _aggregateFlagListPublic[j].AggregateFlagId + "_AggregateFlag"));

								}
							}
							catch (Exception Ex)
							{
							}
						}
						else
						{
							if (_FlagList[Index].Text == "Favorite" || _FlagList[Index].Text == "Recent Flag")
							{

								_FlagList[Index].Attributes.Add("class", "abc");
								FavStart = true;
							}
							if (FavStart)
								FlagList.Items.Add(_FlagList[Index]);
						}
					}
				}
			}
		}
	}
}
