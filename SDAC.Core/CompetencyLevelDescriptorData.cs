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
using SDAC.Data;

namespace SDAC.Core
{
    public class CoreCompetencyLevelDescriptorData
    {
        protected CompetencyLevelDescriptorData _competency = new CompetencyLevelDescriptorData();
        
        /// <summary>
        /// Gets _competency level descriptor details.
        /// </summary>
        /// <param name="accToken">Access token given by the athentication</param>
        /// <returns></returns>
        public JArray GetCompetencyLevelDescriptor(string accToken)
        {
            
            return _competency.GetCompetencyLevelDescriptor(accToken);
            
        }
        /// <summary>
        /// Gets _competency level descriptor by id. 
        /// </summary>
        /// <param name="accToken">Access Token Given by the athentication</param>
        /// <param name="competencyId">Competency id</param>
        /// <returns></returns>
        public JArray GetCompetencyLevelDescriptorById(string accToken, string competencyId)
        {
            
            return _competency.GetCompetencyLevelDescriptorById(accToken, competencyId);
            
        }
        /// <summary>
        /// Gets _competency level descriptor custom details.
        /// </summary>
        /// <param name="accToken">Access Token Given by the athentication</param>
        /// <param name="competencyId">_competency id</param>
        /// <returns></returns>
        public JArray GetCompetencyLevelDescriptorCustom(string accToken, string competencyId)
        {
            
            return _competency.GetCompetencyLevelDescriptorCustom(accToken, competencyId);
            
        }

        #region CUD Methods
        /// <summary>
        /// Creates _competency level descriptor details.
        /// </summary>
        /// <param name="accToken">Access token given by the athentication</param>
        /// <param name="data"></param>
        /// <returns></returns>
        public string PostCompetencyLevelDescriptor(string accToken, string data)
        {
           return _competency.PostCompetencyLevelDescriptor( accToken, data);
           
        }
        /// <summary>
        /// Updates _competency level descriptor details.
        /// </summary>
        /// <param name="accToken">Access token given by the athentication</param>
        /// <param name="data"></param>
        /// <param name="competencyId"></param>
        /// <returns></returns>
        public string PutCompetencyLevelDescriptor(string accToken, string data, string competencyId)
        {
            return _competency.PutCompetencyLevelDescriptor(accToken, data, competencyId);
           
        }

        /// <summary>
        /// Deletes _competency level descriptor details.
        /// </summary>
        /// <param name="accToken">Access token given by the athentication</param>
        /// <param name="competencyId"></param>
        /// <returns></returns>
        public string DeleteCompetencyLevelDescriptor(string accToken, string competencyId)
        {
            return _competency.DeleteCompetencyLevelDescriptor(accToken, competencyId);
           
        }


        #endregion

        /// <summary>
        /// Gets _competency level descriptor types.
        /// </summary>
        /// <param name="accToken">Access Token Given by the athentication</param>       
        /// <returns></returns>
        public JArray GetCompetencyLevelDescriptorTypes(string accToken)
        {
            
            return _competency.GetCompetencyLevelDescriptorTypes(accToken);
            
        }
        /// <summary>
        /// Gets _competency level descriptor types by id.
        /// </summary>
        /// <param name="accToken">Access Token Given by the athentication</param>
        /// <param name="competencyId"></param>
        /// <returns></returns>
        public JArray GetCompetencyLevelDescriptorTypeById(string accToken, string competencyId)
        {
            
            return _competency.GetCompetencyLevelDescriptorTypeById(accToken, competencyId);
            
        }
        /// <summary>
        /// Gets _competency level descriptor types custom details.
        /// </summary>
        /// <param name="accToken">Access Token Given by the athentication</param>
        /// <param name="competencyId"></param>
        /// <returns></returns>
        public JArray GetCompetencyLevelDescriptorTypeCustom(string accToken, string competencyId)
        {
            
            return _competency.GetCompetencyLevelDescriptorTypeCustom(accToken, competencyId);
            
        }


        #region CUD Methods
        /// <summary>
        /// Creates _competency level descriptor types  details.
        /// </summary>
        /// <param name="accToken">Access Token Given by the athentication</param>
        /// <param name="data"></param>
        /// <returns></returns>
        public string PostCompetencyLevelDescriptorTypes(string accToken, string data)
        {
           return _competency.PostCompetencyLevelDescriptorTypes( accToken, data);
           
        }
        /// <summary>
        /// Updates _competency level descriptor types  details.
        /// </summary>
        /// <param name="accToken">Access Token Given by the athentication</param>
        /// <param name="data"></param>
        /// <param name="competencyId"></param>
        /// <returns></returns>
        public string PutCompetencyLevelDescriptorTypes(string accToken, string data, string competencyId)
        {
           return _competency.PutCompetencyLevelDescriptorTypes(accToken, data, competencyId);
           
        }

        /// <summary>
        /// Deletes _competency level descriptor types details.
        /// </summary>
        /// <param name="accToken">Access Token Given by the athentication</param>
        /// <param name="competencyId"></param>
        /// <returns></returns>
        public string DeleteCompetencyLevelDescriptorTypes(string accToken, string competencyId)
        {
            return _competency.DeleteCompetencyLevelDescriptorTypes(accToken, competencyId);
            
        }


        #endregion


    }
}
