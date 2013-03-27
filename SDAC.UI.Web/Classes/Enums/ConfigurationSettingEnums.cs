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


using SDAC.UI.Web.CustomAttributes;

namespace SDAC.UI.Web.Enums
{
    public enum ConfigurationSettingEnums
    {
        [Description("ConnectionString")]
        ConnectionString = 1,
        [Description("ClientId")]
        ClientId  = 2,
        [Description("SlcApiUrl")]
        SlcApiUrl = 3,
        [Description("OauthUrl")]
        OauthUrl = 4,
        [Description("RedirectUrl")]
        RedirectUrl = 5,
        [Description("ClientSecret")]
        ClientSecret = 6 
        
    }
}