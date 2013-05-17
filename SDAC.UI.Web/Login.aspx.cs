using System;
using System.Web.UI.WebControls;
using SDAC.UI.Web.Enums;

namespace SDAC.UI.Web
{
    public partial class Login : System.Web.UI.Page
    {
        protected inBloomApi _inBloomApi = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            _inBloomApi = new inBloomApi();
            try
            {
                _inBloomApi.AccessToken = Session[SessionEnum.AccessToken.ToString()].ToString();

                if (Request.QueryString["LogOut"].ToString().Equals("1"))
                {
                    _inBloomApi.LogOut();
                    Session[SessionEnum.AccessToken.ToString()] = null;
                    Session["UserDetail"] = null;
                    Session["School"] = null;
                    Session["Course"] = null;
                    Session["Section"] = null;
                    Session["SectionAndCourseOfferingId"] = null;
                    Session["CourseIdAndCourseOfferingId"] = null;
                    Response.Redirect("https://portal.sandbox.inbloom.org/portal/c/logout");
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("https://portal.sandbox.inbloom.org/portal/c/logout");
            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
        }

        protected void Login1_Authenticate(object sender, AuthenticateEventArgs e)
        {
        }
    }
}
