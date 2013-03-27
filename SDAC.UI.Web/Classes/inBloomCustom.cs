using System;
using System.Linq;
using Newtonsoft.Json.Linq;
using SDAC.UI.Web.Helpers;
using SDAC.UI.Web.Classes;
using SDAC.Core;

namespace SDAC.UI.Web
{
	public class inBloomCustom
	{
		#region "Private Variables"
		private AuthenticateUser _authenticateUser = null;
		private String _accessToken = String.Empty;
		private inBloomApi _slcApi = null;

		#endregion

		#region "Properties"

		public string AccessToken
		{
			get
			{
				return _accessToken;
			}
			set
			{
				_accessToken = value;
			}
		}
		#endregion

		SqlHelper _sqlHelper = null;

		public inBloomCustom()
		{
			AccessToken = "";
		}

		public inBloomCustom(String accessToken)
		{
			AccessToken = accessToken;
			_authenticateUser = new AuthenticateUser();
			_slcApi = new inBloomApi(accessToken);
			_sqlHelper = new SqlHelper();
		}

		public bool AddAllFlagToCustom()
		{
			return true;
		}

		public void GetFlagListForUser(String educationOrganizationId)
		{
			try
			{
				JObject homeUrlResponse = JObject.Parse(RestApiHelper.CallApi("educationOrganizations/"+educationOrganizationId+"/custom", this._accessToken));
			}
			catch (Exception ex)
			{
				//no custom found or some error
			}
		}

		public String GetEducationOrganizationId(JArray Links)
		{
			try
			{
				string educationOrganization = "";
				for (int index = 0; index < Links.Count(); index++)
				{
					String relation = _authenticateUser.GetStringWithoutQuote(Links["rel"].ToString());
					if (relation.Equals("getEducationOrganizations"))
					{
						String result = RestApiHelper.CallApiWithParameter(_authenticateUser.GetStringWithoutQuote(Links[index]["href"].ToString()), this._accessToken);
						JArray educationOrganizationResponse = JArray.Parse(result);

						for (int i = 0; i < educationOrganizationResponse.Count(); i++)
						{
							JToken token = educationOrganizationResponse[i];
							educationOrganization = _authenticateUser.GetStringWithoutQuote(token["id"].ToString());
							string organizationCategories = token["organizationCategories"].ToString();
							
							if (organizationCategories.Contains("State Education Agency"))
								educationOrganization = "State Education Agency";

							break;
						}
						return educationOrganization;
					}
				}
				return educationOrganization;
			}
			catch (Exception ex)
			{
				return null;
			}
		}

		public JArray GetHomeLinks()
		{
			try
			{
				JObject homeUrlResponse = JObject.Parse(RestApiHelper.CallApi("home", this._accessToken));
				return (JArray)homeUrlResponse["links"];
			}
			catch (Exception ex)
			{
				return null;
			}
		}

		public void PutCustomForStaff(JArray homeLinks, Temp tempObj)
		{
			try
			{
				for (int index = 0; index < homeLinks.Count(); index++)
				{
					String relation = _authenticateUser.GetStringWithoutQuote(homeLinks[index]["rel"].ToString());
					if (relation.Equals("custom"))
					{
						string link = _authenticateUser.GetStringWithoutQuote(homeLinks[index]["href"].ToString());
						string result = _slcApi.FlagObjectToJson(tempObj);
						RestApiHelper.CallApiWithParameterForCustomPUT(link, this.AccessToken, result);
					}
				}
			}
			catch (Exception ex)
			{
			}
		}
	}
}