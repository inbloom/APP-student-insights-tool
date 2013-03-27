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
    public class CoreDisciplineData
    {
        protected DisciplineData _descipline = new DisciplineData();
       
        /// <summary>
        /// Gets discipline actions details.
        /// </summary>
        /// <param name="accToken"></param>
        /// <returns></returns>
        public JArray GetDisciplineActions(string accToken)
        {
             
             return _descipline.GetDisciplineActions(accToken); 
            
        }
        /// <summary>
        /// Gets _descipline action details by id.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="disciplineId"></param>
        /// <returns></returns>
        public JArray GetDisciplineActionById(string accToken, string disciplineId)
        {
             
             return _descipline.GetDisciplineActionById(accToken, disciplineId);
            
        }
        /// <summary>
        /// Gets discipline action custom details.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="disciplineId"></param>
        /// <returns></returns>
        public JArray GetDisciplineActionCustom(string accToken, string disciplineId)
        {
            
            return _descipline.GetDisciplineActionCustom(accToken, disciplineId);
            
        }


        #region CUD Methods
        /// <summary>
        /// Creates discipline action  details.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public string PostDisciplineActions(string accToken, string data)
        {
            
           return _descipline.PostDisciplineActions( accToken, data);
            
        }
        /// <summary>
        /// Updates discipline action  details.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="data"></param>
        /// <param name="disciplineId"></param>
        /// <returns></returns>
        public string PutDisciplineActions(string accToken, string data, string disciplineId)
        {
            
           return _descipline.PutDisciplineActions(accToken, data, disciplineId);
            
        }

        /// <summary>
        /// Deletes discipline action  details.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="disciplineId"></param>
        /// <returns></returns>
        public string DeleteDisciplineActions(string accToken, string disciplineId)
        {
            
           return _descipline.DeleteDisciplineActions(accToken, disciplineId);
            
        }


        #endregion


        /// <summary>
        /// Gets discipline incidents details.
        /// </summary>
        /// <param name="accToken"></param>
        /// <returns></returns>
        public JArray GetDisciplineIncidents(string accToken)
        {
             
             return _descipline.GetDisciplineIncidents(accToken);
            
        }
        /// <summary>
        ///  Gets discipline incidents details by id.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="disciplineId"></param>
        /// <returns></returns>
        public JArray GetDisciplineIncidentById(string accToken, string disciplineId)
        {
            
            return _descipline.GetDisciplineIncidentById(accToken, disciplineId);
            
        }
        /// <summary>
        ///  Gets discipline incident custom details.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="disciplineId"></param>
        /// <returns></returns>
        public JArray GetDisciplineIncidentCustom(string accToken, string disciplineId)
        {
            
            return _descipline.GetDisciplineIncidentCustom(accToken, disciplineId);
            
        }
        /// <summary>
        /// Gets student discipline incident associations with in the  discipline incidents.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="disciplineId"></param>
        /// <returns></returns>
        public JArray GetDisciplineIncidentStudentDisciplineIncidentAssociations(string accToken, string disciplineId)
        {
            
            return _descipline.GetDisciplineIncidentStudentDisciplineIncidentAssociations(accToken, disciplineId);
            
        }
        /// <summary>
        /// Gets students details in student discipline incident associations  with in the  discipline incidents.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="disciplineId"></param>
        /// <returns></returns>
        public JArray GetDisciplineIncidentStudentDisciplineIncidentAssociationStudents(string accToken, string disciplineId)
        {
            
            return _descipline.GetDisciplineIncidentStudentDisciplineIncidentAssociationStudents(accToken, disciplineId);
            
        }

        #region CUD Methods
        /// <summary>
        /// Creates discipline incidents details.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public string PostDisciplineIncidents(string accToken, string data)
        {
            
           return _descipline.PostDisciplineIncidents( accToken,data);
            
        }
        /// <summary>
        /// Updates discipline incidents details.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="data"></param>
        /// <param name="disciplineId"></param>
        /// <returns></returns>
        public string PutDisciplineIncidents(string accToken, string data, string disciplineId)
        {
            
           return _descipline.PutDisciplineIncidents(accToken, data, disciplineId);
            
        }

        /// <summary>
        /// Deletes discipline incidents details.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="disciplineId"></param>
        /// <returns></returns>
        public string DeleteDisciplineIncidents(string accToken, string disciplineId)
        {
            
           return _descipline.DeleteDisciplineIncidents(accToken, disciplineId);
            
        }


        #endregion

    }
}
