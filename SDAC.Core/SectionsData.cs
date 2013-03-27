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

    public class CoreSectionsData
    {
        protected SectionsData _section = new SectionsData();
       
        


        /// <summary>Gets sections details.       
        /// </summary>
        /// <param name="accToken">Access token has to send</param>
        /// <param name="userId">User id  has to send</param>
        /// <returns>Gets Sections Data</returns>        
        public JArray GetSections(string accToken, string userId)
        {
            
            return _section.GetSections(accToken, userId);
             
        }

        /// <summary>
        /// Gets _section details by id.
        /// </summary>
        /// <param name="accToken">Access token has to send</param>
        /// <param name="sectionId">User id  has to send</param>
        /// <returns>Gets Sections Data</returns> 
        public JArray GetSectionById(string accToken, string sectionId)
        {
            
            return _section.GetSectionById(accToken, sectionId);
            
        }


        /// <summary>
        /// Gets _section custom details.
        /// </summary>
        /// <param name="accToken">Access token has to send</param>
        /// <param name="sectionId">User id  has to send</param>
        /// <returns>Gets Sections Data</returns> 
        public JArray GetSectionCustom(string accToken, string sectionId)
        {
            
            return _section.GetSectionCustom(accToken, sectionId);
            
        }

        /// <summary>
        /// Gets Student _section associations details with in the sections.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="sectionId"></param>
        /// <returns></returns>
        public JArray GetSectionStudentAssociations(string accToken, string sectionId)
        {
            
            return _section.GetSectionStudentAssociations(accToken, sectionId);
            
        }
        /// <summary>
        /// Gets students details in student _section associations with in the sections.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="sectionId"></param>
        /// <returns></returns>
        public JArray GetSectionStudentAssociationStudentList(string accToken, string sectionId)
        {
            
            return _section.GetSectionStudentAssociationStudentList(accToken, sectionId);
            
        }
        /// <summary>
        /// Gets teacher _section associations with in the sections.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="sectionId"></param>
        /// <returns></returns>
        public JArray GetSectionTeacherAssociations(string accToken, string sectionId)
        {
            
            return _section.GetSectionTeacherAssociations(accToken, sectionId);
            
        }
        /// <summary>
        /// Gets teachers in teacher _section associations with in the sections. 
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="sectionId"></param>
        /// <returns></returns>
        public JArray GetSectionTeacherAssociationTeacherList(string accToken, string sectionId)
        {
            
            return _section.GetSectionTeacherAssociationTeacherList(accToken, sectionId);
            
        }

        #region CUD Methods
        /// <summary>
        /// Creates sections details
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public string PostSections(string accToken, string data)
        {
            return _section.PostSections( accToken, data);
           
        }
        /// <summary>
        /// Update sections details
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="data"></param>
        /// <param name="sectionId"></param>
        /// <returns></returns>
        public string PutSections(string accToken, string data, string sectionId)
        {
           return _section.PutSections(accToken, data, sectionId);
            
        }

        /// <summary>
        /// Deletes sectins details
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="sectionId"></param>
        /// <returns></returns>
        public string DeleteSections(string accToken, string sectionId)
        {
            return _section.DeleteSections(accToken, sectionId);
          
        }
        #endregion


    }
}
