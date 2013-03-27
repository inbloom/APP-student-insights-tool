using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using System.Web;

namespace SDAC.Core
{
    /// <summary>
    /// 
    /// Purpose of School class is to create object and store the list of schools.
    /// Using this class we can set and access the list of schools.
    /// 
    /// </summary>
    public class School
    {

        #region Private Variable        
      
        protected ListItem[] _listSchool;

        #endregion

        #region Constructor      

        public School()
        {
            _listSchool = null;
        }
        public School(ListItem[] listSchool)
        {
            this._listSchool = listSchool;
        }

        #endregion

        #region Public Method

        /// <summary>
        /// function to get the school list
        /// </summary>
        /// <returns></returns>
        public ListItem[] GetSchoolList()
        {
            return _listSchool;
        }


        /// <summary>
        /// function to set the school list
        /// </summary>
        /// <param name="_ListSchool"></param>
        public void SetSchoolList(ListItem[] listSchool)
        {
            this._listSchool = listSchool;
        }

        #endregion
    }
}