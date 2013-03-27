using System;
using SDAC.UI.Web.Enums;

namespace SDAC.UI.Web
{
    public partial class CustomFlagPage : System.Web.UI.Page
    {
        protected inBloomApi _inBloomApi = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                _inBloomApi = new inBloomApi(Session[SessionEnum.AccessToken.ToString()].ToString());
            }
            catch (Exception Ex)
            {
                Session[SessionEnum.AccessToken.ToString()] = null;
                Response.Redirect("Search.aspx");
            }
        }

        protected void btnPreview_Click(object sender, EventArgs e)
        {
            try
            {
                string schoolId = Session[SessionEnum.SchoolId.ToString()].ToString();
                TextBoxFlagResponse.Text = _inBloomApi.GetFlagFromSchoolCustomBySchoolId(schoolId);
            }
            catch (Exception Ex)
            {
                TextBoxFlagResponse.Text = "";
                Session.Add("ErrorStack", Ex.ToString());
                Response.Redirect("ErrorPage.aspx");
            }
        }
    }
}