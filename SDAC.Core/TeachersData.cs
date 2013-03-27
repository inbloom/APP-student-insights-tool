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
    public class CoreTeachersData
    {
        protected TeachersData _teachers = new TeachersData();
       


        #region Teachers
        /// <summary>
        /// Gets teachers details.
        /// </summary>
        /// <param name="accToken"></param>
        /// <returns></returns>
        public JArray GetTeachers(string accToken)
        {
            
            return _teachers.GetTeachers(accToken);
            
        }
        /// <summary>
        /// Gets teachers custom details.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="teacherId"></param>
        /// <returns></returns>
        public JArray GetTeacherCustom(string accToken, string teacherId)
        {
                      
            return _teachers.GetTeacherCustom(accToken, teacherId);
            
        }
        /// <summary>
        /// Gets teacher school associations with in the teachers.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="teacherId"></param>
        /// <returns></returns>
        public JArray GetTeacherTeacherSchoolAssociations(string accToken, string teacherId)
        {
            
            return _teachers.GetTeacherTeacherSchoolAssociations(accToken, teacherId);
            
        }
        /// <summary>
        /// Gets schools in teacher school associations with in the teachers.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="teacherId"></param>
        /// <returns></returns>
        public JArray GetTeacherTeacherSchoolAssociationSchools(string accToken, string teacherId)
        {
            
            return _teachers.GetTeacherTeacherSchoolAssociationSchools(accToken, teacherId);
            
        }
        /// <summary>
        /// Gets teacher section associations with in the teachers.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="teacherId"></param>
        /// <returns></returns>
        public JArray GetTeacherTeacherSectionAssociations(string accToken, string teacherId)
        {
            
            return _teachers.GetTeacherTeacherSectionAssociations(accToken, teacherId);
            
        }
        /// <summary>
        /// Gets sections in teacher section associations with in the teachers.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="teacherId"></param>
        /// <returns></returns>
        public JArray GetTeacherTeacherSectionAssociationSections(string accToken, string teacherId)
        {
            
            return _teachers.GetTeacherTeacherSectionAssociationSections(accToken, teacherId);
            
        }

        #region CUD Methods
        /// <summary>
        /// Creates teachers Details.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public string PostTeachers(string accToken, string data)
        {
            return _teachers.PostTeachers(accToken, data);
            
        }
        /// <summary>
        /// Updates teachers Details.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="data"></param>
        /// <param name="teacherId"></param>
        /// <returns></returns>
        public string PutTeachers(string accToken, string data, string teacherId)
        {
            return _teachers.PutTeachers(accToken, data, teacherId);
            
        }

        /// <summary>
        /// Deletes teachers Details.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="teacherId"></param>
        /// <returns></returns>
        public string DeleteTeachers(string accToken, string teacherId)
        {
            return _teachers.DeleteTeachers(accToken, teacherId);
           
        }
        #endregion
        #endregion


        #region teacherSchoolAssociations
        /// <summary>
        /// Gets teacher school associations details.
        /// </summary>
        /// <param name="accToken"></param>
        /// <returns></returns>
        public JArray GetTeacherSchoolAssociations(string accToken)
        {
            
            return _teachers.GetTeacherSchoolAssociations(accToken);
            
        }
        /// <summary>
        /// Gets teacher school associations custom details.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="teacherId"></param>
        /// <returns></returns>
        public JArray GetTeacherSchoolAssociationCustom(string accToken, string teacherId)
        {
            
            return _teachers.GetTeacherSchoolAssociationCustom(accToken, teacherId);
            
        }
        /// <summary>
        /// Gets teacher school associations by id.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="teacherId"></param>
        /// <returns></returns>
        public JArray GetTeacherSchoolAssociationById(string accToken, string teacherId)
        {
            
            return _teachers.GetTeacherSchoolAssociationById(accToken, teacherId);
            
        }
        /// <summary>
        /// Gets schools with in the teacher school associations.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="teacherId"></param>
        /// <returns></returns>
        public JArray GetTeacherSchoolAssociationSchools(string accToken, string teacherId)
        {
            
            return _teachers.GetTeacherSchoolAssociationSchools(accToken, teacherId);
            
        }
        /// <summary>
        /// Gets teachers with in the teacher school associations.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="teacherId"></param>
        /// <returns></returns>
        public JArray GetTeacherSchoolAssociationTeachers(string accToken, string teacherId)
        {
            
            return _teachers.GetTeacherSchoolAssociationTeachers(accToken, teacherId);
            
        }

        #region CUD Methods
        /// <summary>
        /// Creates teacher school associations details.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public string PostTeacherSchoolAssociations(string accToken, string data)
        {
             
            return _teachers.PostTeacherSchoolAssociations(accToken, data);
            
        }
        /// <summary>
        /// Updates teacher school associations details.
        /// </summary>
        /// <param name="accToken"></param>
        ///  <param name="data"></param>
        ///  <param name="teacherId"></param>
        /// <returns></returns>
        public string PutTeacherSchoolAssociations(string accToken, string data, string teacherId)
        {
             
            return _teachers.PutTeacherSchoolAssociations(accToken,data, teacherId);
            
        }

        /// <summary>
        /// Deletes teacher school associations details.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="teacherId"></param>
        /// <returns></returns>
        public string DeleteTeacherSchoolAssociations(string accToken, string teacherId)
        {
             
            return _teachers.DeleteTeacherSchoolAssociations(accToken, teacherId);
            
        }
        #endregion

        #endregion

        #region teacherSectionAssociations
        /// <summary>
        /// Gets teacher section associations details.
        /// </summary>
        /// <param name="accToken"></param>
        /// <returns></returns>
        public JArray GetTeacherSectionAssociations(string accToken)
        {
            
            return _teachers.GetTeacherSectionAssociations(accToken);
            
        }
        /// <summary>
        /// Gets teacher section associations custom  details.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="teacherId"></param>
        /// <returns></returns>
        public JArray GetTeacherSectionAssociationCustom(string accToken, string teacherId)
        {
            
            return _teachers.GetTeacherSectionAssociationCustom(accToken, teacherId);
            
        }
        /// <summary>
        /// Gets teacher section associations details by id.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="teacherId"></param>
        /// <returns></returns>
        public JArray GetTeacherSectionAssociationById(string accToken, string teacherId)
        {
            
            return _teachers.GetTeacherSectionAssociationById(accToken, teacherId);
            
        }
        /// <summary>
        /// Gets sections with in the teacher section associations.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="teacherId"></param>
        /// <returns></returns>
        public JArray GetTeacherSectionAssociationSections(string accToken, string teacherId)
        {
            
            return _teachers.GetTeacherSectionAssociationSections(accToken, teacherId);
            
        }
        /// <summary>
        /// Gets teachers with in the teacher section associations.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="teacherId"></param>
        /// <returns></returns>
        public JArray GetTeacherSectionAssociationTeachers(string accToken, string teacherId)
        {
            
            return _teachers.GetTeacherSectionAssociationTeachers(accToken, teacherId);
            
        }


        #region CUD Methods
        /// <summary>
        /// Creates teacher section associations.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public string PostTeacherSectionAssociations(string accToken, string data)
        {
             
            return _teachers.PostTeacherSectionAssociations(accToken, data);
            
        }
        /// <summary>
        /// Updates teacher section associations details.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="data"></param>
        /// <param name="teacherId"></param>
        /// <returns></returns>
        public string PutTeacherSectionAssociations(string accToken, string data, string teacherId)
        {
             
            return _teachers.PutTeacherSectionAssociations(accToken, data, teacherId);
            
        }

        /// <summary>
        /// Deletes teacher section associations details.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="teacherId"></param>
        /// <returns></returns>
        public string DeleteTeacherSectionAssociations(string accToken, string teacherId)
        {
             
            return _teachers.DeleteTeacherSectionAssociations(accToken, teacherId);
            
        }
        #endregion
        #endregion


    }
}
