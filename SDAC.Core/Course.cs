using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using System.Web;

namespace SDAC.Core
{
    /// <summary>
    /// 
    /// Purpose of Course class is to create object and store the list of courses.
    /// Using this class we can set and access the list of course.
    /// 
    /// </summary>
    public class Course
    {

        #region Private Variables

        protected ListItem[] _listCourse;

        #endregion

        #region Constructor

        public Course()
        {
            _listCourse = null;
        }
   
        public Course(ListItem[] listCourse)
        {
            this._listCourse = listCourse;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// This function is used to get the course list
        /// </summary>
        /// <returns></returns>
        public ListItem[] GetCourseList()
        {
            return _listCourse;
        }

        /// <summary>
        /// This function it used to assign the course list to current object
        /// </summary>
        /// <param name="_ListCourse"></param>
        public void SetCourseList(ListItem[] listCourse)
        {
            this._listCourse = listCourse;
        }

        #endregion
    }
}