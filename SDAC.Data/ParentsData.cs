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
    public class ParentsData
    {

        protected GetParentsData _parents = new GetParentsData();
       
        /// <summary>
        /// Gets _parents details.
        /// </summary>
        /// <param name="accToken"></param>
        /// <returns></returns>
        public JArray GetParents(string accToken)
        {
           return _parents.GetParents(accToken);
           
        }
        /// <summary>
        ///  Gets _parents details by id.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="parentsId"></param>
        /// <returns></returns>
        public JArray GetParentById(string accToken, string parentsId)
        {
            return _parents.GetParentById(accToken, parentsId);
            
        }
        /// <summary>
        ///  Gets _parents custom details.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="parentsId"></param>
        /// <returns></returns>
        public JArray GetParentCustom(string accToken, string parentsId)
        {
            return _parents.GetParentCustom(accToken, parentsId);
           
        }
        /// <summary>
        /// Gets student parent associations with in the _parents.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="parentsId"></param>
        /// <returns></returns>
        public JArray GetParentStudentParentAssociations(string accToken, string parentsId)
        {
            return _parents.GetParentStudentParentAssociations(accToken, parentsId);
            
        }
        /// <summary>
        /// Gets student details in student parent associations with in the _parents.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="parentsId"></param>
        /// <returns></returns>
        public JArray GetParentStudentParentAssociationStudents(string accToken, string parentsId)
        {
            return _parents.GetParentStudentParentAssociationStudents(accToken, parentsId);
            
        }

        #region CUD Methods
        /// <summary>
        /// Creates _parents details.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public string PostParents(string accToken, string data)
        {
            return _parents.PostParents( accToken, data);
           
        }
        /// <summary>
        /// Updates _parents details.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="data"></param>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public string PutParents(string accToken, string data, string parentId)
        {
            return _parents.PutParents(accToken, data, parentId);
           
        }

        /// <summary>
        /// Deletes _parents details.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public string DeleteParents(string accToken, string parentId)
        {
            return _parents.DeleteParents(accToken, parentId);
            
        }


        #endregion

    }
}
