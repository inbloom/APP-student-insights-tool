using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using SDAC.DomainModel;

namespace SDAC.Core
{
    public class Temp
    {
        bool isAdmin;
        String userId;
        //Flag[] _flagList;
        AggregateCls[] _aggregateFlagList;

        FlagCls[] _flagList;

        public Temp()
        {
            isAdmin = false;
            _flagList = null;
            _aggregateFlagList = null;
        }

       


        public bool IsAdmin
        {
            get
            {
                return isAdmin;
            }
            set
            {
                isAdmin = value;
            }
        }

        public String UserId
        {
            get
            {
                return userId;
            }
            set
            {
                userId = value;
            }
        }

        public FlagCls[] FlagList
        {
            get
            {
                return _flagList;
            }
            set
            {
                _flagList = value;
            }
        }       

        public AggregateCls[] AggregateFlagList
        {
            get
            {
                return _aggregateFlagList;
            }
            set
            {
                _aggregateFlagList = value;
            }
        }


    }
}