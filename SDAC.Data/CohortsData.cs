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
    public class CohortsData
    {
        protected GetCohortsData _cohorts = new GetCohortsData();
        
        /// <summary>
        /// Gets _cohorts details.
        /// </summary>
        /// <param name="accToken"></param>
        /// <returns></returns>
        public JArray GetCohorts(string accToken)
        {
            return _cohorts.GetCohorts(accToken);
           
        }
        /// <summary>
        /// Gets _cohorts custom details.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="cohortsId"></param>
        /// <returns></returns>
        public JArray GetCohortCustom(string accToken, string cohortsId)
        {
            return _cohorts.GetCohortCustom(accToken, cohortsId);
            
        }
        /// <summary>
        /// Gets _cohorts by id.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="cohortsId"></param>
        /// <returns></returns>
        public JArray GetCohortById(string accToken, string cohortsId)
        {
            return _cohorts.GetCohortById(accToken, cohortsId);
           
        }
        /// <summary>
        /// Gets staff cohort associations.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="cohortsId"></param>
        /// <returns></returns>
        public JArray GetCohortStaffCohortAssociations(string accToken, string cohortsId)
        {
            return _cohorts.GetCohortStaffCohortAssociations(accToken, cohortsId);
           
        }
        /// <summary>
        /// Gets staff details with in the staff cohort associations.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="cohortsId"></param>
        /// <returns></returns>
        public JArray GetCohortStaffCohortAssociationStaff(string accToken, string cohortsId)
        {
            return _cohorts.GetCohortStaffCohortAssociationStaff(accToken, cohortsId);
            
        }
        /// <summary>
        /// Gets student cohort associations.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="cohortsId"></param>
        /// <returns></returns>
        public JArray GetCohortStudentCohortAssociations(string accToken, string cohortsId)
        {
            return _cohorts.GetCohortStudentCohortAssociations(accToken, cohortsId);
           
        }
        /// <summary>
        /// Gets student details with in the student cohort associations.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="cohortsId"></param>
        /// <returns></returns>
        public JArray GetCohortStudentCohortAssociationStudents(string accToken, string cohortsId)
        {
            return _cohorts.GetCohortStudentCohortAssociationStudents(accToken, cohortsId);
           
        }



        #region CUD Methods
        /// <summary>
        /// Creates _cohorts details.
        /// </summary>
        /// <param name="accToken"></param>
        ///  <param name="data"></param>
        /// <returns></returns>
        public string PostCohorts(string accToken, string data)
        {
            return _cohorts.PostCohorts( accToken, data);
           
        }
        /// <summary>
        /// Updates _cohorts details.
        /// </summary>
        /// <param name="accToken"></param>
        ///  <param name="data"></param>
        ///   <param name="cohortsId"></param>
        /// <returns></returns>
        public string PutCohorts(string accToken, string data, string cohortsId)
        {
            return _cohorts.PutCohorts(accToken, data, cohortsId);
            
        }

        /// <summary>
        /// Deletes _cohorts details.
        /// </summary>
        /// <param name="accToken"></param>
        ///  <param name="cohortsId"></param>
        /// <returns></returns>
        public string DeleteCohorts(string accToken, string cohortsId)
        {
            return _cohorts.DeleteCohorts(accToken, cohortsId);
           
        }


        #endregion



    }
}
