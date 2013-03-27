using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SDAC.Core
{
    public class AggregateCls
    {
        int aggregateFlagId;
        String aggregateFlagName, aggregateFlagDescription, keyword;
        bool isPublic, isFavorite;
        String userId;
        bool isDelete;
        String createdBy;
        DateTime createdDate;
        String modifiedBy;
        DateTime modifiedDate;
       
        FlagForAggregate[] _flagForAggregate;

        public bool IsPublic
        {
            get
            {
                return isPublic;
            }
            set
            {
                isPublic = value;
            }
        }

       

        public bool IsFavorite
        {
            get
            {
                return isFavorite;
            }
            set
            {
                isFavorite = value;
            }
        }

        public bool IsDeleted
        {
            get
            {
                return isDelete;
            }
            set
            {
                isDelete = value;
            }
        }

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

        public String AggregateFlagName
        {
            get
            {
                return aggregateFlagName;
            }
            set
            {
                aggregateFlagName = value;
            }
        }

        public String AggregateFlagDescription
        {
            get
            {
                return aggregateFlagDescription;
            }
            set
            {
                aggregateFlagDescription = value;
            }
        }

        public String Keyword
        {
            get
            {
                return keyword;
            }
            set
            {
                keyword = value;
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

        public FlagForAggregate[] FlagForAggregate
        {
            get
            {
                return _flagForAggregate;
            }
            set
            {
                _flagForAggregate = value;
            }
        }
    }
}