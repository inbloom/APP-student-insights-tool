using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using System.Web;

namespace SDAC.Core
{
    public class Staff
    {

        #region Private Variable

        protected ListItem[] _listStaff;

        #endregion

        #region Constructor

        public Staff()
        {
            _listStaff = null;
        }

        public Staff(ListItem[] listStaff)
        {
            this._listStaff = listStaff;
        }

        #endregion

        #region Public Method

        public ListItem[] GetStaffList()
        {
            return _listStaff;
        }

        public void SetStaffList(ListItem[] listStaff)
        {
            this._listStaff = listStaff;
        }

        #endregion
    }
}