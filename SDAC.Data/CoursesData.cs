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
    public class CoursesData
    {
        protected GetCoursesData _courses = new GetCoursesData();
        
        /// <summary>
        /// Gets _courses offerings details.
        /// </summary>
        /// <param name="accToken"></param>
        /// <returns></returns>
        public JArray GetCourseOfferings(string accToken)
        {
            return _courses.GetCourseOfferings(accToken);
          
        }
        /// <summary>
        /// Gets course offering by id.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="courseId"></param>
        /// <returns></returns>
        public JArray GetCourseOfferingById(string accToken, string courseId)
        {
            return _courses.GetCourseOfferingById(accToken, courseId);
           
        }
        /// <summary>
        /// Gets _courses with in the course offerings. 
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="courseId"></param>
        /// <returns></returns>
        public JArray GetCourseOfferingCourses(string accToken, string courseId)
        {
            return _courses.GetCourseOfferingCourses(accToken, courseId);
           
        }
        /// <summary>
        ///  Gets sessions with in the course offerings. 
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="courseId"></param>
        /// <returns></returns>
        public JArray GetCourseOfferingSessions(string accToken, string courseId)
        {
            return _courses.GetCourseOfferingSessions(accToken, courseId);
           
        }
        /// <summary>
        ///  Gets course offerings custom details.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="courseId"></param>
        /// <returns></returns>
        public JArray GetCourseOfferingCustom(string accToken, string courseId)
        {
            return _courses.GetCourseOfferingCustom(accToken, courseId);
            
        }

        #region CUD Methods
        /// <summary>
        ///  Creates course offerings  details.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public string PostCourseOfferings(string accToken, string data)
        {
           return _courses.PostCourseOfferings( accToken, data);
            
        }
        /// <summary>
        ///  Updates course offerings  details.
        /// </summary>
        /// <param name="accToken"></param>
        ///  <param name="data"></param>
        /// <param name="courseId"></param>
        /// <returns></returns>
        public string PutCourseOfferings(string accToken, string data, string courseId)
        {
            return _courses.PutCourseOfferings(accToken, data, courseId);
           
        }

        /// <summary>
        ///  Deletes course offerings  details.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="courseId"></param>
        /// <returns></returns>
        public string DeleteCourseOfferings(string accToken, string courseId)
        {
           return _courses.DeleteCourseOfferings(accToken, courseId);
            
        }


        #endregion


        /// <summary>
        /// Gets course details.
        /// </summary>
        /// <param name="accToken"></param>
        /// <returns></returns>
        public JArray GetCourses(string accToken)
        {
            return _courses.GetCourses(accToken);
            
        }

        /// <summary>
        /// Gets course details by id.
        /// </summary>
        /// <param name="accToken"></param>
        /// /// <param name="courseId"></param>
        /// <returns></returns>
        public JArray GetCourseById(string accToken, string courseId)
        {
           return _courses.GetCourseById(accToken, courseId);
           
        }

        /// <summary>
        ///  Gets course offerings with in the _courses  
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="courseId"></param>
        /// <returns></returns>
        public JArray GetCourseIdCourseOfferings(string accToken, string courseId)
        {
            return _courses.GetCourseIdCourseOfferings(accToken, courseId);
            
        }
        /// <summary>
        /// Gets sesstions details in course offerings with in the course.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="courseId"></param>
        /// <returns></returns>
        public JArray GetCourseIdCourseOfferingSessions(string accToken, string courseId)
        {
           return _courses.GetCourseIdCourseOfferingSessions(accToken, courseId);
            
        }
        /// <summary>
        ///  Gets course transcripts with in the _courses.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="courseId"></param>
        /// <returns></returns>
        public JArray GetCourseCourseTranscripts(string accToken, string courseId)
        {
            return _courses.GetCourseCourseTranscripts(accToken, courseId);
            
        }
        /// <summary>
        /// Gets students details in course transcripts with in the _courses.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="courseId"></param>
        /// <returns></returns>
        public JArray GetCourseCourseTranscriptStudents(string accToken, string courseId)
        {
            return _courses.GetCourseCourseTranscriptStudents(accToken, courseId);
            
        }
        /// <summary>
        /// Gets student parent associations with in the course.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="courseId"></param>
        /// <returns></returns>
        public JArray GetCourseStudentParentAssociations(string accToken, string courseId)
        {
            return _courses.GetCourseStudentParentAssociations(accToken, courseId);
            
        }
        /// <summary>
        /// Gets students details in student parent associations with in the course.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="courseId"></param>
        /// <returns></returns>
        public JArray GetCourseStudentParentAssociationStudents(string accToken, string courseId)
        {
            return _courses.GetCourseStudentParentAssociationStudents(accToken, courseId);
           
        }
        /// <summary>
        /// Gets course custom details.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="courseId"></param>
        /// <returns></returns>
        public JArray GetCourseCustom(string accToken, string courseId)
        {
            return _courses.GetCourseCustom(accToken, courseId);
            
        }

        #region CUD Methods
        /// <summary>
        /// Creates Courses details
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public string PostCourses(string accToken, string data)
        {
            return _courses.PostCourses( accToken, data);
            
        }
        /// <summary>
        /// Updates _courses details.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="data"></param>
        /// <param name="courseId"></param>
        /// <returns></returns>
        public string PutCourses(string accToken, string data, string courseId)
        {
           return _courses.PutCourses(accToken, data, courseId);
            
        }

        /// <summary>
        /// Deletes _courses details.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="courseId"></param>
        /// <returns></returns>
        public string DeleteCourses(string accToken, string courseId)
        {
            return _courses.DeleteCourses(accToken, courseId);
           
        }


        #endregion

       /// <summary>
        /// Gets course transcripts with in the _courses.
       /// </summary>
       /// <param name="accToken"></param>
       /// <returns></returns>
        public JArray GetCourseCourseTranscripts(string accToken)
        {
            return _courses.GetCourseCourseTranscripts(accToken);
           
        }
        /// <summary>
        /// Gets course transcripts with in the course by id.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="courseId"></param>
        /// <returns></returns>
        public JArray GetCourseCourseTranscriptById(string accToken, string courseId)
        {
            return _courses.GetCourseCourseTranscriptById(accToken, courseId);
           
        }
        /// <summary>
        /// Gets course transcript custom details with in the _courses.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="courseId"></param>
        /// <returns></returns>
        public JArray GetCourseCourseTranscriptCustom(string accToken, string courseId)
        {
            return _courses.GetCourseCourseTranscriptCustom(accToken, courseId);
            
        }
        /// <summary>
        /// Gets course in course transcripts with in the _courses.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="courseId"></param>
        /// <returns></returns>
        public JArray GetCourseCourseTranscriptCourses(string accToken, string courseId)
        {
            return _courses.GetCourseCourseTranscriptCourses(accToken, courseId);
            
        }
        /// <summary>
        /// Gets students in course transcripts with in the _courses.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="courseId"></param>
        /// <returns></returns>
        public JArray GetCourseTranscriptStudents(string accToken, string courseId)
        {
            return _courses.GetCourseTranscriptStudents(accToken, courseId);
           
        }

        #region CUD Methods
        /// <summary>
        /// Creates course transcripts.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public string PostCourseTranscripts(string accToken, string data)
        {
           return _courses.PostCourseTranscripts( accToken, data);
            
        }
        /// <summary>
        /// Updates  course transcripts.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="data"></param>
        /// <param name="courseId"></param>
        /// <returns></returns>
        public string PutCourseTranscripts(string accToken, string data, string courseId)
        {
            return _courses.PutCourseTranscripts(accToken, data, courseId);
           
        }

        /// <summary>
        /// Deletes  course transcripts.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="courseId"></param>
        /// <returns></returns>
        public string DeleteCourseTranscripts(string accToken, string courseId)
        {
           return _courses.DeleteCourseTranscripts(accToken, courseId);
           
        }


        #endregion

    }
}
