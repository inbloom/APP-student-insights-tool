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
    public class CoreAssessmentsData
    {
        protected AssessmentsData _assessment = new AssessmentsData();
        
        #region POST 
        

        /// <summary>
        /// this function is used to post the assessment details.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public string PostAssessments(string accToken, string data)
        {
            
            //apiEndPoint = String.Format(cmn.BaseURL + "/assessments");
            return _assessment.PostAssessments( accToken, data);
            
        }
        /// <summary>
        /// Updates assessments details.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="data"></param>
        /// <param name="AssessmentId"></param>
        /// <returns></returns>
        public string PutAssessments(string accToken, string data,string AssessmentId)
        {
            
            return _assessment.PutAssessments(accToken, data, AssessmentId);
            
        }

        /// <summary>
        /// Deletes assessments details.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="AssessmentId"></param>
        /// <returns></returns>
        public string DeleteAssessments(string accToken,  string AssessmentId)
        {
            
            return _assessment.DeleteAssessments(accToken, AssessmentId);
            
        }

        #endregion

        /// <summary>
        /// Gets assessments details.
        /// </summary>
        /// <param name="accToken"></param>
        /// <returns></returns>
        public JArray GetAssessments(string accToken)
        {
            
           return _assessment.GetAssessments(accToken);
           
        }
        /// <summary>
        /// Gets assessments learning objective details.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="assessmentsId"></param>
        /// <returns></returns>
        public JArray GetAssessmentLearningObjective(string accToken, string assessmentsId)
        {
            
           return _assessment.GetAssessmentLearningObjective(accToken, assessmentsId);
           
        }
        /// <summary>
        ///  Gets assessments learning standards.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="assessmentsId"></param>
        /// <returns></returns>
        public JArray GetAssessmentLearningStandards(string accToken, string assessmentsId)
        {
            
           return _assessment.GetAssessmentLearningStandards(accToken, assessmentsId);
           
        }
       /// <summary>
        ///  Gets student assessments details. 
       /// </summary>
       /// <param name="accToken"></param>
       /// <param name="assessmentsId"></param>
       /// <returns></returns>
        public JArray GetAssessmentStudentAssessments(string accToken, string assessmentsId)
        {
            
           return _assessment.GetAssessmentStudentAssessments(accToken, assessmentsId);
           
        }
        /// <summary>
        ///  Gets students details with in the student assessments.  
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="assessmentsId"></param>
        /// <returns></returns>
        public JArray GetAssessmentStudentAssessmentsStudents(string accToken, string assessmentsId)
        {
            
           return _assessment.GetAssessmentStudentAssessmentsStudents(accToken, assessmentsId);
           
        }
        /// <summary>
        ///  Gets assessments custom details. 
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="assessmentsId"></param>
        /// <returns></returns>
        public JArray GetAssessmentCustom(string accToken, string assessmentsId)
        {
            
           return _assessment.GetAssessmentCustom(accToken, assessmentsId);
           
        }
    }
}
