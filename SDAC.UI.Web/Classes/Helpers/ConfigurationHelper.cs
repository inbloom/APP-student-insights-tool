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


using System.Configuration;

namespace SDAC.UI.Web.Helpers
{
    /// <summary>
    /// This class is used to get the global values from web.config file
    /// </summary>
    public static class ConfigurationHelper
    {
        /// <summary>
        /// This function is used for getting the global values using the key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetItem(string key)
        {
            string returnValue = ConfigurationManager.AppSettings[key];
            if (string.IsNullOrEmpty(returnValue))
            {
                // These are default values
                switch (key)
                {
                    case "ConnectionString": returnValue = ""; break;
                    case "VENDORID": returnValue = "18"; break;
                    case "VENDORName": returnValue = "REDI"; break;
                    case "COMPONENTDBVERSION": returnValue = "2.07"; break;
                    case "XSDVERSION": returnValue = " 2.0"; break;
                    case "APPLICATIONIDENTIFIER": returnValue = "0123456789654321"; break;
                    case "APPLICATIONTITLE": returnValue = "COE XML Generator"; break;
                    case "COEXMLFILESFOLDERPATH": returnValue = ""; break;
                }
            }

            return returnValue;
        }
    }
}
