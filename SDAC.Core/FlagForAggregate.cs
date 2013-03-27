using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SDAC.Core
{
    public class FlagForAggregate
    {
        int aggregateFlagId, flagId;
        String createdBy;
        DateTime createdDate;
        String modifiedBy;
        DateTime modifiedDate;

        public int AggregateFlagId
        {
            get
            {
                return aggregateFlagId;
            }
            set
            {
                aggregateFlagId = value;
            }
        }

        public int FlagId
        {
            get
            {
                return flagId;
            }
            set
            {
                flagId = value;
            }
        }

        public String CreatedBy
        {
            get
            {
                return createdBy;
            }
            set
            {
                createdBy = value;
            }
        }

        public String ModifiedBy
        {
            get
            {
                return modifiedBy;
            }
            set
            {
                modifiedBy = value;
            }
        }

        public DateTime CreatedDate
        {
            get
            {
                return createdDate;
            }
            set
            {
                createdDate = value;
            }
        }

        public DateTime ModifiedDate
        {
            get
            {
                return modifiedDate;
            }
            set
            {
                modifiedDate = value;
            }
        }
    }
}