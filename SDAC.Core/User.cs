using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SDAC.Core
{
    /// <summary>   
    /// 
    /// Purpose of User class is create the object and store the all the information of login user.
    /// Using this class we can access and change the attribute of login user.   
    /// 
    /// </summary>
    public class User
    {
        #region Private Class Member        
       
        private bool _authenticated;
        private String _educationOrganization;
        private String _educationOrganizationId;
        private String _email;
        private String _externalId;
        private String _fullName;
        private String[] _grantedAuthorities = null;
        private String _realm;
        private String[] _rights = null;
        private String[] _sliRoles = null;
        private String _tenantId;
        private String _userId;
        private bool _isAdminUser;

        #endregion

        #region Public Get Set Property Of Class Member

        public bool Authenticated
        {
            get
            {
                return _authenticated;
            }
            set
            {
                _authenticated = value;
            }
        }

        public String EducationOrganization
        {
            get
            {
                return _educationOrganization;
            }
            set
            {
                _educationOrganization = value;
            }
        }

        public String EducationOrganizationId
        {
            get
            {
                return _educationOrganizationId;
            }
            set
            {
                _educationOrganizationId = value;
            }
        }

        public String Email
        {
            get
            {
                return _email;
            }
            set
            {
                _email = value;
            }
        }

        public String ExternalId
        {
            get
            {
                return _externalId;
            }
            set
            {
                _externalId = value;
            }
        }

        public String FullName
        {
            get
            {
                return _fullName;
            }
            set
            {
                _fullName = value;
            }
        }

        public String[] GrantedAuthorities
        {
            get
            {
                return _grantedAuthorities;
            }
            set
            {
                _grantedAuthorities = value;
            }
        }

        public String Realm
        {
            get
            {
                return _realm;
            }
            set
            {
                _realm = value;
            }
        }

        public String[] Rights
        {
            get
            {
                return _rights;
            }
            set
            {
                _rights = value;
            }
        }

        public String[] SliRoles
        {
            get
            {
                return _sliRoles;
            }
            set
            {
                _sliRoles = value;
            }
        }

        public String TenantId
        {
            get
            {
                return _tenantId;
            }
            set
            {
                _tenantId = value;
            }
        }

        public String UserId
        {
            get
            {
                return _userId;
            }
            set
            {
                _userId = value;
            }
        }


        public bool IsAdminUser
        {
            get
            {
                return _isAdminUser;
            }
            set
            {
                _isAdminUser = value;
            }
        }

        #endregion
    }

}


   