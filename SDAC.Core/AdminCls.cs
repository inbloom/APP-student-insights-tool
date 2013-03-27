using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace SDAC.Core
{
    public class AdminCls
    {
        Temp[] _temp;

        public AdminCls()
        {
            _temp = null;
        }

        public Temp[] AdminList
        {
            get
            {
                return _temp;
            }
            set
            {
                _temp = value;
            }
        }
    }
}