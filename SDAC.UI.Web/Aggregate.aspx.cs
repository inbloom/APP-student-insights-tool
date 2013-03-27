using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using SDAC.DomainModel;
using SDAC.UI.Web.Enums;
using SDAC.Core;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace SDAC.UI.Web
{
    public partial class Aggregate : System.Web.UI.Page
    {
        protected SLC_SDACEntities _sDACEntities = new SLC_SDACEntities();
        protected User _user = null;
        protected SqlHelper _sqlHelper = null;
        protected inBloomApi _inBloomApi = null;

        protected void Page_Load(object sender, EventArgs e)
        {

            _sqlHelper = new SqlHelper();

            try
            {
                _inBloomApi = new inBloomApi(Session[SessionEnum.AccessToken.ToString()].ToString());
            }
            catch (Exception Ex)
            {
                Response.Redirect("Search.aspx");
            }

            if (!IsPostBack)
            {

                 _user = (User)Session["UserDetail"];

                 if (_user == null)
                 {
                     Session[SessionEnum.AccessToken.ToString()] = null;
                     Response.Redirect("Search.aspx");
                 }
                 else
                 {
                     //gridStudentInfo.DataSource = _sDACEntities.Flags;
                     //gridStudentInfo.DataSource = _sqlHelper.GetFlagList(_user.ExternalId);
                     //gridStudentInfo.DataBind();

                     IList<Flag> _flag = _sqlHelper.GetFlagList(_user.ExternalId);

                     IList<Flag> _flagListTemp = null;

                     String EducationOrganizationId = Session["EducationOrganizationId"].ToString();
                     String[] AdminUserId = _inBloomApi.GetAdminIdByEducationOrganizationId(EducationOrganizationId);
                     if (AdminUserId != null)
                     {
                         for (int i = 0; i < AdminUserId.Count(); i++)
                         {
                             _flagListTemp = _sqlHelper.GetPublicFlagListForAdmin(AdminUserId[i]);
                             if (_flag == null)
                             {
                                 _flag = _flagListTemp;
                             }
                             else
                             {
                                 for (int j = 0; j < _flagListTemp.Count; j++)
                                 {
                                     _flag.Add(_flagListTemp[j]);
                                 }

                             }
                         }
                     }

                     gridStudentInfo.DataSource = _flag;

                     gridStudentInfo.DataBind();

                     if (_user.IsAdminUser)
                     {
                     }
                     else
                     {
                         PanelFlagType.Visible = false;
                     }

                 }

                 

               
            }

            String script = "function abc() {" +

                         "var count = 0;" +
                         "$('#lstcategory option').each(function (e) {" +
                             "count = count + 1;" +
                         "});" +

                         "$('#gridStudentInfo tr input:checkbox').each(function () {" +

                             "if (this.checked) {" +
                                 "count = count + 1;" +
                             "}" +

                         "});" +

                         "if (count > 5) {" +

                             "DisplayErrorPopup('You can select only 5 flags.');" +
                             "return false;" +
                         "}" +

                         "return true;" +

                     "}";

           
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), Guid.NewGuid().ToString(), script, true);
            
          
        }




        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                String AggregateFlagName = txtFlagName.Text.Trim();
                String AggregateDescription = txtDescription.Text.Trim();
                String AggregateKeyword = txtFlag.Text.Trim();

                bool FlagValue = false;
                bool IsAdded = false;



                int[] FlagId = new int[lstcategory.Items.Count];

                for (int i = 0; i < lstcategory.Items.Count; i++)
                {
                    String FlagName = lstcategory.Items[i].Text;
                    //FlagId[i] = _sqlHelper.GetFlagId(FlagName);
                    FlagId[i] = Convert.ToInt16(lstcategory.Items[i].Value);
                }

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

                if (AggregateKeyword.Equals("Enter one or more keywords for the flag (optional)"))
                {
                    AggregateKeyword = "";
                }

                _user = (User)Session["UserDetail"];

                String UserId = _user.ExternalId;

                IsAdded = _sqlHelper.AddAggregateFlag(AggregateFlagName, AggregateDescription, AggregateKeyword, FlagValue, FlagId, UserId);

                if (IsAdded)
                {
                    JArray _links = (JArray)Session["HomeLinks"];

                    String GetCustomLink = _inBloomApi.GetCustomLink(_links);
                    String EducationOrganizationId = Session["EducationOrganizationId"].ToString();

                    AggregateCls[] _aggregateCls = new AggregateCls[1];
                    int AggregateFlagId = _sqlHelper.GetAggregateFlagIdByNameDescriptionAndKeyword(AggregateFlagName, AggregateDescription, AggregateKeyword);

                    IList<AggregateFlag> _aggregateFlag = _sqlHelper.GetAggregateFlag(AggregateFlagId);

                    _aggregateCls[0] = new AggregateCls();
                    _aggregateCls[0].AggregateFlagId = _aggregateFlag[0].AggregateFlagId;
                    _aggregateCls[0].AggregateFlagDescription = _aggregateFlag[0].AggregateFlagDescription;
                    _aggregateCls[0].AggregateFlagName = _aggregateFlag[0].AggregateFlagName;
                    _aggregateCls[0].Keyword = _aggregateFlag[0].Keyword;

                    _aggregateCls[0].IsPublic = false;
                    _aggregateCls[0].IsFavorite = false;
                    _aggregateCls[0].UserId = _user.ExternalId;
                    _aggregateCls[0].CreatedBy = _user.FullName;
                    _aggregateCls[0].ModifiedBy = _user.FullName;
                    _aggregateCls[0].CreatedDate = DateTime.Now;
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

                    if (_user.IsAdminUser && FlagValue == true)
                    {
                        // process to store the public flag into organization
                        _aggregateCls[0].IsPublic = true;
                        _inBloomApi.AddAggregateFlagIntoEducationOrganization(_user, EducationOrganizationId, _aggregateCls);
                    }
                    else
                    {
                        // process to add flag 
                        _inBloomApi.AddAggregateFlagsIntoCustom(GetCustomLink, _user, _aggregateCls);
                    }



                    Session.Add("Success", "Aggregate flag saved successfully.");
                    Response.Redirect("Search.aspx");
                }
                else
                {
                    Session.Add("Success", "A flag with the name " + AggregateFlagName + " already exists. Please enter a different name.");
                    Response.Redirect("Aggregate.aspx");
                }
            }
            catch (Exception Ex)
            {
                
            }

        }

        public void ShowHidePublicFlag()
        {
            String PublicFlag = String.Empty;

            if (CheckBoxShowTag.Checked == true)
            {
                // code to show the with public flag
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

        protected void btnForward_Click(object sender, EventArgs e)
        {

            try
            {
                lstcategory.Items.Clear();
                int cnt = 0;
                foreach (GridViewRow row in gridStudentInfo.Rows)
                {
                    CheckBox cb = (CheckBox)row.FindControl("chkBoxSelect");

                    if (cb != null && cb.Checked)
                    {
                        cnt = cnt + 1;
                        lstcategory.Items.Add(new ListItem(row.Cells[1].Text, row.Cells[4].Text));
                        gridStudentInfo.Rows[(int)row.RowIndex].Visible = false;
                    }
                }
                HiddenFieldFlagCount.Value = cnt.ToString();
            }
            catch (Exception ex)
            {

                
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
                        int FlagId = Convert.ToInt16(row.Cells[4].Text);
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

        protected void CheckBoxShowTag_CheckedChanged(object sender, EventArgs e)
        {
            ShowHidePublicFlag();
        }
}
}
