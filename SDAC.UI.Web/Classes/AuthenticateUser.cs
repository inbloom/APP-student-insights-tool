/*
 * Copyright 2012-2013 inBloom, Inc. and its affiliates.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.Web;
using System.Net;
using SDAC.UI.Web.Enums;
using SDAC.UI.Web.Helpers;

namespace SDAC.UI.Web.Classes
{
	/// <summary>
	/// User Authentication.
	/// </summary>
	public class AuthenticateUser
	{
		#region Public Methods

		/// <summary>
		/// This function is used for removing the quotes from input string.
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		public String GetStringWithoutQuote(string input)
		{
			string result = "";
			for (int i = 0; i < input.Length; i++)
			{
				if (input[i] != '\"')
				{
					result = result + input[i];
				}
			}
			return result;
		}      

		/// <summary>
		///  This function is used for applicaton authentication.
		///  If the application successfully authenticates, it returns to application url with access token.
		/// </summary>
		public void AuthorizeUser()
		{
			var oAuthUrl = ConfigurationHelper.GetItem(ConfigurationSettingEnums.OauthUrl.ToDescription());
			var clientId = ConfigurationHelper.GetItem(ConfigurationSettingEnums.ClientId.ToDescription());
			var redirectUrl = ConfigurationHelper.GetItem(ConfigurationSettingEnums.RedirectUrl.ToDescription());

			string authorizeUrl = string.Format(oAuthUrl + " authorize?client_id={0}&redirect_uri={1}", clientId, redirectUrl);
			HttpContext.Current.Response.Redirect(authorizeUrl);
		}

		/// <summary>
		/// This function is used for getting the access token from input string.
		/// Registering the access token with inBloom. 
		/// The access token is used for calling the api.
		/// </summary>
		public String GetAccessToken()
		{
			string authorizationCode = HttpContext.Current.Request.QueryString[QueryStringTokenEnum.Code.ToString()];
			var clientId = ConfigurationHelper.GetItem(ConfigurationSettingEnums.ClientId.ToDescription());
			var clientSecret = ConfigurationHelper.GetItem(ConfigurationSettingEnums.ClientSecret.ToDescription());
			var redirectUrl = ConfigurationHelper.GetItem(ConfigurationSettingEnums.RedirectUrl.ToDescription());

			// Set the authorization URL
			string sessionUrl = string.Format(ConfigurationHelper.GetItem(ConfigurationSettingEnums.OauthUrl.ToDescription()) 
				+ "token?client_id={0}&client_secret={1}&grant_type=authorization_code&redirect_uri={2}&code={3}",
				clientId,
				clientSecret,
				redirectUrl, authorizationCode);
			
			var webClient = new WebClient();
			webClient.Headers.Add("Content-Type", "application/vnd.slc+json");
			webClient.Headers.Add("Accept", "application/vnd.slc+json");
			string result = webClient.DownloadString(sessionUrl);
			string[] token = result.Split(':');

			// Remove the " , } characters from token[1] and get the 38 character long token
			string accessToken = String.Empty;
			for (int i = 0; i < token[1].Length; i++)
			{
				if ((token[1][i] >= '0' && token[1][i] <= '9') || (token[1][i] >= 'a' && token[1][i] <= 'z') || (token[1][i] >= 'A' && token[1][i] <= 'Z') || token[1][i] == '-')
				{
					accessToken += token[1][i];
				}
			}
			return accessToken;
		}

		#endregion 
	}
}
