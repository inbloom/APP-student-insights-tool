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
    public class CoreSessionsData
    {
        protected SessionsData _session = new SessionsData();
       
        
        /// <summary>
        /// Gets sessions details.
        /// </summary>
        /// <param name="accToken"></param>
        /// <returns></returns>
        public JArray GetSessions(string accToken)
        {
            
            return _session.GetSessions(accToken);
            
        }
       /// <summary>
        /// Gets sessions custom details.
       /// </summary>
       /// <param name="accToken"></param>
       /// <param name="userId"></param>
       /// <returns></returns>
        public JArray GetSessionCustom(string accToken, string userId)
        {
            
            return _session.GetSessionCustom(accToken, userId);
            
        }
        /// <summary>
        /// Gets sessions details by id.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="sessionId"></param>
        /// <returns></returns>
        public JArray GetSessionById(string accToken, string sessionId)
        {
            
            return _session.GetSessionById(accToken, sessionId);
            
        }
        /// <summary>
        ///  Gets course offerings with in the sessions.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="sessionId"></param>
        /// <returns></returns>
        public JArray GetSessionCourseOfferings(string accToken, string sessionId)
        {
            
            return _session.GetSessionCourseOfferings(accToken, sessionId);
            
        }
        /// <summary>
        /// Gets courses in course offerings with in the sessions.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="sessionId"></param>
        /// <returns></returns>
        public JArray GetSessionCourseOfferingCourses(string accToken, string sessionId)
        {
            
            return _session.GetSessionCourseOfferingCourses(accToken, sessionId);
            
        }
        #region CUD Methods
        /// <summary>
        /// Creates Sessions
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public string PostSessions(string accToken, string data)
        {
            return _session.PostSessions( accToken, data);
           
        }
        /// <summary>
        /// Updates sesstions
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="data"></param>
        /// <param name="sessionId"></param>
        /// <returns></returns>
        public string PutSessions(string accToken, string data, string sessionId)
        {
           return _session.PutSessions(accToken, data, sessionId);
          
        }

        /// <summary>
        /// Deletes sessions
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="sessionId"></param>
        /// <returns></returns>
        public string DeleteSessions(string accToken, string sessionId)
        {
            return _session.DeleteSessions(accToken, sessionId);
            
        }
        #endregion
    }
}
