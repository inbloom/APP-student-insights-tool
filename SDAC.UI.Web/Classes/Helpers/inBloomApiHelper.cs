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
using System.Net;
using NLog;
using SDAC.UI.Web.Enums;

namespace SDAC.UI.Web.Helpers
{
	/// <summary>
	/// This class is used for calling the inBloom api with different parameters with different modes.
	/// </summary>
	public static class RestApiHelper
	{
		private static Logger _logger = LogManager.GetCurrentClassLogger();

		/// <summary>
		/// CallApi is used to call the api and return the response
		/// </summary>
		/// <param name="apiEndPoint"></param>
		/// <param name="accessToken"></param>
		/// <returns></returns>
		public static string CallApi(string apiEndPoint, string accessToken)
		{
			try
			{
				string bearerToken = string.Format("bearer {0}", accessToken);
				
				var webClient = new WebClient();
				webClient.Headers.Add("Authorization", bearerToken);
				webClient.Headers.Add("Content-Type", "application/vnd.slc+json");
				webClient.Headers.Add("Accept", "application/vnd.slc+json");
				return webClient.DownloadString(ConfigurationHelper.GetItem(ConfigurationSettingEnums.SlcApiUrl.ToDescription()) + "" + apiEndPoint);
			}
			catch (Exception ex)
			{
				_logger.FatalException("Error calling API: " + apiEndPoint, ex);
				throw ex;
			}
		}

		/// <summary>
		/// This function is used for calling the api for custom with PUT method
		/// </summary>
		/// <param name="apiEndPoint"></param>
		/// <param name="accessToken"></param>
		/// <param name="data"></param>
		/// <returns></returns>
		public static string CallApiForCustomPut(string apiEndPoint, string accessToken, string data)
		{
			try
			{
				string bearerToken = string.Format("bearer {0}", accessToken);

				var webClient = new WebClient();
				webClient.Headers.Add("Authorization", bearerToken);
				webClient.Headers.Add("Content-Type", "application/vnd.slc+json");
				webClient.Headers.Add("Accept", "application/vnd.slc+json");
				return webClient.UploadString(ConfigurationHelper.GetItem(ConfigurationSettingEnums.SlcApiUrl.ToDescription()) + "" + apiEndPoint, "PUT", data);
			}
			catch (Exception ex)
			{
				_logger.FatalException("Error calling API: " + apiEndPoint, ex);
				throw ex;
			}
		}

		/// <summary>
		/// This function is used for calling the api for custom with POST method
		/// </summary>
		/// <param name="apiEndPoint"></param>
		/// <param name="accessToken"></param>
		/// <param name="data"></param>
		/// <returns></returns>
		public static string CallApiForCustomPost(String apiEndPoint, string accessToken, string data)
		{
			try
			{
				string bearerToken = string.Format("bearer {0}", accessToken);

				var webClient = new WebClient();
				webClient.Headers.Add("Authorization", bearerToken);
				webClient.Headers.Add("Content-Type", "application/vnd.slc+json");
				webClient.Headers.Add("Accept", "application/vnd.slc+json");
				return webClient.UploadString(ConfigurationHelper.GetItem(ConfigurationSettingEnums.SlcApiUrl.ToDescription()) + "" + apiEndPoint, "POST", data);
			}
			catch (Exception ex)
			{
				_logger.FatalException("Error calling API: " + apiEndPoint, ex);
				throw ex;
			}
		}

		public static string CallApiWithParameterForCustomPUT(string apiEndPoint, string accessToken, string Data)
		{
			try
			{
				string bearerToken = string.Format("bearer {0}", accessToken);

				var webClient = new WebClient();
				webClient.Headers.Add("Authorization", bearerToken);
				webClient.Headers.Add("Content-Type", "application/vnd.slc+json");
				webClient.Headers.Add("Accept", "application/vnd.slc+json");
				return webClient.UploadString(apiEndPoint, "PUT", Data);
			}
			catch (Exception ex)
			{
				_logger.FatalException("Error calling API: " + apiEndPoint, ex);
				throw ex;
			}
		}

		/// <summary>
		/// CallApiWithParameter call the inBloom api with all the parameter.
		/// </summary>
		/// <param name="apiEndPoint"></param>
		/// <param name="accessToken"></param>
		/// <returns></returns>
		public static string CallApiWithParameter(string apiEndPoint, string accessToken)
		{
			try
			{
				string bearerToken = string.Format("bearer {0}", accessToken);

				var webClient = new WebClient();
				webClient.Headers.Add("Authorization", bearerToken);
				webClient.Headers.Add("Content-Type", "application/vnd.slc+json");
				webClient.Headers.Add("Accept", "application/vnd.slc+json");
				return webClient.DownloadString(apiEndPoint);
			}
			catch (Exception ex)
			{
				_logger.FatalException("Error calling API: " + apiEndPoint, ex);
				throw ex;
			}
		}
	}
}