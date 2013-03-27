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
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using inBloomApiLibrary;



namespace SDAC.Data
{
    public class EducationOrganizationsData
    {
        protected GetEducationOrganizationsData _education = new GetEducationOrganizationsData();
       
        /// <summary>
        /// Gets _education organizations details
        /// </summary>
        /// <param name="accToken"></param>
        /// <returns></returns>
        public JArray GetEducationOrganizations(string accToken)
        {
            return _education.GetEducationOrganizations(accToken);
            
        }
        /// <summary>
        /// Gets _education organizations by id
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="educationId"></param>
        /// <returns></returns>
        public JArray GetEducationOrganizationById(string accToken, string educationId)
        {
            return _education.GetEducationOrganizationById(accToken, educationId);
            
        }
        /// <summary>
        /// Gets staff Education Organization Assignment Associations with in the _education organizations
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="educationId"></param>
        /// <returns></returns>
        public JArray GetEducationOrganizationStaffEducationOrganizationAssociations(string accToken, string educationId)
        {
            return _education.GetEducationOrganizationStaffEducationOrganizationAssociations(accToken, educationId);
           
        }
        /// <summary>
        ///  Gets staff details in staff Education Organization Assignment Associations with in the _education organizations
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="educationId"></param>
        /// <returns></returns>
        public JArray GetEducationOrganizationStaffEducationOrganizationAssociationByStaff(string accToken, string educationId)
        {
            return _education.GetEducationOrganizationStaffEducationOrganizationAssociationByStaff(accToken, educationId);
           
        }
        /// <summary>
        /// Gets _education organizations custom details.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="educationId"></param>
        /// <returns></returns>
        public JArray GetEducationOrganizationCustom(string accToken, string educationId)
        {
            return _education.GetEducationOrganizationCustom(accToken, educationId);
           
        }

        #region CUD Methods
        /// <summary>
        /// Creates _education organizations  details.
        /// </summary>
        /// <param name="accToken"></param>
        ///<param name="data"></param>
        /// <returns></returns>
        public string PostEducationOrganizations(string accToken, string data)
        {
           return _education.PostEducationOrganizations( accToken, data);
            
        }
        /// <summary>
        /// Updates _education organizations  details.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="data"></param>
        /// <param name="educationId"></param>
        /// <returns></returns>
        public string PutEducationOrganizations(string accToken, string data, string educationId)
        {
            return _education.PutEducationOrganizations(accToken, data, educationId);
           
        }

        /// <summary>
        /// Deletes _education organizations  details.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="educationId"></param>
        /// <returns></returns>
        public string DeleteEducationOrganizations(string accToken, string educationId)
        {
            return _education.DeleteEducationOrganizations(accToken, educationId);
            
        }


        #endregion

    }
}
