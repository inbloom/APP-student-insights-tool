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
    public class CoreProgramsData
    {
        protected ProgramsData _programs = new ProgramsData();
        
        /// <summary>
        /// Gets program details.
        /// </summary>
        /// <param name="accToken"></param>
        /// <returns></returns>
        public JArray GetPrograms(string accToken)
        {
            
            return _programs.GetPrograms(accToken);
            
        }
        /// <summary>
        /// Gets _programs details by id
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="programId"></param>
        /// <returns></returns>
        public JArray GetProgramById(string accToken, string programId)
        {
            
            return _programs.GetProgramById(accToken, programId);
            
        }
        /// <summary>
        /// Gets program custom details.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="programId"></param>
        /// <returns></returns>
        public JArray GetProgramCustom(string accToken, string programId)
        {
            
            return _programs.GetProgramCustom(accToken, programId);
            
        }
        /// <summary>
        /// Gets staff program associations with in the _programs.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="programId"></param>
        /// <returns></returns>
        public JArray GetProgramStaffProgramAssociations(string accToken, string programId)
        {
            
            return _programs.GetProgramStaffProgramAssociations(accToken, programId);
            
        }
        /// <summary>
        /// Gets student details in staff program associations with in the _programs.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="programId"></param>
        /// <returns></returns>
        public JArray GetProgramStaffProgramAssociationStaff(string accToken, string programId)
        {
            
            return _programs.GetProgramStaffProgramAssociationStaff(accToken, programId);
            
        }
        /// <summary>
        /// Gets student program associations with in the _programs.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="programId"></param>
        /// <returns></returns>
        public JArray GetProgramStudentProgramAssociations(string accToken, string programId)
        {
            
            return _programs.GetProgramStudentProgramAssociations(accToken, programId);
            
        }
        /// <summary>
        /// Gets students details in student program associations with in the _programs.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="programId"></param>
        /// <returns></returns>
        public JArray GetProgramStudentProgramAssociationStudents(string accToken, string programId)
        {
            
            return _programs.GetProgramStudentProgramAssociationStudents(accToken, programId);
            
        }

        #region CUD Methods
        /// <summary>
        /// Creates program details.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public string PostPrograms(string accToken, string data)
        {
           
            return _programs.PostPrograms( accToken, data);
            
        }
        /// <summary>
        /// Updates program details.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="programId"></param>
        /// <param name="data"></programId>
        /// <returns></returns>
        public string PutPrograms(string accToken, string data, string programId)
        {
           
            return _programs.PutPrograms(accToken, data, programId);
            
        }

        /// <summary>
        /// Deletes program details.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="programId"></param>
        /// <returns></returns>
        public string DeletePrograms(string accToken, string programId)
        {
           
            return _programs.DeletePrograms(accToken, programId);
            
        }
        #endregion
    }
}
