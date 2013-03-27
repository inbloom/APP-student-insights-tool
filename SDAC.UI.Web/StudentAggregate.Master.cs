using System;
using System.Linq;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using SDAC.UI.Web.Enums;
using SDAC.DomainModel;
using SDAC.Core;

namespace SDAC.UI.Web
{
    public partial class StudentAggregate : System.Web.UI.MasterPage
    {
        protected inBloomApi _inBloomApi = null;
        protected User _user = null;
        protected SqlHelper _sqlHelper = null;
        protected SLC_SDACEntities _sDACEntities = new SLC_SDACEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session[SessionEnum.AccessToken.ToString()] != null)
                {
                    _inBloomApi = new inBloomApi(Session[SessionEnum.AccessToken.ToString()].ToString());
                    _sqlHelper = new SqlHelper();

                    _user = (User)Session["UserDetail"];
                    //Label lbl = (Label)Master.FindControl("lblUser");
                    lblUser.Text = _user.FullName;

                    _user = _inBloomApi.UserDetails();
                    if (_user == null)
                        Response.Redirect("Search.aspx");
                    else
                        if (Session["UserDetail"] == null)
                        {
                            Session.Add("UserDetail", _user);
                        }

                    if (!IsPostBack)
                    {
                        DropDownListFlag.Items.Clear();
                        GetFlag();

                        if (Session["FlagId"] == null && Session["FlagType"] == null)
                        {
                        }
                        else
                        {
                            String SelectFlag = Session["FlagId"].ToString() + "_" + Session["FlagType"].ToString();
                            //DropDownListSection.SelectedValue = SelectFlag;
                            DropDownListFlag.SelectedValue = SelectFlag;
                        }
                    }

                    if (Session["Success"] != null)
                    {
                        //lblSuccess.Text = Session["Success"].ToString();
                        hdnFieldMessage.Value = Session["Success"].ToString();
                        Session["Success"] = null;

                    }
                    else
                    {
                        hdnFieldMessage.Value = "";
                    }


                    if (!IsPostBack)
                    {


                        if (_user.IsAdminUser || IsLeader())
                        {
                            GetDistrictList();
                        }
                        else
                        {
                            DropDownListDistrict.Visible = false;
                            LabelDistrict.Visible = false;
                        }

                        //|| IsLeader() need to add
                        if (IsEducator())
                        {
                            dropDownListStaff.Visible = false;
                            LabelStaff.Visible = false;
                        }



                        if (Session["School"] == null)
                        {
                            GetSchoolList();
                        }
                        else
                        {
                            try
                            {
                                DropDownListSchool.Items.Clear();
                                School school = (School)Session["School"];
                                DropDownListSchool.Items.AddRange(school.GetSchoolList());
                                DropDownListSchool.SelectedValue = Session[SessionEnum.SchoolId.ToString()].ToString();
                            }
                            catch (Exception Ex)
                            {
                            }
                        }


                        if (Session["Staff"] == null)
                        {
                            if (!IsEducator())
                                GetStaffList();
                        }
                        else
                            if (!IsEducator())
                            {
                                dropDownListStaff.Items.Clear();
                                Staff staff = (Staff)Session["Staff"];
                                dropDownListStaff.Items.AddRange(staff.GetStaffList());
                                dropDownListStaff.SelectedValue = Session[SessionEnum.StaffID.ToString()].ToString();
                            }



                        if (Session["Course"] == null)
                        {
                            GetCourseListNew();
                            //GetCourseList();
                        }
                        else
                        {
                            try
                            {
                                Course course = (Course)Session["Course"];
                                ListItem[] _courseList = course.GetCourseList();
                                //DropDownListCourse.Items.AddRange(course.GetCourseList());
                                for (int i = 0; i < _courseList.Count(); i++)
                                {
                                    if (_courseList[i] != null)
                                        DropDownListCourse.Items.Add(_courseList[i]);
                                }
                                DropDownListCourse.SelectedValue = Session[SessionEnum.CourseId.ToString()].ToString();
                            }
                            catch (Exception Ex)
                            {
                            }
                        }

                        if (Session["Section"] == null)
                        {
                            GetSectionListNew();
                            //GetSectionList();
                        }
                        else
                        {
                            try
                            {
                                Section section = (Section)Session["Section"];
                                ListItem[] _listSection = section.GetSectionList();
                                if (_listSection != null)
                                {
                                    for (int Index = 0; Index < _listSection.Length; Index++)
                                    {
                                        if (_listSection[Index] != null)
                                        {
                                            DropDownListSection.Items.Add(new ListItem(_listSection[Index].Text, _listSection[Index].Value.ToString()));
                                        }
                                    }

                                }

                                DropDownListSection.SelectedValue = Session[SessionEnum.SectionId.ToString()].ToString();
                            }
                            catch (Exception Ex)
                            {
                            }
                        }



                        try
                        {
                            Session.Add(SessionEnum.SchoolId.ToString(), DropDownListSchool.SelectedValue);
                            Session.Add(SessionEnum.CourseId.ToString(), DropDownListCourse.SelectedValue);
                            Session.Add(SessionEnum.SectionId.ToString(), DropDownListSection.SelectedValue);
                            Session.Add(SessionEnum.StaffID.ToString(), dropDownListStaff.SelectedValue);
                        }
                        catch (Exception Ex)
                        {
                        }

                    }

                }
                else
                {
                    Response.Redirect("Search.aspx");
                }
            }
            catch (Exception ex)
            {
                throw;
                //Response.Redirect("Search.aspx");
            }
        }
        public void GetSchoolList()
        {
            DropDownListSchool.Items.Clear();
            try
            {
                ListItem[] _listSchool = null;
                if (_user.IsAdminUser || IsLeader())
                {
                    String DistrictId = DropDownListDistrict.SelectedValue.ToString();
                    _listSchool = _inBloomApi.GetSchoolForAdmin(DistrictId);
                }
                else
                {
                    _listSchool = _inBloomApi.GetSchool();
                }

                if (_listSchool != null)
                {
                    for (int Index = 0; Index < _listSchool.Length; Index++)
                    {
                        if (_listSchool[Index] == null)
                        {
                        }
                        else
                        {
                            DropDownListSchool.Items.Add(_listSchool[Index]);
                        }
                    }


                }

                if (DropDownListSchool.Items.Count > 0)
                {
                    School school = new School(_listSchool);
                    Session["School"] = school;
                }
                else
                {
                    Session["School"] = null;
                }

            }
            catch (Exception ex)
            {
            }
        }


        public void GetStaffList()
        {
            try
            {
                dropDownListStaff.Items.Clear();
                ListItem[] _listStaff = _inBloomApi.GetStaffBySchoolId(DropDownListSchool.SelectedItem.Value);
                if (_listStaff != null)
                {
                    for (int i = 0; i < _listStaff.Count(); i++)
                    {
                        dropDownListStaff.Items.Add(_listStaff[i]);
                    }
                    Staff staff = new Staff(_listStaff);
                    Session["Staff"] = staff;
                }
                else
                {
                    Session["Staff"] = null;
                }

            }
            catch (Exception Ex)
            {
            }
        }



        public void GetDistrictList()
        {
            try
            {


                ListItem[] _listDistrict = _inBloomApi.GetDistrictForAdmin();
                DropDownListDistrict.Items.Clear();
                if (_listDistrict != null)
                {
                    String CategoryName = _listDistrict[0].Text;
                    if (CategoryName == "Local Education Agency")
                    {
                        // hide district drop down
                        for (int Index = 1; Index < _listDistrict.Length; Index++)
                        {
                            if (_listDistrict[Index] == null)
                            {
                            }
                            else
                            {
                                DropDownListDistrict.Items.Add(_listDistrict[Index]);
                            }
                        }
                        DropDownListDistrict.Visible = false;
                        LabelDistrict.Visible = false;
                    }
                    else
                        if (CategoryName == "School")
                        {
                            // hide district drop down
                            // add the result into the school dropdown with session
                            ListItem[] _listSchool = new ListItem[_listDistrict.Count() - 1];
                            DropDownListSchool.Items.Clear();
                            for (int Index = 1; Index < _listDistrict.Length; Index++)
                            {
                                if (_listDistrict[Index] == null)
                                {
                                }
                                else
                                {
                                    DropDownListSchool.Items.Add(_listDistrict[Index]);
                                    _listSchool[Index - 1] = new ListItem(_listDistrict[Index].Text, _listDistrict[Index].Value);
                                }
                            }

                            if (DropDownListSchool.Items.Count > 0)
                            {
                                School school = new School(_listSchool);
                                Session["School"] = school;
                            }
                            else
                            {
                                Session["School"] = null;
                            }
                            DropDownListDistrict.Visible = false;
                            LabelDistrict.Visible = false;
                        }
                        else
                            if (CategoryName == "State Education Agency")
                            {
                                // show the district drop down
                                // load all the district into the dropdown
                                for (int Index = 1; Index < _listDistrict.Length; Index++)
                                {
                                    if (_listDistrict[Index] == null)
                                    {
                                    }
                                    else
                                    {
                                        DropDownListDistrict.Items.Add(_listDistrict[Index]);
                                    }
                                }
                            }



                    //DropDownListDistrict.SelectedValue = "Daybreak School District 4529";
                }


            }
            catch (Exception ex)
            {


            }

        }


        public void GetCourseListNew()
        {
            try
            {
                _user = (User)Session["UserDetail"];
                DropDownListCourse.Items.Clear();
                ListItem[] _sectionList = null;
                if (_user.IsAdminUser || IsLeader())
                {
                    _sectionList = _inBloomApi.GetSectionByStaffIdAndSchoolId(dropDownListStaff.SelectedItem.Value, DropDownListSchool.SelectedItem.Value);
                }
                else
                {
                    _sectionList = _inBloomApi.GetSectionListForLoginUser();
                }
                ListItem[] _sectionAndCourseOfferingId = _inBloomApi.GetSectionAndCourseOfferingIdList();
                ListItem[] _temp = new ListItem[_sectionAndCourseOfferingId.Count()];

                ListItem[] _courseAndSectionIdTemp = new ListItem[_sectionAndCourseOfferingId.Count()];
                ListItem[] _sectionAndCourseIdTemp = new ListItem[_sectionAndCourseOfferingId.Count()];

                for (int i = 0; i < _sectionAndCourseOfferingId.Count(); i++)
                {
                    ListItem _course = _inBloomApi.GetCourseBySectionCourseOfferingId(_sectionAndCourseOfferingId[i].Value);
                    bool IsPresent = false;
                    for (int j = 0; j < _temp.Count(); j++)
                    {
                        if (_temp[j] != null)
                        {
                            if (_temp[j].Value == _course.Value)
                            {
                                IsPresent = true;
                                break;
                            }

                        }

                    }
                    if (!IsPresent)
                    {
                        _temp[i] = new ListItem(_course.Text, _course.Value);

                    }
                    _courseAndSectionIdTemp[i] = new ListItem(_course.Value, _sectionList[i].Value);
                    _sectionAndCourseIdTemp[i] = new ListItem(_sectionList[i].Value, _course.Value);
                }


                for (int i = 0; i < _temp.Count(); i++)
                {
                    if (_temp[i] != null)
                    {
                        DropDownListCourse.Items.Add(_temp[i]);
                    }
                }
                //DropDownListCourse.Items.AddRange(_temp);

                Course course = new Course(_temp);
                Session["Course"] = course;
                Session["CourseAndSectionIdTemp"] = _courseAndSectionIdTemp;
                Session["SectionAndCourseIdTemp"] = _sectionAndCourseIdTemp;

            }
            catch (Exception Ex)
            {
            }
        }


        public void GetCourseList()
        {
            DropDownListCourse.Items.Clear();
            try
            {
                ListItem[] _listCourse = _inBloomApi.GetCourseBySchool(DropDownListSchool.SelectedItem.Value.ToString());
                _user = (User)Session["UserDetail"];

                if (_user.IsAdminUser || IsLeader())
                {
                    _inBloomApi.GetSectionByStaffIdAndSchoolId(dropDownListStaff.SelectedItem.Value, DropDownListSchool.SelectedItem.Value);

                    //_sectionAndCourseOfferingId=> sectionid and courseofferingid
                    ListItem[] _sectionAndCourseOfferingId = _inBloomApi.GetSectionAndCourseOfferingIdList();
                    ListItem[] _temp = new ListItem[_sectionAndCourseOfferingId.Count()];
                    ListItem[] _courseIdAndCourseOfferingId = new ListItem[_sectionAndCourseOfferingId.Count()];


                    for (int i = 0; i < _sectionAndCourseOfferingId.Count(); i++)
                    {
                        ListItem _course = _inBloomApi.GetCourseBySectionCourseOfferingId(_sectionAndCourseOfferingId[i].Value);
                        _temp[i] = new ListItem(_course.Text, _course.Value);
                        _courseIdAndCourseOfferingId[i] = new ListItem(_course.Value, _sectionAndCourseOfferingId[i].Value);

                    }

                    DropDownListCourse.Items.AddRange(_temp);
                    Course course = new Course(_temp);
                    Session["Course"] = course;
                    Session["SectionAndCourseOfferingId"] = _sectionAndCourseOfferingId;
                    Session["CourseIdAndCourseOfferingId"] = _courseIdAndCourseOfferingId;

                }
                else
                {

                    if (!IsEducator())
                    {
                        _inBloomApi.GetSectionByStaffIdAndSchoolId(dropDownListStaff.SelectedItem.Value, DropDownListSchool.SelectedItem.Value);
                    }
                    else
                    {
                        _inBloomApi.GetSectionListForLoginUser();
                    }
                    ListItem[] _sectionAndCourseOfferingId = _inBloomApi.GetSectionAndCourseOfferingIdList();
                    ListItem[] _temp = new ListItem[_sectionAndCourseOfferingId.Count()];
                    ListItem[] _courseIdAndCourseOfferingId = new ListItem[_sectionAndCourseOfferingId.Count()];
                    if (_listCourse != null)
                    {

                        for (int i = 0; i < _sectionAndCourseOfferingId.Count(); i++)
                        {
                            ListItem _course = _inBloomApi.GetCourseBySectionCourseOfferingId(_sectionAndCourseOfferingId[i].Value);

                            // check more than one course
                            bool IsCoursePresent = false;
                            for (int j = 0; j < _temp.Count(); j++)
                            {
                                if (_temp[j] != null)
                                {
                                    if (_temp[j].Value == _course.Value)
                                    {
                                        IsCoursePresent = true;
                                        break;
                                    }
                                }
                            }

                            if (IsCoursePresent)
                            {
                            }
                            else
                            {
                                _temp[i] = new ListItem(_course.Text, _course.Value);
                            }
                            _courseIdAndCourseOfferingId[i] = new ListItem(_course.Value, _sectionAndCourseOfferingId[i].Value);

                            //for (int Index = 0; Index < _listCourse.Count(); Index++)
                            //{                               
                            //    bool Find = _inBloomApi.IsSectionContainOfferingOfThisCourse(_sectionAndCourseOfferingId[i].Value, _listCourse[Index].Value);
                            //    if (Find)
                            //    {
                            //        _temp[i] = new ListItem(_listCourse[Index].Text, _listCourse[Index].Value);
                            //        _courseIdAndCourseOfferingId[i] = new ListItem(_listCourse[Index].Value, _sectionAndCourseOfferingId[i].Value);
                            //        break;
                            //    }
                            //}
                        }



                        //DropDownListCourse.Items.AddRange(_listCourse);
                        //Course course = new Course(_listCourse);
                        //Session["Course"] = course;

                        for (int i = 0; i < _temp.Count(); i++)
                        {
                            if (_temp[i] != null)
                                DropDownListCourse.Items.Add(_temp[i]);
                        }

                        // DropDownListCourse.Items.AddRange(_temp);
                        Course course = new Course(_temp);
                        Session["Course"] = course;
                        Session["SectionAndCourseOfferingId"] = _sectionAndCourseOfferingId;
                        Session["CourseIdAndCourseOfferingId"] = _courseIdAndCourseOfferingId;
                    }
                    else
                    {
                        Session["Course"] = null;
                    }

                }



            }
            catch (Exception ex)
            {
            }

        }

        public bool IsEducator()
        {
            _user = (User)Session["UserDetail"];
            String[] _sliRoles = _user.SliRoles;
            for (int i = 0; i < _sliRoles.Count(); i++)
            {
                if (_sliRoles[i] == "Educator")
                {
                    return true;
                }
            }
            return false;
        }

        public bool IsLeader()
        {
            _user = (User)Session["UserDetail"];
            String[] _sliRoles = _user.SliRoles;
            for (int i = 0; i < _sliRoles.Count(); i++)
            {
                if (_sliRoles[i] == "Leader")
                {
                    return true;
                }
            }
            return false;
        }


        public bool IsITAdministrator()
        {
            _user = (User)Session["UserDetail"];
            String[] _sliRoles = _user.SliRoles;
            for (int i = 0; i < _sliRoles.Count(); i++)
            {
                if (_sliRoles[i] == "IT Administrator")
                {
                    return true;
                }
            }
            return false;
        }
        public void GetSectionListNew()
        {
            try
            {
                _user = (User)Session["UserDetail"];
                DropDownListSection.Items.Clear();
                if (_user.IsAdminUser || IsLeader() || IsEducator())
                {
                    // _courseAndSectionIdTemp course id and section id
                    ListItem[] _courseAndSectionIdTemp = (ListItem[])Session["CourseAndSectionIdTemp"];

                    // _sectionAndCourseIdTemp section id and course id
                    ListItem[] _sectionAndCourseIdTemp = (ListItem[])Session["SectionAndCourseIdTemp"];
                    ListItem[] _temp = new ListItem[_courseAndSectionIdTemp.Count()];


                    String selectedCourseId = DropDownListCourse.SelectedItem.Value;
                    int count = 0;
                    for (int i = 0; i < _sectionAndCourseIdTemp.Count(); i++)
                    {
                        if (selectedCourseId == _sectionAndCourseIdTemp[i].Value)
                        {
                            ListItem _section = _inBloomApi.GetSectionById(_sectionAndCourseIdTemp[i].Text);
                            _temp[count] = new ListItem(_section.Text, _section.Value);
                            DropDownListSection.Items.Add(_section);
                        }
                    }

                    Section section = new Section(_temp);
                    Session["Section"] = section;


                }

            }
            catch (Exception Ex)
            {

            }
        }


        public void GetSectionList()
        {
            DropDownListSection.Items.Clear();
            try
            {
                _user = (User)Session["UserDetail"];
                if (_user.IsAdminUser)
                {
                    String CourseName = DropDownListCourse.SelectedItem.Value;
                    ListItem[] _sectionList = _inBloomApi.GetSectionForAdmin(DropDownListSchool.SelectedItem.Value.ToString(), CourseName);

                    if (_sectionList != null)
                    {
                        for (int i = 0; i < _sectionList.Count(); i++)
                        {
                            if (_sectionList[i] != null)
                            {
                                DropDownListSection.Items.Add(new ListItem(_sectionList[i].Text, _sectionList[i].Value.ToString()));
                            }
                        }
                    }

                    ListItem[] _temp = new ListItem[DropDownListSection.Items.Count];
                    for (int i = 0; i < DropDownListSection.Items.Count; i++)
                    {
                        _temp[i] = new ListItem(DropDownListSection.Items[i].Text, DropDownListSection.Items[i].Value);
                    }
                    Section section = new Section(_temp);
                    Session["Section"] = section;

                }
                else
                {
                    ListItem[] _courseIdAndCourseOfferingId = (ListItem[])Session["CourseIdAndCourseOfferingId"];
                    String SelectedCourseId = DropDownListCourse.SelectedItem.Value;
                    String CourseOfferingId = "";
                    for (int i = 0; i < _courseIdAndCourseOfferingId.Count(); i++)
                    {
                        if (_courseIdAndCourseOfferingId[i].Text == SelectedCourseId)
                        {
                            CourseOfferingId = _courseIdAndCourseOfferingId[i].Value;
                        }
                    }

                    ListItem[] _listSection = _inBloomApi.GetSection(DropDownListSchool.SelectedItem.Value.ToString(), CourseOfferingId);



                    if (_listSection != null)
                    {
                        ListItem[] _sectionAndCourseOfferingId = (ListItem[])Session["SectionAndCourseOfferingId"];
                        ListItem[] _temp = new ListItem[_sectionAndCourseOfferingId.Count()];
                        for (int Index = 0; Index < _listSection.Length; Index++)
                        {
                            if (_listSection[Index] != null)
                            {
                                for (int i = 0; i < _sectionAndCourseOfferingId.Count(); i++)
                                {
                                    if (_sectionAndCourseOfferingId[i].Text == _listSection[Index].Value.ToString())
                                    {
                                        DropDownListSection.Items.Add(new ListItem(_listSection[Index].Text, _listSection[Index].Value.ToString()));
                                        _temp[i] = new ListItem(_listSection[Index].Text, _listSection[Index].Value.ToString());
                                    }
                                }

                            }




                        }

                        //ListItem[] _temp = new ListItem[_listSection.Count()];
                        //int count = 0;
                        //for (int i = 0; i < _listSection.Count(); i++)
                        //{
                        //    if (_listSection[i] != null)
                        //    {
                        //        _temp[count] = new ListItem(_listSection[i].Text, _listSection[i].Value);
                        //        count = count + 1;
                        //    }
                        //}

                        //Section section = new Section(_listSection);
                        //Session["Section"] = section;
                        Section section = new Section(_temp);
                        Session["Section"] = section;
                    }
                    else
                    {
                        Session["Section"] = null;
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        public void GetFlag()
        {
            try
            {

                _user = (User)Session["UserDetail"];
                ListItem[] _FlagList = _sqlHelper.GetFlagListByCategory(_user.ExternalId);

                String EducationOrganizationId = Session["EducationOrganizationId"].ToString();
                String[] AdminUserId = _inBloomApi.GetAdminIdByEducationOrganizationId(EducationOrganizationId);



                IList<Flag> _flagListTemp = null;
                IList<Flag> _flagListPublic = null;

                IList<AggregateFlag> _aggregateFlagListTemp = null;
                IList<AggregateFlag> _aggregateFlagListPublic = null;

                if (AdminUserId != null)
                {

                    for (int i = 0; i < AdminUserId.Count(); i++)
                    {
                        _flagListTemp = _sqlHelper.GetPublicFlagListForAdmin(AdminUserId[i]);
                        if (_flagListPublic == null)
                        {
                            _flagListPublic = _flagListTemp;
                        }
                        else
                        {
                            for (int j = 0; j < _flagListTemp.Count; j++)
                            {
                                _flagListPublic.Add(_flagListTemp[j]);
                            }

                        }
                    }




                    for (int i = 0; i < AdminUserId.Count(); i++)
                    {
                        _aggregateFlagListTemp = _sqlHelper.GetAllAggregatePublicFlagByUserId(AdminUserId[i]);
                        if (_aggregateFlagListPublic == null)
                        {
                            _aggregateFlagListPublic = _aggregateFlagListTemp;
                        }
                        else
                        {
                            for (int j = 0; j < _aggregateFlagListTemp.Count; j++)
                            {
                                _aggregateFlagListPublic.Add(_aggregateFlagListTemp[j]);
                            }
                        }

                    }


                }



                if (_FlagList != null)
                {
                    bool FavStart = false;
                    for (int Index = 0; Index < _FlagList.Length; Index++)
                    {
                        if (_FlagList[Index] == null)
                        {
                        }
                        else
                        {
                            if (_FlagList[Index].Text == "Public")
                            {

                                _FlagList[Index].Attributes.Add("class", "abc");
                                DropDownListFlag.Items.Add(_FlagList[Index]);

                                try
                                {
                                    for (int j = 0; j < _flagListPublic.Count(); j++)
                                    {
                                        DropDownListFlag.Items.Add(new ListItem(_flagListPublic[j].FlagName, _flagListPublic[j].FlagId + "Flag"));

                                    }

                                }
                                catch (Exception Ex)
                                {
                                }

                                try
                                {
                                    for (int j = 0; j < _aggregateFlagListPublic.Count(); j++)
                                    {
                                        DropDownListFlag.Items.Add(new ListItem(_aggregateFlagListPublic[j].AggregateFlagName, _aggregateFlagListPublic[j].AggregateFlagId + "_AggregateFlag"));

                                    }
                                }
                                catch (Exception Ex)
                                {
                                }

                            }
                            else
                            {
                                if (_FlagList[Index].Text == "Favorite" || _FlagList[Index].Text == "Recent Flag")
                                {

                                    _FlagList[Index].Attributes.Add("class", "abc");
                                    //_FlagList[Index].Attributes.CssStyle.Add("color", "black");
                                    //_FlagList[Index].Attributes.CssStyle.Add("font-weight", "bold");
                                    FavStart = true;
                                }
                                if (FavStart)
                                    DropDownListFlag.Items.Add(_FlagList[Index]);
                            }
                        }
                    }
                    //DropDownListFlag.Items.AddRange(_FlagList);
                }



            }
            catch (Exception Ex)
            {
            }
        }

        protected void DropDownListSchool_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownListCourse.Items.Clear();


            if (!IsEducator())
            {
                GetStaffList();
                Session.Add(SessionEnum.StaffID.ToString(), dropDownListStaff.SelectedValue);
            }


            GetCourseListNew();
            GetSectionListNew();


            //GetCourseList();
            //GetSectionList();
            Session.Add(SessionEnum.SchoolId.ToString(), DropDownListSchool.SelectedValue);
            Session.Add(SessionEnum.CourseId.ToString(), DropDownListCourse.SelectedValue);
            Session.Add(SessionEnum.SectionId.ToString(), DropDownListSection.SelectedValue);



        }

        protected void DropDownListCourse_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownListSection.Items.Clear();
            GetSectionListNew();
            //GetSectionList();
            Session.Add(SessionEnum.CourseId.ToString(), DropDownListCourse.SelectedValue);
            Session.Add(SessionEnum.SectionId.ToString(), DropDownListSection.SelectedValue);

        }
        protected void DropDownListSection_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session.Add(SessionEnum.SectionId.ToString(), DropDownListSection.SelectedValue);
        }
        protected void btnGo_Click(object sender, EventArgs e)
        {
            Session.Add(SessionEnum.SchoolId.ToString(), DropDownListSchool.SelectedValue);
            Session.Add(SessionEnum.CourseId.ToString(), DropDownListCourse.SelectedValue);
            Session.Add(SessionEnum.SectionId.ToString(), DropDownListSection.SelectedValue);
            try
            {
                String SelectFlag = DropDownListFlag.SelectedValue;
                String[] Items = SelectFlag.Split('_');
                int FlagId = Convert.ToInt16(Items[0]);
                String FlagType = Items[1];
                if (SelectFlag.Contains("Flag"))
                {

                    Session.Add("FlagId", FlagId);
                    Session.Add("FlagType", FlagType);
                    Response.Redirect("Result.aspx");
                }
                else
                    if (SelectFlag.Contains("AggregateFlag"))
                    {
                        Session.Add("FlagId", FlagId);
                        Session.Add("FlagType", FlagType);
                        Response.Redirect("Result.aspx");
                    }
            }
            catch (Exception Ex)
            {
            }
        }

        protected void dropDownListStaff_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (IsEducator() || IsLeader())
            {

                GetCourseListNew();
                GetSectionListNew();

                //GetCourseList();
                //GetSectionList();

                Session.Add(SessionEnum.StaffID.ToString(), dropDownListStaff.SelectedValue);
                Session.Add(SessionEnum.CourseId.ToString(), DropDownListCourse.SelectedValue);
                Session.Add(SessionEnum.SectionId.ToString(), DropDownListSection.SelectedValue);
            }
        }
    }
}
