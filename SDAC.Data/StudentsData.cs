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
    public class StudentsData
    {
        protected GetStudentsData _students = new GetStudentsData();        
       


        # region  StudentAcademicRecords
        /// <summary>
        /// Gets student academic records details.
        /// </summary>
        /// <param name="accToken"></param>
        /// <returns></returns>
        public JArray GetStudentAcademicRecords(string accToken)
        {
                        
            return _students.GetStudentAcademicRecords(accToken);
           
        }
        /// <summary>
        /// Gets student academic records custom details.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public JArray GetStudentAcademicRecordCustom(string accToken, string studentId)
        {
            
            return _students.GetStudentAcademicRecordCustom(accToken, studentId);
           
        }
        /// <summary>
        /// Gets student academic records details by id.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public JArray GetStudentAcademicRecordById(string accToken, string studentId)
        {
            
            return _students.GetStudentAcademicRecordById(accToken, studentId);
           
        }

        #region CUD Methods
        /// <summary>
        /// Creates student Academic Records details.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public string PostStudentAcademicRecords(string accToken, string data)
        {
            
            return _students.PostStudentAcademicRecords(accToken, data);
            
        }
        /// <summary>
        /// Updates student Academic Records details.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="data"></param>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public string PutStudentAcademicRecords(string accToken, string Data, string studentId)
        {
            
            return _students.PutStudentAcademicRecords(accToken, Data, studentId);
            
        }

        /// <summary>
        /// Deletes student Academic Records details.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public string DeleteStudentAcademicRecords(string accToken, string studentId)
        {
            
           return _students.DeleteStudentAcademicRecords(accToken, studentId);
            
        }
        #endregion
        #endregion

        #region studentAssessments
        /// <summary>
        /// Gets student assessments details.
        /// </summary>
        /// <param name="accToken"></param>
        /// <returns></returns>
        public JArray GetStudentAssessments(string accToken)
        {
            
            return _students.GetStudentAssessments(accToken);
           
        }
        /// <summary>
        /// Gets student assessments custom  details.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public JArray GetStudentAssessmentCustom(string accToken, string studentId)
        {
            
            return _students.GetStudentAssessmentCustom(accToken, studentId);
           
        }
        /// <summary>
        /// Gets student assessments details by id.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public JArray GetStudentAssessmentById(string accToken, string studentId)
        {
            
            return _students.GetStudentAssessmentById(accToken, studentId);
           
        }
        /// <summary>
        /// Gets assessments with in the student assessments.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public JArray GetStudentAssessmentAssessments(string accToken, string studentId)
        {
            
            return _students.GetStudentAssessmentAssessments(accToken, studentId);
           
        }
        /// <summary>
        ///  Gets _students with in the student assessments.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public JArray GetStudentAssessmentStudents(string accToken, string studentId)
        {
            
            return _students.GetStudentAssessmentStudents(accToken, studentId);
           
        }

        #region CUD Methods
        /// <summary>
        /// Creates student Assessments details.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public string PostStudentAssessments(string accToken, string data)
        {
            
            return _students.PostStudentAssessments(accToken, data);
            
        }
        /// <summary>
        /// Updates student Assessments details.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="data"></param>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public string PutStudentAssessments(string accToken, string data, string studentId)
        {
            
            return _students.PutStudentAssessments(accToken, data, studentId);
            
        }

        /// <summary>
        /// Deletes student Assessments details.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public string DeleteStudentAssessments(string accToken, string studentId)
        {
            
            return _students.DeleteStudentAssessments(accToken, studentId);
            
        }
        #endregion
        #endregion

        #region StudentCohortAssociations
        /// <summary>
        /// Gets student cohort associations details.
        /// </summary>
        /// <param name="accToken"></param>
        /// <returns></returns>
        public JArray GetStudentCohortAssociations(string accToken)
        {
            
            return _students.GetStudentCohortAssociations(accToken);
           
        }
        /// <summary>
        /// Gets student cohort associations custom  details.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public JArray GetStudentCohortAssociationCustom(string accToken, string studentId)
        {
            
            return _students.GetStudentCohortAssociationCustom(accToken, studentId);
           
        }
        /// <summary>
        /// Gets student cohort associations details by id.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public JArray GetStudentCohortAssociationById(string accToken, string studentId)
        {
            
            return _students.GetStudentCohortAssociationById(accToken, studentId);
           
        }
        /// <summary>
        /// Gets cohorts with in the student cohort associations details.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public JArray GetStudentCohortAssociationCohorts(string accToken, string studentId)
        {
            
            return _students.GetStudentCohortAssociationCohorts(accToken, studentId);
           
        }
        /// <summary>
        /// Gets _students with in the student cohort associations details.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public JArray GetStudentCohortAssociationStudents(string accToken, string studentId)
        {
            
            return _students.GetStudentCohortAssociationStudents(accToken, studentId);
           
        }
        #region CUD Methods
        /// <summary>
        /// Creates student Cohort Associations details.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public string PostStudentCohortAssociations(string accToken, string data)
        {
            
            return _students.PostStudentCohortAssociations( accToken, data);
            
        }
        /// <summary>
        /// Updates student Cohort Associations details.
        /// 
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="data"></param>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public string PutStudentCohortAssociations(string accToken, string data, string studentId)
        {
            
            return _students.PutStudentCohortAssociations(accToken, data, studentId);
            
        }

        /// <summary>
        /// Deletes student Cohort Associations details.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public string DeleteStudentCohortAssociations(string accToken, string studentId)
        {
            
            return _students.DeleteStudentCohortAssociations(accToken, studentId);
            
        }
        #endregion
        #endregion

        #region studentCompetencies
        /// <summary>
        /// Gets student competencies details.
        /// </summary>
        /// <param name="accToken"></param>
        /// <returns></returns>
        public JArray GetStudentCompetencies(string accToken)
        {
            
            return _students.GetStudentCompetencies(accToken);
           
        }
        /// <summary>
        /// Gets student competencies custom details.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public JArray GetStudentCompetenciesCustom(string accToken, string studentId)
        {
            
            return _students.GetStudentCompetenciesCustom(accToken, studentId);
           
        }
        /// <summary>
        /// Gets student competencies details by id.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public JArray GetStudentCompetenciesById(string accToken, string studentId)
        {
            
            return _students.GetStudentCompetenciesById(accToken, studentId);
           
        }
        /// <summary>
        /// Gets report cards with in the student competencies.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public JArray GetStudentCompetenciesReportCards(string accToken, string studentId)
        {
            
            return _students.GetStudentCompetenciesReportCards(accToken, studentId);
           
        }
        #region CUD Methods
        /// <summary>
        /// Creates student Competencies details. 
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public string PostStudentCompetencies(string accToken, string data)
        {
            
            return _students.PostStudentCompetencies(accToken, data);
            
        }
        /// <summary>
        /// Updates  student Competencies details. 
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="data"></param>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public string PutStudentCompetencies(string accToken, string data, string studentId)
        {
            
            return _students.PutStudentCompetencies(accToken, data, studentId);
            
        }

        /// <summary>
        /// Deletes  student Competencies details. 
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public string DeleteStudentCompetencies(string accToken, string studentId)
        {
            
            return _students.DeleteStudentCompetencies(accToken, studentId);
            
        }
        #endregion
        #endregion

        #region studentCompetencyObjectives
        /// <summary>
        /// Gets student competency objectives details.
        /// </summary>
        /// <param name="accToken"></param>
        /// <returns></returns>
        public JArray GetStudentCompetencyObjectives(string accToken)
        {
            
            return _students.GetStudentCompetencyObjectives(accToken);
           
        }
        /// <summary>
        /// Gets student competency objectives custom details.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public JArray GetStudentCompetencyObjectiveCustom(string accToken, string studentId)
        {
            
            return _students.GetStudentCompetencyObjectiveCustom(accToken, studentId);
           
        }
        /// <summary>
        /// Gets student competency objectives details by id.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public JArray GetStudentCompetencyObjectiveById(string accToken, string studentId)
        {
            
            return _students.GetStudentCompetencyObjectiveById(accToken, studentId);
           
        }
        #region CUD Methods
        /// <summary>
        /// Creates student Competency Objectives details.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public string PostStudentCompetencyObjectives(string accToken, string data)
        {
            
            return _students.PostStudentCompetencyObjectives(accToken, data);
            
        }
        /// <summary>
        /// Updates  student Competency Objectives details.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="data"></param>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public string PutStudentCompetencyObjectives(string accToken, string Data, string studentId)
        {
            
            return _students.PutStudentCompetencyObjectives(accToken, Data, studentId);
            
        }

        /// <summary>
        /// Deletes  student Competency Objectives details.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public string DeleteStudentCompetencyObjectives(string accToken, string studentId)
        {
            
           return _students.DeleteStudentCompetencyObjectives(accToken, studentId);
            
        }
        #endregion
        #endregion

        #region studentDisciplineIncidentAssociations
        /// <summary>
        /// Gets student discipline incident associations details.
        /// </summary>
        /// <param name="accToken"></param>
        /// <returns></returns>
        public JArray GetStudentDisciplineIncidentAssociations(string accToken)
        {
            
            return _students.GetStudentDisciplineIncidentAssociations(accToken);
           
        }
        /// <summary>
        /// Gets student discipline incident associations custom details.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public JArray GetStudentDisciplineIncidentAssociationCustom(string accToken, string studentId)
        {
            
           return _students.GetStudentDisciplineIncidentAssociationCustom(accToken, studentId);
           
        }
        /// <summary>
        /// Gets student discipline incident associations details by id.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public JArray GetStudentDisciplineIncidentAssociationById(string accToken, string studentId)
        {
            
           return _students.GetStudentDisciplineIncidentAssociationById(accToken, studentId);
           
        }
        /// <summary>
        /// Gets discipline incidents with in the student discipline incident associations.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public JArray GetStudentDisciplineIncidentAssociationDisciplineIncidents(string accToken, string studentId)
        {
            
            return _students.GetStudentDisciplineIncidentAssociationDisciplineIncidents(accToken, studentId);
           
        }
        /// <summary>
        /// Gets _students with in the student discipline incident associations.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public JArray GetStudentDisciplineIncidentAssociationStudents(string accToken, string studentId)
        {
            
            return _students.GetStudentDisciplineIncidentAssociationStudents(accToken, studentId);
           
        }

        #region CUD Methods
        /// <summary>
        /// Creates student Discipline Incident Associations details.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public string PostStudentDisciplineIncidentAssociations(string accToken, string data)
        {
            
            return _students.PostStudentDisciplineIncidentAssociations(accToken, data);
            
        }
        /// <summary>
        /// Updates student Discipline Incident Associations details.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="data"></param>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public string PutStudentDisciplineIncidentAssociations(string accToken, string data, string studentId)
        {
            
            return _students.PutStudentDisciplineIncidentAssociations(accToken, data, studentId);
            
        }

        /// <summary>
        /// Deletes student Discipline Incident Associations details.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public string DeleteStudentDisciplineIncidentAssociations(string accToken, string studentId)
        {
            
            return _students.DeleteStudentDisciplineIncidentAssociations(accToken, studentId);
            
        }
        #endregion

        #endregion

        #region studentGradebookEntries
        /// <summary>
        /// Gets student grade book entries details.
        /// </summary>
        /// <param name="accToken"></param>
        /// <returns></returns>
        public JArray GetStudentGradeBookEntries(string accToken)
        {
            
            return _students.GetStudentGradeBookEntries(accToken);
           
        }
        /// <summary>
        /// Gets student grade book entries custom  details.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public JArray GetStudentGradeBookEntriesCustom(string accToken, string studentId)
        {
            
            return _students.GetStudentGradeBookEntriesCustom(accToken, studentId);
           
        }
        /// <summary>
        /// Gets student grade book entries details by id.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public JArray GetStudentGradeBookEntriesById(string accToken, string studentId)
        {
            
            return _students.GetStudentGradeBookEntriesById(accToken, studentId);
           
        }


        #region CUD Methods
        /// <summary>
        /// Creates student Gradebook Entries details.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public string PostStudentGradebookEntries(string accToken, string data)
        {
            
            return _students.PostStudentGradebookEntries( accToken, data);
            
        }
        /// <summary>
        /// Updates  student Gradebook Entries details.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="data"></param>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public string PutStudentGradebookEntries(string accToken, string data, string studentId)
        {
            
            return _students.PutStudentGradebookEntries(accToken, data, studentId);
            
        }

        /// <summary>
        /// Deletes  student Gradebook Entries details.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public string DeleteStudentGradebookEntries(string accToken, string studentId)
        {
            
            return _students.DeleteStudentGradebookEntries(accToken, studentId);
            
        }
        #endregion
        #endregion

        #region studentParentAssociations
        /// <summary>
        /// Gets student parent associations details.
        /// </summary>
        /// <param name="accToken"></param>
        /// <returns></returns>
        public JArray GetStudentParentAssociations(string accToken)
        {
            
            return _students.GetStudentParentAssociations(accToken);
           
        }
        /// <summary>
        /// Gets student parent associations custom details.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public JArray GetStudentParentAssociationCustom(string accToken, string studentId)
        {
            
            return _students.GetStudentParentAssociationCustom(accToken, studentId);
           
        }
        /// <summary>
        /// Gets student parent associations details by id.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public JArray GetStudentParentAssociationById(string accToken, string studentId)
        {
            
            return _students.GetStudentParentAssociationById(accToken, studentId);
           
        }
        /// <summary>
        /// Gets parents with in the student parent associations details.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public JArray GetStudentParentAssociationParents(string accToken, string studentId)
        {
            
            return _students.GetStudentParentAssociationParents(accToken, studentId);
           
        }
        /// <summary>
        /// Gets _students with in the student parent associations.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public JArray GetStudentParentAssociationStudents(string accToken, string studentId)
        {
            
            return _students.GetStudentParentAssociationStudents(accToken, studentId);
           
        }


        #region CUD Methods
        /// <summary>
        /// Creates student Parent Associations details.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public string PostStudentParentAssociations(string accToken, string data)
        {
            
            return _students.PostStudentParentAssociations(accToken, data);
            
        }
        /// <summary>
        /// Updates  student Parent Associations details.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="data"></param>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public string PutStudentParentAssociations(string accToken, string data, string studentId)
        {
            
            return _students.PutStudentParentAssociations(accToken, data, studentId);
            
        }

        /// <summary>
        /// Deletes  student Parent Associations details.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public string DeleteStudentParentAssociations(string accToken, string studentId)
        {
            
            return _students.DeleteStudentParentAssociations(accToken, studentId);
            
        }
        #endregion
        #endregion

        #region studentProgramAssociations
        /// <summary>
        /// Gets student program associations details.
        /// </summary>
        /// <param name="accToken"></param>
        /// <returns></returns>
        public JArray GetStudentProgramAssociations(string accToken)
        {
            
            return _students.GetStudentProgramAssociations(accToken);
           
        }
        /// <summary>
        /// Gets student program associations custom details.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public JArray GetStudentProgramAssociationCustom(string accToken, string studentId)
        {
            
            return _students.GetStudentProgramAssociationCustom(accToken, studentId);
           
        }
        /// <summary>
        /// Gets student program associations details by program id.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public JArray GetStudentProgramAssociationByProgramId(string accToken, string studentId)
        {
            
            return _students.GetStudentProgramAssociationByProgramId(accToken, studentId);
           
        }
        /// <summary>
        /// Gets student program associations details by id.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public JArray GetStudentProgramAssociationById(string accToken, string studentId)
        {
            
            return _students.GetStudentProgramAssociationById(accToken, studentId);
           
        }
        /// <summary>
        /// Gets programs with in the student program associations.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public JArray GetStudentProgramAssociationPrograms(string accToken, string studentId)
        {
            
            return _students.GetStudentProgramAssociationPrograms(accToken, studentId);
           
        }
        /// <summary>
        /// Gets _students with in the student program associations.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public JArray GetStudentProgramAssociationStudents(string accToken, string studentId)
        {
            
            return _students.GetStudentProgramAssociationStudents(accToken, studentId);
           
        }

        #region CUD Methods
        /// <summary>
        /// Creates  student Program Associations details.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public string PostStudentProgramAssociations(string accToken, string data)
        {
            
            return _students.PostStudentProgramAssociations(accToken, data);
            
        }
        /// <summary>
        /// Updates  student Program Associations details.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="data"></param>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public string PutStudentProgramAssociations(string accToken, string data, string studentId)
        {
            
            return _students.PutStudentProgramAssociations(accToken, data, studentId);
            
        }

        /// <summary>
        /// Deletes  student Program Associations details.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public string DeleteStudentProgramAssociations(string accToken, string studentId)
        {
            
            return _students.DeleteStudentProgramAssociations(accToken, studentId);
            
        }
        #endregion
        #endregion

        #region Students
        /// <summary>
        /// Gets _students details.
        /// </summary>
        /// <param name="accToken"></param>
        /// <returns></returns>
        public JArray GetStudents(string accToken)
        {
            
            return _students.GetStudents(accToken);
           
        }
        /// <summary>
        ///  Gets _students custom  details.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public JArray GetStudentCustom(string accToken, string studentId)
        {
            
            return _students.GetStudentCustom(accToken, studentId);
           
        }
        /// <summary>
        ///  Gets attendances with in the _students.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public JArray GetStudentAttendances(string accToken, string studentId)
        {
            
            return _students.GetStudentAttendances(accToken, studentId);
           
        }
        /// <summary>
        /// Gets course transcripts with in the _students.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public JArray GetStudentCourseTranscripts(string accToken, string studentId)
        {
            
            return _students.GetStudentCourseTranscripts(accToken, studentId);
           
        }
        /// <summary>
        /// Gets courses in course transcripts with in the _students.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public JArray GetStudentCourseTranscriptCourses(string accToken, string studentId)
        {
            
            return _students.GetStudentCourseTranscriptCourses(accToken, studentId);
           
        }
        /// <summary>
        /// Gets report cards with in the _students.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public JArray GetStudentReportCards(string accToken, string studentId)
        {
            
            return _students.GetStudentReportCards(accToken, studentId);
           
        }
        /// <summary>
        /// Gets student assessments with in the _students.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public JArray GetStudentStudentAssessments(string accToken, string studentId)
        {
            
            return _students.GetStudentStudentAssessments(accToken, studentId);
           
        }
        /// <summary>
        /// Gets assessments in student assessments with in the _students.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public JArray GetStudentStudentAssessmentAssessments(string accToken, string studentId)
        {
            
            return _students.GetStudentStudentAssessmentAssessments(accToken, studentId);
           
        }
        /// <summary>
        /// Gets student cohort associations with in the _students.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public JArray GetStudentStudentCohortAssociations(string accToken, string studentId)
        {
            
            return _students.GetStudentStudentCohortAssociations(accToken, studentId);
           
        }
        /// <summary>
        /// Gets cohorts in student cohort associations with in the _students.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public JArray GetStudentStudentCohortAssociationCohorts(string accToken, string studentId)
        {
            
            return _students.GetStudentStudentCohortAssociationCohorts(accToken, studentId);
           
        }
        /// <summary>
        /// Gets student discipline incident associations with in the _students.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public JArray GetStudentStudentDisciplineIncidentAssociations(string accToken, string studentId)
        {
            
            return _students.GetStudentStudentDisciplineIncidentAssociations(accToken, studentId);
           
        }
        /// <summary>
        /// Gets discipline incidents in student discipline incident associations with in the _students.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public JArray GetStudentStudentDisciplineIncidentAssociationDisciplineIncidents(string accToken, string studentId)
        {
            
            return _students.GetStudentStudentDisciplineIncidentAssociationDisciplineIncidents(accToken, studentId);
           
        }
        /// <summary>
        /// Gets student parent associations with in the _students.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public JArray GetStudentStudentParentAssociations(string accToken, string studentId)
        {
            
            return _students.GetStudentStudentParentAssociations(accToken, studentId);
           
        }
        /// <summary>
        /// Gets parents in student parent associations with in the _students.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public JArray GetStudentStudentParentAssociationParents(string accToken, string studentId)
        {
            
            return _students.GetStudentStudentParentAssociationParents(accToken, studentId);
           
        }
        /// <summary>
        /// Gets student program associations with in the _students.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public JArray GetStudentStudentProgramAssociations(string accToken, string studentId)
        {
            
            return _students.GetStudentStudentProgramAssociations(accToken, studentId);
           
        }
        /// <summary>
        ///  Gets programs in student program associations with in the _students.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public JArray GetStudentStudentProgramAssociationPrograms(string accToken, string studentId)
        {
            
            return _students.GetStudentStudentProgramAssociationPrograms(accToken, studentId);
           
        }
        /// <summary>
        /// Gets student school associations with in the _students.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public JArray GetStudentStudentSchoolAssociations(string accToken, string studentId)
        {
            
            return _students.GetStudentStudentSchoolAssociations(accToken, studentId);
           
        }
        /// <summary>
        ///  Gets schools in student school associations with in the _students.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public JArray GetStudentStudentSchoolAssociationSchools(string accToken, string studentId)
        {
            
            return _students.GetStudentStudentSchoolAssociationSchools(accToken, studentId);
           
        }
        /// <summary>
        /// Gets student section associations with in the _students.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public JArray GetStudentStudentSectionAssociations(string accToken, string studentId)
        {
            
            return _students.GetStudentStudentSectionAssociations(accToken, studentId);
           
        }
        /// <summary>
        /// Gets sections in student section associations with in the _students.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public JArray GetStudentStudentSectionAssociationSections(string accToken, string studentId)
        {
            
            return _students.GetStudentStudentSectionAssociationSections(accToken, studentId);
           
        }

        #region CUD Methods
        /// <summary>
        /// Creates Students details.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public string PostStudents(string accToken, string data)
        {
            
            return _students.PostStudents(accToken, data);
            
        }
        /// <summary>
        /// Updates  Students details.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="data"></param>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public string PutStudents(string accToken, string data, string studentId)
        {
            
            return _students.PutStudents(accToken, data, studentId);
            
        }

        /// <summary>
        /// Deletes  Students details.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public string DeleteStudents(string accToken, string studentId)
        {
            
            return _students.DeleteStudents(accToken, studentId);
            
        }
        #endregion
        #endregion

        #region studentSchoolAssociations
        /// <summary>
        /// Gets student school associations details.
        /// </summary>
        /// <param name="accToken"></param>
        /// <returns></returns>
        public JArray GetStudentSchoolAssociations(string accToken)
        {
            
            return _students.GetStudentSchoolAssociations(accToken);
           
        }
        /// <summary>
        /// Gets student school associations details by id.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public JArray GetStudentSchoolAssociationById(string accToken, string studentId)
        {
            
            return _students.GetStudentSchoolAssociationById(accToken, studentId);
           
        }
        /// <summary>
        /// Gets student school associations custom details.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public JArray GetStudentSchoolAssociationCustom(string accToken, string studentId)
        {
            
            return _students.GetStudentSchoolAssociationCustom(accToken, studentId);
           
        }
        /// <summary>
        /// Gets schools with in the student school associations.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public JArray GetStudentSchoolAssociationSchools(string accToken, string studentId)
        {
            
            return _students.GetStudentSchoolAssociationSchools(accToken, studentId);
           
        }
        /// <summary>
        /// Gets _students with in the student school associations.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public JArray GetStudentSchoolAssociationStudents(string accToken, string studentId)
        {
            
            return _students.GetStudentSchoolAssociationStudents(accToken, studentId);
           
        }

        #region CUD Methods
        /// <summary>
        /// Creates student School Associations details.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public string PostStudentSchoolAssociations(string accToken, string data)
        {
            
            return _students.PostStudentSchoolAssociations(accToken, data);
            
        }
        /// <summary>
        /// Updates student School Associations details.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="data"></param>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public string PutStudentSchoolAssociations(string accToken, string data, string studentId)
        {
            
            return _students.PutStudentSchoolAssociations(accToken, data, studentId);
            
        }

        /// <summary>
        /// Deletes student School Associations details.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public string DeleteStudentSchoolAssociations(string accToken, string studentId)
        {
            
            return _students.DeleteStudentSchoolAssociations(accToken, studentId);
            
        }
        #endregion

        #endregion

        #region studentSectionAssociations
        /// <summary>
        /// Gets student section associations details.
        /// </summary>
        /// <param name="accToken"></param>
        /// <returns></returns>
        public JArray GetStudentSectionAssociations(string accToken)
        {
            
            return _students.GetStudentSectionAssociations(accToken);
           
        }
        /// <summary>
        /// Gets student section associations details by id.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public JArray GetStudentSectionAssociationById(string accToken, string studentId)
        {
            
            return _students.GetStudentSectionAssociationById(accToken, studentId);
           
        }
        /// <summary>
        /// Gets student section associations custom  details.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public JArray GetStudentSectionAssociationCustom(string accToken, string studentId)
        {
            
            return _students.GetStudentSectionAssociationCustom(accToken, studentId);
           
        }
        /// <summary>
        /// Gets grades with in the student section associations.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public JArray GetStudentSectionAssociationGrades(string accToken, string studentId)
        {
            
            return _students.GetStudentSectionAssociationGrades(accToken, studentId);
           
        }
        /// <summary>
        /// Gets sections with in the student section associations.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public JArray GetStudentSectionAssociationSections(string accToken, string studentId)
        {
            
            return _students.GetStudentSectionAssociationSections(accToken, studentId);
           
        }
        /// <summary>
        /// Gets student competencies with in the student section associations.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public JArray GetStudentSectionAssociationStudentCompetencies(string accToken, string studentId)
        {
            
            return _students.GetStudentSectionAssociationStudentCompetencies(accToken, studentId);
           
        }
        /// <summary>
        ///  Gets _students with in the student section associations.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public JArray GetStudentSectionAssociationStudents(string accToken, string studentId)
        {
            
            return _students.GetStudentSectionAssociationStudents(accToken, studentId);
           
        }

        #region CUD Methods
        /// <summary>
        /// Creates student Section Associations details.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public string PostStudentSectionAssociations(string accToken, string data)
        {
            
            return _students.PostStudentSectionAssociations( accToken, data);
            
        }
        /// <summary>
        /// Updates  student Section Associations details.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="data"></param>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public string PutStudentSectionAssociations(string accToken, string data, string studentId)
        {
            
            return _students.PutStudentSectionAssociations(accToken, data, studentId);
            
        }

        /// <summary>
        /// Deletes  student Section Associations details.
        /// </summary>
        /// <param name="accToken"></param>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public string DeleteStudentSectionAssociations(string accToken, string studentId)
        {
            
            return _students.DeleteStudentSectionAssociations(accToken, studentId);
            
        }
        #endregion

        #endregion


    }
}
