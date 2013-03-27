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
    public class GradesData
    {
        protected GetGradesData _grades = new GetGradesData();
      
        /// <summary>
        /// Gets Grade Book Entries details. 
        /// </summary>
        /// <param name="accToken"></param>
        /// <returns></returns>
        public JArray GetGradeBookEntries(string accToken)
        {
            return _grades.GetGradeBookEntries(accToken);
           
        }
        /// <summary>
        /// Gets Grade Book Entries details by id. 
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="gradeId"></param>
        /// <returns></returns>
        public JArray GetGradeBookEntrieById(string accToken, string gradeId)
        {
            return _grades.GetGradeBookEntrieById(accToken, gradeId);
           
        }
        /// <summary>
        /// Gets Grade Book Entries custom details. 
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="gradeId"></param>
        /// <returns></returns>
        public JArray GetGradeBookEntrieCustom(string accToken, string gradeId)
        {
            return _grades.GetGradeBookEntrieCustom(accToken, gradeId);
           
        }

        #region CUD Methods
        /// <summary>
        /// Creates Grade Book Entries  details. 
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="gradeId"></param>
        /// <returns></returns>
        public string PostGradebookEntries(string accToken, string data)
        {
            return  _grades.PostGradebookEntries( accToken, data);
            
        }
        /// <summary>
        /// Updates Grade Book Entries  details. 
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="gradeId"></param>
        /// <returns></returns>
        public string PutGradebookEntries(string accToken, string data, string gradeId)
        {
            return _grades.PutGradebookEntries(accToken, data, gradeId);
            
        }

        /// <summary>
        /// Deletes Grade Book Entries  details. 
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="gradeId"></param>
        /// <returns></returns>
        public string DeleteGradebookEntries(string accToken, string gradeId)
        {
            return _grades.DeleteGradebookEntries(accToken, gradeId);
           
        }


        #endregion



        /// <summary>
        /// Gets Grade details.
        /// </summary>
        /// <param name="accToken"></param>
        /// <returns></returns>
        public JArray GetGrades(string accToken)
        {
            return _grades.GetGrades(accToken);
            
        }
        /// <summary>
        /// Gets grade custom details.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="gradeId"></param>
        /// <returns></returns>
        public JArray GetGradeCustom(string accToken, string gradeId)
        {
            return _grades.GetGradeCustom(accToken, gradeId);
            
        }

        #region CUD Methods
        /// <summary>
        /// Creates Grade details.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public string PostGrades(string accToken, string data)
        {
            return _grades.PostGrades( accToken, data);
           
        }
        /// <summary>
        /// Updates Grade details.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="data"></param>
        /// <param name="gradeId"></param>
        /// <returns></returns>
        public string PutGrades(string accToken, string data, string gradeId)
        {
           return _grades.PutGrades(accToken, data, gradeId);
           
        }

        /// <summary>
        /// Deletes Grade details.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="gradeId"></param>
        /// <returns></returns>
        public string DeleteGrades(string accToken, string gradeId)
        {
            return _grades.DeleteGrades(accToken, gradeId);
           
        }


        #endregion


       /// <summary>
       /// Gets grading period details.
       /// </summary>
       /// <param name="accToken"></param>
       /// <returns></returns>
        public JArray GetGradingPeriods(string accToken)
        {
            return _grades.GetGradingPeriods(accToken);
           
        }
        /// <summary>
        /// Gets _grades with in the grading periods.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="gradeId"></param>
        /// <returns></returns>
        public JArray GetGradingPeriodGrades(string accToken, string gradeId)
        {
            return _grades.GetGradingPeriodGrades(accToken, gradeId);
            
        }
        /// <summary>
        /// Gets report cards with in the grading periods.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="gradeId"></param>
        /// <returns></returns>
        public JArray GetGradingPeriodReportCards(string accToken, string gradeId)
        {
            return _grades.GetGradingPeriodReportCards(accToken, gradeId);
            
        }
        /// <summary>
        /// Gets grading periods by id.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="gradeId"></param>
        /// <returns></returns>
        public JArray GetGradingPeriodById(string accToken, string gradeId)
        {
            return _grades.GetGradingPeriodById(accToken, gradeId);
            
        }
        /// <summary>
        /// Gets grading periods custom details.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="gradeId"></param>
        /// <returns></returns>
        public JArray GetGradingPeriodCustom(string accToken, string gradeId)
        {
            return _grades.GetGradingPeriodCustom(accToken, gradeId);
           
        }


        #region CUD Methods
        /// <summary>
        /// Creates grading periods details.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public string PostGradingPeriods(string accToken, string data)
        {
            return _grades.PostGradingPeriods( accToken, data);
           
        }
        /// <summary>
        /// Updates grading periods details.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="data"></param>
        /// <param name="gradeId"></param>
        /// <returns></returns>
        public string PutGradingPeriods(string accToken, string data, string gradeId)
        {
            return _grades.PutGradingPeriods(accToken, data, gradeId);
           
        }

        /// <summary>
        /// Deletes grading periods details.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="gradeId"></param>
        /// <returns></returns>
        public string DeleteGradingPeriods(string accToken, string gradeId)
        {
            return _grades.DeleteGradingPeriods(accToken, gradeId);
            
        }


        #endregion


    }
}
