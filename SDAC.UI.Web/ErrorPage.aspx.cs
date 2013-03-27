using System;
using System.Web.UI;
using SDAC.Core;
using SDAC.UI.Web.Enums;

namespace SDAC.UI.Web
{
	public partial class ErrorPage : Page
	{
		protected inBloomApi _inBloomApi = null;
		protected User _user = null;

		protected void Page_Load(object sender, EventArgs e)
		{
			_user = (User) Session["UserDetail"];
			Session[SessionEnum.AccessToken.ToString()] = null;
			
			if (Session["ErrorStack"] != null)
			{
				if (_user != null)
					lblUser.Text = _user.FullName;
				
				lblErrorMessage.Text = Session["ErrorStack"].ToString();
				
				Session["ErrorStack"] = null;
			}
		}
	}
}