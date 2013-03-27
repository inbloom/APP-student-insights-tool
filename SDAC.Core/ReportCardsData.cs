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
    public class CoreReportCardsData
    {
        protected ReportCardsData _reports = new ReportCardsData();
        
        /// <summary>
        /// Gets report cards details.
        /// </summary>
        /// <param name="accToken"></param>
        /// <returns></returns>
        public JArray GetReportCards(string accToken)
        {
            
            return _reports.GetReportCards(accToken);
            
        }
        /// <summary>
        /// Gets report card custom  details.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="reportId"></param>
        /// <returns></returns>
        public JArray GetReportCardCustom(string accToken, string reportId)
        {
            
            return _reports.GetReportCardCustom(accToken, reportId);
            
        }

        #region CUD Methods
        /// <summary>
        /// Creates Report cards details.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public string PostReportCards(string accToken, string data)
        {
            return _reports.PostReportCards(accToken, data);
            
        }
        /// <summary>
        /// Updates Report cards details.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="data"></param>
        /// <param name="reportId"></param>
        /// <returns></returns>
        public string PutReportCards(string accToken, string data, string reportId)
        {
            return _reports.PutReportCards(accToken, data, reportId);
           
        }

        /// <summary>
        /// Deletes Report cards details.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="reportId"></param>
        /// <returns></returns>
        public string DeleteReportCards(string accToken, string reportId)
        {
            return _reports.DeleteReportCards(accToken, reportId);
           
        }
        #endregion
    }
}
