/*
 * Copyright 2012-2013 inBloom, Inc. and its affiliates.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */


using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using SDAC.DomainModel;



namespace SDAC.Core
{

    /// <summary>
    /// 
    /// Purpose of SqlHelper class is to handle the database related queries and connections.
    /// SqlHelper class contains all the functions to perform various operations on Flag and Aggregate flags with Entity Model.
    /// Contains functionality of Insert/Update/Delete for Flag and Aggregate Flag along with the appropriate validation for them.    /// 
    /// 
    /// </summary>
    public class SqlHelper
    {

        #region Private Variables       
       
        protected SLC_SDACEntities _sDACEntities = new SLC_SDACEntities();
        protected Flag _flag = null;
        protected AggregateFlag _aggregateFlag = null;
        protected PublicFavorite _publicFavorite = null;
        protected FlagAggregateFlag _flagAggregateFlag = null;

        #endregion

        #region Constructor
    
        public SqlHelper()
        {
        }

        #endregion

        #region Public Methods
              

        /// <summary>
        /// function to check if the Flag exists with the given name
        /// </summary>
        /// <param name="FlagName"></param>
        /// <returns></returns>
        public bool FlagIsExist(String flagName, String userId)
        {
            try
            {
                var Query = from _flag in _sDACEntities.Flags
                            where (_flag.FlagName == flagName && _flag.IsDeleted == null && _flag.IsPublic == false && _flag.UserId == userId)
                            select _flag;

                var List= Query.ToList();
                if (List.Count>0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception Ex)
            {
                return true;
            }
        }

        public bool FlagIsExistForAdmin(String flagName, String userId)
        {
            try
            {
                var Query = from _flag in _sDACEntities.Flags
                            where (_flag.FlagName == flagName && _flag.IsDeleted == null && _flag.IsPublic == true && _flag.UserId == userId)
                            select _flag;

                var List = Query.ToList();
                if (List.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception Ex)
            {
                return true;
            }
        }

        /// <summary>
        /// function to check the flag has same name on edit page
        /// </summary>
        /// <param name="FlagId"></param>
        /// <param name="FlagName"></param>
        /// <returns></returns>
        public bool FlagHasSameName(int flagId, String flagName)
        {
            try
            {
                var Query = from _flag in _sDACEntities.Flags
                            where (_flag.FlagName == flagName && _flag.IsDeleted == null && _flag.FlagId==flagId)
                            select _flag;

                var List = Query.ToList();
                if (List.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception Ex)
            {
                return true;
            }
        }

        /// <summary>
        /// function to check the Aggregate flag has the same name on edit aggregate page
        /// </summary>
        /// <param name="AggregateFlagId"></param>
        /// <param name="AggregateFlagName"></param>
        /// <returns></returns>
        public bool AggregateFlagHasSameName(int aggregateFlagId, String aggregateFlagName)
        {
            try
            {
                var Query = from _aggregateFlag in _sDACEntities.AggregateFlags
                            where (_aggregateFlag.AggregateFlagName == aggregateFlagName && _aggregateFlag.IsDelete == null && _aggregateFlag.AggregateFlagId == aggregateFlagId)
                            select _aggregateFlag;

                var List = Query.ToList();
                if (List.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception Ex)
            {
                return true;
            }
        }

       /// <summary>
       /// function to add the flag
       /// </summary>
       /// <param name="FlagName"></param>
       /// <param name="FlagDescription"></param>
       /// <param name="Keyword"></param>
       /// <param name="DataElementId"></param>
       /// <param name="ConditionId"></param>
       /// <param name="ValueSet1"></param>
       /// <param name="ValueSet2"></param>
       /// <param name="UserId"></param>
       /// <param name="UserName"></param>
       /// <param name="IsPublic"></param>
       /// <returns></returns>
        public bool AddFlag(String flagName, String flagDescription, String keyword, int dataElementId, int conditionId, String valueSet1, String valueSet2, String userId, String userName, bool isPublic)
        {
             _flag = new Flag();
            _flag.FlagName = flagName;
            _flag.FlagDescription =flagDescription;
            _flag.Keyword = keyword;
            _flag.IsPublic = isPublic;
            _flag.DataElementId = dataElementId;
            _flag.ConditionId = conditionId;
            _flag.ValueSet1 = valueSet1;
            _flag.ValueSet2 = valueSet2;
            _flag.UserId =userId;
            _flag.CreatedBy =userName;
            _flag.CreatedDate = DateTime.Now;
            _flag.ModifiedBy = userName;
            _flag.ModifiedDate = DateTime.Now;

            if (isPublic)
            {
                if (FlagIsExistForAdmin(flagName, userId))
                    return false;
            }
            else
                if (FlagIsExist(flagName, userId))
                {
                    return false;
                }
            
         
                _sDACEntities.AddToFlags(_flag);
                _sDACEntities.SaveChanges();
                return true;

           
           
        }

        /// <summary>
        /// function to update flag
        /// </summary>
        /// <param name="FlagId"></param>
        /// <param name="FlagName"></param>
        /// <param name="FlagDescription"></param>
        /// <param name="Keyword"></param>
        /// <param name="DataElementId"></param>
        /// <param name="ConditionId"></param>
        /// <param name="ValueSet1"></param>
        /// <param name="ValueSet2"></param>
        /// <param name="UserId"></param>
        /// <param name="UserName"></param>
        /// <param name="IsPublic"></param>
        /// <returns></returns>
        public bool UpdateFlag(int flagId, String flagName, String flagDescription, String keyword, int dataElementId, int conditionId, String valueSet1, String valueSet2, String userId, String userName, bool isPublic)
        {
            try
            {
                if (FlagHasSameName(flagId, flagName))
                {
                }
                else
                    if (isPublic)
                    {
                        if (FlagIsExistForAdmin(flagName, userId))
                            return false;
                    }
                    else
                        if (FlagIsExist(flagName, userId))
                    {
                        return false;
                    }
                _flag = new Flag();

                _flag = _sDACEntities.Flags.First(i => i.FlagId == flagId);


                _flag.FlagName = flagName;
                _flag.FlagDescription = flagDescription;
                _flag.Keyword = keyword;
                _flag.IsPublic = isPublic;
                _flag.DataElementId = dataElementId;
                _flag.ConditionId = conditionId;
                _flag.ValueSet1 = valueSet1;
                _flag.ValueSet2 = valueSet2;                        
               
                _flag.ModifiedBy = userName;
                _flag.ModifiedDate = DateTime.Now;

                _sDACEntities.SaveChanges();
                return true;
            }
            catch (Exception Ex)
            {
                return false;
            }

        }

        /// <summary>
        /// function to get the flag list added by user
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public IList<Flag> GetFlagList(String userId)
        {
            var Query = from _flag in _sDACEntities.Flags
                        where ((_flag.UserId == userId) || (_flag.IsPublic == true)) && (_flag.IsDeleted == null || _flag.IsDeleted == false)
                        select _flag;
            
            var FlagList = Query.ToList();
            return FlagList;
        }

        /// <summary>
        /// function to delete the flag by flag id
        /// </summary>
        /// <param name="FlagId"></param>
        /// <returns></returns>
        public bool DeleteFlag(int flagId)
        {
            _flag = new Flag();
            try
            {              
                
                _flag = _sDACEntities.Flags.First(i => i.FlagId == flagId);
                _flag.IsDeleted = true;
                _sDACEntities.SaveChanges();
                 return true;
            }
            catch (Exception Ex)
            {
                return false;
            }

        }
      
       
        /// <summary>
        /// function to get flag details by flag id
        /// </summary>
        /// <param name="FlagId"></param>
        /// <returns></returns>
        public IList<Flag> GetFlag(int FlagId)
        {
            var Query = from _flag in _sDACEntities.Flags
                        where _flag.FlagId == FlagId && (_flag.IsDeleted == null || _flag.IsDeleted == false)
                        select _flag;

            var FlagList = Query.ToList();
            return FlagList;
        }

        /// <summary>
        /// function to get aggregate flag details by aggregate flag id
        /// </summary>
        /// <param name="AggregateFlagId"></param>
        /// <returns></returns>
        public IList<AggregateFlag> GetAggregateFlag(int aggregateFlagId)
        {
            var Query = from _aggregateFlag in _sDACEntities.AggregateFlags
                        where _aggregateFlag.AggregateFlagId == aggregateFlagId && _aggregateFlag.IsDelete == null
                        select _aggregateFlag;

            var FlagList = Query.ToList();
            return FlagList;
        }
       
        /// <summary>
        /// function to get the list of flag selected for aggregate flag
        /// </summary>
        /// <param name="AgggregateFlagId"></param>
        /// <returns></returns>
        public IList<Flag> GetFlagAddedForInAggregate(int agggregateFlagId)
        {          

            var Query = from _flagAgg in _sDACEntities.FlagAggregateFlags
                        join _agg in _sDACEntities.AggregateFlags on _flagAgg.AggregateFlagId equals _agg.AggregateFlagId
                        join _flag in _sDACEntities.Flags on _flagAgg.FlagId equals _flag.FlagId
                        where _agg.AggregateFlagId == agggregateFlagId && (_flag.IsDeleted == null || _flag.IsDeleted == false)
                        select _flag;

            var FlagList = Query.ToList();
            return FlagList;

        }

        /// <summary>
        /// function to check the flag is favorite or not
        /// </summary>
        /// <param name="FlagId"></param>
        /// <returns></returns>
        public bool IsFav(int flagId)
        {
            var Query = from _flag in _sDACEntities.Flags
                        where (_flag.FlagId == flagId && _flag.IsFavorite==true)
                        select _flag;
            var Flag = Query.ToList();

            if (Flag.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        
        /// <summary>
        /// function to get the user id who added the flag
        /// </summary>
        /// <param name="FlagId"></param>
        /// <returns></returns>
        public string GetUserIdByFlagId(int flagId)
        {
            var Query = from _flag in _sDACEntities.Flags
                        where (_flag.FlagId == flagId)
                        select _flag;
            var Flag = Query.ToList();
            return Flag[0].UserId;
        }
        
        /// <summary>
        /// function to get the condition id which is selected for flag
        /// </summary>
        /// <param name="FlagId"></param>
        /// <returns></returns>
        public int GetConditionIdByFlagId(int flagId)
        {
            var Query = from _flag in _sDACEntities.Flags
                        where (_flag.FlagId == flagId)
                        select _flag;
            var Flag = Query.ToList();
            return Flag[0].ConditionId;
        }

        /// <summary>
        /// function to get the first value added for flag
        /// </summary>
        /// <param name="FlagId"></param>
        /// <returns></returns>
        public String GetValue1ByFlagId(int flagId)
        {
            var Query = from _flag in _sDACEntities.Flags
                        where (_flag.FlagId == flagId)
                        select _flag;
            var Flag = Query.ToList();
            return Flag[0].ValueSet1;
        }

        /// <summary>
        /// function to get the second value added for flag
        /// </summary>
        /// <param name="FlagId"></param>
        /// <returns></returns>
        public String GetValue2ByFlagId(int flagId)
        {
            var Query = from _flag in _sDACEntities.Flags
                        where (_flag.FlagId == flagId)
                        select _flag;
            var Flag = Query.ToList();
            return Flag[0].ValueSet2;
        }

        /// <summary>
        /// function to get the flag id by flag name
        /// </summary>
        /// <param name="FlagName"></param>
        /// <returns></returns>
        public int GetFlagId(String flagName)
        {

            try
            {
                var Query = from _flag in _sDACEntities.Flags
                            where (_flag.FlagName == flagName)
                            select _flag;
                var Flag = Query.ToList();
                return Convert.ToInt16(Flag[0].FlagId);
            }
            catch (Exception Ex)
            {
                return 0;
            }
        }

        /// <summary>
        /// function to get the aggregate flag id by aggregate flag name
        /// </summary>
        /// <param name="AggregateFlagName"></param>
        /// <returns></returns>
        public int GetAggregateFlagById(String aggregateFlagName)
        {

            try
            {
                var Query = from _aggregateFlag in _sDACEntities.AggregateFlags
                            where (_aggregateFlag.AggregateFlagName == aggregateFlagName)
                            select _aggregateFlag;
                var AggregateFlag = Query.ToList();
                return Convert.ToInt16(AggregateFlag[0].AggregateFlagId);
            }
            catch (Exception Ex)
            {
                return 0;
            }
        }

        /// <summary>
        /// Function to delete the aggregate flag
        /// </summary>
        /// <param name="AggregateFlagId"></param>
        /// <returns></returns>
        public bool DeleteAggregateFlag(int aggregateFlagId)
        {
            

            _flagAggregateFlag = new FlagAggregateFlag();
            _aggregateFlag = new AggregateFlag();
            try
            {

                var flag = _sDACEntities.FlagAggregateFlags.Where(c => c.AggregateFlagId == aggregateFlagId);
                if (flag.Count() > 0)
                {
                    foreach (var c  in flag)
                    {
                        _sDACEntities.DeleteObject(c);
                    }
                }

                _sDACEntities.SaveChanges();

                _aggregateFlag = _sDACEntities.AggregateFlags.First(i => i.AggregateFlagId == aggregateFlagId);
                _aggregateFlag.IsDelete = true;
                _sDACEntities.SaveChanges();

                return true;
            }
            catch (Exception Ex)
            {
                return false;
            }

        }

        /// <summary>
        /// Function to delete the record of Aggregate flag from FlagAggregateFlag table
        /// </summary>
        /// <param name="AggregateFlagId"></param>
        public void DeleteFromFlagAggregateFlagByAggregateFlagId(int aggregateFlagId)
        {
            _flagAggregateFlag = new FlagAggregateFlag();
            try
            {
                var flag = _sDACEntities.FlagAggregateFlags.Where(c => c.AggregateFlagId == aggregateFlagId);
                if (flag.Count() > 0)
                {
                    foreach (var c in flag)
                    {
                        _sDACEntities.DeleteObject(c);
                    }
                }               
                _sDACEntities.SaveChanges();
            }
            catch (Exception Ex)
            {
            }
        }

        /// <summary>
        /// function to get all the flag id which are added for aggregate flag
        /// </summary>
        /// <param name="AggregateFlagId"></param>
        /// <returns></returns>
        public int[] GetAllFlagIdOfAggregateFlagByAggregateFlagId(int aggregateFlagId)
        {

            var Query = from _flagAggregateFlag in _sDACEntities.FlagAggregateFlags
                        where (_flagAggregateFlag.AggregateFlagId == aggregateFlagId)
                        select _flagAggregateFlag;
            var FlagAggregateFlag = Query.ToList();
            int []FlagId = new int[FlagAggregateFlag.Count];
            for (int i = 0; i < FlagAggregateFlag.Count; i++)
            {
                FlagId[i] = FlagAggregateFlag[i].FlagId;
            }

            return FlagId;
        }       

        /// <summary>
        /// Function to check if the flag is already selected in aggregate flag
        /// </summary>
        /// <param name="AggregateFlagId"></param>
        /// <param name="FlagId"></param>
        /// <returns></returns>
        public bool ExistInAggregateFlag(int aggregateFlagId, int flagId)
        {
             var Query = from _flagAggregateFlag in _sDACEntities.FlagAggregateFlags
                         where (_flagAggregateFlag.AggregateFlagId == aggregateFlagId && _flagAggregateFlag.FlagId == flagId)
                        select _flagAggregateFlag;
            var Flag = Query.ToList();

            if (Flag.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// function to add aggregate flag
        /// </summary>
        /// <param name="AggregateFlagName"></param>
        /// <param name="AggregateDescription"></param>
        /// <param name="AggregateKeyword"></param>
        /// <param name="IsPublic"></param>
        /// <param name="FlagId"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public bool AddAggregateFlag(String aggregateFlagName, String aggregateDescription, String aggregateKeyword, bool isPublic, int[] flagId, String userId)
        {
            if (AggregateFlagExist(aggregateFlagName,userId))
            {
                return false;
            }
            else
            {

                _aggregateFlag = new AggregateFlag();

                _aggregateFlag.AggregateFlagName = aggregateFlagName;
                _aggregateFlag.AggregateFlagDescription = aggregateDescription;
                _aggregateFlag.Keyword = aggregateKeyword;
                _aggregateFlag.IsPublic = isPublic;
                _aggregateFlag.UserId = userId;
                _aggregateFlag.CreatedBy = userId;


                _aggregateFlag.CreatedDate = DateTime.Now;
                _aggregateFlag.ModifiedBy = userId;
                _aggregateFlag.ModifiedDate = DateTime.Now;


                _sDACEntities.AddToAggregateFlags(_aggregateFlag);
                _sDACEntities.SaveChanges();


                int AggregateFlagId=GetAggregateFlagById(aggregateFlagName);
               

                for (int i = 0; i < flagId.Count(); i++)
                {
                    
                        int Flag = flagId[i];
                        if (ExistInAggregateFlag(AggregateFlagId, Flag))
                        {

                        }
                        else
                        {
                            try
                            {

                                _flagAggregateFlag = new FlagAggregateFlag();
                                _flagAggregateFlag.AggregateFlagId = AggregateFlagId;
                                _flagAggregateFlag.FlagId = Flag;
                                _flagAggregateFlag.CreatedBy = userId;
                                _flagAggregateFlag.CreatedDate = DateTime.Now;
                                _flagAggregateFlag.ModifiedBy = userId;
                                _flagAggregateFlag.ModifiedDate = DateTime.Now;
                                _sDACEntities.AddToFlagAggregateFlags(_flagAggregateFlag);
                                _sDACEntities.SaveChanges();
                                _flagAggregateFlag = null;
                            }
                            catch (Exception Ex)
                            {
                            }
                        }
                   
                }


                return true;

            }            
        }

        /// <summary>
        /// Function to edit the aggregate flag
        /// </summary>
        /// <param name="AggregateFlagId"></param>
        /// <param name="AggregateFlagName"></param>
        /// <param name="AggregateDescription"></param>
        /// <param name="AggregateKeyword"></param>
        /// <param name="IsPublic"></param>
        /// <param name="FlagId"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public bool EditAddAggregateFlag(int aggregateFlagId, String aggregateFlagName, String aggregateDescription, String aggregateKeyword, bool isPublic, int[] flagId, String userId)
        {
            try
            {
                if (AggregateFlagHasSameName(aggregateFlagId, aggregateFlagName))
                {
                }
                else
                    if (AggregateFlagExist(aggregateFlagName, userId))
                    {
                        return false;
                    }
                

                _aggregateFlag = new AggregateFlag();
                _aggregateFlag = _sDACEntities.AggregateFlags.First(i => i.AggregateFlagId == aggregateFlagId);
                _aggregateFlag.AggregateFlagName = aggregateFlagName;
                _aggregateFlag.AggregateFlagDescription = aggregateDescription;
                _aggregateFlag.Keyword = aggregateKeyword;
                _aggregateFlag.IsPublic = isPublic;
                _aggregateFlag.ModifiedBy =userId;
                _aggregateFlag.ModifiedDate = DateTime.Now;
                _sDACEntities.SaveChanges();


                // delete all the flags associated with aggregate flag
                DeleteFromFlagAggregateFlagByAggregateFlagId(aggregateFlagId);
              


                for (int i = 0; i < flagId.Count(); i++)
                {
                    int Flag = flagId[i];
                    if (ExistInAggregateFlag(aggregateFlagId, Flag))
                    {

                    }
                    else
                    {
                        try
                        {
                            _flagAggregateFlag = new FlagAggregateFlag();
                            _flagAggregateFlag.AggregateFlagId = aggregateFlagId;
                            _flagAggregateFlag.FlagId = Flag;
                            _flagAggregateFlag.CreatedBy = userId;
                            _flagAggregateFlag.CreatedDate = DateTime.Now;
                            _flagAggregateFlag.ModifiedBy = userId;
                            _flagAggregateFlag.ModifiedDate = DateTime.Now;
                            _sDACEntities.AddToFlagAggregateFlags(_flagAggregateFlag);
                            _sDACEntities.SaveChanges();
                            _flagAggregateFlag = null;
                        }
                        catch (Exception Ex)
                        {
                        }
                    }

                }

                return true;
            }
            catch (Exception Ex)
            {
                return false;
            }
         
               

        }

        /// <summary>
        /// Function to check if the aggregate flag already exists with given name
        /// </summary>
        /// <param name="AggregateFlagName"></param>
        /// <returns></returns>
        public bool AggregateFlagExist(String aggregateFlagName, String userName)
        {

            try
            {
                var Query = from _aggregateFlag in _sDACEntities.AggregateFlags
                            where (_aggregateFlag.AggregateFlagName == aggregateFlagName && _aggregateFlag.IsDelete == null && _aggregateFlag.CreatedBy == userName)
                            select _aggregateFlag;

                var List = Query.ToList();
                if (List.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception Ex)
            {
                return true;
            }           
        }

       /// <summary>
       /// Function to check the flag is added by login user
       /// </summary>
       /// <param name="UserId"></param>
       /// <param name="FlagId"></param>
       /// <returns></returns>
        public bool AddedByUser(String userId, int flagId)
        {
            var Query = from _flag in _sDACEntities.Flags
                        where (_flag.FlagId == flagId && _flag.UserId == userId)
                        select _flag;

            var Flag = Query.ToList();

            if (Flag.Count > 0)
            {
                // flag is added by the user itself
                return true;
            }
            else
            {
                // flag is added by another user or public flag
                return false;
            }
            
        }

        /// <summary>
        /// Function to check the aggregate flag is added by login user
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="FlagId"></param>
        /// <returns></returns>
        public bool AggregateAddedByUser(String userId, int flagId)
        {
            var Query = from _aggregateFlag in _sDACEntities.AggregateFlags
                        where (_aggregateFlag.AggregateFlagId == flagId && _aggregateFlag.UserId == userId)
                        select _aggregateFlag;

            var Flag = Query.ToList();

            if (Flag.Count > 0)
            {
                // flag is added by the user itself
                return true;
            }
            else
            {
                // flag is added by another user or public flag
                return false;
            }
            
        }

        /// <summary>
        /// Function to get the flag list by categories like public, favorite and recent flag
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public ListItem[] GetFlagListByCategory(String userId)
        {
            ListItem[] _flag = new ListItem[100];

            try
            {
               
                int Count = 0;


                #region GetAllPublic Flag & Aggregate Flag


                IList<Flag> _flagList = GetAllPublicFlag();

                ListItem _public = new ListItem("Public");               
                _flag[Count] = _public;
                Count++;

                for (int i = 0; i < _flagList.Count; i++)
                {
                    ListItem _Flag = new ListItem(HttpUtility.HtmlDecode("&nbsp;&nbsp;&nbsp;&nbsp;") + _flagList[i].FlagName.ToString(), _flagList[i].FlagId.ToString()+"_Flag");
                    _flag[Count] = _Flag;
                    Count++;
                }

                // get all public aggregate flag
                IList<AggregateFlag> _aggregateFlagList = GetAllAggregatePublicFlag();
                for (int i = 0; i < _aggregateFlagList.Count; i++)
                {
                    ListItem _Flag = new ListItem(HttpUtility.HtmlDecode("&nbsp;&nbsp;&nbsp;&nbsp;") + _aggregateFlagList[i].AggregateFlagName.ToString(), _aggregateFlagList[i].AggregateFlagId.ToString() + "_AggregateFlag");
                    _flag[Count] = _Flag;
                    Count++;
                }

                
                #endregion


                #region GetAllFavorite Flag & Aggregate Flag 

                // get all favorite flag added by user
                _flagList = GetFavoriteFlag(userId);
                ListItem _Favorite = new ListItem("Favorite");             
                _flag[Count] = _Favorite;
                Count++;

                for (int i = 0; i < _flagList.Count; i++)
                {
                    ListItem _Flag = new ListItem(HttpUtility.HtmlDecode("&nbsp;&nbsp;&nbsp;&nbsp;") + _flagList[i].FlagName.ToString(), _flagList[i].FlagId.ToString() + "_Flag");
                    _flag[Count] = _Flag;
                    Count++;
                }


                // get favorite flag added by another user                
                IList<PublicFavorite> _publicFavorite = GetFavoriteFlagAddedByAnother(userId);
                for (int i = 0; i < _publicFavorite.Count; i++)
                {
                    try
                    {
                        int FlagId = Convert.ToInt16(_publicFavorite[i].FlagId);
                        String FlagName = ((IList<Flag>)GetFlag(FlagId))[0].FlagName;

                        ListItem _Flag = new ListItem(HttpUtility.HtmlDecode("&nbsp;&nbsp;&nbsp;&nbsp;") + FlagName, FlagId.ToString() + "_Flag");
                        _flag[Count] = _Flag;
                        Count++;
                    }
                    catch (Exception Ex)
                    {

                    }
                }


                // get aggregate fav flag added by user
                IList<AggregateFlag> _AggregateFav = GetAggregateFavoriteFlag(userId);
                for (int i = 0; i < _AggregateFav.Count; i++)
                {
                    int FlagId = Convert.ToInt16(_AggregateFav[i].AggregateFlagId);
                    String FlagName = _AggregateFav[i].AggregateFlagName;

                    ListItem _Flag = new ListItem(HttpUtility.HtmlDecode("&nbsp;&nbsp;&nbsp;&nbsp;") + FlagName, FlagId.ToString() + "_AggregateFlag");
                    _flag[Count] = _Flag;
                    Count++;
                }

                // get aggregate fav flag added by another usr
                IList<PublicFavorite> _aggregatepublicFavorite = GetFavoriteAggregateFlagAddedByAnother(userId);
                for (int i = 0; i < _aggregatepublicFavorite.Count; i++)
                {
                    int FlagId = Convert.ToInt16(_aggregatepublicFavorite[i].FlagId);
                    String FlagName = ((IList<AggregateFlag>)GetAggregateFlag(FlagId))[0].AggregateFlagName;

                    ListItem _Flag = new ListItem(HttpUtility.HtmlDecode("&nbsp;&nbsp;&nbsp;&nbsp;") + FlagName, FlagId.ToString() + "_AggregateFlag");
                    _flag[Count] = _Flag;
                    Count++;
                }
                
                #endregion


                #region GetRecent Flag & Aggregate Flag

                ListItem _Recent = new ListItem("Recent Flag");               
                _flag[Count] = _Recent;
                Count++;

                var Flag = (from flag in _sDACEntities.Flags
                                  where flag.UserId == userId &&
                                  (flag.IsDeleted == null || flag.IsDeleted==false)
                                  orderby flag.ModifiedDate descending
                                  select new
                                  {
                                      FlagId=flag.FlagId,
                                      FlagName=flag.FlagName,
                                      CreatedDate=flag.CreatedDate,
                                      Type="Flag"
                                  }).Take(5);

                var FlagList = Flag.ToList();

                var AggregateFlag = (from aggregateFlag in _sDACEntities.AggregateFlags
                                  where aggregateFlag.UserId == userId &&
                                  (aggregateFlag.IsDelete == null || aggregateFlag.IsDelete==false)
                                  orderby aggregateFlag.ModifiedDate descending
                                  select new
                                  {
                                      FlagId = aggregateFlag.AggregateFlagId,
                                      FlagName = aggregateFlag.AggregateFlagName,
                                      CreatedDate = aggregateFlag.CreatedDate,
                                      Type = "AggregateFlag"
                                  }).Take(5);



                FlagList.InsertRange(FlagList.Count, AggregateFlag.ToList());
                for (int i = 0; i < FlagList.Count; i++)
                {
                    for (int j = i + 1; j < FlagList.Count; j++)
                    {
                        if (FlagList[i].CreatedDate < FlagList[j].CreatedDate)
                        {
                            var temp = FlagList[i];
                            FlagList[i] = FlagList[j];
                            FlagList[j] = temp;
                        }
                    }
                }


                for (int i = 0; i < 5; i++)
                {
                    try
                    {
                        String FlagName = FlagList[i].FlagName.ToString();
                        String FlagId = FlagList[i].FlagId.ToString();
                        if (FlagList[i].Type == "Flag")
                        {
                            FlagId = FlagId + "_Flag";
                        }
                        else
                        {
                            FlagId = FlagId + "_AggregateFlag";
                        }
                        ListItem _Flag = new ListItem(HttpUtility.HtmlDecode("&nbsp;&nbsp;&nbsp;&nbsp;") + FlagName, FlagId);
                        _flag[Count] = _Flag;
                        Count++;
                    }
                    catch (Exception Ex)
                    {
                        break;
                    }
                }

                
                
                #endregion

               




                return _flag;
            }
            catch (Exception Ex)
            {
                return _flag;
            }
        }               

        /// <summary>
        /// function to get the condition name by condition id
        /// </summary>
        /// <param name="ConditionId"></param>
        /// <returns></returns>
        public String  GetConditionName(int conditionId)
        {
            try
            {
                var Query = from _condition in _sDACEntities.Conditions
                            where (_condition.ConditionId == conditionId)
                            select _condition;

                var Condtion = Query.ToList();

                return Condtion[0].ConditionName;
            }
            catch (Exception Ex)
            {
                return null;
            }

        }
       
        /// <summary>
        /// Function to get all the public flags
        /// </summary>
        /// <returns></returns>
        public IList<Flag> GetAllPublicFlag()
        {
            var Query = from _flag in _sDACEntities.Flags
                        where _flag.IsPublic == true && _flag.IsDeleted==null
                        select _flag;

            var FlagList = Query.ToList();
            return FlagList;
        }

       /// <summary>
       /// Function to get all the public aggregate flags
       /// </summary>
       /// <returns></returns>
        public IList<AggregateFlag> GetAllAggregatePublicFlag()
        {
            var Query = from _aggregateFlag in _sDACEntities.AggregateFlags
                        where _aggregateFlag.IsPublic == true && ( _aggregateFlag.IsDelete == null || _aggregateFlag.IsDelete==false)
                        select _aggregateFlag;

            var FlagList = Query.ToList();
            return FlagList;
        }

        /// <summary>
        /// Function to get all the favorite flags
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public IList<Flag> GetFavoriteFlag(String userId)
        {
            // get favorite flag added by user
            var Query = from _flag in _sDACEntities.Flags
                        where _flag.IsFavorite == true && _flag.IsDeleted == null && _flag.UserId == userId
                        select _flag;

            var FlagList = Query.ToList();

            return FlagList;
           
        }

        /// <summary>
        /// Function to get all the flags added by another user, favorite by login user
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public IList<PublicFavorite> GetFavoriteFlagAddedByAnother(String userId)
        {
            // get favorite flag added by admin or other and favorite by user
            var Query = from _publicFavorite in _sDACEntities.PublicFavorites
                         where _publicFavorite.UserId == userId && (_publicFavorite.IsAggregate==null || _publicFavorite.IsAggregate==false) 
                         select _publicFavorite;

            var FlagList = Query.ToList();

            return FlagList;
            
        }

        /// <summary>
        /// Function to get all the favorite aggregate flags by user id
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public IList<AggregateFlag> GetAggregateFavoriteFlag(String userId)
        {
            // get favorite flag added by user
            var Query = from _aggregateFlag in _sDACEntities.AggregateFlags
                        where _aggregateFlag.IsFavorite == true && _aggregateFlag.IsDelete == null && _aggregateFlag.UserId == userId
                        select _aggregateFlag;

            var FlagList = Query.ToList();
            return FlagList;

        }

        /// <summary>
        /// Function to get the aggregate flag added by another user, favorite by login user
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public IList<PublicFavorite> GetFavoriteAggregateFlagAddedByAnother(String userId)
        {
            // get favorite flag added by admin or other and favorite by user
            var Query = from _publicFavorite in _sDACEntities.PublicFavorites
                        where _publicFavorite.UserId == userId && _publicFavorite.IsAggregate == true 
                        select _publicFavorite;

            var FlagList = Query.ToList();

            return FlagList;

        }

        /// <summary>
        /// Function to check if the flag record is already added in table
        /// </summary>
        /// <param name="FlagId"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public bool IsInPublicFavorite(int flagId, String userId)
        {
            var Query = from _publicFavorite in _sDACEntities.PublicFavorites
                        where (_publicFavorite.FlagId == flagId) && (_publicFavorite.UserId == userId) && (_publicFavorite.IsAggregate == null || _publicFavorite.IsAggregate == false)
                        select _publicFavorite;

            var PublicFavorite = Query.ToList();

            if (PublicFavorite.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }

        /// <summary>
        /// Function to check the flag is present in public favorite table
        /// </summary>
        /// <param name="FlagId"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public bool IsAggregateInPublicFavorite(int flagId, String userId)
        {
            var Query = from _publicFavorite in _sDACEntities.PublicFavorites
                        where (_publicFavorite.FlagId == flagId) && (_publicFavorite.UserId == userId) && (_publicFavorite.IsAggregate==true)
                        select _publicFavorite;

            var PublicFavorite = Query.ToList();

            if (PublicFavorite.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        /// <summary>
        /// Function to get all the data element
        /// </summary>
        /// <returns></returns>
         public IList<DataElement> GetAllDataElement()
        {
            var Query = from _dataElement in _sDACEntities.DataElements
                        select _dataElement;
            var DataElement = Query.ToList();
            return DataElement;
        }

        /// <summary>
        /// Function to get all the entities from data element table
        /// </summary>
        /// <returns></returns>
        public IList<String> GetAllEntityFromDataElement()
        {
            var Query = _sDACEntities.DataElements.Select(k => k.Entity).Distinct();
            var DataElement = Query.ToList();
            return DataElement;
        }

        /// <summary>
        /// Function to get all DataElement by Entity name
        /// </summary>
        /// <param name="Entity"></param>
        /// <returns></returns>
        public IList<DataElement> GetAllDataElementByEntity(String entity)
        {
            var Query = from _dataElement in _sDACEntities.DataElements
                        where _dataElement.Entity==entity
                        select _dataElement;
            var DataElement = Query.ToList();
            return DataElement;
        }

        /// <summary>
        /// Function to get all DataElement by DataDomain
        /// </summary>
        /// <param name="DataDomain"></param>
        /// <returns></returns>
        public IList<DataElement> GetAllDataElementByDataDomain(String dataDomain)
        {
            var Query = from _dataElement in _sDACEntities.DataElements
                        where _dataElement.DataDomain == dataDomain
                        orderby _dataElement.ExternalField
                        select _dataElement;
            var DataElement = Query.ToList();
            return DataElement;
        }

        /// <summary>
        /// Function to get ExternalField by Entity Name
        /// </summary>
        /// <param name="FieldName"></param>
        /// <param name="EnitityName"></param>
        /// <returns></returns>
        public string GetExternalEntityByFieldNameAndEntityName(String fieldName, String enitityName)
        {
            var Query = from _dataElement in _sDACEntities.DataElements
                        where (_dataElement.FieldName == fieldName && _dataElement.Entity==enitityName)
                        select _dataElement;

            var DataElement = Query.ToList();

            return DataElement[0].ExternalField;
        }


        /// <summary>
        /// Function to get all the DataDomain
        /// </summary>
        /// <returns></returns>
        public IList<String> GetAllDataDomainFromDataElement()
        {
            var Query = _sDACEntities.DataElements.Select(k => k.DataDomain).Distinct();
            var DataElement = Query.ToList();
            return DataElement;
        }

        
        /// <summary>
        /// Function to get EntityName by DataElement
        /// </summary>
        /// <param name="DataElementId"></param>
        /// <returns></returns>
        public String GetEntityNameByDataElementId(int dataElementId)
        {
            try
            {
                var Query = from _dataElementId in _sDACEntities.DataElements
                            where (_dataElementId.DataElementId == dataElementId)
                            select _dataElementId;

                var DataElement = Query.ToList();

                return DataElement[0].Entity;
            }
            catch (Exception Ex)
            {
                return null;
            }
        }


        /// <summary>
        /// Function to get Data Element Id selected for flag by flag Id
        /// </summary>
        /// <param name="FlagId"></param>
        /// <returns></returns>
        public int GetDataElementIdByFlagId(int flagId)
        {
            
            var Query = from _flag in _sDACEntities.Flags
                        where (_flag.FlagId == flagId)
                        select _flag;

            var DataElement = Query.ToList();

            return DataElement[0].DataElementId;
        }


        /// <summary>
        /// Function to get DataDomainName by Data Element id
        /// </summary>
        /// <param name="DataElementId"></param>
        /// <returns></returns>
        public String GetDataDomainNameByDataElementId(int dataElementId)
        {
            try
            {
                var Query = from _dataElementId in _sDACEntities.DataElements
                            where (_dataElementId.DataElementId == dataElementId)
                            select _dataElementId;

                var DataElement = Query.ToList();

                return DataElement[0].DataDomain;
            }
            catch (Exception Ex)
            {
                return null;
            }
        }


        /// <summary>
        /// Function to get FieldName by DataElement 
        /// </summary>
        /// <param name="DataElementId"></param>
        /// <returns></returns>
        public String GetFieldNameByDataElementId(int dataElementId)
        {
            try
            {
                var Query = from _dataElementId in _sDACEntities.DataElements
                            where (_dataElementId.DataElementId == dataElementId)
                            select _dataElementId;

                var DataElement = Query.ToList();

                return DataElement[0].FieldName;
            }
            catch (Exception Ex)
            {
                return null;
            }
        }


        /// <summary>
        /// Function to get DataType by DataElement
        /// </summary>
        /// <param name="DataElementId"></param>
        /// <returns></returns>
        public String GetDataTypeByDataElementId(int dataElementId)
        {
            try
            {
                var Query = from _dataElementId in _sDACEntities.DataElements
                            where (_dataElementId.DataElementId == dataElementId)
                            select _dataElementId;

                var DataElement = Query.ToList();

                return DataElement[0].DataType;
            }
            catch (Exception Ex)
            {
                return null;
            }
        }


        /// <summary>
        /// Function to toggle from favorite to not favorite and vice versa
        /// </summary>
        /// <param name="FlagId"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public bool FavoriteFlag(int flagId, String userId)
        {
            try
            {

                if (AddedByUser(userId, flagId))
                {
                    // flag is added by user changes in Flag table only

                    _flag = new Flag();
                    _flag = _sDACEntities.Flags.First(i => i.FlagId == flagId);

                    if (IsFav(flagId))
                    {
                        // make to unfavorite
                        _flag.IsFavorite = false;
                    }
                    else
                    {
                        // make to favorite
                        _flag.IsFavorite = true;
                    }
                    _sDACEntities.SaveChanges();
                    return true;
                }
                else
                {
                 
                    if (IsInPublicFavorite(flagId, userId))
                    {
                        // already exist i.e it is favorite delete it

                        _publicFavorite = (from _publicFav in _sDACEntities.PublicFavorites where (_publicFav.UserId == userId && _publicFav.FlagId == flagId) select _publicFav).First();
                        _sDACEntities.DeleteObject(_publicFavorite);
                        _sDACEntities.SaveChanges();
                        return true;
                    }
                    else
                    {
                        _publicFavorite = new PublicFavorite();
                        _publicFavorite.UserId = userId;
                        _publicFavorite.FlagId = flagId;

                        _sDACEntities.AddToPublicFavorites(_publicFavorite);
                        _sDACEntities.SaveChanges();
                         return true;

                    }
                   
                }

                
            }
            catch (Exception Ex)
            {
                return false;
            }
        }


        /// <summary>
        /// function to make aggregate flag favorite 
        /// </summary>
        /// <param name="AgggregateFlagId"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public bool AggregatetFavoriteFlag(int agggregateFlagId, String userId)
        {
            try
            {

                if (AggregatetAddedByUser(userId, agggregateFlagId))
                {
                    // flag is added by user changes in Flag table only
                    _aggregateFlag = new AggregateFlag();
                    _aggregateFlag = _sDACEntities.AggregateFlags.First(i => i.AggregateFlagId == agggregateFlagId);

                    if (IsAggregateIsFav(agggregateFlagId))
                    {
                        // make to unfavorite
                        _aggregateFlag.IsFavorite = false;
                    }
                    else
                    {
                        // make to favorite
                        _aggregateFlag.IsFavorite = true;
                    }
                    _sDACEntities.SaveChanges();
                    return true;
                }
                else
                {
                    if (IsAggregateInPublicFavorite(agggregateFlagId, userId))
                    {
                        // already exist i.e it is favorite delete it

                        _publicFavorite = (from _publicFav in _sDACEntities.PublicFavorites where (_publicFav.UserId == userId && _publicFav.FlagId == agggregateFlagId && _publicFav.IsAggregate == true) select _publicFav).First();
                        _sDACEntities.DeleteObject(_publicFavorite);
                        _sDACEntities.SaveChanges();
                        return true;
                    }
                    else
                    {
                        try
                        {
                            _publicFavorite = new PublicFavorite();
                            _publicFavorite.UserId = userId;
                            _publicFavorite.FlagId = agggregateFlagId;
                            _publicFavorite.IsAggregate = true;

                            _sDACEntities.PublicFavorites.AddObject(_publicFavorite);
                            _sDACEntities.SaveChanges();
                            return true;
                        }
                        catch (Exception Ex)
                        {
                            throw Ex;
                        }

                    }

                }


            }
            catch (Exception Ex)
            {
                throw Ex;
               // return false;
            }
        }


        /// <summary>
        /// function to check the aggregate flag is added by login user
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="AgggregateFlagId"></param>
        /// <returns></returns>
        bool AggregatetAddedByUser(String userId, int aggregateFlagId)
        {
            var Query = from _aggregateFlag in _sDACEntities.AggregateFlags
                        where (_aggregateFlag.AggregateFlagId == aggregateFlagId && _aggregateFlag.UserId == userId)
                        select _aggregateFlag;

            var Flag = Query.ToList();

            if (Flag.Count > 0)
            {
                // flag is added by the user itself
                return true;
            }
            else
            {
                // flag is added by another user or public flag
                return false;
            }
            
        }


        /// <summary>
        /// function to check the aggregate flag is favorite
        /// </summary>
        /// <param name="AggregateFlagId"></param>
        /// <returns></returns>
        public bool IsAggregateIsFav(int aggregateFlagId)
        {
            var Query = from _aggregateFlag in _sDACEntities.AggregateFlags
                        where (_aggregateFlag.AggregateFlagId == aggregateFlagId && _aggregateFlag.IsFavorite == true)
                        select _aggregateFlag;
            var Flag = Query.ToList();

            if (Flag.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion

        // for performance


        #region RunAggregate function for performance
        
     



        /// <summary>
        /// function to get Data Element id selected for flag by flag id
        /// </summary>
        /// <param name="FlagId"></param>
        /// <returns></returns>
        public int GetDataElementIdByFlagIdForRunAggregate(int flagId)
        {
            System.Threading.Thread.Sleep(new Random().Next(200,1000));
            var Query = from _flag in _sDACEntities.Flags
                        where (_flag.FlagId == flagId)
                        select _flag;

            var DataElement = Query.ToList();

            return DataElement[0].DataElementId;
        }


        /// <summary>
        /// function to get FieldName by DataElement 
        /// </summary>
        /// <param name="DataElementId"></param>
        /// <returns></returns>
        public String GetFieldNameByDataElementIdForRunAggregate(int dataElementId)
        {
            try
            {
                System.Threading.Thread.Sleep(new Random().Next(200, 1000));
                var Query = from _dataElementId in _sDACEntities.DataElements
                            where (_dataElementId.DataElementId == dataElementId)
                            select _dataElementId;

                var DataElement = Query.ToList();

                return DataElement[0].FieldName;
            }
            catch (Exception Ex)
            {
                return null;
            }
        }


        /// <summary>
        /// function to get EntityName by DataElement
        /// </summary>
        /// <param name="DataElementId"></param>
        /// <returns></returns>
        public String GetEntityNameByDataElementIdForRunAggregate(int dataElementId)
        {
            try
            {
                System.Threading.Thread.Sleep(new Random().Next(200, 1000));
                var Query = from _dataElementId in _sDACEntities.DataElements
                            where (_dataElementId.DataElementId == dataElementId)
                            select _dataElementId;

                var DataElement = Query.ToList();

                return DataElement[0].Entity;
            }
            catch (Exception Ex)
            {
                return null;
            }
        }


        /// <summary>
        /// function to get DataType by DataElement
        /// </summary>
        /// <param name="DataElementId"></param>
        /// <returns></returns>
        public String GetDataTypeByDataElementIdForRunAggregate(int dataElementId)
        {
            try
            {
                System.Threading.Thread.Sleep(new Random().Next(200, 1000));
                var Query = from _dataElementId in _sDACEntities.DataElements
                            where (_dataElementId.DataElementId == dataElementId)
                            select _dataElementId;

                var DataElement = Query.ToList();

                return DataElement[0].DataType;
            }
            catch (Exception Ex)
            {
                return null;
            }
        }



        /// <summary>
        /// function to get the user id who added the flag
        /// </summary>
        /// <param name="FlagId"></param>
        /// <returns></returns>
        public string GetUserIdByFlagIdForRunAggregate(int flagId)
        {
            System.Threading.Thread.Sleep(new Random().Next(200, 1000));
            var Query = from _flag in _sDACEntities.Flags
                        where (_flag.FlagId == flagId)
                        select _flag;
            var Flag = Query.ToList();
            return Flag[0].UserId;
        }

        /// <summary>
        /// function to get the condition id which is selected for flag
        /// </summary>
        /// <param name="FlagId"></param>
        /// <returns></returns>
        public int GetConditionIdByFlagIdForRunAggregate(int flagId)
        {
            System.Threading.Thread.Sleep(new Random().Next(200, 1000));
            var Query = from _flag in _sDACEntities.Flags
                        where (_flag.FlagId == flagId)
                        select _flag;
            var Flag = Query.ToList();
            return Flag[0].ConditionId;
        }

        /// <summary>
        /// function to get the first value added for flag
        /// </summary>
        /// <param name="FlagId"></param>
        /// <returns></returns>
        public String GetValue1ByFlagIdForRunAggregate(int flagId)
        {
            System.Threading.Thread.Sleep(new Random().Next(200, 1000));
            var Query = from _flag in _sDACEntities.Flags
                        where (_flag.FlagId == flagId)
                        select _flag;
            var Flag = Query.ToList();
            return Flag[0].ValueSet1;
        }


        /// <summary>
        /// function to get the second value added for flag
        /// </summary>
        /// <param name="FlagId"></param>
        /// <returns></returns>
        public String GetValue2ByFlagIdForRunAggregate(int flagId)
        {
            System.Threading.Thread.Sleep(new Random().Next(200, 1000));
            var Query = from _flag in _sDACEntities.Flags
                        where (_flag.FlagId == flagId)
                        select _flag;
            var Flag = Query.ToList();
            return Flag[0].ValueSet2;
        }

        #endregion


        //Function which will add the fields of Student Identification & Demographics fields to the checkbox on Results page

        public IList<String> GetAllFields()
        {
         
            try
            {
                var Query = from _dataElement in _sDACEntities.DataElements
                            where (_dataElement.DataDomain == "Student Identification and Demographics")
                            select _dataElement.FieldName;
                var DataElement = Query.ToList();
                return DataElement;
            }
            catch (Exception Ex)
            {
                return null;
            }
        }


        public int GetFlagIdByNameDescriptionAndKeyword(String FlagName, String Description, String Keyword)
        {
            try
            {
                var Query = from _flag in _sDACEntities.Flags
                            where (_flag.FlagName == FlagName && _flag.FlagDescription == Description && _flag.Keyword == Keyword) && (_flag.IsDeleted == false || _flag.IsDeleted == null)
                            select _flag;
                var Flag = Query.ToList();
                return Convert.ToInt16(Flag[0].FlagId);
            }
            catch (Exception Ex)
            {
                return 0;
            }
        }

        public int GetAggregateFlagIdByNameDescriptionAndKeyword(String AggregateFlagName, String Description, String Keyword)
        {
            try
            {
                var Query = from _aggregateFlag in _sDACEntities.AggregateFlags
                            where (_aggregateFlag.AggregateFlagName == AggregateFlagName && _aggregateFlag.AggregateFlagDescription == Description && _aggregateFlag.Keyword == Keyword) && (_aggregateFlag.IsDelete == null || _aggregateFlag.IsDelete == false)
                            select _aggregateFlag;
                var AggregateFlag = Query.ToList();
                return Convert.ToInt16(AggregateFlag[0].AggregateFlagId);
            }
            catch (Exception Ex)
            {
                return 0;
            }
        }
        public IList<Flag> GetPublicFlagListForAdmin(String UserId)
        {
            var Query = from _flag in _sDACEntities.Flags
                        where ((_flag.UserId == UserId)) && (_flag.IsDeleted == null || _flag.IsDeleted == false)
                        select _flag;

            var FlagList = Query.ToList();
            return FlagList;
        }


        public IList<AggregateFlag> GetAllAggregatePublicFlagByUserId(String UserId)
        {
            var Query = from _aggregateFlag in _sDACEntities.AggregateFlags
                        where (_aggregateFlag.IsDelete == null || _aggregateFlag.IsDelete == false) && _aggregateFlag.UserId == UserId
                        select _aggregateFlag;

            var FlagList = Query.ToList();
            return FlagList;
        }







      
    }




}