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
    public class StaffData
    {
        protected GetStaffData _staff = new GetStaffData();
        
        
        /// <summary>
        /// Gets _staff details.
        /// </summary>
        /// <param name="accToken"></param>
        /// <returns></returns>
        public JArray GetStaff(string accToken)
        {
            return _staff.GetStaff(accToken);
           
        }
        /// <summary>
        ///  Gets _staff custom details.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="staffId"></param>
        /// <returns></returns>
        public JArray GetStaffCustom(string accToken, string staffId)
        {
            
            return _staff.GetStaffCustom(accToken, staffId);
            
        }
        /// <summary>
        ///  Gets _staff cohort associations with in the _staff.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="staffId"></param>
        /// <returns></returns>
        public JArray GetStaffStaffCohortAssociations(string accToken, string staffId)
        {
            
            return _staff.GetStaffStaffCohortAssociations(accToken, staffId);
            
        }
        /// <summary>
        /// Gets cohorts in _staff cohort associations with in the _staff.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="staffId"></param>
        /// <returns></returns>
        public JArray GetStaffStaffCohortAssociationCohorts(string accToken, string staffId)
        {
            
            return _staff.GetStaffStaffCohortAssociationCohorts(accToken, staffId);
            
        }
        /// <summary>
        /// Gets _staff education organization assignment associations with in the _staff.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="staffId"></param>
        /// <returns></returns>
        public JArray GetStaffStaffEducationOrganizationAssignmentAssociations(string accToken, string staffId)
        {
            
            return _staff.GetStaffStaffEducationOrganizationAssignmentAssociations(accToken, staffId);
            
        }
        /// <summary>
        ///  Gets education organizations in _staff education organization assignment associations with in the _staff.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="staffId"></param>
        /// <returns></returns>
        public JArray GetStaffStaffEducationOrganizationAssignmentAssociationEducationOrganizations(string accToken, string staffId)
        {
            
            return _staff.GetStaffStaffEducationOrganizationAssignmentAssociationEducationOrganizations(accToken, staffId);
            
        }
        /// <summary>
        /// Gets _staff program associations with in the _staff.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="staffId"></param>
        /// <returns></returns>
        public JArray GetStaffStaffProgramAssociations(string accToken, string staffId)
        {
            
            return _staff.GetStaffStaffProgramAssociations(accToken, staffId);
            
        }
        /// <summary>
        /// Gets programs in _staff program associations with in the _staff.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="staffId"></param>
        /// <returns></returns>
        public JArray GetStaffStaffProgramAssociationPrograms(string accToken, string staffId)
        {
            
            return _staff.GetStaffStaffProgramAssociationPrograms(accToken, staffId);
            
        }

        #region CUD Methods
        /// <summary>
        /// Creates _staff details
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="Data"></param>
        /// <returns></returns>
        public string PostStaff(string accToken, string data)
        {
            
            return _staff.PostStaff(accToken, data);
           
        }
        /// <summary>
        /// Updates Staff Details.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="Data"></param>
        /// <param name="sessionId"></param>
        /// <returns></returns>
        public string Putstaff(string accToken, string data, string sessionId)
        {
            
            return _staff.Putstaff(accToken, data, sessionId);
           
        }

        /// <summary>
        /// Deletes _staff details.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="sessionId"></param>
        /// <returns></returns>
        public string Deletestaff(string accToken, string sessionId)
        {
            
            return _staff.Deletestaff(accToken, sessionId);
           
        }
        #endregion
        

        /// <summary>
        /// Gets _staff cohort associations details.
        /// </summary>
        /// <param name="accToken"></param>
        /// <returns></returns>
        public JArray GetStaffCohortAssociations(string accToken)
        {
            
            return _staff.GetStaffCohortAssociations(accToken);
            
        }
        /// <summary>
        ///  Gets _staff cohort associations custom  details.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="staffId"></param>
        /// <returns></returns>
        public JArray GetStaffCohortAssociationCustom(string accToken, string staffId)
        {
            
            return _staff.GetStaffCohortAssociationCustom(accToken, staffId);
            
        }
        /// <summary>
        ///  Gets _staff cohort associations details by id.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="staffId"></param>
        /// <returns></returns>
        public JArray GetStaffCohortAssociationById(string accToken, string staffId)
        {
            
            return _staff.GetStaffCohortAssociationById(accToken, staffId);
            
        }
        /// <summary>
        ///  Gets cohorts details with in the _staff cohort associations details.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="staffId"></param>
        /// <returns></returns>
        public JArray GetStaffCohortAssociationCohorts(string accToken, string staffId)
        {
            
            return _staff.GetStaffCohortAssociationCohorts(accToken, staffId);
            
        }
        /// <summary>
        /// Gets _staff details with in the _staff cohort associations details.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="staffId"></param>
        /// <returns></returns>
        public JArray GetStaffCohortAssociationStaff(string accToken, string staffId)
        {
            
            return _staff.GetStaffCohortAssociationStaff(accToken, staffId);
            
        }
        


        #region CUD Methods
        /// <summary>
        /// Creates  _staff Cohort Associations details
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public string PostStaffCohortAssociations(string accToken, string data)
        {
            
            return _staff.PostStaffCohortAssociations(accToken, data);
           
        }
        /// <summary>
        /// Updates _staff Cohort Associations details.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="data"></param>
        /// <param name="staffId"></param>
        /// <returns></returns>
        public string PutStaffCohortAssociations(string accToken, string data, string staffId)
        {
            
            return _staff.PutStaffCohortAssociations(accToken, data, staffId);
           
        }

        /// <summary>
        /// Deletes _staff Cohort Associations details.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="staffId"></param>
        /// <returns></returns>
        public string DeleteStaffCohortAssociations(string accToken, string staffId)
        {
            
            return _staff.DeleteStaffCohortAssociations(accToken, staffId);
           
        }
        #endregion


        /// <summary>
        /// Gets _staff education organization assignment associations details.
        /// </summary>
        /// <param name="accToken"></param>
        /// <returns></returns>
        public JArray GetStaffEducationOrganizationAssignmentAssociations(string accToken)
        {
            
            return _staff.GetStaffEducationOrganizationAssignmentAssociations(accToken);
            
        }
        /// <summary>
        /// Gets _staff education organization assignment associations custom details.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="staffId"></param>
        /// <returns></returns>
        public JArray GetStaffEducationOrganizationAssignmentAssociationCustom(string accToken, string staffId)
        {
            
            return _staff.GetStaffEducationOrganizationAssignmentAssociationCustom(accToken, staffId);
            
        }
        /// <summary>
        /// Gets _staff education organization assignment associations details by id.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="staffId"></param>
        /// <returns></returns>
        public JArray GetStaffEducationOrganizationAssignmentAssociationById(string accToken, string staffId)
        {
            
            return _staff.GetStaffEducationOrganizationAssignmentAssociationById(accToken, staffId);
            
        }
        /// <summary>
        /// Gets education organizations with in the _staff education organization assignment associations.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="staffId"></param>
        /// <returns></returns>
        public JArray GetStaffEducationOrganizationAssignmentAssociationEducationOrganizations(string accToken, string staffId)
        {
            
            return _staff.GetStaffEducationOrganizationAssignmentAssociationEducationOrganizations(accToken, staffId);
            
        }
        /// <summary>
        /// Gets _staff details with in the _staff education organization assignment associations.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="staffId"></param>
        /// <returns></returns>
        public JArray GetStaffEducationOrganizationAssignmentAssociationStaff(string accToken, string staffId)
        {
            
            return _staff.GetStaffEducationOrganizationAssignmentAssociationStaff(accToken, staffId);
            
        }


        #region CUD Methods
        /// <summary>
        /// Creates _staff Education Organization Assignment Associations dettails.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public string PostStaffEducationOrgAssignmentAssociations(string accToken, string data)
        {
            return _staff.PostStaffEducationOrgAssignmentAssociations( accToken, data);
           
        }
        /// <summary>
        /// Updates _staff Education Organization Assignment Associations dettails.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="data"></param>
        /// <param name="staffId"></param>
        /// <returns></returns>
        public string PutStaffEducationOrgAssignmentAssociations(string accToken, string data, string staffId)
        {
            return   _staff.PutStaffEducationOrgAssignmentAssociations(accToken, data, staffId);
           
        }

        /// <summary>
        /// Deletes _staff Education Organization Assignment Associations dettails.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="staffId"></param>
        /// <returns></returns>
        public string DeleteStaffEducationOrgAssignmentAssociations(string accToken, string staffId)
        {
            return _staff.DeleteStaffEducationOrgAssignmentAssociations(accToken, staffId);
          
        }
        #endregion



        /// <summary>
        /// Gets _staff program associations details.
        /// </summary>
        /// <param name="accToken"></param>
        /// <returns></returns>
        public JArray GetStaffProgramAssociations(string accToken)
        {
            
            return _staff.GetStaffProgramAssociations(accToken);
            
        }
        /// <summary>
        /// Gets _staff program associations custom  details.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="staffId"></param>
        /// <returns></returns>
        public JArray GetStaffProgramAssociationCustom(string accToken, string staffId)
        {
            
            return _staff.GetStaffProgramAssociationCustom(accToken, staffId);
            
        }
        /// <summary>
        /// Gets _staff program associations details by program id.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="programId"></param>
        /// <returns></returns>
        public JArray GetStaffProgramAssociationByProgramId(string accToken, string programId)
        {
            
            return _staff.GetStaffProgramAssociationByProgramId(accToken, programId);
            
        }
        /// <summary>
        /// Gets _staff program associations details by id.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="staffId"></param>
        /// <returns></returns>
        public JArray GetStaffProgramAssociationById(string accToken, string staffId)
        {
            
            return _staff.GetStaffProgramAssociationById(accToken, staffId);
            
        }
        /// <summary>
        /// Gets programs with in the _staff program associations.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="staffId"></param>
        /// <returns></returns>
        public JArray GetStaffProgramAssociationPrograms(string accToken, string staffId)
        {
            
            return _staff.GetStaffProgramAssociationPrograms(accToken, staffId);
            
        }
        /// <summary>
        /// Gets _staff with in the _staff program associations.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="staffId"></param>
        /// <returns></returns>
        public JArray GetStaffProgramAssociationStaff(string accToken, string staffId)
        {
            
            return _staff.GetStaffProgramAssociationStaff(accToken, staffId);
            
        }


        #region CUD Methods
        /// <summary>
        /// Creates _staff program associations details.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public string PostStaffProgramAssociations(string accToken, string data)
        {
            return _staff.PostStaffProgramAssociations(accToken, data);
          
        }
        /// <summary>
        /// Updates  _staff program associations details.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="data"></param>
        /// <param name="staffId"></param>
        /// <returns></returns>
        public string PutStaffProgramAssociations(string accToken, string data, string staffId)
        {
            return _staff.PutStaffProgramAssociations(accToken, data, staffId);
            
        }

        /// <summary>
        /// Deletes  _staff program associations details.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="staffId"></param>
        /// <returns></returns>
        public string DeleteStaffProgramAssociations(string accToken, string staffId)
        {
            return _staff.DeleteStaffProgramAssociations(accToken, staffId);
           
        }
        #endregion
    }
}
