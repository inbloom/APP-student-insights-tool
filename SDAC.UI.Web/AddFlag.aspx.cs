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
    public partial class AddEditflag : System.Web.UI.Page
    {
        protected SLC_SDACEntities _sDACEntities = new SLC_SDACEntities();
        protected User _user = null;
        protected SqlHelper _sqlHelper = null;
        protected inBloomApi _inBloomApi = null;
        protected int _conditionId;

        protected void Page_Load(object sender, EventArgs e)
        {          
           
            _sqlHelper = new SqlHelper();

            try
            {
                _inBloomApi = new inBloomApi(Session[SessionEnum.AccessToken.ToString()].ToString());
            }
            catch (Exception Ex)
            {
                Session[SessionEnum.AccessToken.ToString()] = null;
                Response.Redirect("Search.aspx");

            }

            if (!IsPostBack)
            {
                try
                {
                  
                    LoadDataDomainList();
                    LoadGridWithAttribute();
                    lstCondition.DataSource = _sDACEntities.Conditions;
                    lstCondition.DataBind();

                    DataTable dt = new DataTable();
                    dt.Columns.Add("student_Name");
                    dt.Columns.Add("GPA");

                    for (int i = 0; i < 5; i++)
                    {
                        dt.Rows.Add((new object[] { "", "" }));
                    }

                    gridDisplayResult.DataSource = dt;
                    gridDisplayResult.DataBind();

                    _user = (User)Session["UserDetail"];

                    if (_user.IsAdminUser)
                    {
                    }
                    else
                    {                        
                        PanelFlagType.Visible = false;                        
                    }
                   
                }
                catch (Exception Ex)
                {
                }

            }
            
        }
       
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                 bool FlagValue = false;

                 _sqlHelper = new SqlHelper();

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

                    String FlagKeyword = txtFlag.Text.Trim();
                    if (FlagKeyword.Equals("Enter one or more keywords for the flag (optional)"))
                    {
                        FlagKeyword = "";
                    }

                    bool IsFlagAdded = false;

                    if (Request.Form["ctl00$ContentPlaceHolder1$hdnDataElementId"] != null)
                    {
                        IsFlagAdded = _sqlHelper.AddFlag(txtFlagName.Text.Trim(), txtDescription.Text.Trim(), FlagKeyword, Convert.ToInt16(Request.Form["ctl00$ContentPlaceHolder1$hdnDataElementId"]),
                            Convert.ToInt16(lstCondition.SelectedItem.Value), txtSetVal.Text.Trim(), txtSetVal2.Text.Trim(), _user.ExternalId, _user.FullName, FlagValue
                            );
                       
                    }


                    if (IsFlagAdded)
                    {
                        Session.Add("Success", "Flag added successfully.");
                       

                        int FlagId = _sqlHelper.GetFlagIdByNameDescriptionAndKeyword(txtFlagName.Text.Trim(), txtDescription.Text.Trim(), FlagKeyword);


                        JArray _links = (JArray)Session["HomeLinks"];

                        String GetCustomLink = _inBloomApi.GetCustomLink(_links);
                        String EducationOrganizationId = Session["EducationOrganizationId"].ToString();


                        FlagCls[] _flagCls = new FlagCls[1];
                        _flagCls[0] = new FlagCls();
                        _flagCls[0].FlagId = FlagId;
                        _flagCls[0].FlagName = txtFlagName.Text.Trim();
                        _flagCls[0].FlagDescription = txtDescription.Text.Trim();
                        _flagCls[0].FlagKeyword = FlagKeyword;
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


                        if (_user.IsAdminUser && FlagValue == true)
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


                        Response.Redirect("Search.aspx");

                    }
                    else
                    {
                        //flag is already exist or some exception
                        Session.Add("Success", "A flag with the name " + txtFlagName.Text.ToString()  + " already exists. Please enter a different name.");
                        // Session.Add("Success", "Flag is name already exist");
                        Response.Redirect("AddFlag.aspx");

                    }
                }
                else
                {
                    Response.Redirect("Search.aspx");
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


                
                //ADDED CODE FOR CONCATENATING ENTITY
                //If this code is placed as it is after adding the new datadomains it fails and the query executes indefinitely and does not display any result

                if (!FieldName.Equals(""))
                {

                

                      if (!ExternalEntity.Equals(""))
                          e.Row.Cells[1].Text = _inBloomApi.GetWellFormattedString(char.ToUpper(entity[0]) + entity.Substring(1)) + ". " + _inBloomApi.GetWellFormattedString(ExternalEntity) + ". " + _inBloomApi.GetWellFormattedString(FieldName);
                      else
                          e.Row.Cells[1].Text = _inBloomApi.GetWellFormattedString(char.ToUpper(entity[0]) + entity.Substring(1)) + ". " + _inBloomApi.GetWellFormattedString(FieldName);


                }

                //// END ADDED CODE


                RowID = "row" + e.Row.RowIndex;
                e.Row.Attributes.Add("id", "row" + e.Row.RowIndex);
                e.Row.Attributes.Add("onclick", "ChangeRowColor(" + "'" + RowID + "'" + ")");


            }

           
        }
            

        protected void btnPreview_Click(object sender, EventArgs e)
        {
            try
            {
                int DataElementId = Convert.ToInt16(Request.Form["ctl00$ContentPlaceHolder1$hdnDataElementId"]);
                _conditionId = Convert.ToInt16(lstCondition.SelectedItem.Value);
                String Entity = _sqlHelper.GetEntityNameByDataElementId(DataElementId);
                String FieldName = _sqlHelper.GetFieldNameByDataElementId(DataElementId);
                String DataType = _sqlHelper.GetDataTypeByDataElementId(DataElementId);
                bool _responseType = false;
                _user = (User)Session["UserDetail"];
                String UserId = _user.ExternalId;
                String SchoolId = Session[SessionEnum.SchoolId.ToString()].ToString();
                String CourseId = Session[SessionEnum.CourseId.ToString()].ToString();
                String SectionId = Session[SessionEnum.SectionId.ToString()].ToString();
                String Value1 = txtSetVal.Text;
                String Value2 = txtSetVal2.Text;

               // String ExternalField = "";
                bool _isPreview = true;

                DataTable dt = _inBloomApi.RunFlag(FieldName, DataType, _responseType, UserId, SchoolId, CourseId, SectionId, _conditionId, Value1, Value2, Entity, _isPreview);                            

                gridDisplayResult.DataSource = dt;
                gridDisplayResult.DataBind();
                gridDisplayResult.HeaderRow.Cells[1].Text = FieldName;
            }
            catch (Exception Ex)
            {
               
            }
        }


        protected void DropDownListEntity_SelectedIndexChanged(object sender, EventArgs e)
        {
            autocomplete.Text = "";
            hdnDataElementId.Value = "";
            hdnDataType.Value = "";

            DataTable dt = new DataTable();
            dt.Columns.Add("student_Name");
            dt.Columns.Add("GPA");

            for (int i = 0; i < 5; i++)
            {

                dt.Rows.Add((new object[] { "", "" }));
            }

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
            String DataDomain = DropDownListEntity.SelectedItem.Text;
            if (DataDomain == "" || DataDomain == null)
            {

            }
            else
            {
                GridResult.DataSource = _sqlHelper.GetAllDataElementByDataDomain(DataDomain);
                GridResult.DataBind();
            }
        }
       
}
}

