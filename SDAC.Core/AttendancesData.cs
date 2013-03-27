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
    public class CoreAttendancesData 
    {
        protected AttendancesData _attendance = new AttendancesData();
       
        /// <summary>
        /// Gets Student Attendance Details
        /// </summary>
        /// <param name="accToken"></param>
        /// <returns></returns>
        public JArray GetAttendances(string accToken)
        {
            
            return _attendance.GetAttendances(accToken);
           
        }
        /// <summary>
        /// Gets Student Attendance Custom Details
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="attendanceId"></param>
        /// <returns></returns>
        public JArray GetAttendanceCustom(string accToken, string attendanceId)
        {
            
            return _attendance.GetAttendanceCustom(accToken, attendanceId);
            
        }


        /// <summary>
        /// Creates Student Attendance Details
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public string PostAttendance(string accToken, string data)
        {
            
            return _attendance.PostAttendance( accToken, data);
            
        }
        /// <summary>
        /// Updates Student Attendance Details
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="data"></param>
        /// <param name="attendanceId"></param>
        /// <returns></returns>
        public string PutAttendance(string accToken, string data, string attendanceId)
        {
            
            return _attendance.PutAttendance(accToken, data, attendanceId);
            
        }
        /// <summary>
        /// Deletes Student Attendance Details
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="attendanceId"></param>
        /// <returns></returns>
        public string DeleteAttendance(string accToken,  string attendanceId)
        {
            
            return _attendance.DeleteAttendance(accToken, attendanceId);
            
        }


    }
}
