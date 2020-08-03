using ONLINEAPP.DAL;
using ONLINEAPP.MODEL;
using ONLINEAPP.TRANSPORTS.INTERFACE;
using ONLINEAPP.TRANSPORTS.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ONLINEAPP.TRANSPORTS.BL.Operations
{
    public class ReservedMarkOperation : IReservedMark
    {
        public List<ReservedMark> GetReservedMarks(string siteUrl, string token , string prefix)
        {
            try
            {
                string RestUrl = string.Concat(siteUrl, ListURLs.RestUrlListItemWithQuery(typeof(ReservedMark).Name, true),
                                                   string.Format(RESTFilters.ByContainRegMarkPrefix, prefix),string.Format(RESTFilters.topItems, /*COMMENTED BY HEMA 22.01.2019 GetTop._10000*/ GetTop._5000), string.Format(RESTFilters.orderByDescending, Fields.ID));

                var _result = CRUDOperations.GetListByRestURL<ReservedMark>(RestUrl, token);

                return _result.ToList();
            }
            catch (Exception ex)
            {
                string guid = CRUDOperations.WriteException(ex, MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.Name);
                throw new Exception(string.Format(Messages.MsgExceptionOccured, guid));
            }
        }

        public ReservedMark ReservedMarkByID(string id, string siteUrl, string token)
        {
            try
            {
                string RestUrl = string.Concat(siteUrl, ListURLs.RestUrlListItemWithQuery(typeof(ReservedMark).Name, true),
                                                    string.Format(RESTFilters.ByID, id));

               

                return CRUDOperations.GetListByRestURL<ReservedMark>(RestUrl, token).FirstOrDefault();
            }
            catch (Exception ex)
            {
                string guid = CRUDOperations.WriteException(ex, MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.Name);
                throw new Exception(string.Format(Messages.MsgExceptionOccured, guid));
            }
        }

        public ReservedMark ReservedRegistrationMark(string RegistrationMark, string siteUrl, string token , string prefix)
        {
            try
            {
                string RestUrl = string.Concat(siteUrl, ListURLs.RestUrlListItemWithQuery(typeof(ReservedMark).Name, true),
                                                    string.Format(RESTFilters.ByContainPrefixAndRegistrationMark, prefix, RegistrationMark), string.Format(RESTFilters.topItems, /*COMMENTED BY HEMA 22.01.2019 GetTop._10000*/ GetTop._5000), string.Format(RESTFilters.orderByDescending, Fields.ID));

                return CRUDOperations.GetListByRestURL<ReservedMark>(RestUrl, token).FirstOrDefault();
            }
            catch (Exception ex)
            {
                string guid = CRUDOperations.WriteException(ex, MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.Name);
                throw new Exception(string.Format(Messages.MsgExceptionOccured, guid));
            }
        }

        public Result AddReservedMark(ReservedMark objReservedMark, string siteUrl, string token)
        {
            Result res = new Result();
            try
            {
                res = CRUDOperations.AddListItem<ReservedMark>(siteUrl, token, typeof(ReservedMark).Name, objReservedMark);

                res.StatusCode = StatusCode.Success;
                res.Message = Messages.MsgReservedMarkAddedSuccessfully;
                return res;
            }
            catch (Exception ex)
            {
                string guid = CRUDOperations.WriteException(ex, MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.Name);
                res.Message = Messages.MsgSomethingWentWrong;
                res.StatusCode = StatusCode.Error;
                return res;
            }
        }

        public Result UpdateReservedMarkByID(ReservedMark objReservedMark, string siteUrl, string token)
        {
            Result res = new Result();
            try
            {
                res = CRUDOperations.UpdateListItem<ReservedMark>(siteUrl, token, typeof(ReservedMark).Name, objReservedMark);

                res.StatusCode = StatusCode.Success;
                res.Message = Messages.MsgReservedMarkUpdatedSuccessfully;
                return res;
            }
            catch (Exception ex)
            {
                string guid = CRUDOperations.WriteException(ex, MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.Name);
                res.Message = Messages.MsgSomethingWentWrong;
                res.StatusCode = StatusCode.Error;
                return res;
            }
        }

        public Result DeleteReservedMarkByID(string id, string siteUrl, string token)
        {
            Result res = new Result();
            try
            {
                res = CRUDOperations.DeleteListItem(siteUrl, typeof(ReservedMark).Name, id);

                res.StatusCode = StatusCode.Success;
                res.Message = Messages.MsgReservedMarkDeletedSuccessfully;
                return res;
            }
            catch (Exception ex)
            {
                string guid = CRUDOperations.WriteException(ex, MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.Name);
                res.Message = Messages.MsgSomethingWentWrong;
                res.StatusCode = StatusCode.Error;
                return res;
            }
        }

        public List<ReservedMark> ReservedMarkByStatus(string Status, string siteUrl, string token)
        {
            try
            {
                string RestUrl = string.Concat(siteUrl, ListURLs.RestUrlListItemWithQuery(typeof(ReservedMark).Name, true),
                                                    string.Format(RESTFilters.ByStatus, Status));

                return CRUDOperations.GetListByRestURL<ReservedMark>(RestUrl, token);
            }
            catch (Exception ex)
            {
                string guid = CRUDOperations.WriteException(ex, MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.Name);
                throw new Exception(string.Format(Messages.MsgExceptionOccured, guid));
            }
        }

      
    }
}
