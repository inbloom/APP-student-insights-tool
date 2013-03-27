using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using SDAC.DomainModel;
using SDAC.UI.Web.Enums;
using SDAC.Core;
using Newtonsoft.Json.Linq;

namespace SDAC.UI.Web
{
	public partial class EditAggregateFlag : Page
	{
		protected SLC_SDACEntities _sDACEntities = new SLC_SDACEntities();
		protected User _user = null;
		protected SqlHelper _sqlHelper = null;
		protected int _aggregateFlagId;
		protected inBloomApi _inBloomApi = null;
		
		protected void Page_Load(object sender, EventArgs e)
		{
			_sqlHelper = new SqlHelper();
			
			try
			{
				_inBloomApi = new inBloomApi(Session[SessionEnum.AccessToken.ToString()].ToString());
			}
			catch (Exception ex)
			{
				Response.Redirect("Search.aspx");
			}

			if (!IsPostBack)
			{
				if (Session["EditAggregateFlag"] != null)
				{
					_aggregateFlagId = Convert.ToInt16(Session["EditAggregateFlag"].ToString());

					IList<AggregateFlag> _AggregateFlag = _sqlHelper.GetAggregateFlag(_aggregateFlagId);

				   txtFlagName.Text = _AggregateFlag[0].AggregateFlagName;
				   txtDescription.Text = _AggregateFlag[0].AggregateFlagDescription;
				   txtFlag.Text = _AggregateFlag[0].Keyword;

					_user=(User)Session["UserDetail"];
					if (_user == null)
					{
						Session[SessionEnum.AccessToken.ToString()] = null;
						Response.Redirect("Search.aspx");
					}
					else
					{
						if (_user.IsAdminUser)
						{
							String IsPublic = _AggregateFlag[0].IsPublic.ToString();
							if (IsPublic == "True")
							{
								radioFlagType.SelectedIndex = 0;
							}
							else
							{
								radioFlagType.SelectedIndex = 1;
							}
						}
						else
						{
							PanelFlagType.Visible = false;
						}


						_user = (User)Session["UserDetail"];

						IList<Flag> _flagForGrid = _sqlHelper.GetFlagList(_user.ExternalId);
						IList<Flag> _flagListTemp = null;

						String EducationOrganizationId = Session["EducationOrganizationId"].ToString();
						String[] AdminUserId = _inBloomApi.GetAdminIdByEducationOrganizationId(EducationOrganizationId);

						if (AdminUserId != null)
						{
							for (int i = 0; i < AdminUserId.Count(); i++)
							{
								_flagListTemp = _sqlHelper.GetPublicFlagListForAdmin(AdminUserId[i]);
								if (_flagForGrid == null)
								{
									_flagForGrid = _flagListTemp;
								}
								else
								{
									for (int j = 0; j < _flagListTemp.Count; j++)
									{
										_flagForGrid.Add(_flagListTemp[j]);
									}

								}
							}
						}

						gridStudentInfo.DataSource = _flagForGrid;
						gridStudentInfo.DataBind();

						// add to listbox
						IList<Flag> _flag = _sqlHelper.GetFlagAddedForInAggregate(_aggregateFlagId);
						for (int i = 0; i < _flag.Count; i++)
						{
							int FlagId = _flag[i].FlagId;
							String FlagName = _flag[i].FlagName;

							lstcategory.Items.Add(new ListItem(FlagName, FlagId.ToString()));
						}

						foreach (GridViewRow row in gridStudentInfo.Rows)
						{
							int FlagId = Convert.ToInt16(row.Cells[4].Text);
							for (int i = 0; i < _flag.Count; i++)
							{
								if (_flag[i].FlagId == FlagId)
								{
									row.Visible = false;
									break;
								}
							}
						}
					}
				}
			}
		}

		protected void btnForward_Click(object sender, EventArgs e)
		{
			foreach (GridViewRow row in gridStudentInfo.Rows)
			{
				CheckBox cb = (CheckBox)row.FindControl("chkBoxSelect");

				if (cb != null && cb.Checked)
				{
					lstcategory.Items.Add(new ListItem(row.Cells[1].Text, row.Cells[4].Text));
					gridStudentInfo.Rows[row.RowIndex].Visible = false;
				}
			}
		}

		protected void btnBackword_Click(object sender, EventArgs e)
		{
			try
			{
				int[] arr = lstcategory.GetSelectedIndices();
				ListItem[] li = new ListItem[arr.Length];
				int cnt = 0;
				foreach (GridViewRow row in gridStudentInfo.Rows)
				{
					for (int i = 0; i < arr.Length; i++)
					{
						int FlagId =Convert.ToInt16(row.Cells[4].Text);
						int ListFlagId = Convert.ToInt16(lstcategory.Items[arr[i]].Value.ToString());
						if (FlagId == ListFlagId)
						{
							row.Visible = true;
							li[cnt] = new ListItem(lstcategory.Items[arr[i]].Text, lstcategory.Items[arr[i]].Value.ToString());
							cnt++;
							
						}
					}
				}

				for (int i = 0; i < li.Count(); i++)
				{
					lstcategory.Items.Remove(li[i]);
				}
			}
			catch (Exception ex)
			{
			}
		}

		public void ShowHidePublicFlag()
		{
			String PublicFlag = String.Empty;

			if (CheckBoxShowTag.Checked)
			{
				foreach (GridViewRow row in gridStudentInfo.Rows)
				{
					PublicFlag = row.Cells[5].Text;
					if (PublicFlag == "True")
					{
						row.Visible = true;
					}
				}
			}
			else
			{
				// code to show only private flag 
				foreach (GridViewRow row in gridStudentInfo.Rows)
				{
					PublicFlag = row.Cells[5].Text;
					if (PublicFlag == "True")
					{
						row.Visible = false;
					}
				}
			}
		}

		protected void btnSave_Click(object sender, EventArgs e)
		{
			String AggregateFlagName = txtFlagName.Text.Trim();
			String AggregateDescription = txtDescription.Text.Trim();
			String AggregateKeyword = txtFlag.Text.Trim();

			bool FlagValue = false;
			bool IsAdded = false;

			if (Session["EditAggregateFlag"] != null)
			{
				int AggregateFlagId = Convert.ToInt16(Session["EditAggregateFlag"].ToString());
				int[] FlagId = new int[lstcategory.Items.Count];

				for (int i = 0; i < lstcategory.Items.Count; i++)
				{
					FlagId[i] = Convert.ToInt16(lstcategory.Items[i].Value);
				}

				_user = (User)Session["UserDetail"];

				if (_user.IsAdminUser)
				{
					FlagValue = radioFlagType.SelectedItem.Text == "Public";
				}

				_user = (User)Session["UserDetail"];

				String UserId = _user.ExternalId;

				IsAdded = _sqlHelper.EditAddAggregateFlag(AggregateFlagId, AggregateFlagName, AggregateDescription, AggregateKeyword, FlagValue, FlagId, UserId);

				if (IsAdded)
				{
					JArray _links = (JArray)Session["HomeLinks"];

					String GetCustomLink = _inBloomApi.GetCustomLink(_links);
					String EducationOrganizationId = Session["EducationOrganizationId"].ToString();

					String FlagUserId = Session["CustomUserId"].ToString();
					bool IsAdminUser = (bool)Session["CustomIsAdminUser"];
					bool IsPublic = (bool)Session["CustomIsPublic"];

					AggregateCls[] _aggregateCls = new AggregateCls[1];

					IList<AggregateFlag> _aggregateFlag = _sqlHelper.GetAggregateFlag(AggregateFlagId);

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

					int[] flagId = _sqlHelper.GetAllFlagIdOfAggregateFlagByAggregateFlagId(_aggregateFlag[0].AggregateFlagId);
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

					_inBloomApi.UpateAggregateFlag(AggregateFlagId, GetCustomLink, EducationOrganizationId, IsAdminUser, IsPublic, UserId, _aggregateCls, _user);                      
				  
					Session.Add("Success", "Aggregate flag was updated successfully.");
					Session["EditAggregateFlag"] = null;
					Response.Redirect("Search.aspx");
				}
				else
				{
					Session["Success"] = "A Aggregate flag with the name " + txtFlagName.Text.ToString() + " already exists.  Please enter a different name.";
					Session["EditAggregateFlag"] = AggregateFlagId;
					Response.Redirect("EditAggregateFlag.aspx");
				}
			}
		}

		protected void CheckBoxShowTag_CheckedChanged(object sender, EventArgs e)
		{
			ShowHidePublicFlag();
		}
	}
}