using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using System.Web;

namespace SDAC.Core
{
    /// <summary>
    /// 
    /// Purpose of Section class is to create object and store the list of sections.
    /// Using this class we can set and access the list of sections.
    /// 
    /// </summary>
    public class Section
    {

        #region Private Variable      
      
        protected ListItem[] _listSection;

        #endregion

        #region Constructor

        public Section()
        {
            _listSection = null;
        }
        public Section(ListItem[] listSection)
        {
            this._listSection = listSection;
        }

        #endregion

        #region Public Method       
        
        /// <summary>
        /// function to get the section list
        /// </summary>
        /// <returns></returns>
        public ListItem[] GetSectionList()
        {
            return _listSection;
        }


        /// <summary>
        /// function to set the section list
        /// </summary>
        /// <param name="_ListSection"></param>
        public void SetSectionList(ListItem[] listSection)
        {
            this._listSection = listSection;
        }

        #endregion
    }
}