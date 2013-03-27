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
    public class CoreLearningObjectivesData
    {

        protected LearningObjectivesData _learningObj = new LearningObjectivesData();
        
        /// <summary>
        /// Gets Learning Objectives details.
        /// </summary>
        /// <param name="accToken"></param>
        /// <returns></returns>
        public JArray GetLearningObjectives(string accToken)
        {
            
            return _learningObj.GetLearningObjectives(accToken);
            
        }
        /// <summary>
        /// Gets learning objective by id.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="learningId"></param>
        /// <returns></returns>
        public JArray GetLearningObjectiveById(string accToken, string learningId)
        {
            
            return _learningObj.GetLearningObjectiveById(accToken, learningId);
            
        }
        /// <summary>
        /// Gets learning objective custom details.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="learningId"></param>
        /// <returns></returns>
        public JArray GetLearningObjectiveCustom(string accToken, string learningId)
        {
            
            return _learningObj.GetLearningObjectiveCustom(accToken, learningId);
            
        }
        /// <summary>
        /// Gets child learning objectives with in the learning objective.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="learningId"></param>
        /// <returns></returns>
        public JArray GetLearningObjectiveChildLearningObjectives(string accToken, string learningId)
        {
            
            return _learningObj.GetLearningObjectiveChildLearningObjectives(accToken, learningId);
            
        }
        /// <summary>
        /// Gets learning standards with in the learning objective.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="learningId"></param>
        /// <returns></returns>
        public JArray GetLearningObjectiveLearningStandards(string accToken, string learningId)
        {
            
            return _learningObj.GetLearningObjectiveLearningStandards(accToken, learningId);
            
        }
        /// <summary>
        /// Gets parent learning objectives with in the learning objective.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="learningId"></param>
        /// <returns></returns>
        public JArray GetLearningObjectiveParentLearningObjectives(string accToken, string learningId)
        {
            
            return _learningObj.GetLearningObjectiveParentLearningObjectives(accToken, learningId);
            
        }
        /// <summary>
        /// Gets student competencies with in the  learning objective.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="learningId"></param>
        /// <returns></returns>
        public JArray GetLearningObjectiveStudentCompetencies(string accToken, string learningId)
        {
            
            return _learningObj.GetLearningObjectiveStudentCompetencies(accToken, learningId);
            
        }

        #region CUD Methods
        /// <summary>
        ///  Creates Learning Objectives
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public string PostLearningObjectives(string accToken, string data)
        {
            
            return _learningObj.PostLearningObjectives( accToken, data);
            
        }
        /// <summary>
        /// Updates  Learning Objectives
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="data"></param>
        /// <param name="learningId"></param>
        /// <returns></returns>
        public string PutLearningObjectives(string accToken, string data, string learningId)
        {
            
            return _learningObj.PutLearningObjectives(accToken, data, learningId);
            
        }

        /// <summary>
        /// Deletes  Learning Objectives
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="learningId"></param>
        /// <returns></returns>
        public string DeleteLearningObjectives(string accToken, string learningId)
        {
            
            return _learningObj.DeleteLearningObjectives(accToken, learningId);
            
        }


        #endregion



        /// <summary>
        /// Gets learning standards details.
        /// </summary>
        /// <param name="accToken"></param>
        /// <returns></returns>
        public JArray GetLearningStandards(string accToken)
        {
            
            return _learningObj.GetLearningStandards(accToken);
            
        }
        /// <summary>
        ///  Gets learning standards details by id.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="learningId"></param>
        /// <returns></returns>
        public JArray GetLearningStandardById(string accToken, string learningId)
        {
            
            return _learningObj.GetLearningStandardById(accToken, learningId);
            
        }
        /// <summary>
        ///  Gets learning standards custom details.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="learningId"></param>
        /// <returns></returns>
        public JArray GetLearningStandardCustom(string accToken, string learningId)
        {
            
            return _learningObj.GetLearningStandardCustom(accToken, learningId);
            
        }

        #region CUD Methods
        /// <summary>
        ///  Creates learning standards  details.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="learningId"></param>
        /// <returns></returns>
        public string PostLearningStandards(string accToken, string data)
        {
            
            return _learningObj.PostLearningStandards( accToken, data);
            
        }
        /// <summary>
        ///  Updates learning standards  details.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="learningId"></param>
        /// <returns></returns>
        public string PutLearningStandards(string accToken, string data, string learningId)
        {
            
            return _learningObj.PutLearningStandards(accToken, data, learningId);
            
        }

        /// <summary>
        ///  Deletes learning standards  details.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="learningId"></param>
        /// <returns></returns>
        public string DeleteLearningStandards(string accToken, string learningId)
        {
            
            return _learningObj.DeleteLearningStandards(accToken, learningId);
            
        }


        #endregion

    }
}
