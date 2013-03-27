using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SDAC.Core
{
    public class FlagCustom
    {
        #region Private Variables

        protected int flagId;
        protected String flagName;
        protected String flagDescription;
        protected String flagKeyword;
        protected bool isPublic;
        protected int dataElementId;
        protected int conditionId;
        protected String valueSet1;
        protected String valueSet2;
        protected String userId;
        protected bool isFavorite;
        protected bool isDeleted;
        protected String createdBy;
        protected DateTime createdDate;
        protected String modifiedBy;
        protected DateTime modifiedDate;

        #endregion

        #region Constructor

        public FlagCustom()
        {

        }

        #endregion

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
                return isDeleted;
            }
            set
            {
                isDeleted = value;
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

        public int DataElementId
        {
            get
            {
                return dataElementId;
            }
            set
            {
                dataElementId = value;
            }
        }

        public int ConditionId
        {
            get
            {
                return conditionId;
            }
            set
            {
                conditionId = value;
            }
        }

        public String FlagName
        {
            get
            {
                return flagName;
            }
            set
            {
                flagName = value;
            }
        }

        public String FlagDescription
        {
            get
            {
                return flagDescription;
            }
            set
            {
                flagDescription = value;
            }
        }

        public String FlagKeyword
        {
            get
            {
                return flagKeyword;
            }
            set
            {
                flagKeyword = value;
            }
        }

        public String ValueSet1
        {
            get
            {
                return valueSet1;
            }
            set
            {
                valueSet1 = value;
            }
        }

        public String ValueSet2
        {
            get
            {
                return valueSet2;
            }
            set
            {
                valueSet2 = value;
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


    }
}