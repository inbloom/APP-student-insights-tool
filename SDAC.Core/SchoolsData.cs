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
    public class CoreSchoolsData
    {
        protected SchoolsData _school = new SchoolsData();
        
        /// <summary>
        /// Gets schools details.
        /// </summary>
        /// <param name="accToken"></param>
        /// <returns></returns>
        public JArray GetSchools(string accToken)
        {
            
            return _school.GetSchools(accToken);
           
        }
        /// <summary>
        /// Gets schools custom details.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="schoolId"></param>
        /// <returns></returns>
        public JArray GetSchoolCustom(string accToken, string schoolId)
        {
            
            return _school.GetSchoolCustom(accToken, schoolId);
           
        }
        /// <summary>
        /// Gets sections with in the schools.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="schoolId"></param>
        /// <returns></returns>
        public JArray GetSchoolSections(string accToken, string schoolId)
        {
            
            return _school.GetSchoolSections(accToken, schoolId);
           
        }
        /// <summary>
        ///  Gets student _school associations with in the schools.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="schoolId"></param>
        /// <returns></returns>
        public JArray GetSchoolStudentSchoolAssociations(string accToken, string schoolId)
        {
            
            return _school.GetSchoolStudentSchoolAssociations(accToken, schoolId);
           
        }
        /// <summary>
        /// Gets students in student _school associations with in the schools.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="schoolId"></param>
        /// <returns></returns>
        public JArray GetSchoolStudentSchoolAssociationStudents(string accToken, string schoolId)
        {
            
            return _school.GetSchoolStudentSchoolAssociationStudents(accToken, schoolId);
           
        }
        /// <summary>
        /// Gets teacher _school associations with in the schools.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="schoolId"></param>
        /// <returns></returns>
        public JArray GetSchoolTeacherSchoolAssociations(string accToken, string schoolId)
        {
            
            return _school.GetSchoolTeacherSchoolAssociations(accToken, schoolId);
           
        }
        /// <summary>
        /// Gets teachers in teacher _school associations with in the schools.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="schoolId"></param>
        /// <returns></returns>
        public JArray GetSchoolTeacherSchoolAssociationTeachers(string accToken, string schoolId)
        {
            
            return _school.GetSchoolTeacherSchoolAssociationTeachers(accToken, schoolId);
           
        }

        #region CUD Methods
        /// <summary>
        /// Creates Schools details
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public string PostSchools(string accToken, string data)
        {
            return _school.PostSchools( accToken, data);
            
        }
        /// <summary>
        /// Updates Schools details
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="data"></param>
        /// <param name="schoolId"></param>
        /// <returns></returns>
        public string PutSchools(string accToken, string data, string schoolId)
        {
            return _school.PutSchools(accToken, data, schoolId);
          
        }

        /// <summary>
        /// Deletes Schools details
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="schoolId"></param>
        /// <returns></returns>
        public string DeleteSchools(string accToken, string schoolId)
        {
            return _school.DeleteSchools(accToken, schoolId);
            
        }
        #endregion
    }
}
