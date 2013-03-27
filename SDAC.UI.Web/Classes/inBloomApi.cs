using System;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Collections;
using System.Web.SessionState;
using System.Threading;
using System.Threading.Tasks;
using SDAC.UI.Web.Helpers;
using SDAC.UI.Web.Classes;
using System.Web.Script.Serialization;
using SDAC.Core;

namespace SDAC.UI.Web
{
    /// <summary>
    /// Purpose of InBloomApi class is to handel the api call, call the appropriate api and return the formatted result.
    /// SlcApi class get the data from inBloom sandbox by using api.
    /// </summary>
    public sealed class inBloomApi
    {
        #region "Private Variables"
        private AuthenticateUser _authenticateUser = null;
        private String _accessToken = String.Empty;
        public ListItem[] _sectionAndCourseOfferingId = null;

        CoreHomeData home = new CoreHomeData();
        CoreSchoolsData schoolData = new CoreSchoolsData();
        CoreSectionsData sectionData = new CoreSectionsData();
        CoreStudentsData studentData = new CoreStudentsData();
        CoreTeachersData tescherData = new CoreTeachersData();
        CoreCoursesData courseData = new CoreCoursesData();
        
        #endregion

        #region "Properties"

        public string AccessToken
        {
            get
            {
                return _accessToken;
            }
            set
            {
                _accessToken = value; 
            }
        }
        #endregion

        SqlHelper _sqlHelper = null;

        #region "Ctors"
        public inBloomApi() :this(string.Empty)
        {
            // ToDo: Do we need default constructor? 
        }

        public inBloomApi(String accessToken)
        {
            this._accessToken = accessToken;
            _authenticateUser = new AuthenticateUser();
            _sqlHelper = new SqlHelper();
        }

        #endregion
        
        #region "Public Methods"

        /// <summary>
        /// This function is used to get the school list according to the role of user who logged in.
        /// </summary>
        /// <returns></returns>
        public ListItem[] GetSchoolList()
        {
            try
            {
                JArray SchoolUrlResponse = JArray.Parse(RestApiHelper.CallApi("schools", this._accessToken));
                ListItem[] _listSchool = new ListItem[SchoolUrlResponse.Count];
                for (int Index = 0; Index < SchoolUrlResponse.Count(); Index++)
                {
                    JToken Token = SchoolUrlResponse[Index];
                    JArray SchoolName = (JArray)Token["educationOrgIdentificationCode"];
                    try
                    {
                        _listSchool[Index] = new ListItem(_authenticateUser.GetStringWithoutQuote(SchoolName[0]["ID"] + ""), _authenticateUser.GetStringWithoutQuote(Token["id"].ToString()));
                    }
                    catch (Exception ex)
                    {
                    }



                }
                return _listSchool;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        /// <summary>
        /// This function is used to get the User session with all user details.
        /// </summary>
        /// <returns></returns>
        public User UserDetails()
        {
            try
            {
                JObject UserResponse = JObject.Parse(RestApiHelper.CallApiWithParameter(ConfigurationHelper.GetItem("UserSession"), this._accessToken));
                User _user=new User();
                _user.Authenticated =(bool) UserResponse["authenticated"];

                _user.EducationOrganization = _authenticateUser.GetStringWithoutQuote(UserResponse["edOrg"].ToString());
                _user.EducationOrganizationId=_authenticateUser.GetStringWithoutQuote(UserResponse["edOrgId"].ToString());
                _user.Email = _authenticateUser.GetStringWithoutQuote(UserResponse["email"].ToString());
                _user.ExternalId = _authenticateUser.GetStringWithoutQuote(UserResponse["external_id"].ToString());
                _user.FullName = _authenticateUser.GetStringWithoutQuote(UserResponse["full_name"].ToString());

                JToken Token= UserResponse["granted_authorities"];
                String[] Auth = new String[Token.Count()];
                for (int i = 0; i < Token.Count(); i++)
                {
                    Auth[i] = _authenticateUser.GetStringWithoutQuote(Token[i].ToString());

                }
                _user.GrantedAuthorities = Auth;
                _user.Realm=_authenticateUser.GetStringWithoutQuote(UserResponse["realm"].ToString());

                Token = UserResponse["rights"];

                String[] Rights = new String[Token.Count()];

                for (int i = 0; i < Token.Count(); i++)
                {
                    Rights[i] = _authenticateUser.GetStringWithoutQuote(Token[i].ToString());
                }
                _user.Rights = Rights;


                Token = UserResponse["sliRoles"];

                String[] SliRoles = new String[Token.Count()];
                for (int i = 0; i < Token.Count(); i++)
                {
                    SliRoles[i] = _authenticateUser.GetStringWithoutQuote(Token[i].ToString());
                }
                _user.SliRoles = SliRoles;

                _user.TenantId = _authenticateUser.GetStringWithoutQuote(UserResponse["tenantId"].ToString());
                _user.UserId = _authenticateUser.GetStringWithoutQuote(UserResponse["user_id"].ToString());

               
                _user.IsAdminUser =(bool)UserResponse["isAdminUser"];

                return _user;
               
            }
            catch (Exception Ex)
            {
               return null;
            }
        }

        /// <summary>
        /// This function is used to get the course list according to school.
        /// </summary>
        /// <param name="SchoolId"></param>
        /// <returns></returns>
        public ListItem[] GetCourseBySchool(String schoolId)
        {
            try
            {
                JArray CourseUrlResponse = JArray.Parse(RestApiHelper.CallApi("courses?schoolId=" + schoolId, this._accessToken));
                ListItem[] _listSchool = new ListItem[CourseUrlResponse.Count];
                for (int Index = 0; Index < CourseUrlResponse.Count(); Index++)
                {
                    JToken token = CourseUrlResponse[Index];
                    JArray CourseName = (JArray)token["courseCode"];
                    _listSchool[Index] = new ListItem(_authenticateUser.GetStringWithoutQuote(CourseName[0]["ID"] + ""), _authenticateUser.GetStringWithoutQuote(token["id"].ToString()));
                }
                return _listSchool;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        private JObject JArrayToJObject(JArray _jArray)
        {
            JObject JReturn=new JObject();
            for (int i = 0; i < _jArray.Count; i++)
            {
                JReturn = (JObject)_jArray[i];
            
            }



            return JReturn;
        
        
        }


        public ListItem GetCourseBySectionCourseOfferingId(String courseOfferingId)
        {
            ListItem _course = null;
            try
            {
                JObject SectionUrlResponse = JArrayToJObject(courseData.GetCourseOfferingById(this._accessToken, courseOfferingId));
                if (SectionUrlResponse != null)
                {
                    String CourseId = SectionUrlResponse["courseId"].ToString();
                  
                    JObject CourseResponse = JArrayToJObject(courseData.GetCourseById(this._accessToken, CourseId));

                    JArray CourseName = (JArray)CourseResponse["courseCode"];

                    for (int i = 0; i < CourseName.Count; i++)
                    {
                        JToken Token = CourseName[i];
                        _course = new ListItem(_authenticateUser.GetStringWithoutQuote(Token["ID"] + ""),CourseId);
                    }

                   

                }

                return _course;
            }
            catch (Exception Ex)
            {
                return null;
            }
        }
        

        /// <summary>
        /// function GetSchool return the school list for login user  
        /// </summary>
        /// <returns></returns>
        public ListItem[] GetSchool()
        {
            ListItem[] _listSchool = new ListItem[1];
            try
            {
                 JObject HomeUrlResponse = (JObject)home.GetHome(this._accessToken)[0];
                JArray Links = (JArray)HomeUrlResponse["links"];
                for (int Index = 0; Index < Links.Count(); Index++)
                {
                    String Relation = _authenticateUser.GetStringWithoutQuote(HomeUrlResponse["links"][Index]["rel"].ToString());
                    if (Relation.Equals("getSchools"))
                    {
                        String Result = RestApiHelper.CallApiWithParameter(_authenticateUser.GetStringWithoutQuote(HomeUrlResponse["links"][Index]["href"].ToString()),this._accessToken);
                        JArray SchoolUrlResponse = JArray.Parse(Result);
                        _listSchool = new ListItem[SchoolUrlResponse.Count];
                        for (int SchoolIndex = 0; SchoolIndex < SchoolUrlResponse.Count(); SchoolIndex++)
                        {
                            JToken Token = SchoolUrlResponse[SchoolIndex];
                            JArray SchoolName = (JArray)Token["educationOrgIdentificationCode"];                                                    
                            _listSchool[SchoolIndex] = new ListItem(_authenticateUser.GetStringWithoutQuote(SchoolName[0]["ID"] + ""), _authenticateUser.GetStringWithoutQuote(Token["id"].ToString()));

                        }
                        return _listSchool;
                    }
                }
                return _listSchool;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        /// <summary>
        /// This function is used to get only those section to which the user teach.
        /// </summary>
        /// <returns></returns>
        public ListItem[] GetSectionListForLoginUser()
        {
            ListItem[] _listSection = new ListItem[1];
            try
            {
                JObject HomeUrlResponse = (JObject)home.GetHome(this._accessToken)[0];
                JArray Links = (JArray)HomeUrlResponse["links"];
                for (int Index = 0; Index < Links.Count(); Index++)
                {
                    String Relation = _authenticateUser.GetStringWithoutQuote(HomeUrlResponse["links"][Index]["rel"].ToString());
                    if (Relation.Equals("getSections"))
                    {
                        String Result = RestApiHelper.CallApiWithParameter(_authenticateUser.GetStringWithoutQuote(HomeUrlResponse["links"][Index]["href"].ToString()), this._accessToken);
                        JArray SchoolUrlResponse = JArray.Parse(Result);
                        _listSection = new ListItem[SchoolUrlResponse.Count];
                        _sectionAndCourseOfferingId = new ListItem[SchoolUrlResponse.Count];
                        for (int SectionIndex = 0; SectionIndex < SchoolUrlResponse.Count(); SectionIndex++)
                        {
                            JToken Token = SchoolUrlResponse[SectionIndex];
                            String SectionId= _authenticateUser.GetStringWithoutQuote(Token["id"].ToString());
                            String SectionName = _authenticateUser.GetStringWithoutQuote(Token["uniqueSectionCode"].ToString());
                            String CourseOfferingId = _authenticateUser.GetStringWithoutQuote(Token["courseOfferingId"].ToString());
                            _listSection[SectionIndex] = new ListItem(SectionName,SectionId);
                            _sectionAndCourseOfferingId[SectionIndex] = new ListItem(SectionId, CourseOfferingId);

                        }
                        return _listSection;
                    }
                }
                return _listSection;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public ListItem[] GetSectionAndCourseOfferingIdList()
        {
            return _sectionAndCourseOfferingId;
        }


        public ListItem GetSectionById(String sectionId)
        {
            ListItem _section = null;
            try
            {
               JObject SectionUrlResponse = (JObject)sectionData.GetSectionById(this._accessToken, sectionId)[0];
                if (SectionUrlResponse != null)
                {                  
                    String SectionName = _authenticateUser.GetStringWithoutQuote(SectionUrlResponse["uniqueSectionCode"].ToString());
                    _section = new ListItem(SectionName,sectionId);
                   
                }
                return _section;
            }
            catch (Exception Ex)
            {
                return null;
            }
        }



        public bool IsSectionContainOfferingOfThisCourse(String courseOfferingId, String courseId)
        {
            try
            {
                String Response=RestApiHelper.CallApi("courseOfferings/" + courseOfferingId+"?courseId="+courseId, this._accessToken);
               

                if(Response==null || Response=="")
                    return false;
                else
                    return true; ;
            }
            catch (Exception Ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Function to clear the session of login user
        /// </summary>
        public void LogOut()
        {
            RestApiHelper.CallApiWithParameter("https://api.sandbox.inbloom.org/api/rest/system/session/logout", this._accessToken);
          
            
        }

        /// <summary>
        /// This function is used to get the section list according to school and course.
        /// </summary>
        /// <param name="SchoolId"></param>
        /// <param name="CourseName"></param>
        /// <returns></returns>
        public ListItem[] GetSection(String schoolId, String courseOfferingId)
        {
            try
            {
                JArray SectionUrlResponse = JArray.Parse(RestApiHelper.CallApi("sections?schoolId=" + schoolId, this._accessToken));
                ListItem[] _listSection = new ListItem[SectionUrlResponse.Count];
                for (int Index = 0; Index < SectionUrlResponse.Count(); Index++)
                {
                    JToken Token = SectionUrlResponse[Index];
                    String SectionName = _authenticateUser.GetStringWithoutQuote(Token["uniqueSectionCode"].ToString());
                    String SectionId = _authenticateUser.GetStringWithoutQuote(Token["id"].ToString());

                    String CourseOfferingIdFromResponse = _authenticateUser.GetStringWithoutQuote(Token["courseOfferingId"].ToString());

                    
                    if (CourseOfferingIdFromResponse == courseOfferingId)
                    {
                        _listSection[Index] = new ListItem(SectionName, SectionId);
                    }
                    else
                    {
                        // Do Nothing
                    }
                }
                return _listSection;
            }
            catch (Exception ee)
            {
                return null;
            }

        }




        public ListItem[] GetSectionForAdmin(String schoolId, String courseId)
        {
            try
            {
                JArray SectionUrlResponse = JArray.Parse(RestApiHelper.CallApi("sections?schoolId=" + schoolId, this._accessToken));
                ListItem[] _listSection = new ListItem[SectionUrlResponse.Count];
                for (int Index = 0; Index < SectionUrlResponse.Count(); Index++)
                {
                    JToken Token = SectionUrlResponse[Index];
                    String SectionName = _authenticateUser.GetStringWithoutQuote(Token["uniqueSectionCode"].ToString());
                    String SectionId = _authenticateUser.GetStringWithoutQuote(Token["id"].ToString());

                    String CourseOfferingIdFromResponse = _authenticateUser.GetStringWithoutQuote(Token["courseOfferingId"].ToString());


                    if (IsSectionContainOfferingOfThisCourse(CourseOfferingIdFromResponse,courseId))
                    {
                        _listSection[Index] = new ListItem(SectionName, SectionId);
                    }
                    else
                    {
                        // Do Nothing
                    }
                }
                return _listSection;
            }
            catch (Exception ee)
            {
                return null;
            }

        }



        public ListItem[] GetSectionByStaffIdAndSchoolId (String staffId, String schoolId)
        {
            try
            {
               JArray SectionUrlResponse = tescherData.GetTeacherTeacherSectionAssociationSections(this._accessToken, staffId); ;
                ListItem[] _listSection = new ListItem[SectionUrlResponse.Count];
                _sectionAndCourseOfferingId = new ListItem[SectionUrlResponse.Count];
                for (int Index = 0; Index < SectionUrlResponse.Count(); Index++)
                {
                    JToken Token = SectionUrlResponse[Index];
                    String SectionName = _authenticateUser.GetStringWithoutQuote(Token["uniqueSectionCode"].ToString());
                    String SectionId = _authenticateUser.GetStringWithoutQuote(Token["id"].ToString());

                    String CourseOfferingIdFromResponse = _authenticateUser.GetStringWithoutQuote(Token["courseOfferingId"].ToString());
                    String SchoolIdFromResponse = _authenticateUser.GetStringWithoutQuote(Token["schoolId"].ToString());

                    if (SchoolIdFromResponse == schoolId)
                    {
                        _listSection[Index] = new ListItem(SectionName, SectionId);
                        _sectionAndCourseOfferingId[Index] = new ListItem(SectionId, CourseOfferingIdFromResponse);
                    }
                    else
                    {
                        // Do Nothing
                    }
                }
                return _listSection;
            }
            catch (Exception ee)
            {
                return null;
            }

        }


       /// <summary>
       /// Run the flag created by user and return the result.
       /// </summary>
       /// <param name="FieldName"></param>
       /// <param name="DataType"></param>
       /// <param name="ResponseType"></param>
       /// <param name="UserId"></param>
       /// <param name="SchoolId"></param>
       /// <param name="CourseId"></param>
       /// <param name="SectionId"></param>
       /// <param name="ConditionId"></param>
       /// <param name="Value1"></param>
       /// <param name="Value2"></param>
       /// <param name="EntityName"></param>
       /// <param name="IsPreview"></param>
       /// <returns></returns>
        public DataTable RunFlag(String fieldName, String dataType, bool responseType, String userId, String schoolId, String courseId, String sectionId, int conditionId, String value1, String value2, String entityName, bool isPreview)
        {
            
            String FirstName = "";
            String LastSurName = "";
            String MiddleName = "";
            String City = "";
            String NameOfCountry = "";
            String ApartmentRoomSuiteNumber = "";
            String StreetNumberName = "";
            String PostalCode = "";
            String StateAbbreviation = "";
            String AddressType = "";
            String EmailAddress = "";
            String EmailAddressType = "";
            String TelephoneNumber = "";
            String TelephoneNumberType = "";
            String Sex = "";
            String HispanicLatinoEthnicity = "";
            String BirthDate = "";
            String Id = "";
            String LimitedEnglishProficiency = "";
            String EntityType = "";


            String ParentFirstName = "";
            String ParentLastSurName = "";
            String ParentMiddleName = "";
            String ParentCity = "";
            String ParentNameOfCountry = "";
            String ParentApartmentRoomSuiteNumber = "";
            String ParentStreetNumberName = "";
            String ParentPostalCode = "";
            String ParentStateAbbreviation = "";
            String ParentAddressType = "";
            String ParentEmailAddress = "";
            String ParentEmailAddressType = "";
            String ParentTelephoneNumber = "";
            String ParentTelephoneNumberType = "";
            String ParentSex = "";
            String ParentParentUniqueStateId = "";
            String ParentId = "";
            String ParentEntityType = "";


            String Operator = GetOperator(conditionId);
            //String ExternalField = GetExternalField(FieldName);
            String ExternalField = _sqlHelper.GetExternalEntityByFieldNameAndEntityName(fieldName, entityName);

            String ApiCall = "";
            bool DataTypeBool = false;
            int count = 0;

            if (IsNumericDataType(dataType))
            {
                DataTypeBool = true;
            }
            else
                if (IsStringDataType(dataType))
                    DataTypeBool = false;

            
            DataTable _dtStudentList = new DataTable();
            _dtStudentList.Columns.AddRange(new DataColumn[] 
                { 
                    new DataColumn("Student ID", typeof(string)), 
                    new DataColumn("student_Name", typeof(string)), 
                    new DataColumn("Gender", typeof(string)), 
                    new DataColumn("GPA", typeof(string)),
                    new DataColumn("Student.First Name", typeof(string)),
                    new DataColumn("Student.Middle Name", typeof(string)),
                    new DataColumn("Student.Last Surname", typeof(string)),
                    new DataColumn("Student.City", typeof(string)),
                    new DataColumn("Student.Name Of Country", typeof(string)),
                    new DataColumn("Student.Apartment Room Suite Number", typeof(string)),
                    new DataColumn("Student.Street Number Name", typeof(string)),
                    new DataColumn("Student.Postal Code", typeof(string)), 
                    new DataColumn("Student.State Abbreviation", typeof(string)), 
                    new DataColumn("Student.Address Type", typeof(string)), 
                    new DataColumn("Student.Email Address", typeof(string)), 
                    new DataColumn("Student.Email Address Type", typeof(string)), 
                    new DataColumn("Student.Telephone Number", typeof(string)), 
                    new DataColumn("Student.Telephone Number Type", typeof(string)), 
                    new DataColumn("Student.Sex", typeof(string)), 
                    new DataColumn("Student.Hispanic Latino Ethnicity", typeof(string)), 
                    new DataColumn("Student.Birth Date", typeof(string)), 
                    new DataColumn("Student.Id", typeof(string)), 
                    new DataColumn("Student.Limited English Proficiency", typeof(string)), 
                    new DataColumn("Student.Entity Type", typeof(string)),
 

                    new DataColumn("Parent.First Name", typeof(string)),
                    new DataColumn("Parent.Middle Name", typeof(string)),
                    new DataColumn("Parent.Last Surname", typeof(string)),
                    new DataColumn("Parent.City", typeof(string)),
                    new DataColumn("Parent.Name Of Country", typeof(string)),
                    new DataColumn("Parent.Apartment Room Suite Number", typeof(string)),
                    new DataColumn("Parent.Street Number Name", typeof(string)),
                    new DataColumn("Parent.Postal Code", typeof(string)), 
                    new DataColumn("Parent.State Abbreviation", typeof(string)), 
                    new DataColumn("Parent.Address Type", typeof(string)), 
                    new DataColumn("Parent.Email Address", typeof(string)), 
                    new DataColumn("Parent.Email Address Type", typeof(string)), 
                    new DataColumn("Parent.Telephone Number", typeof(string)), 
                    new DataColumn("Parent.Telephone Number Type", typeof(string)), 
                    new DataColumn("Parent.Sex", typeof(string)), 
                    new DataColumn("Parent.Parent Unique State Id", typeof(string)),                  
                    new DataColumn("Parent.Id", typeof(string)),                    
                    new DataColumn("Parent.Entity Type", typeof(string)) 
                    

                });
           
      
                   if (!sectionId.Equals(""))
                   {

                       {
                             JArray StudentSectionResponse = sectionData.GetSectionStudentAssociationStudentList(this._accessToken, sectionId);
                               if (StudentSectionResponse.Count() > 0)
                               {
                                   // student

                                        //ApiCall = GetApi(FieldName);
                                        ApiCall = GetApiFromEntity(entityName, fieldName);

                                       if (ApiCall == "" || ApiCall == null)
                                       {
                                                   // presnt in student access from student response
                                                   for (int Index = 0; Index < StudentSectionResponse.Count(); Index++)
                                                   {
                                                               JToken Token = StudentSectionResponse[Index];

                                                               String name = _authenticateUser.GetStringWithoutQuote(Token["name"]["firstName"].ToString()) + " " + _authenticateUser.GetStringWithoutQuote(Token["name"]["lastSurname"].ToString());

                                                               String Gender = _authenticateUser.GetStringWithoutQuote(Token["sex"].ToString());

                                                            //   String city = _authenticateUser.GetStringWithoutQuote(Token["address"]["city"].ToString());

                                                               String StudentId = _authenticateUser.GetStringWithoutQuote(Token["studentUniqueStateId"].ToString());

                                                               String StudId = _authenticateUser.GetStringWithoutQuote(Token["id"].ToString());

                                                               String fieldval = "";


                                                               DataTable _studentAttribute = GetAllStudentAttribute(Token);
                                                               Object []obj=_studentAttribute.Rows[0].ItemArray;
                                                               try
                                                               {
                                                                   FirstName = obj[0].ToString();
                                                                   MiddleName  = obj[1].ToString();
                                                                   LastSurName = obj[2].ToString();
                                                                   City = obj[3].ToString();
                                                                   NameOfCountry = obj[4].ToString();
                                                                   ApartmentRoomSuiteNumber = obj[5].ToString();
                                                                   StreetNumberName = obj[6].ToString();
                                                                   PostalCode = obj[7].ToString();
                                                                   StateAbbreviation = obj[8].ToString();
                                                                   AddressType = obj[9].ToString();
                                                                   EmailAddress = obj[10].ToString();
                                                                   EmailAddressType = obj[11].ToString();
                                                                   TelephoneNumber = obj[12].ToString();
                                                                   TelephoneNumberType = obj[13].ToString();
                                                                   Sex = obj[14].ToString(); 
                                                                   HispanicLatinoEthnicity = obj[15].ToString();
                                                                   BirthDate = obj[16].ToString(); 
                                                                   Id = obj[17].ToString();
                                                                   LimitedEnglishProficiency = obj[18].ToString();
                                                                   EntityType = obj[19].ToString();
                                                               }
                                                               catch (Exception Ex1)
                                                               {
                                                               }
                                                           
                                                              
                                                               DataTable _studentParentAttribute = GetAllStudentParentAttribute(StudId);
                                                               obj=_studentParentAttribute.Rows[0].ItemArray;
                                                               try
                                                               {
                                                                   ParentFirstName = obj[0].ToString();
                                                                   ParentMiddleName = obj[1].ToString();
                                                                   ParentLastSurName = obj[2].ToString();
                                                                   ParentCity = obj[3].ToString();
                                                                   ParentNameOfCountry = obj[4].ToString();
                                                                   ParentApartmentRoomSuiteNumber = obj[5].ToString();
                                                                   ParentStreetNumberName = obj[6].ToString();
                                                                   ParentPostalCode = obj[7].ToString();
                                                                   ParentStateAbbreviation = obj[8].ToString();
                                                                   ParentAddressType = obj[9].ToString();
                                                                   ParentEmailAddress = obj[10].ToString();
                                                                   ParentEmailAddressType = obj[11].ToString();
                                                                   ParentTelephoneNumber = obj[12].ToString();
                                                                   ParentTelephoneNumberType = obj[13].ToString();
                                                                   ParentSex = obj[14].ToString();
                                                                   ParentParentUniqueStateId = obj[15].ToString();
                                                                   ParentId = obj[16].ToString();
                                                                   ParentEntityType = obj[17].ToString();                                                                   
                                                               }
                                                               catch (Exception Ex1)
                                                               {
                                                               }

                                                               try
                                                               {
                                                                   // direct present
                                                                   fieldval = _authenticateUser.GetStringWithoutQuote(Token[fieldName].ToString());
                                                               }
                                                               catch (Exception Ex)
                                                               {
                                                                   try
                                                                   {
                                                                       // with external field i.e array
                                                                       fieldval = _authenticateUser.GetStringWithoutQuote(Token[ExternalField][fieldName].ToString());
                                                                   }
                                                                   catch (Exception Ex1)
                                                                   {
                                                                       // its object
                                                                       try
                                                                       {

                                                                           JArray ja = JArray.Parse(Token[ExternalField].ToString());
                                                                           for (int i = 0; i < ja.Count(); i++)
                                                                           {
                                                                               JToken ObjectToken = ja[i];
                                                                               fieldval = _authenticateUser.GetStringWithoutQuote(ObjectToken[fieldName].ToString());
                                                                               break;  // get only one record
                                                                           }
                                                                       }
                                                                       catch (Exception Ex2)
                                                                       {
                                                                           // array of object
                                                                           try
                                                                           {
                                                                               // take only first
                                                                               JArray ja = JArray.Parse(Token[ExternalField].ToString());
                                                                               JToken ObjectToken = ja[0];
                                                                               fieldval = _authenticateUser.GetStringWithoutQuote(ObjectToken.First[fieldName].ToString());
                                                                           }
                                                                           catch (Exception Ex4)
                                                                           {

                                                                           }
                                                                       }
                                                                   }
                                                               }                                                             
                                                              
                                                             
                                                               if (IsValueCorrect(value1,value2, Operator, fieldval, DataTypeBool))
                                                                       {
                                                                           _dtStudentList.Rows.Add(new object[] { StudentId,
                                                                               name,
                                                                               Gender,
                                                                               fieldval,
                                                                               FirstName,MiddleName,LastSurName,
                                                                               City,NameOfCountry,ApartmentRoomSuiteNumber,StreetNumberName,PostalCode,StateAbbreviation,AddressType,
                                                                               EmailAddress,EmailAddressType,
                                                                               TelephoneNumber,TelephoneNumberType,
                                                                               Sex,HispanicLatinoEthnicity,BirthDate,Id,LimitedEnglishProficiency,EntityType,

                                                                               ParentFirstName,ParentLastSurName,ParentMiddleName,
                                                                               ParentCity,ParentNameOfCountry,ParentApartmentRoomSuiteNumber,ParentStreetNumberName,ParentPostalCode,ParentStateAbbreviation,ParentAddressType,
                                                                               ParentEmailAddress,ParentEmailAddressType,
                                                                               ParentTelephoneNumber,ParentTelephoneNumberType,
                                                                               ParentSex,ParentParentUniqueStateId,ParentId,ParentEntityType
                                                                           });
                                                                           count++;
                                                                       }
                                                               if (isPreview && count > 5)
                                                                 break;
                                                   }
                                       }
                                       else
                                       {
                                                // call the api 
                                                    for (int Index = 0; Index < StudentSectionResponse.Count(); Index++)
                                                    {
                                                        JToken Token = StudentSectionResponse[Index];

                                                        String StudentId = _authenticateUser.GetStringWithoutQuote(Token["studentUniqueStateId"].ToString());

                                                        String StudentIdForRun = _authenticateUser.GetStringWithoutQuote(Token["id"].ToString());

                                                        String Gender = _authenticateUser.GetStringWithoutQuote(Token["sex"].ToString());

                                                   //     String city = _authenticateUser.GetStringWithoutQuote(Token["address"]["city"].ToString());

                                                        String CallApiCall = GetApiWithParameter(ApiCall, StudentIdForRun, schoolId, courseId, sectionId);

                                                        String StudentResult = RestApiHelper.CallApi(CallApiCall, _accessToken);

                                                        JArray StudentResultResponse = JArray.Parse(StudentResult);

                                                        String name = _authenticateUser.GetStringWithoutQuote(Token["name"]["firstName"].ToString()) + " " + _authenticateUser.GetStringWithoutQuote(Token["name"]["lastSurname"].ToString());

                                                        String StudId = _authenticateUser.GetStringWithoutQuote(Token["id"].ToString());

                                                        DataTable _studentAttribute = GetAllStudentAttribute(Token);
                                                        Object[] obj = _studentAttribute.Rows[0].ItemArray;
                                                        try
                                                        {
                                                            FirstName = obj[0].ToString();
                                                            MiddleName = obj[1].ToString();
                                                            LastSurName = obj[2].ToString();
                                                            City = obj[3].ToString();
                                                            NameOfCountry = obj[4].ToString();
                                                            ApartmentRoomSuiteNumber = obj[5].ToString();
                                                            StreetNumberName = obj[6].ToString();
                                                            PostalCode = obj[7].ToString();
                                                            StateAbbreviation = obj[8].ToString();
                                                            AddressType = obj[9].ToString();
                                                            EmailAddress = obj[10].ToString();
                                                            EmailAddressType = obj[11].ToString();
                                                            TelephoneNumber = obj[12].ToString();
                                                            TelephoneNumberType = obj[13].ToString();
                                                            Sex = obj[14].ToString();
                                                            HispanicLatinoEthnicity = obj[15].ToString();
                                                            BirthDate = obj[16].ToString();
                                                            Id = obj[17].ToString();
                                                            LimitedEnglishProficiency = obj[18].ToString();
                                                            EntityType = obj[19].ToString();
                                                        }
                                                        catch (Exception Ex1)
                                                        {
                                                        }

                                                        DataTable _studentParentAttribute = GetAllStudentParentAttribute(StudId);
                                                        obj = _studentParentAttribute.Rows[0].ItemArray;
                                                        try
                                                        {
                                                            ParentFirstName = obj[0].ToString();
                                                            ParentMiddleName  = obj[1].ToString();
                                                            ParentLastSurName = obj[2].ToString();
                                                            ParentCity = obj[3].ToString();
                                                            ParentNameOfCountry = obj[4].ToString();
                                                            ParentApartmentRoomSuiteNumber = obj[5].ToString();
                                                            ParentStreetNumberName = obj[6].ToString();
                                                            ParentPostalCode = obj[7].ToString();
                                                            ParentStateAbbreviation = obj[8].ToString();
                                                            ParentAddressType = obj[9].ToString();
                                                            ParentEmailAddress = obj[10].ToString();
                                                            ParentEmailAddressType = obj[11].ToString();
                                                            ParentTelephoneNumber = obj[12].ToString();
                                                            ParentTelephoneNumberType = obj[13].ToString();
                                                            ParentSex = obj[14].ToString();
                                                            ParentParentUniqueStateId = obj[15].ToString();
                                                            ParentId = obj[16].ToString();
                                                            ParentEntityType = obj[17].ToString();
                                                        }
                                                        catch (Exception Ex1)
                                                        {
                                                        }
                                                        
                                                        //String fieldval = "";

                                                       
                                                        
                                                       if (responseType)
                                                        {
                                                            // response in array
                                                        }
                                                        else
                                                        {
                                                            // response in object
                                                            for (int i = 0; i < StudentResultResponse.Count(); i++)
                                                            {
                                                                JToken StudentResultResponseToken = StudentResultResponse[i];                                                                

                                                                String fieldval = "";



                                                                try
                                                                {
                                                                    // direct access
                                                                    fieldval = _authenticateUser.GetStringWithoutQuote(StudentResultResponseToken[fieldName].ToString());
                                                                }
                                                                catch (Exception Ex)
                                                                {                                                                    
                                                                    try
                                                                    {
                                                                        // with external field i.e array
                                                                        fieldval = _authenticateUser.GetStringWithoutQuote(StudentResultResponseToken[ExternalField][fieldName].ToString());
                                                                    }
                                                                    catch (Exception Ex2)
                                                                    {
                                                                        // object
                                                                        try
                                                                        {
                                                                            JArray ja = JArray.Parse(StudentResultResponseToken[ExternalField].ToString());
                                                                            for (int j = 0; j < ja.Count(); j++)
                                                                            {
                                                                                JToken ObjectToken = ja[j];
                                                                                fieldval = _authenticateUser.GetStringWithoutQuote(ObjectToken[fieldName].ToString());
                                                                                break;
                                                                            }
                                                                        }
                                                                        catch (Exception Ex3)
                                                                        {
                                                                            // array of object
                                                                            try
                                                                            {
                                                                                // take only first
                                                                                JArray ja = JArray.Parse(StudentResultResponseToken[ExternalField].ToString());
                                                                                JToken ObjectToken = ja[0];
                                                                                fieldval = _authenticateUser.GetStringWithoutQuote(ObjectToken.First[fieldName].ToString());
                                                                            }
                                                                            catch (Exception Ex4)
                                                                            {

                                                                            }
                                                                        }
                                                                    }
                                                                  
                                                                }

                                                               
                                                                if (IsValueCorrect(value1, value2, Operator, fieldval, DataTypeBool))
                                                                        {
                                                                            _dtStudentList.Rows.Add(new object[] { StudentId,
                                                                               name,
                                                                               Gender,
                                                                               fieldval,
                                                                               FirstName,MiddleName,LastSurName,
                                                                               City,NameOfCountry,ApartmentRoomSuiteNumber,StreetNumberName,PostalCode,StateAbbreviation,AddressType,
                                                                               EmailAddress,EmailAddressType,
                                                                               TelephoneNumber,TelephoneNumberType,
                                                                               Sex,HispanicLatinoEthnicity,BirthDate,Id,LimitedEnglishProficiency,EntityType,

                                                                               ParentFirstName,ParentLastSurName,ParentMiddleName,
                                                                               ParentCity,ParentNameOfCountry,ParentApartmentRoomSuiteNumber,ParentStreetNumberName,ParentPostalCode,ParentStateAbbreviation,ParentAddressType,
                                                                               ParentEmailAddress,ParentEmailAddressType,
                                                                               ParentTelephoneNumber,ParentTelephoneNumberType,
                                                                               ParentSex,ParentParentUniqueStateId,ParentId,ParentEntityType
                                                                            
                                                                            });
                                                                            count++;
                                                                        }
                                                                        break;

                                                            }



                                                        }
                                                       if (isPreview && count > 5)
                                                           break;

                                                        }

                                       }

                               }
                       }

                                              
                   }
                   if (_dtStudentList.Rows.Count > 0)
                   {
                   }
                   else
                   {
                       _dtStudentList.Rows.Add(new object[] { "", "" });  
                   }

                   return _dtStudentList;
        }


        public DataTable GetAllStudentAttribute(JToken token)
        {
            String StudentResponse = token.ToString();
            DataTable _studentAttribute = new DataTable();
            _studentAttribute.Columns.AddRange(new DataColumn[] 
                { 
                    new DataColumn("FirstName", typeof(string)),
                    new DataColumn("MiddleName", typeof(string)),
                    new DataColumn("LastSurname", typeof(string)),
                    new DataColumn("City", typeof(string)),
                    new DataColumn("NameOfCountry", typeof(string)),
                    new DataColumn("ApartmentRoomSuiteNumber", typeof(string)),
                    new DataColumn("StreetNumberName", typeof(string)),
                    new DataColumn("PostalCode", typeof(string)), 
                    new DataColumn("StateAbbreviation", typeof(string)), 
                    new DataColumn("AddressType", typeof(string)), 
                    new DataColumn("EmailAddress", typeof(string)), 
                    new DataColumn("EmailAddressType", typeof(string)), 
                    new DataColumn("TelephoneNumber", typeof(string)), 
                     new DataColumn("TelephoneNumberType", typeof(string)), 
                    new DataColumn("Sex", typeof(string)), 
                    new DataColumn("HispanicLatinoEthnicity", typeof(string)), 
                    new DataColumn("BirthDate", typeof(string)), 
                    new DataColumn("Id", typeof(string)), 
                    new DataColumn("LimitedEnglishProficiency", typeof(string)), 
                    new DataColumn("EntityType", typeof(string)) 
                });

            String FirstName = "";
            String LastSurName = "";
            String MiddleName = "";
            String City = "";
            String NameOfCountry = "";
            String ApartmentRoomSuiteNumber = "";
            String StreetNumberName = "";
            String PostalCode = "";
            String StateAbbreviation = "";
            String AddressType = "";
            String EmailAddress = "";
            String EmailAddressType = "";
            String TelephoneNumber = "";
            String TelephoneNumberType = "";
            String Sex = "";
            String HispanicLatinoEthnicity = "";
            String BirthDate = "";
            String Id = "";
            String LimitedEnglishProficiency = "";
            String EntityType = "";

            try
            {
                try
                {
                    if (StudentResponse.Contains("firstName"))
                    FirstName = _authenticateUser.GetStringWithoutQuote(token["name"]["firstName"].ToString());

                    if (StudentResponse.Contains("lastSurname"))
                    LastSurName = _authenticateUser.GetStringWithoutQuote(token["name"]["lastSurname"].ToString());

                    if (StudentResponse.Contains("middleName"))
                    MiddleName = _authenticateUser.GetStringWithoutQuote(token["name"]["middleName"].ToString());
                   
                   
                }
                catch (Exception Ex1)
                {
                }

                try
                {
                    if (StudentResponse.Contains("sex"))
                    Sex = _authenticateUser.GetStringWithoutQuote(token["sex"].ToString());

                    if (StudentResponse.Contains("hispanicLatinoEthnicity"))
                    HispanicLatinoEthnicity = token["hispanicLatinoEthnicity"].ToString();

                    if (StudentResponse.Contains("birthDate"))
                    BirthDate = _authenticateUser.GetStringWithoutQuote(token["birthData"]["birthDate"].ToString());

                    if (StudentResponse.Contains("id"))
                    Id = _authenticateUser.GetStringWithoutQuote(token["id"].ToString());

                    if (StudentResponse.Contains("limitedEnglishProficiency"))
                    LimitedEnglishProficiency = _authenticateUser.GetStringWithoutQuote(token["limitedEnglishProficiency"].ToString());

                    if (StudentResponse.Contains("entityType"))
                    EntityType = _authenticateUser.GetStringWithoutQuote(token["entityType"].ToString());
                }
                catch (Exception Ex2)
                {
                }

                JArray ja = JArray.Parse(token["address"].ToString());
                for (int i = 0; i < ja.Count(); i++)
                {
                    try
                    {
                        JToken ObjectToken = ja[i];

                        if (StudentResponse.Contains("city"))
                        City = _authenticateUser.GetStringWithoutQuote(ObjectToken["city"].ToString());

                        if (StudentResponse.Contains("nameOfCounty"))
                        NameOfCountry = _authenticateUser.GetStringWithoutQuote(ObjectToken["nameOfCounty"].ToString());

                        if (StudentResponse.Contains("apartmentRoomSuiteNumber"))
                        ApartmentRoomSuiteNumber = _authenticateUser.GetStringWithoutQuote(ObjectToken["apartmentRoomSuiteNumber"].ToString());

                        if (StudentResponse.Contains("streetNumberName"))
                        StreetNumberName = _authenticateUser.GetStringWithoutQuote(ObjectToken["streetNumberName"].ToString());

                        if (StudentResponse.Contains("postalCode"))
                        PostalCode = _authenticateUser.GetStringWithoutQuote(ObjectToken["postalCode"].ToString());

                        if (StudentResponse.Contains("stateAbbreviation"))
                        StateAbbreviation = _authenticateUser.GetStringWithoutQuote(ObjectToken["stateAbbreviation"].ToString());

                        if (StudentResponse.Contains("addressType"))
                        AddressType = _authenticateUser.GetStringWithoutQuote(ObjectToken["addressType"].ToString());
                        break;
                    }
                    catch (Exception Ex4)
                    {
                    }

                }

                ja = JArray.Parse(token["electronicMail"].ToString());
                for (int i = 0; i < ja.Count(); i++)
                {
                    try
                    {
                        JToken ObjectToken = ja[i];

                        if (StudentResponse.Contains("emailAddress"))
                        EmailAddress = _authenticateUser.GetStringWithoutQuote(ObjectToken["emailAddress"].ToString());

                        if (StudentResponse.Contains("emailAddressType"))
                        EmailAddressType = _authenticateUser.GetStringWithoutQuote(ObjectToken["emailAddressType"].ToString());
                        break;
                    }
                    catch (Exception Ex5)
                    {
                    }
                }

                ja = JArray.Parse(token["telephone"].ToString());
                for (int i = 0; i < ja.Count(); i++)
                {
                    try
                    {
                        JToken ObjectToken = ja[i];

                        if (StudentResponse.Contains("telephoneNumber"))
                        TelephoneNumber = _authenticateUser.GetStringWithoutQuote(ObjectToken["telephoneNumber"].ToString());

                        if (StudentResponse.Contains("telephoneNumberType"))
                        TelephoneNumberType = _authenticateUser.GetStringWithoutQuote(ObjectToken["telephoneNumberType"].ToString());
                        break;
                    }
                    catch (Exception Ex6)
                    {
                    }

                }
                               
                _studentAttribute.Rows.Add(new object[] 
                    { 
                          FirstName, MiddleName, LastSurName, 
                          City,NameOfCountry,ApartmentRoomSuiteNumber,StreetNumberName,PostalCode,StateAbbreviation,AddressType,
                          EmailAddress,EmailAddressType,
                          TelephoneNumber,TelephoneNumberType,
                          Sex,HispanicLatinoEthnicity,BirthDate,Id,LimitedEnglishProficiency,EntityType
                    });

            }
            catch (Exception Ex)
            {
            }

            if (_studentAttribute.Rows.Count > 0)
            {
            }
            else
            {
                _studentAttribute.Rows.Add(new object[] { "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "" });
            }
            return _studentAttribute;
        }




        public DataTable GetAllStudentParentAttribute(String studentId)
        {
            DataTable _studentParentAttribute = new DataTable();
            _studentParentAttribute.Columns.AddRange(new DataColumn[] 
                    { 
                        new DataColumn("FirstName", typeof(string)),
                        new DataColumn("MiddleName", typeof(string)),
                        new DataColumn("LastSurname", typeof(string)),
                        new DataColumn("City", typeof(string)),
                        new DataColumn("NameOfCountry", typeof(string)),
                        new DataColumn("ApartmentRoomSuiteNumber", typeof(string)),
                        new DataColumn("StreetNumberName", typeof(string)),
                        new DataColumn("PostalCode", typeof(string)), 
                        new DataColumn("StateAbbreviation", typeof(string)), 
                        new DataColumn("AddressType", typeof(string)), 
                        new DataColumn("EmailAddress", typeof(string)), 
                        new DataColumn("EmailAddressType", typeof(string)), 
                        new DataColumn("TelephoneNumber", typeof(string)), 
                         new DataColumn("TelephoneNumberType", typeof(string)), 
                        new DataColumn("Sex", typeof(string)), 
                        new DataColumn("ParentUniqueStateId", typeof(string)),                   
                        new DataColumn("Id", typeof(string)),                   
                        new DataColumn("EntityType", typeof(string)) 
                    });
            try
            {
                JArray StudentResultResponse = studentData.GetStudentStudentParentAssociationParents(this._accessToken, studentId);
                JToken Token;
               

                for (int Index = 0; Index < StudentResultResponse.Count(); Index++)
                {
                    Token = StudentResultResponse[Index];

                    String StudentParentResponse = Token.ToString();


                    String FirstName = "";
                    String LastSurName = "";
                    String MiddleName = "";
                    String City = "";
                    String NameOfCountry = "";
                    String ApartmentRoomSuiteNumber = "";
                    String StreetNumberName = "";
                    String PostalCode = "";
                    String StateAbbreviation = "";
                    String AddressType = "";
                    String EmailAddress = "";
                    String EmailAddressType = "";
                    String TelephoneNumber = "";
                    String TelephoneNumberType = "";
                    String Sex = "";
                    String ParentUniqueStateId = "";
                    String Id = "";
                    String EntityType = "";

                    try
                    {
                        try
                        {
                            if (StudentParentResponse.Contains("firstName"))
                                FirstName = _authenticateUser.GetStringWithoutQuote(Token["name"]["firstName"].ToString());

                            if (StudentParentResponse.Contains("lastSurname"))
                                LastSurName = _authenticateUser.GetStringWithoutQuote(Token["name"]["lastSurname"].ToString());

                            if (StudentParentResponse.Contains("middleName"))
                                MiddleName = _authenticateUser.GetStringWithoutQuote(Token["name"]["middleName"].ToString());


                        }
                        catch (Exception Ex1)
                        {
                        }

                        try
                        {
                            if (StudentParentResponse.Contains("sex"))
                                Sex = _authenticateUser.GetStringWithoutQuote(Token["sex"].ToString());

                            if (StudentParentResponse.Contains("parentUniqueStateId"))
                                ParentUniqueStateId = Token["parentUniqueStateId"].ToString();

                            //if (StudentParentResponse.Contains("telephoneNumber"))
                            //    TelephoneNumber = _authenticateUser.GetStringWithoutQuote(Token["telephoneNumber"].ToString());

                            if (StudentParentResponse.Contains("id"))
                                Id = _authenticateUser.GetStringWithoutQuote(Token["id"].ToString());

                            if (StudentParentResponse.Contains("entityType"))
                                EntityType = _authenticateUser.GetStringWithoutQuote(Token["entityType"].ToString());
                        }
                        catch (Exception Ex2)
                        {
                        }

                        JArray ja = JArray.Parse(Token["address"].ToString());
                        for (int i = 0; i < ja.Count(); i++)
                        {
                            try
                            {
                                JToken ObjectToken = ja[i];

                                if (StudentParentResponse.Contains("city"))
                                    City = _authenticateUser.GetStringWithoutQuote(ObjectToken["city"].ToString());

                                if (StudentParentResponse.Contains("nameOfCounty"))
                                    NameOfCountry = _authenticateUser.GetStringWithoutQuote(ObjectToken["nameOfCounty"].ToString());

                                if (StudentParentResponse.Contains("apartmentRoomSuiteNumber"))
                                    ApartmentRoomSuiteNumber = _authenticateUser.GetStringWithoutQuote(ObjectToken["apartmentRoomSuiteNumber"].ToString());

                                if (StudentParentResponse.Contains("streetNumberName"))
                                    StreetNumberName = _authenticateUser.GetStringWithoutQuote(ObjectToken["streetNumberName"].ToString());

                                if (StudentParentResponse.Contains("postalCode"))
                                    PostalCode = _authenticateUser.GetStringWithoutQuote(ObjectToken["postalCode"].ToString());

                                if (StudentParentResponse.Contains("stateAbbreviation"))
                                    StateAbbreviation = _authenticateUser.GetStringWithoutQuote(ObjectToken["stateAbbreviation"].ToString());

                                if (StudentParentResponse.Contains("addressType"))
                                    AddressType = _authenticateUser.GetStringWithoutQuote(ObjectToken["addressType"].ToString());
                                break;
                            }
                            catch (Exception Ex4)
                            {
                            }

                        }

                        ja = JArray.Parse(Token["electronicMail"].ToString());
                        for (int i = 0; i < ja.Count(); i++)
                        {
                            try
                            {
                                JToken ObjectToken = ja[i];

                                if (StudentParentResponse.Contains("emailAddress"))
                                    EmailAddress = _authenticateUser.GetStringWithoutQuote(ObjectToken["emailAddress"].ToString());

                                if (StudentParentResponse.Contains("emailAddressType"))
                                    EmailAddressType = _authenticateUser.GetStringWithoutQuote(ObjectToken["emailAddressType"].ToString());
                                break;
                            }
                            catch (Exception Ex5)
                            {
                            }
                        }

                        ja = JArray.Parse(Token["telephone"].ToString());
                        for (int i = 0; i < ja.Count(); i++)
                        {
                            try
                            {
                                JToken ObjectToken = ja[i];

                                if (StudentParentResponse.Contains("telephoneNumber"))
                                    TelephoneNumber = _authenticateUser.GetStringWithoutQuote(ObjectToken["telephoneNumber"].ToString());

                                if (StudentParentResponse.Contains("telephoneNumberType"))
                                    TelephoneNumberType = _authenticateUser.GetStringWithoutQuote(ObjectToken["telephoneNumberType"].ToString());
                                break;
                            }
                            catch (Exception Ex6)
                            {
                            }

                        }

                        _studentParentAttribute.Rows.Add(new object[] 
                            { 
                                  FirstName, MiddleName, LastSurName, 
                                  City,NameOfCountry,ApartmentRoomSuiteNumber,StreetNumberName,PostalCode,StateAbbreviation,AddressType,
                                  EmailAddress,EmailAddressType,
                                  TelephoneNumber,TelephoneNumberType,
                                  Sex,ParentUniqueStateId,Id,EntityType
                            });

                    }
                    catch (Exception Ex)
                    {
                    }


                    break;
                }

                if (_studentParentAttribute.Rows.Count > 0)
                {
                }
                else
                {
                    _studentParentAttribute.Rows.Add(new object[] { "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "" });
                }

                return _studentParentAttribute;
            }
            catch (Exception Ex)
            {
                _studentParentAttribute.Rows.Add(new object[] { "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "" });
                return _studentParentAttribute;
            }
        }
        


        /// <summary>
        /// Run the aggregate flag and return the user
        /// </summary>
        /// <param name="FlagId"></param>
        /// <param name="SchoolId"></param>
        /// <param name="CourseId"></param>
        /// <param name="SectionId"></param>
        /// <returns></returns>
        public DataTable RunAggregateFlag(int[] flagId, String schoolId, String courseId, String sectionId)
        {
            try
            {
                // get all the student


                String FieldName = "";
                String DataType = "";
                bool ResponseType = false;
                String UserId = "";
                int ConditionId;
                String Value1 = "";
                String Value2 = "";
                String EntityName = "";
                bool IsPreview = false;
                String ApiCall = "";
                String FlagUser = "";               

                String[] FieldNameArray = new string[flagId.Count()];
                String[] EntityNameArray = new string[flagId.Count()];
              

                DataTable []dt = new DataTable[flagId.Count()];
             
                ConditionId = 0; 

              
               for (int i = 0; i < flagId.Count(); i++)
               //Parallel.For(0, FlagId.Count(), i => 
                {                   
                    
                    Random r = new Random();

                    //System.Threading.Thread.Sleep(1000);
                    int DataElementId = _sqlHelper.GetDataElementIdByFlagId(flagId[i]);

                    //System.Threading.Thread.Sleep(1000);
                    FieldName = _sqlHelper.GetFieldNameByDataElementId(DataElementId);
                    FieldNameArray[i] = FieldName;

                    //System.Threading.Thread.Sleep(1000);
                    EntityName = _sqlHelper.GetEntityNameByDataElementId(DataElementId);
                    EntityNameArray[i] = EntityName;

                    //System.Threading.Thread.Sleep(1000);
                    DataType = _sqlHelper.GetDataTypeByDataElementId(DataElementId);

                    //System.Threading.Thread.Sleep(1000);
                    FlagUser = _sqlHelper.GetUserIdByFlagId(flagId[i]);

                    //System.Threading.Thread.Sleep(1000);
                    ConditionId = _sqlHelper.GetConditionIdByFlagId(flagId[i]);

                    //System.Threading.Thread.Sleep(1000);
                    Value1 = _sqlHelper.GetValue1ByFlagId(flagId[i]);

                    //System.Threading.Thread.Sleep(1000);
                    Value2 = _sqlHelper.GetValue2ByFlagId(flagId[i]);


                    //System.Threading.Thread.Sleep(1000);
                    dt[i] = RunFlag(FieldName, DataType, ResponseType, FlagUser, schoolId, courseId, sectionId, ConditionId, Value1, Value2, EntityName, IsPreview); 

                //}); 
                }
              
                             

              
                DataTable re = new DataTable();
                re = dt[0];

                int[] RowId = new int[re.Rows.Count];
                int Count = 0;
                String []StudentIdArray =new string[re.Rows.Count];

                bool IsFound = false;
                int RowIndex;
                for (int i = 1; i < flagId.Count(); i++)
                {
                    FieldName = FieldNameArray[i];
                    EntityName = EntityNameArray[i];
                    String FormattedFieldName = GetWellFormattedString(char.ToUpper(FieldName[0]) + FieldName.Substring(1));
                    String FormattedEntityName = GetWellFormattedString(char.ToUpper(EntityName[0]) + EntityName.Substring(1));
                    re.Columns.Add(FormattedEntityName + "." + FormattedFieldName);
                
                    foreach (DataRow Row in re.Rows)
                    {
                        IsFound = false;
                        String StudentIdRes = "";
                        String StudentIdDT = "";
                        foreach (DataRow Rd in dt[i].Rows)
                        {
                            StudentIdRes = Row["Student ID"].ToString();
                            StudentIdDT = Rd["Student ID"].ToString();

                            try
                            {
                                String ab = Rd["GPA"].ToString();
                            }
                            catch (Exception Ex)
                            { 
                            }


                            if (StudentIdRes == StudentIdDT)
                            {
                                IsFound = true;
                                String Field = FieldNameArray[i];
                                Row["" + FormattedEntityName + "." + FormattedFieldName] = Rd["GPA"];
                                break;
                            }
                        }

                        if (!IsFound)
                        {
                            RowId[Count] = Count;
                            StudentIdArray[Count] = StudentIdRes;
                        }
                        else
                        {
                            //StudentIdArray[Count] = "";
                        }

                        Count++;
                     
                    }

                    Count = 0;
                }

                for (int i = 0; i < StudentIdArray.Count(); i++)
                {
                    String Id = StudentIdArray[i];
                    if (Id == null || Id == "")
                        continue;
                    else
                    {
                        foreach (DataRow Rd in re.Rows)
                        {
                            if (Id == Rd["Student ID"].ToString())
                            {
                                Rd.Delete();
                                break;
                            }
                        }
                    }
                }
               


                try
                {
                    re.Columns[0].ColumnName = "Student.Student Unique State Id";
                    
                    //re.Columns[1].ColumnName = "studentName";
                    //re.Columns[2].ColumnName = "gender";
                    
                    FieldName=FieldNameArray[0];
                    EntityName = EntityNameArray[0];

                    re.Columns[3].ColumnName = GetWellFormattedString(char.ToUpper(EntityName[0]) + EntityName.Substring(1)) + "." + GetWellFormattedString(char.ToUpper(FieldName[0]) + FieldName.Substring(1));
                    re.Columns.RemoveAt(2);
                    re.Columns.RemoveAt(1);
                }
                catch (Exception Ex)
                {
                }

                if (re.Rows.Count > 0)
                {
                }
                else
                {
                    try
                    {
                        Object[] _obj = new object[re.Columns.Count];
                        for (int i = 0; i < re.Columns.Count; i++)
                        {
                            _obj[i] = "";
                        }
                        re.Rows.Add(_obj);
                    }
                    catch (Exception Ex1)
                    {
                    }
                }

                return re;
            }
            catch (Exception Ex)
            {
                return null;
            }
        }

        /// <summary>
        /// This function is used to add the flag to school custom.
        /// </summary>
        /// <param name="SchoolId"></param>
        /// <param name="JSONObject"></param>
        public void AddFlagToSchoolCustom(String schoolId, String jsonObject)
        {
            try
            {
                String SchoolCustom = RestApiHelper.CallApiForCustomPut("schools/" + schoolId + "/custom", _accessToken, jsonObject);
            }
            catch (Exception Ex)
            {
                
            }
        }

        /// <summary>
        /// This function is used to get the custom flag by school id.
        /// </summary>
        /// <param name="SchoolId"></param>
        /// <returns></returns>
        public String GetFlagFromSchoolCustomBySchoolId(String schoolId)
        {
            String SchoolCustom = RestApiHelper.CallApi("schools/" + schoolId + "/custom", _accessToken);
            return SchoolCustom;
        }

        /// <summary>
        /// Get the operator by condition id that user can apply on flag
        /// </summary>
        /// <param name="ConditionId"></param>
        /// <returns></returns>
        public String GetOperator(int conditionId)
        {
            String ConditionName =_sqlHelper.GetConditionName(conditionId);

            String Operator = null;
            switch (ConditionName)
            {
                case "Is equal to": Operator = "=="; break;
                case "Is not equal to": Operator = "!="; break;
                case "Is greater than": Operator = ">"; break;
                case "Is greater than or equal to": Operator = ">="; break;
                case "Is less than": Operator = "<"; break;
                case "Is less than or equal to": Operator = "<="; break;
                case "Starts with": Operator = "StartsWith"; break;
                case "Does not start with": Operator = "DoesNotStartsWith"; break;
                case "Ends with": Operator = "EndsWith"; break;
                case "Does not end with": Operator = "DoesNotEndsWith"; break;
                case "Contains": Operator = "Contains"; break;
                case "Does not contain": Operator = "DoesNotContains"; break;
                case "Is between": Operator = "IsBetween"; break;
                case "Is not between": Operator = "IsNotBetween"; break;
                case "Is one of": Operator = "IsOneOf"; break;
                case "Is not one of": Operator = "IsNotOneOf"; break;
                case "Is blank or empty": Operator = "IsBlankOrEmpty"; break;
                case "Is not blank nor empty": Operator = "IsNotBlankOrEmpty"; break;
                default: Operator = null; break;

            }

            return Operator;
        }


      


        /// <summary>
        /// Get the api call according to entity
        /// </summary>
        /// <param name="Entity"></param>
        /// <param name="FieldName"></param>
        /// <returns></returns>
        public String GetApiFromEntity(String entity, String fieldName)
        {
            String Api="";

            switch (entity)
            {
                case "Student": Api = ""; break;

                case "StudentAcademicRecord": Api = "studentAcademicRecords?studentId={StudentId}"; break;

                case "StudentSchoolAssociation": Api = "students/{StudentId}/studentSchoolAssociations"; break;

                case "StudentAssessment" : Api = "students/{StudentId}/studentAssessments"; break;

                case "StudentGradebookEntry": Api = "students/{StudentId}/studentGradebookEntries"; break;

                case "StudentParentAssociation": Api = "students/{StudentId}/studentParentAssociations"; break;

                case "Parent": Api = "students/{StudentId}/studentParentAssociations/parents"; break;

                case "Assessment": Api = "students/{StudentId}/studentAssessments/assessments"; break;

                case "StudentProgramAssociation": Api = "students/{StudentId}/studentProgramAssociations"; break;

                case "Program": Api = "students/{StudentId}/studentProgramAssociations/programs"; break;

                case "Cohort": Api = "students/{StudentId}/studentCohortAssociations/cohorts"; break;

                case "StudentSectionAssociation": Api = "students/{StudentId}/studentSectionAssociations?sectionId={SectionId}"; break;

                case "DisciplineAction": Api = "noApi"; break;
                case "DisciplineIncident": Api = "noApi"; break;
                case "StudentDisciplineIncidentAssociation": Api = "noApi"; break;
                case "GraduationPlan": Api = "noApi"; break;
                case "StaffCohortAssociation": Api = "noApi"; break;
                case "Staff": Api = "noApi"; break;
                case "Teacher": Api = "noApi"; break;
                case "CredentialFieldDescriptor": Api = "noApi"; break;
                case "TeacherSchoolAssociation": Api = "noApi"; break;
                case "AssessmentItem": Api = "noApi"; break;
                case "AssessmentPeriodDescriptor": Api = "noApi"; break;
                case "LearningObjective": Api = "noApi"; break;
                case "LearningStandard": Api = "noApi"; break;
                case "ObjectiveAssessment": Api = "noApi"; break;
                case "PerformanceLevelDescriptor": Api = "noApi"; break;
                case "AttendanceEvent": Api = "noApi"; break;
                case "CourseTranscript": Api = "noApi"; break;
                case "StudentAssessmentItem": Api = "noApi"; break;
                case "StudentObjectiveAssessment": Api = "noApi"; break;
                   
            }

            return Api;
        }


    

        /// <summary>
        /// Function to check whether the value entered by user is match with the values comes from api with condition
        /// </summary>
        /// <param name="Value1"></param>
        /// <param name="Value2"></param>
        /// <param name="Operator"></param>
        /// <param name="ApiValue"></param>
        /// <param name="DataType"></param>
        /// <returns></returns>
        public bool IsValueCorrect(String value1, String value2, String Operator, String apiValue, bool dataType)
        {

            //String Operator = GetOperator(ConditionId);
            bool res = false;

            int v1 = 0, v2 = 0, ApiValueInt=0;
            double v3 = 0, v4 = 0, ApiValueDouble = 0;


            switch (Operator)
            {
                case "==":
                            if (dataType)
                            {
                                v3 = Convert.ToDouble(value1);
                                ApiValueDouble = Convert.ToDouble(apiValue);
                                if (ApiValueDouble == v3)
                                    res = true;
                            }
                            else
                            {
                                try
                                {
                                    // for date type
                                    DateTime dt1 = Convert.ToDateTime(value1);
                                    DateTime dt2 = Convert.ToDateTime(apiValue);
                                    if (dt1 == dt2)
                                        res = true;

                                }
                                catch (Exception Ex)
                                {
                                    apiValue = apiValue.ToLower();
                                    value1 = value1.ToLower();
                                    if (apiValue == value1)
                                        res = true;
                                }
                            }
                            break;

                case "!=":
                            if (dataType)
                            {
                                v3 = Convert.ToDouble(value1);
                                ApiValueDouble = Convert.ToDouble(apiValue);
                                if (ApiValueDouble != v3)
                                    res = true;
                            }
                            else
                            {
                                try
                                {
                                    // for date type
                                    DateTime dt1 = Convert.ToDateTime(value1);
                                    DateTime dt2 = Convert.ToDateTime(apiValue);
                                    if (dt1 != dt2)
                                        res = true;
                                }
                                catch (Exception Ex)
                                {
                                    apiValue = apiValue.ToLower();
                                    value1 = value1.ToLower();
                                    if (apiValue != value1)
                                        res = true;
                                }

                            }
                            break;

                case ">":
                            if (dataType)
                            {
                                v3 = Convert.ToDouble(value1);
                                ApiValueDouble = Convert.ToDouble(apiValue);
                                if (ApiValueDouble > v3)
                                    res = true;
                            }
                            else
                            {
                                try
                                {
                                    // for date type
                                    DateTime dt1 = Convert.ToDateTime(value1);
                                    DateTime dt2 = Convert.ToDateTime(apiValue);
                                    if (dt2 > dt1)
                                        res = true;
                                }
                                catch (Exception Ex)
                                {
                                }
                            }
                            break;

                case ">=":
                            if (dataType)
                            {
                                v3 = Convert.ToDouble(value1);
                                ApiValueDouble = Convert.ToDouble(apiValue);
                                if (ApiValueDouble >= v3)
                                    res = true;
                            }
                            else
                            {
                                try
                                {
                                    // for date type
                                    DateTime dt1 = Convert.ToDateTime(value1);
                                    DateTime dt2 = Convert.ToDateTime(apiValue);
                                    if (dt2 >= dt1)
                                        res = true;
                                }
                                catch (Exception Ex)
                                {
                                }
                            }
                            break;

                case "<":
                            if (dataType)
                            {
                                v3 = Convert.ToDouble(value1);
                                ApiValueDouble = Convert.ToDouble(apiValue);
                                if (ApiValueDouble < v3)
                                    res = true;
                            }
                            else
                            {
                                try
                                {
                                    // for date type
                                    DateTime dt1 = Convert.ToDateTime(value1);
                                    DateTime dt2 = Convert.ToDateTime(apiValue);
                                    if (dt2 < dt1)
                                        res = true;
                                }
                                catch (Exception Ex)
                                {
                                }
                            }
                            break;

                case "<=":
                            if (dataType)
                            {
                                v3 = Convert.ToDouble(value1);
                                ApiValueDouble = Convert.ToDouble(apiValue);
                                if (ApiValueDouble <= v3)
                                    res = true;
                            }
                            else
                            {
                                try
                                {
                                    // for date type
                                    DateTime dt1 = Convert.ToDateTime(value1);
                                    DateTime dt2 = Convert.ToDateTime(apiValue);
                                    if (dt2 <= dt1)
                                        res = true;
                                }
                                catch (Exception Ex)
                                {
                                }
                            }
                            break;

                case "StartsWith":
                            apiValue = apiValue.ToLower();
                            value1 = value1.ToLower();
                            if (apiValue.StartsWith(value1)) 
                                res = true; break;
                case "DoesNotStartsWith": 
                            apiValue = apiValue.ToLower();
                            value1 = value1.ToLower();
                            if (!(apiValue.StartsWith(value1))) 
                                res = true; break;
                case "EndsWith": 
                            apiValue = apiValue.ToLower();
                            value1 = value1.ToLower();
                            if (apiValue.EndsWith(value1)) 
                                res = true; break;
                case "DoesNotEndsWith": 
                            apiValue = apiValue.ToLower();
                            value1 = value1.ToLower();
                            if (!(apiValue.EndsWith(value1))) 
                                res = true; break;
                case "Contains": 
                            apiValue = apiValue.ToLower();
                            value1 = value1.ToLower();
                            if (apiValue.Contains(value1)) res = true; break;
                case "DoesNotContains": 
                            apiValue = apiValue.ToLower();
                            value1 = value1.ToLower();
                            if (!(apiValue.Contains(value1))) res = true; break;
                case "IsBetween":
                            if (dataType)
                            {
                                v3 = Convert.ToDouble(value1);
                                v4 = Convert.ToDouble(value2);
                                ApiValueDouble = Convert.ToDouble(apiValue);
                                if (ApiValueDouble > v3 && ApiValueDouble < v4)
                                    res = true;
                            }
                            else
                            {
                                try
                                {
                                    DateTime dt1 = Convert.ToDateTime(value1);
                                    DateTime dt2 = Convert.ToDateTime(value2);
                                    DateTime ApiValueDate = Convert.ToDateTime(apiValue);
                                    if (ApiValueDate > dt1 && ApiValueDate < dt2)
                                        res = true;
                                    
                                }
                                catch (Exception Ex)
                                {
                                }
                            }
                             break;
                case "IsNotBetween": 
                            if (dataType)
                            {
                                v3 = Convert.ToDouble(value1);
                                v4 = Convert.ToDouble(value2);
                                ApiValueDouble = Convert.ToDouble(apiValue);
                                if (!(ApiValueDouble > v3 && ApiValueDouble < v4)) 
                                    res = true; 
                            }
                            else
                            {
                                try
                                {
                                    DateTime dt1 = Convert.ToDateTime(value1);
                                    DateTime dt2 = Convert.ToDateTime(value2);
                                    DateTime ApiValueDate = Convert.ToDateTime(apiValue);
                                    if (!(ApiValueDate > dt1 && ApiValueDate < dt2))
                                        res = true;

                                }
                                catch (Exception Ex)
                                {
                                }
                            }
                            break;
                case "IsBlankOrEmpty": 
                            //if (ApiValueInt != v1) res = true; 
                            if (dataType)
                            {
                                // check for numeric
                                try
                                {
                                    v3 = Convert.ToDouble(apiValue);
                                    if (v3 == 0)
                                        res= true;
                                    else
                                        res= false;
                                }
                                catch (Exception Ex)
                                {
                                    res= true;
                                }
                            }
                            else
                                if (apiValue.Trim() == null || apiValue.Trim() == "")
                                    res= true;
                                else
                                    res= false;
                            break;
                case "IsNotBlankOrEmpty": 
                            
                            //if (ApiValueInt != v1) res = true; 
                             if (dataType)
                            {
                                // check for numeric
                                try
                                {
                                    v3 = Convert.ToDouble(apiValue);
                                    if (!(v3 == 0))
                                        res= true;
                                    else
                                        res= false;
                                }
                                catch (Exception Ex)
                                {
                                    res= true;
                                }
                            }
                            else
                                if (!(apiValue.Trim() == null || apiValue.Trim() == ""))
                                    res= true;
                                else
                                    res= false;
                         
                            break;

                default: res = false; break;

            }

            return res;


        }

        /// <summary>
        /// This function used to check the data type is of string type
        /// </summary>
        /// <param name="DataType"></param>
        /// <returns></returns>
        public bool IsStringDataType(String dataType)
        {
            dataType = dataType.ToLower();
            if (dataType == "string")
                return true;
            else
                return false;
        }

        /// <summary>
        /// function to check the data type is of numeric type
        /// </summary>
        /// <param name="DataType"></param>
        /// <returns></returns>
        public bool IsNumericDataType(String dataType)
        {
            dataType = dataType.ToLower();
            if (dataType == "int" || dataType == "float" || dataType=="double")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// This function is used to get api with all the parameters
        /// </summary>
        /// <param name="ApiCall"></param>
        /// <param name="StudentId"></param>
        /// <param name="SchoolId"></param>
        /// <param name="CourseId"></param>
        /// <param name="SectionId"></param>
        /// <returns></returns>
        public String GetApiWithParameter(String apiCall,String studentId, String schoolId, String courseId, String sectionId)
        {
            if (apiCall.Contains("{SchoolId}"))
            {
                apiCall = apiCall.Replace("{SchoolId}", schoolId);
            }

            if (apiCall.Contains("{CourseId}"))
            {
                apiCall = apiCall.Replace("{CourseId}", courseId);
            }

            if (apiCall.Contains("{SectionId}"))
            {
                apiCall = apiCall.Replace("{SectionId}", sectionId);
            }

            if (apiCall.Contains("{StudentId}"))
            {
                apiCall = apiCall.Replace("{StudentId}", studentId);
            }

            return apiCall;

        }

        /// <summary>
        /// function to check the section has students
        /// </summary>
        /// <param name="SectionId"></param>
        /// <returns></returns>
        public bool IsSectionHasStudent(String sectionId)
        {
             JArray StudentSectionResponse = sectionData.GetSectionStudentAssociationStudentList(this._accessToken,sectionId);
               if (StudentSectionResponse.Count() > 0)
               {
                    return true;
               }
               else
               {
                    return false;
               }
        }        

        /// <summary>
        /// This function is to get total absences of student.
        /// </summary>
        /// <param name="StudentId"></param>
        /// <param name="SchoolId"></param>
        /// <returns></returns>
        public int GetStudentAttendenceRecord(String studentId, String schoolId)
        {
            try
            {
                int StudentAbsenceCount = 0;
                //string apiEndPoint = "attendances?studentId="+student_id+"?schoolId="+school_id;
                JArray StudentAbsenceCountResponse = JArray.Parse(RestApiHelper.CallApi("attendances?studentId=" + studentId, this._accessToken));
                for (int Index = 0; Index < StudentAbsenceCountResponse.Count(); Index++)
                {
                    JToken Token = StudentAbsenceCountResponse[Index];
                    
                    String StudentSchoolId = _authenticateUser.GetStringWithoutQuote(Token["schoolId"].ToString());
                    if (StudentSchoolId.Equals(schoolId))
                    {
                        // get the school year attendence for student
                        JArray CourseName = (JArray)Token["schoolYearAttendance"];
                        for (int i = 0; i < CourseName.Count(); i++)
                        {
                            // get the attendence for current year only
                            String SchoolYear = CourseName[i]["schoolYear"].ToString();
                            JArray AttendenceEvent = (JArray)CourseName[i]["attendanceEvent"];
                            if (AttendenceEvent.Count() > 0)
                            {
                                for (int j = 0; j < AttendenceEvent.Count(); j++)
                                {                                                                
                                    if (AttendenceEvent[j]["reason"] != null)
                                    {
                                        StudentAbsenceCount++;
                                        String AbsenseReason = AttendenceEvent[j]["event"].ToString();                                       
                                    }
                                }
                            }
                        }
                    }
                }
                return StudentAbsenceCount;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }


        /// <summary>
        /// This function is used to get the student list by school.
        /// </summary>
        /// <param name="SchoolId"></param>
        /// <returns></returns>
        public DataTable GetStudentListBySchool(String schoolId)
        {
            DataTable _dtStudentList = new DataTable();
            try
            {                
                _dtStudentList.Columns.AddRange(new DataColumn[] { new DataColumn("Student Id", typeof(string)), new DataColumn("Name", typeof(string)), new DataColumn("Ethnicity", typeof(string)), new DataColumn("Gender", typeof(string)), new DataColumn("Grade Level", typeof(string)), new DataColumn("Absences", typeof(int)) });

                JArray StudentListResponse = schoolData.GetSchoolStudentSchoolAssociationStudents(this._accessToken, schoolId);
                for (int Index = 0; Index < StudentListResponse.Count(); Index++)
                {
                    try
                    {
                        JToken Token = StudentListResponse[Index];
                         String StudentId = _authenticateUser.GetStringWithoutQuote(Token["id"].ToString());
                        //int absences1 = GetStudentAttendenceRecord(stud_id, school_id);
                         float GradeLevel = GetStudentGrade(StudentId);

                         
                        _dtStudentList.Rows.Add(new object[] { _authenticateUser.GetStringWithoutQuote(Token["studentUniqueStateId"].ToString()), _authenticateUser.GetStringWithoutQuote(Token["name"]["firstName"].ToString()) + " " + _authenticateUser.GetStringWithoutQuote(Token["name"]["lastSurname"].ToString()), "", _authenticateUser.GetStringWithoutQuote(Token["sex"].ToString()), GradeLevel, null });
                    }
                    catch (Exception ex)
                    {
                    }

                }
            }
            catch (Exception ex)
            {
                // Added blank row in gridview
                _dtStudentList.Rows.Add(new object[] { " ", " ", " ", " ", " ", null });
                return _dtStudentList;
            }
            return _dtStudentList;
        }

        /// <summary>
        ///  This function is used to get the student grade by student id.
        /// </summary>
        /// <param name="StudentId"></param>
        /// <returns></returns>
        public float GetStudentGrade(String studentId)
        {
            float GradeLevel = 0;
            JArray GradeResponse = JArray.Parse(RestApiHelper.CallApi("studentAcademicRecords?studentId=" + studentId, this._accessToken));
            for (int Index = 0; Index < GradeResponse.Count(); Index++)
            {
                try
                {
                    JToken Token = GradeResponse[Index];
                    GradeLevel = (float)Token["cumulativeCreditsAttempted"]["credit"];
                }
                catch (Exception ex)
                {
                    return GradeLevel;
                }
            }
            return GradeLevel;
        }


        /// <summary>
        /// function is used to get th district list
        /// </summary>
        /// <returns></returns>
        public ListItem[] GetDistrict()
       {

           //JArray DistrictResponse = JArray.Parse(RestApiHelper.CallApi("schools", this._accessToken));
           JArray DistrictResponse = schoolData.GetSchools(this._accessToken);
           ListItem[] _listDistrict = new ListItem[DistrictResponse.Count];
           for (int Index = 0; Index < DistrictResponse.Count(); Index++)
           {
               JToken Token = DistrictResponse[Index];
               //JArray SchoolName = (JArray)Token["educationOrgIdentificationCode"];               
               String ParentEducation = _authenticateUser.GetStringWithoutQuote(Token["parentEducationAgencyReference"].ToString());              
               try
               {
                   JObject DistrictOrgResponse = JObject.Parse(RestApiHelper.CallApi("educationOrganizations/" + ParentEducation, this._accessToken));                   
                   String DistrictName = _authenticateUser.GetStringWithoutQuote(DistrictOrgResponse["nameOfInstitution"].ToString());
                   if (!_listDistrict.Contains(new ListItem(DistrictName)))
                   {
                       _listDistrict[Index] = new ListItem(DistrictName);
                   }                   
               }
               catch (Exception ex)
               {
                   return null;
               }              
               
           }
           return _listDistrict;

       }

        /// <summary>
        /// function to get the district list for admin user
        /// </summary>
        /// <returns></returns>
        public ListItem[] GetDistrictForAdmin()
        {
            //JObject DistrictResponse = JObject.Parse(RestApiHelper.CallApi("home", this._accessToken));
           
            
            JObject DistrictResponse1 = (JObject)home.GetHome(this._accessToken)[0];
            //JArrayToJObject
            JObject DistrictResponse = JArrayToJObject(home.GetHome(this._accessToken));
            ListItem[] _listDistrict = null;

            JArray Links = (JArray)DistrictResponse["links"];
            

            for (int Index = 0; Index < Links.Count(); Index++)
            {
                String Relation = _authenticateUser.GetStringWithoutQuote(DistrictResponse["links"][Index]["rel"].ToString());
                if (Relation.Equals("getEducationOrganizations"))
                {
                    String Result = RestApiHelper.CallApiWithParameter(_authenticateUser.GetStringWithoutQuote(DistrictResponse["links"][Index]["href"].ToString()), this._accessToken);
                    JArray DistrictListResponse = JArray.Parse(Result);
                    _listDistrict = new ListItem[DistrictListResponse.Count+1];
                    for (int DistrictIndex = 0; DistrictIndex < DistrictListResponse.Count(); DistrictIndex++)
                    {

                        JToken Token1 = DistrictListResponse[DistrictIndex];
                        String OrganizationCategories = Token1["organizationCategories"].ToString();
                        OrganizationCategories.Trim();
                        OrganizationCategories = _authenticateUser.GetStringWithoutQuote(OrganizationCategories);

                        String Name="";
                        String Id="";
                        Name = _authenticateUser.GetStringWithoutQuote(Token1["nameOfInstitution"].ToString());
                        Id = _authenticateUser.GetStringWithoutQuote(Token1["id"].ToString());

                        if (OrganizationCategories.Contains("Local Education Agency"))
                        {
                            if(DistrictIndex==0)
                            {
                                _listDistrict[DistrictIndex]=new ListItem("Local Education Agency","");
                            }
                            Name = _authenticateUser.GetStringWithoutQuote(Token1["nameOfInstitution"].ToString());
                            Id = _authenticateUser.GetStringWithoutQuote(Token1["id"].ToString());
                        }
                        else

                            if (OrganizationCategories.Contains("School"))
                            {

                                if (DistrictIndex == 0)
                                {
                                    _listDistrict[DistrictIndex] = new ListItem("School", "");
                                }

                               JArray SchoolName = (JArray)Token1["educationOrgIdentificationCode"];
                                Name=_authenticateUser.GetStringWithoutQuote(SchoolName[0]["ID"].ToString());
                                Id = _authenticateUser.GetStringWithoutQuote(Token1["id"].ToString());                             
                              
                            }
                            else
                                if (OrganizationCategories.Contains("State Education Agency"))
                                {
                                    try
                                    {
                                        JArray DistrictResponseLinks = (JArray)Token1["links"];
                                        for (int i = 0; i < DistrictResponseLinks.Count(); i++)
                                        {
                                            String DistrictLink = _authenticateUser.GetStringWithoutQuote(Token1["links"][i]["rel"].ToString());
                                            if (DistrictLink.Equals("getFeederEducationOrganizations"))
                                            {
                                                Result = RestApiHelper.CallApiWithParameter(_authenticateUser.GetStringWithoutQuote(Token1["links"][i]["href"].ToString()), this._accessToken);
                                                JArray _districtReponse = JArray.Parse(Result);
                                                _listDistrict = new ListItem[_districtReponse.Count + 1];
                                                for (int j = 0; j < _districtReponse.Count; j++)
                                                {
                                                    if (j == 0)
                                                    {
                                                        _listDistrict[j] = new ListItem("State Education Agency", "");
                                                    }
                                                    JToken Token2 = _districtReponse[DistrictIndex];
                                                    Name = _authenticateUser.GetStringWithoutQuote(Token2["nameOfInstitution"].ToString());
                                                    Id = _authenticateUser.GetStringWithoutQuote(Token2["id"].ToString());
                                                    _listDistrict[j + 1] = new ListItem(Name, Id);
                                                }

                                                return _listDistrict;
                                                
                                            }
                                        }
                                    }
                                    catch (Exception Ex)
                                    {
                                         //process with get links
                                        return null;
                                    }
                                }
                     
                        _listDistrict[DistrictIndex+1] = new ListItem(Name, Id);
                    }
                    return _listDistrict;
                }
            }
            
            return _listDistrict;
        }


        public ListItem[] GetStaffBySchoolId(String schoolId)
        {
            try
            {
                 JArray SatffResponse = schoolData.GetSchoolTeacherSchoolAssociationTeachers(this._accessToken,schoolId);


                ListItem[] _listStaff = null;
                _listStaff = new ListItem[SatffResponse.Count];
                String StaffName = "";
                String StaffId = "";
                for (int Index = 0; Index < SatffResponse.Count(); Index++)
                {
                    JToken Token = SatffResponse[Index];
                    StaffName = _authenticateUser.GetStringWithoutQuote(Token["name"]["personalTitlePrefix"].ToString()) + " " + _authenticateUser.GetStringWithoutQuote(Token["name"]["firstName"].ToString()) + " " + _authenticateUser.GetStringWithoutQuote(Token["name"]["lastSurname"].ToString());
                    StaffId = _authenticateUser.GetStringWithoutQuote(Token["id"].ToString());
                    _listStaff[Index] = new ListItem(StaffName, StaffId);
                }

                return _listStaff;
            }
            catch (Exception Ex)
            {
                return null;
            }
        }
        
       

        /// <summary>
        /// This function is used to get school list for admin
        /// </summary>
        /// <param name="DistrictId"></param>
        /// <returns></returns>
        public ListItem[] GetSchoolForAdmin(String districtId)
        {

            JArray SchoolResponse = JArray.Parse(RestApiHelper.CallApi("schools?parentEducationAgencyReference=" + districtId, this._accessToken));

            ListItem[] _listSchool = null;

            _listSchool = new ListItem[SchoolResponse.Count];
            for (int SchoolIndex = 0; SchoolIndex < SchoolResponse.Count(); SchoolIndex++)
            {

                JToken Token1 = SchoolResponse[SchoolIndex];
                String Name = _authenticateUser.GetStringWithoutQuote(Token1["nameOfInstitution"].ToString());
                String Id = _authenticateUser.GetStringWithoutQuote(Token1["id"].ToString());
                _listSchool[SchoolIndex] = new ListItem(Name, Id);
            }
            return _listSchool;
           
        }

        /// <summary>
        /// function to get well formatted string with spacing and first letter capital for each word 
        /// </summary>
        /// <param name="FieldName"></param>
        /// <returns></returns>
        public String GetWellFormattedString(String fieldName)
        {
            try
            {
                String Res = "";
                String Res1 = "";

                int MaxIteration = fieldName.Length + 20;
                int Count = 0;

                for (int i = 0; i < fieldName.Count(); i++)
                {

                    if (fieldName[i] >= 'a' && fieldName[i] <= 'z')
                    {
                        Res1 = Res1 + fieldName[i];
                    }
                    else
                        if (fieldName[i] >= '0' && fieldName[i] <= '9')
                            break;
                        else
                        {
                            if (Res1.Count() > 0)
                                fieldName = fieldName.Replace(Res1, "");
                            if (fieldName.Count() == 0 || fieldName == "")
                                break;
                            fieldName = char.ToLower(fieldName[0]) + fieldName.Substring(1);
                            if (Res1.Count() > 0)
                                Res1 = char.ToUpper(Res1[0]) + Res1.Substring(1);
                            Res = Res + "" + Res1 + " ";
                            Res1 = "";
                            i = -1;

                        }
                    Count++;
                    if (Count > MaxIteration)
                        break;
                }
                if (Res == "")
                {
                    return char.ToUpper(Res1[0]) + Res1.Substring(1);
                }
                else
                {
                    return Res + " " + char.ToUpper(Res1[0]) + Res1.Substring(1);
                }
            }
            catch (Exception Ex)
            {
                return fieldName;
            }
        }

        /// <summary>
        /// This function is used to to convert the FlagCustom object to Json object.
        /// </summary>
        /// <param name="flagCustom"></param>
        /// <returns></returns>
        public String FlagObjectToJson(FlagCustom flagCustom)
        {
            return new JavaScriptSerializer().Serialize(flagCustom);
        }

       

        #endregion

        /// <summary>
        /// This function is used to to convert the FlagCustom object to Json object.
        /// </summary>
        /// <param name="flagCustom"></param>
        /// <returns></returns>
        public String FlagObjectToJson(Temp obj)
        {
            return new JavaScriptSerializer().Serialize(obj);
        }

        public Temp FlagObjectDeserialize(String obj)
        {
            return new JavaScriptSerializer().Deserialize<Temp>(obj);
        }

        public String AdminObjectToJson(AdminCls obj)
        {
            return new JavaScriptSerializer().Serialize(obj);
        }

        public AdminCls AdminObjectDeserialize(String obj)
        {
            return new JavaScriptSerializer().Deserialize<AdminCls>(obj);
        }

        public String GetCustomLink(JArray Links)
        {
            try
            {
                //JObject HomeUrlResponse = JObject.Parse(RestApiHelper.CallApi("home", this._accessToken));
                //JArray Links = (JArray)HomeUrlResponse["links"];
                String CustomLink = "";
                for (int Index = 0; Index < Links.Count(); Index++)
                {
                    String Relation = _authenticateUser.GetStringWithoutQuote(Links[Index]["rel"].ToString());
                    if (Relation.Equals("custom"))
                        CustomLink = _authenticateUser.GetStringWithoutQuote(Links[Index]["href"].ToString());

                }
                return CustomLink;
            }
            catch (Exception Ex)
            {
                return null;
            }
        }

        public JArray GetHomeLinks()
        {
            JArray Links = null;
            try
            {
                JObject HomeUrlResponse = JObject.Parse(RestApiHelper.CallApi("home", this._accessToken));
                Links = (JArray)HomeUrlResponse["links"];
                return Links;
            }
            catch (Exception Ex)
            {
                return null;
            }
        }

        public String GetEducationOrganizationId(JArray Links)
        {
            try
            {
                //JObject HomeUrlResponse = JObject.Parse(RestApiHelper.CallApi("home", this._accessToken));
                //JArray Links = (JArray)HomeUrlResponse["links"];
                String EducationOrganization = "";
                for (int Index = 0; Index < Links.Count(); Index++)
                {
                    String Relation = _authenticateUser.GetStringWithoutQuote(Links[Index]["rel"].ToString());
                    if (Relation.Equals("getEducationOrganizations"))
                    {
                        String Result = RestApiHelper.CallApiWithParameter(_authenticateUser.GetStringWithoutQuote(Links[Index]["href"].ToString()), this._accessToken);
                        JArray EducationOrganizationResponse = JArray.Parse(Result);

                        for (int i = 0; i < EducationOrganizationResponse.Count(); i++)
                        {
                            JToken Token = EducationOrganizationResponse[i];
                            EducationOrganization = _authenticateUser.GetStringWithoutQuote(Token["id"].ToString());
                            String OrganizationCategories = Token["organizationCategories"].ToString();
                            if (OrganizationCategories.Contains("State Education Agency"))
                                EducationOrganization = "State Education Agency";

                            break;
                        }
                        return EducationOrganization;
                    }
                }
                return EducationOrganization;
            }
            catch (Exception Ex)
            {
                return null;
            }
        }


        public Temp GetCustomForStaff(String CustomLink)
        {
            try
            {
                String Result = RestApiHelper.CallApiWithParameter(CustomLink, this._accessToken);
                JObject EducationOrganizationResponse = JObject.Parse(Result);
                return FlagObjectDeserialize(EducationOrganizationResponse.ToString());
            }
            catch (Exception Ex)
            {
                return null;
            }
        }

        public void PutCustomForAdmin(String EducationOrganizationId, AdminCls _obj)
        {
            try
            {
                String Result = AdminObjectToJson(_obj);
                RestApiHelper.CallApiForCustomPut("educationOrganizations/" + EducationOrganizationId + "/custom", this.AccessToken, Result);
            }
            catch (Exception Ex)
            {
            }
        }

        public AdminCls GetFlagListForAdminUser(String EducationOrganizationId, String UserId)
        {
            try
            {
                AdminCls _adminCls = null;
                JObject EducationOrganizationResponse = JObject.Parse(RestApiHelper.CallApi("educationOrganizations/" + EducationOrganizationId + "/custom", this._accessToken));
                if (EducationOrganizationResponse == null)
                {
                    // no data in custom
                }
                else
                {
                    _adminCls = AdminObjectDeserialize(EducationOrganizationResponse.ToString());
                }
                return _adminCls;
            }
            catch (Exception Ex)
            {
                //no custom found or some error
                if (Ex.ToString().Contains("(404) Not Found"))
                {
                    // no data present in education organization custom
                }
                return null;

            }
        }

        public Temp[] GetFlagListByEducationOrganization(User _user, String EducationOrganizationId)
        {
            try
            {
                AdminCls _adminCls = null;
                Temp[] _temp = null;
                JObject EducationOrganizationResponse = JObject.Parse(RestApiHelper.CallApi("educationOrganizations/" + EducationOrganizationId + "/custom", this._accessToken));
                if (EducationOrganizationResponse == null)
                {
                    // no data in custom
                }
                else
                {
                    _adminCls = AdminObjectDeserialize(EducationOrganizationResponse.ToString());
                    _temp = _adminCls.AdminList;
                    if (_user != null)
                    {
                        Temp[] _tempForNew = new Temp[1];

                        for (int i = 0; i < _temp.Count(); i++)
                        {
                            if (_temp[i].UserId == _user.ExternalId)
                            {
                                _tempForNew[0] = new Temp();
                                _tempForNew[0] = _temp[i];

                            }
                        }
                        return _tempForNew;
                    }

                }
                return _temp;
            }
            catch (Exception Ex)
            {
                return null;
            }

        }



        public Temp[] GetFlagListOfStaffForAdminUser(User _user, String EducationOrganizationId, ListItem[] _schoolList)
        {
            Temp[] _temp = null;
            try
            {
                for (int i = 0; i < _schoolList.Count(); i++)
                {
                    Temp[] _tempStaff = null;
                    JArray _staffResponse = JArray.Parse(RestApiHelper.CallApi("schools/" + _schoolList[i].Value + "/teacherSchoolAssociations/teachers", this._accessToken));
                    if (_staffResponse != null)
                    {
                        String CustomLink = "";
                        _tempStaff = new Temp[_staffResponse.Count()];
                        for (int Index = 0; Index < _staffResponse.Count(); Index++)
                        {
                            JArray Links = JArray.Parse(_staffResponse[Index]["links"].ToString());
                            for (int j = 0; j < Links.Count; j++)
                            {
                                String Relation = _authenticateUser.GetStringWithoutQuote(Links[j]["rel"].ToString());
                                if (Relation.Equals("custom"))
                                {
                                    CustomLink = _authenticateUser.GetStringWithoutQuote(Links[j]["href"].ToString());
                                    Temp _response = GetCustomForStaff(CustomLink);
                                    _tempStaff[Index] = new Temp();
                                    _tempStaff[Index] = _response;
                                    break;
                                }
                            }

                            if (_temp == null)
                            {
                                _temp = new Temp[_tempStaff.Count()];
                                for (int j = 0; j < _tempStaff.Count(); j++)
                                {
                                    _temp[j] = new Temp();
                                    _temp[j] = _tempStaff[j];
                                }
                            }
                            else
                            {
                                // store _temp array into another
                                Temp[] _forTemp = new Temp[_temp.Count()];
                                for (int j = 0; j < _temp.Count(); j++)
                                {
                                    _forTemp[j] = new Temp();
                                    _forTemp[j] = _temp[j];
                                }

                                // get newly added staff temp
                                _temp = new Temp[_tempStaff.Count() + _temp.Count()];
                                for (int j = 0; j < _tempStaff.Count(); j++)
                                {
                                    _temp[j] = new Temp();
                                    _temp[j] = _tempStaff[j];
                                }

                                // get previous staff
                                int k = 0;
                                for (int j = _tempStaff.Count(); j < _tempStaff.Count() + _forTemp.Count(); j++)
                                {
                                    _temp[j] = new Temp();
                                    _temp[j] = _forTemp[k];
                                    k = k + 1;
                                }
                            }

                        }

                    }
                }

                // check for the blank
                int totalBlank = 0;
                for (int i = 0; i < _temp.Count(); i++)
                {
                    if (_temp[i] == null)
                        totalBlank = totalBlank + 1;
                }

                Temp[] _returnTemp = new Temp[_temp.Count() - totalBlank];
                int p = 0;
                for (int i = 0; i < _temp.Count(); i++)
                {
                    if (_temp[i] != null)
                    {
                        _returnTemp[p] = new Temp();
                        _returnTemp[p] = _temp[i];
                        p++;

                    }
                }

                return _returnTemp;
            }
            catch (InRowChangingEventException Ex)
            {
                return null;
            }
        }



        public void AddFlagsIntoCustom(String Link, User _user, FlagCls[] _flagCls)
        {
            FlagCls[] _forNew = _flagCls;
            AggregateCls[] _forAggregateCls = null;
            try
            {
                Temp _editedFlagByUser = new Temp();
                _editedFlagByUser.FlagList = _flagCls;

                JObject UserCustomResponse = JObject.Parse(RestApiHelper.CallApiWithParameter(Link, this.AccessToken));
                if (UserCustomResponse != null)
                {
                    Temp _list = FlagObjectDeserialize(UserCustomResponse.ToString());
                    if (_list != null)
                    {
                        Temp _temp = new Temp();
                        _forAggregateCls = _list.AggregateFlagList;

                        FlagCls[] _flagClsNew = new FlagCls[_list.FlagList.Count() + 1];
                        for (int i = 0; i < _list.FlagList.Count(); i++)
                        {
                            _flagClsNew[i] = new FlagCls();
                            _flagClsNew[i] = _list.FlagList[i];
                        }
                        _flagClsNew[_list.FlagList.Count()] = new FlagCls();
                        _flagClsNew[_list.FlagList.Count()] = _flagCls[0];

                        _temp.FlagList = _flagClsNew;
                        _temp.AggregateFlagList = _list.AggregateFlagList;
                        _temp.UserId = _user.ExternalId;
                        _temp.IsAdmin = _user.IsAdminUser;

                        String Result = FlagObjectToJson(_temp);

                        RestApiHelper.CallApiWithParameterForCustomPUT(Link, this.AccessToken, Result);
                    }
                }


            }
            catch (Exception Ex)
            {
                // process to add first
                Temp _temp = new Temp();
                _temp.FlagList = _forNew;
                _temp.AggregateFlagList = _forAggregateCls;
                _temp.UserId = _user.ExternalId;
                _temp.IsAdmin = _user.IsAdminUser;
                String Result = FlagObjectToJson(_temp);
                RestApiHelper.CallApiWithParameterForCustomPUT(Link, this.AccessToken, Result);
            }

        }

        public void AddFlagIntoEducationOrganization(User _user, String EducationOrganizationId, FlagCls[] _flagCls)
        {
            try
            {
                AdminCls _adminCls = GetFlagListForAdminUser(EducationOrganizationId, _user.ExternalId);
                Temp _flagListPrivateAdminCustom = null;
                if (_adminCls == null)
                {
                    // no data present in organization
                    Temp[] _temp = new Temp[1];
                    _temp[0] = new Temp();
                    _temp[0].FlagList = _flagCls;
                    _temp[0].UserId = _user.ExternalId;
                    _temp[0].IsAdmin = _user.IsAdminUser;

                    _adminCls = new AdminCls();
                    _adminCls.AdminList = _temp;

                    PutCustomForAdmin(EducationOrganizationId, _adminCls);


                }
                else
                {
                    // data is present need to add new

                    Temp[] _temp = _adminCls.AdminList;
                    bool UserExist = false;
                    int Index = 0;
                    int UserIndex = 0;
                    for (int i = 0; i < _temp.Count(); i++)
                    {
                        if (_temp[i].UserId == _user.ExternalId)
                        {
                            // user record exist 
                            UserExist = true;
                            Index = i;
                            break;
                        }
                    }

                    if (UserExist)
                    {

                        Temp _flagForEdit = _temp[Index];

                        FlagCls[] _flagClsForEdit = _flagForEdit.FlagList;

                        if (_flagClsForEdit != null)
                        {

                            FlagCls[] _flagClsNew = new FlagCls[_flagClsForEdit.Count() + 1];

                            for (int i = 0; i < _flagClsForEdit.Count(); i++)
                            {
                                _flagClsNew[i] = new FlagCls();
                                _flagClsNew[i] = _flagClsForEdit[i];
                            }

                            _flagClsNew[_flagClsForEdit.Count()] = new FlagCls();
                            _flagClsNew[_flagClsForEdit.Count()] = _flagCls[0];

                            _flagForEdit.FlagList = _flagClsNew;
                        }
                        else
                        {
                            _flagForEdit.FlagList = _flagCls;
                        }
                        _temp[Index] = _flagForEdit;
                        _adminCls.AdminList = _temp;

                        PutCustomForAdmin(EducationOrganizationId, _adminCls);

                    }
                    else
                    {
                        // new admin user
                        Temp[] _tempForNew = new Temp[_temp.Count() + 1];
                        for (int i = 0; i < _temp.Count(); i++)
                        {
                            _tempForNew[i] = new Temp();
                            _tempForNew[i] = _temp[i];
                        }

                        Temp _tempAdmin = new Temp();
                        _tempAdmin.IsAdmin = _user.IsAdminUser;
                        _tempAdmin.UserId = _user.ExternalId;
                        _tempAdmin.FlagList = _flagCls;

                        _tempForNew[_tempForNew.Count() - 1] = new Temp();
                        _tempForNew[_tempForNew.Count() - 1] = _tempAdmin;


                        _adminCls.AdminList = _tempForNew;

                        PutCustomForAdmin(EducationOrganizationId, _adminCls);
                    }
                }
            }
            catch (Exception Ex)
            {

            }
        }


        public void DeleteFlagsIntoCustom(String Link, User _user, FlagCls[] _flagCls)
        {
            FlagCls[] _forNew = _flagCls;
            AggregateCls[] _forAggregateCls = null;
            try
            {
                Temp _editedFlagByUser = new Temp();
                _editedFlagByUser.FlagList = _flagCls;

                JObject UserCustomResponse = JObject.Parse(RestApiHelper.CallApiWithParameter(Link, this.AccessToken));
                if (UserCustomResponse != null)
                {
                    Temp _list = FlagObjectDeserialize(UserCustomResponse.ToString());
                    if (_list != null)
                    {
                        Temp _temp = new Temp();
                        _forAggregateCls = _list.AggregateFlagList;


                        if (_list.FlagList.Count() == 1)
                        {
                            // only one need to delete
                            _temp.FlagList = null;
                        }
                        else
                        {
                            FlagCls[] _flagClsNew = new FlagCls[_list.FlagList.Count() - 1];
                            int Count = 0;
                            for (int i = 0; i < _list.FlagList.Count(); i++)
                            {
                                if (_list.FlagList[i].FlagId == _flagCls[0].FlagId)
                                    continue;
                                _flagClsNew[Count] = new FlagCls();
                                _flagClsNew[Count] = _list.FlagList[i];
                                Count++;
                            }

                            _temp.FlagList = _flagClsNew;
                            _temp.AggregateFlagList = _list.AggregateFlagList;
                            _temp.UserId = _user.ExternalId;
                            _temp.IsAdmin = _user.IsAdminUser;
                        }
                        String Result = FlagObjectToJson(_temp);

                        RestApiHelper.CallApiWithParameterForCustomPUT(Link, this.AccessToken, Result);
                    }
                }


            }
            catch (Exception Ex)
            {

            }

        }

        public void DeleteFlagIntoEducationOrganization(User _user, String EducationOrganizationId, FlagCls[] _flagCls)
        {
            try
            {
                AdminCls _adminCls = GetFlagListForAdminUser(EducationOrganizationId, _user.ExternalId);
                Temp _flagListPrivateAdminCustom = null;
                if (_adminCls == null)
                {


                }
                else
                {
                    // data is present need to add new

                    Temp[] _temp = _adminCls.AdminList;
                    bool UserExist = false;
                    int Index = 0;
                    int UserIndex = 0;
                    for (int i = 0; i < _temp.Count(); i++)
                    {
                        if (_temp[i].UserId == _user.ExternalId)
                        {
                            // user record exist 
                            UserExist = true;
                            Index = i;
                            break;
                        }
                    }

                    if (UserExist)
                    {

                        Temp _flagForEdit = _temp[Index];

                        FlagCls[] _flagClsForEdit = _flagForEdit.FlagList;
                        FlagCls[] _flagClsNew = null;

                        if (_flagClsForEdit.Count() == 1)
                        {
                            _flagClsNew = null;
                        }
                        else
                        {
                            int Count = 0;
                            _flagClsNew = new FlagCls[_flagClsForEdit.Count() - 1];

                            for (int i = 0; i < _flagClsForEdit.Count(); i++)
                            {
                                if (_flagClsForEdit[i].FlagId == _flagCls[0].FlagId)
                                    continue;
                                _flagClsNew[Count] = new FlagCls();
                                _flagClsNew[Count] = _flagClsForEdit[i];
                                Count++;
                            }


                        }

                        _flagForEdit.FlagList = _flagClsNew;

                        _temp[Index] = _flagForEdit;
                        _adminCls.AdminList = _temp;

                        PutCustomForAdmin(EducationOrganizationId, _adminCls);

                    }
                    else
                    {

                    }
                }
            }
            catch (Exception Ex)
            {

            }
        }


        public void UpdateFlagsIntoCustom(String Link, User _user, FlagCls[] _flagCls)
        {
            FlagCls[] _forNew = _flagCls;
            AggregateCls[] _forAggregateCls = null;
            try
            {
                Temp _editedFlagByUser = new Temp();
                _editedFlagByUser.FlagList = _flagCls;

                JObject UserCustomResponse = JObject.Parse(RestApiHelper.CallApiWithParameter(Link, this.AccessToken));
                if (UserCustomResponse != null)
                {
                    Temp _list = FlagObjectDeserialize(UserCustomResponse.ToString());
                    if (_list != null)
                    {
                        Temp _temp = new Temp();
                        _forAggregateCls = _list.AggregateFlagList;

                        FlagCls[] _flagClsNew = new FlagCls[_list.FlagList.Count()];
                        for (int i = 0; i < _list.FlagList.Count(); i++)
                        {
                            _flagClsNew[i] = new FlagCls();
                            if (_list.FlagList[i].FlagId == _flagCls[0].FlagId)
                            {
                                _flagClsNew[i] = _flagCls[0];
                            }
                            else
                                _flagClsNew[i] = _list.FlagList[i];
                        }

                        _temp.FlagList = _flagClsNew;
                        _temp.AggregateFlagList = _list.AggregateFlagList;

                        String Result = FlagObjectToJson(_temp);

                        RestApiHelper.CallApiWithParameterForCustomPUT(Link, this.AccessToken, Result);
                    }
                }


            }
            catch (Exception Ex)
            {

            }

        }

        public void UpdateFlagIntoEducationOrganization(User _user, String EducationOrganizationId, FlagCls[] _flagCls)
        {
            try
            {
                AdminCls _adminCls = GetFlagListForAdminUser(EducationOrganizationId, _user.ExternalId);
                Temp _flagListPrivateAdminCustom = null;
                if (_adminCls == null)
                {


                }
                else
                {
                    // data is present need to add new

                    Temp[] _temp = _adminCls.AdminList;
                    bool UserExist = false;
                    int Index = 0;
                    int UserIndex = 0;
                    for (int i = 0; i < _temp.Count(); i++)
                    {
                        if (_temp[i].UserId == _user.ExternalId)
                        {
                            // user record exist 
                            UserExist = true;
                            Index = i;
                            break;
                        }
                    }

                    if (UserExist)
                    {

                        Temp _flagForEdit = _temp[Index];

                        FlagCls[] _flagClsForEdit = _flagForEdit.FlagList;

                        if (_flagClsForEdit != null)
                        {

                            FlagCls[] _flagClsNew = new FlagCls[_flagClsForEdit.Count()];

                            for (int i = 0; i < _flagClsForEdit.Count(); i++)
                            {
                                _flagClsNew[i] = new FlagCls();
                                if (_flagClsForEdit[i].FlagId == _flagCls[0].FlagId)
                                {
                                    _flagClsNew[i] = _flagCls[0];
                                }
                                else
                                    _flagClsNew[i] = _flagClsForEdit[i];
                            }

                            _flagForEdit.FlagList = _flagClsNew;
                        }

                        _temp[Index] = _flagForEdit;
                        _adminCls.AdminList = _temp;

                        PutCustomForAdmin(EducationOrganizationId, _adminCls);

                    }
                    else
                    {

                    }
                }
            }
            catch (Exception Ex)
            {

            }
        }



        public void AddAggregateFlagsIntoCustom(String Link, User _user, AggregateCls[] _aggregateCls)
        {
            AggregateCls[] _forNew = _aggregateCls;
            FlagCls[] _forFlagCls = null;
            try
            {
                Temp _editedFlagByUser = new Temp();
                _editedFlagByUser.AggregateFlagList = _aggregateCls;

                JObject UserCustomResponse = JObject.Parse(RestApiHelper.CallApiWithParameter(Link, this.AccessToken));
                if (UserCustomResponse != null)
                {
                    Temp _list = FlagObjectDeserialize(UserCustomResponse.ToString());
                    if (_list != null)
                    {
                        Temp _temp = new Temp();
                        _forFlagCls = _list.FlagList;
                        AggregateCls[] _flagClsNew = new AggregateCls[_list.AggregateFlagList.Count() + 1];

                        for (int i = 0; i < _list.AggregateFlagList.Count(); i++)
                        {
                            _flagClsNew[i] = new AggregateCls();
                            _flagClsNew[i] = _list.AggregateFlagList[i];
                        }
                        _flagClsNew[_list.AggregateFlagList.Count()] = new AggregateCls();
                        _flagClsNew[_list.AggregateFlagList.Count()] = _aggregateCls[0];

                        _temp.AggregateFlagList = _flagClsNew;
                        _temp.FlagList = _list.FlagList;
                        _temp.UserId = _user.ExternalId;
                        _temp.IsAdmin = _user.IsAdminUser;

                        String Result = FlagObjectToJson(_temp);

                        RestApiHelper.CallApiWithParameterForCustomPUT(Link, this.AccessToken, Result);
                    }
                }


            }
            catch (Exception Ex)
            {
                // process to add first
                Temp _temp = new Temp();
                _temp.AggregateFlagList = _forNew;
                _temp.FlagList = _forFlagCls;
                _temp.UserId = _user.ExternalId;
                _temp.IsAdmin = _user.IsAdminUser;
                String Result = FlagObjectToJson(_temp);
                RestApiHelper.CallApiWithParameterForCustomPUT(Link, this.AccessToken, Result);
            }
        }

        public void AddAggregateFlagIntoEducationOrganization(User _user, String EducationOrganizationId, AggregateCls[] _aggregateFlagCls)
        {
            try
            {
                AdminCls _adminCls = GetFlagListForAdminUser(EducationOrganizationId, _user.ExternalId);
                Temp _flagListPrivateAdminCustom = null;
                if (_adminCls == null)
                {
                    // no data present in organization
                    Temp[] _temp = new Temp[1];
                    _temp[0] = new Temp();
                    _temp[0].AggregateFlagList = _aggregateFlagCls;
                    _temp[0].UserId = _user.ExternalId;
                    _temp[0].IsAdmin = _user.IsAdminUser;

                    _adminCls = new AdminCls();
                    _adminCls.AdminList = _temp;

                    PutCustomForAdmin(EducationOrganizationId, _adminCls);


                }
                else
                {
                    // data is present need to add new

                    Temp[] _temp = _adminCls.AdminList;
                    bool UserExist = false;
                    int Index = 0;
                    int UserIndex = 0;
                    for (int i = 0; i < _temp.Count(); i++)
                    {
                        if (_temp[i].UserId == _user.ExternalId)
                        {
                            // user record exist 
                            UserExist = true;
                            Index = i;
                            break;
                        }
                    }

                    if (UserExist)
                    {

                        Temp _flagForEdit = _temp[Index];

                        AggregateCls[] _flagClsForEdit = _flagForEdit.AggregateFlagList;

                        if (_flagClsForEdit != null)
                        {


                            AggregateCls[] _flagClsNew = new AggregateCls[_flagClsForEdit.Count() + 1];

                            for (int i = 0; i < _flagClsForEdit.Count(); i++)
                            {
                                _flagClsNew[i] = new AggregateCls();
                                _flagClsNew[i] = _flagClsForEdit[i];
                            }

                            _flagClsNew[_flagClsForEdit.Count()] = new AggregateCls();
                            _flagClsNew[_flagClsForEdit.Count()] = _aggregateFlagCls[0];

                            _flagForEdit.AggregateFlagList = _flagClsNew;
                        }
                        else
                        {
                            _flagForEdit.AggregateFlagList = _aggregateFlagCls;
                        }
                        _temp[Index] = _flagForEdit;
                        _adminCls.AdminList = _temp;

                        PutCustomForAdmin(EducationOrganizationId, _adminCls);

                    }
                    else
                    {
                        // new admin user
                        Temp[] _tempForNew = new Temp[_temp.Count() + 1];
                        for (int i = 0; i < _temp.Count(); i++)
                        {
                            _tempForNew[i] = new Temp();
                            _tempForNew[i] = _temp[i];
                        }

                        Temp _tempAdmin = new Temp();
                        _tempAdmin.IsAdmin = _user.IsAdminUser;
                        _tempAdmin.UserId = _user.ExternalId;
                        _tempAdmin.AggregateFlagList = _aggregateFlagCls;

                        _tempForNew[_tempForNew.Count() - 1] = new Temp();
                        _tempForNew[_tempForNew.Count() - 1] = _tempAdmin;


                        _adminCls.AdminList = _tempForNew;

                        PutCustomForAdmin(EducationOrganizationId, _adminCls);
                    }
                }
            }
            catch (Exception Ex)
            {

            }
        }


        public void DeleteAggregateFlagsIntoCustom(String Link, User _user, AggregateCls[] _aggregateCls)
        {
            AggregateCls[] _forNew = _aggregateCls;
            FlagCls[] _forFlagCls = null;
            try
            {
                Temp _editedFlagByUser = new Temp();
                _editedFlagByUser.AggregateFlagList = _aggregateCls;

                JObject UserCustomResponse = JObject.Parse(RestApiHelper.CallApiWithParameter(Link, this.AccessToken));
                if (UserCustomResponse != null)
                {
                    Temp _list = FlagObjectDeserialize(UserCustomResponse.ToString());
                    if (_list != null)
                    {
                        Temp _temp = new Temp();
                        _forFlagCls = _list.FlagList;

                        if (_list.AggregateFlagList.Count() == 1)
                        {
                            _list.AggregateFlagList = null;
                        }
                        else
                        {
                            AggregateCls[] _flagClsNew = new AggregateCls[_list.AggregateFlagList.Count() - 1];
                            int Count = 0;
                            for (int i = 0; i < _list.AggregateFlagList.Count(); i++)
                            {
                                if (_list.AggregateFlagList[i].AggregateFlagId == _aggregateCls[0].AggregateFlagId)
                                    continue;
                                _flagClsNew[Count] = new AggregateCls();
                                _flagClsNew[Count] = _list.AggregateFlagList[i];
                                Count++;
                            }
                            _list.AggregateFlagList = _flagClsNew;

                        }
                        String Result = FlagObjectToJson(_list);

                        RestApiHelper.CallApiWithParameterForCustomPUT(Link, this.AccessToken, Result);
                    }
                }


            }
            catch (Exception Ex)
            {

            }
        }

        public void DeleteAggregateFlagIntoEducationOrganization(User _user, String EducationOrganizationId, AggregateCls[] _aggregateFlagCls)
        {
            try
            {
                AdminCls _adminCls = GetFlagListForAdminUser(EducationOrganizationId, _user.ExternalId);
                Temp _flagListPrivateAdminCustom = null;
                if (_adminCls == null)
                {

                }
                else
                {
                    // data is present need to add new

                    Temp[] _temp = _adminCls.AdminList;
                    bool UserExist = false;
                    int Index = 0;
                    int UserIndex = 0;
                    for (int i = 0; i < _temp.Count(); i++)
                    {
                        if (_temp[i].UserId == _user.ExternalId)
                        {
                            // user record exist 
                            UserExist = true;
                            Index = i;
                            break;
                        }
                    }

                    if (UserExist)
                    {

                        Temp _flagForEdit = _temp[Index];

                        AggregateCls[] _flagClsForEdit = _flagForEdit.AggregateFlagList;


                        if (_flagForEdit.AggregateFlagList.Count() == 1)
                        {
                            _flagForEdit.AggregateFlagList = null;
                        }
                        else
                        {
                            AggregateCls[] _flagClsNew = new AggregateCls[_flagClsForEdit.Count() - 1];
                            int Count = 0;
                            for (int i = 0; i < _flagClsForEdit.Count(); i++)
                            {
                                if (_flagClsForEdit[i].AggregateFlagId == _aggregateFlagCls[0].AggregateFlagId)
                                    continue;
                                _flagClsNew[Count] = new AggregateCls();
                                _flagClsNew[Count] = _flagClsForEdit[i];
                                Count++;
                            }

                            _flagForEdit.AggregateFlagList = _flagClsNew;

                        }

                        _temp[Index] = _flagForEdit;
                        _adminCls.AdminList = _temp;

                        PutCustomForAdmin(EducationOrganizationId, _adminCls);

                    }
                    else
                    {

                    }
                }
            }
            catch (Exception Ex)
            {

            }
        }


        public void UpdateAggregateFlagsIntoCustom(String Link, User _user, AggregateCls[] _aggregateCls)
        {
            AggregateCls[] _forNew = _aggregateCls;
            FlagCls[] _forFlagCls = null;
            try
            {
                Temp _editedFlagByUser = new Temp();
                _editedFlagByUser.AggregateFlagList = _aggregateCls;

                JObject UserCustomResponse = JObject.Parse(RestApiHelper.CallApiWithParameter(Link, this.AccessToken));
                if (UserCustomResponse != null)
                {
                    Temp _list = FlagObjectDeserialize(UserCustomResponse.ToString());
                    if (_list != null)
                    {
                        Temp _temp = new Temp();
                        _forFlagCls = _list.FlagList;
                        AggregateCls[] _flagClsNew = new AggregateCls[_list.AggregateFlagList.Count()];

                        for (int i = 0; i < _list.AggregateFlagList.Count(); i++)
                        {
                            _flagClsNew[i] = new AggregateCls();
                            if (_list.AggregateFlagList[i].AggregateFlagId == _aggregateCls[0].AggregateFlagId)
                            {
                                _flagClsNew[i] = _aggregateCls[0];
                            }
                            else
                            {
                                _flagClsNew[i] = _list.AggregateFlagList[i];
                            }
                        }

                        _temp.AggregateFlagList = _flagClsNew;
                        _temp.FlagList = _list.FlagList;

                        String Result = FlagObjectToJson(_temp);

                        RestApiHelper.CallApiWithParameterForCustomPUT(Link, this.AccessToken, Result);
                    }
                }


            }
            catch (Exception Ex)
            {

            }
        }

        public void UpdateAggregateFlagIntoEducationOrganization(User _user, String EducationOrganizationId, AggregateCls[] _aggregateFlagCls)
        {
            try
            {
                AdminCls _adminCls = GetFlagListForAdminUser(EducationOrganizationId, _user.ExternalId);
                Temp _flagListPrivateAdminCustom = null;
                if (_adminCls == null)
                {


                }
                else
                {
                    // data is present need to add new

                    Temp[] _temp = _adminCls.AdminList;
                    bool UserExist = false;
                    int Index = 0;
                    int UserIndex = 0;
                    for (int i = 0; i < _temp.Count(); i++)
                    {
                        if (_temp[i].UserId == _user.ExternalId)
                        {
                            // user record exist 
                            UserExist = true;
                            Index = i;
                            break;
                        }
                    }

                    if (UserExist)
                    {

                        Temp _flagForEdit = _temp[Index];

                        AggregateCls[] _flagClsForEdit = _flagForEdit.AggregateFlagList;

                        if (_flagClsForEdit != null)
                        {


                            AggregateCls[] _flagClsNew = new AggregateCls[_flagClsForEdit.Count()];

                            for (int i = 0; i < _flagClsForEdit.Count(); i++)
                            {
                                _flagClsNew[i] = new AggregateCls();
                                if (_flagClsForEdit[i].AggregateFlagId == _aggregateFlagCls[0].AggregateFlagId)
                                {
                                    _flagClsNew[i] = _aggregateFlagCls[0];
                                }
                                else
                                {
                                    _flagClsNew[i] = _flagClsForEdit[i];
                                }
                            }

                            _flagForEdit.AggregateFlagList = _flagClsNew;
                        }

                        _temp[Index] = _flagForEdit;
                        _adminCls.AdminList = _temp;

                        PutCustomForAdmin(EducationOrganizationId, _adminCls);

                    }
                    else
                    {

                    }
                }
            }
            catch (Exception Ex)
            {

            }
        }



        public void UpateFlag(int FlagId, String UserCustomLink, String EducationOrganizationId, bool IsAdmin, bool IsPublic, String FlagUserId, FlagCls[] _flagClsUpdated, User _user)
        {

            if (IsAdmin)
            {

                if (IsPublic)
                {
                    // get from orgnization
                    if (_flagClsUpdated[0].IsPublic == false)
                    {
                        // process to delete flag from organization and add to custom of admin
                        //public to private
                        DeleteFlagIntoEducationOrganization(_user, EducationOrganizationId, _flagClsUpdated);

                        AddFlagsIntoCustom(UserCustomLink, _user, _flagClsUpdated);
                    }
                    else
                    {
                        // update at organization
                        UpdateFlagIntoEducationOrganization(_user, EducationOrganizationId, _flagClsUpdated);
                    }
                }
                else
                    if ((!IsPublic) && _flagClsUpdated[0].IsPublic)
                    {
                        // private to public
                        DeleteFlagsIntoCustom(UserCustomLink, _user, _flagClsUpdated);

                        AddFlagIntoEducationOrganization(_user, EducationOrganizationId, _flagClsUpdated);
                    }
                    else
                    {
                        // update at custom
                        UpdateFlagsIntoCustom(UserCustomLink, _user, _flagClsUpdated);
                    }
            }
            else
                UpdateFlagsIntoCustom(UserCustomLink, _user, _flagClsUpdated);
        }

        public void UpateAggregateFlag(int FlagId, String UserCustomLink, String EducationOrganizationId, bool IsAdmin, bool IsPublic, String FlagUserId, AggregateCls[] _aggregateFlagClsUpdated, User _user)
        {

            if (IsAdmin)
            {

                if (IsPublic)
                {
                    // get from orgnization
                    if (_aggregateFlagClsUpdated[0].IsPublic == false)
                    {
                        // process to delete flag from organization and add to custom of admin
                        //public to private
                        DeleteAggregateFlagIntoEducationOrganization(_user, EducationOrganizationId, _aggregateFlagClsUpdated);

                        AddAggregateFlagsIntoCustom(UserCustomLink, _user, _aggregateFlagClsUpdated);
                    }
                    else
                    {
                        // update at organization
                        UpdateAggregateFlagIntoEducationOrganization(_user, EducationOrganizationId, _aggregateFlagClsUpdated);
                    }
                }
                else
                    if ((!IsPublic) && _aggregateFlagClsUpdated[0].IsPublic)
                    {
                        // private to public

                        DeleteAggregateFlagsIntoCustom(UserCustomLink, _user, _aggregateFlagClsUpdated);
                        AddAggregateFlagIntoEducationOrganization(_user, EducationOrganizationId, _aggregateFlagClsUpdated);
                    }
                    else
                    {
                        // update at custom
                        UpdateAggregateFlagsIntoCustom(UserCustomLink, _user, _aggregateFlagClsUpdated);
                    }
            }
            else
                UpdateAggregateFlagsIntoCustom(UserCustomLink, _user, _aggregateFlagClsUpdated);
        }


        public String[] GetAdminIdByEducationOrganizationId(String EducationOrganizationId)
        {
            try
            {
                AdminCls _adminCls = null;
                Temp[] _temp = null;
                String[] AdminUserId = null;
                JObject EducationOrganizationResponse = JObject.Parse(RestApiHelper.CallApi("educationOrganizations/" + EducationOrganizationId + "/custom", this._accessToken));
                if (EducationOrganizationResponse != null)
                {
                    // no data in custom
                    _adminCls = AdminObjectDeserialize(EducationOrganizationResponse.ToString());
                    _temp = _adminCls.AdminList;

                    AdminUserId = new String[_temp.Count()];

                    for (int i = 0; i < _temp.Count(); i++)
                    {
                        AdminUserId[i] = _temp[i].UserId;
                    }

                }
                return AdminUserId;
            }
            catch (Exception Ex)
            {
                return null;
            }
        }








    }
}
