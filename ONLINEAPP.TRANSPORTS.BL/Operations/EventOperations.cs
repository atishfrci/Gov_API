using ONLINEAPP.DAL;
using ONLINEAPP.MODEL;
using ONLINEAPP.TRANSPORTS.INTERFACE;
using ONLINEAPP.TRANSPORTS.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ONLINEAPP.TRANSPORTS.BL.Operations
{
    public class EventOperations : IEvents
    {
        public List<Events> GetEvents(string siteUrl, string token)
        {
            try
            {
                string RestUrl = string.Concat(siteUrl, ListURLs.RestUrlListItemWithQuery(Lists.FrciEvents, true),
                                                    string.Format(RESTFilters.topItems, GetTop._1000), string.Format(RESTFilters.orderByDescending, Fields.ID));

                var _result = CRUDOperations.GetListByRestURL<Events>(RestUrl, token);

                return _result.ToList();
            }
            catch (Exception ex)
            {
                string guid = CRUDOperations.WriteException(ex, MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.Name);
                throw new Exception(string.Format(Messages.MsgExceptionOccured, guid));
            }
        }

        public Events EventsByID(string id, string siteUrl, string token)
        {
            try
            {
                string RestUrl = string.Concat(siteUrl, ListURLs.RestUrlListItemWithQuery(Lists.FrciEvents, true),
                                                    string.Format(RESTFilters.ByID, id));

                return CRUDOperations.GetListByRestURL<Events>(RestUrl, token).FirstOrDefault();
            }
            catch (Exception ex)
            {
                string guid = CRUDOperations.WriteException(ex, MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.Name);
                throw new Exception(string.Format(Messages.MsgExceptionOccured, guid));
            }
        }

        public List<Events> EventsByStatus(string Status, string siteUrl, string token)
        {
            try
            {
                string RestUrl = string.Concat(siteUrl, ListURLs.RestUrlListItemWithQuery(Lists.FrciEvents, true),
                                                    string.Format(RESTFilters.ByStatus, Status));

                return CRUDOperations.GetListByRestURL<Events>(RestUrl, token);
            }
            catch (Exception ex)
            {
                string guid = CRUDOperations.WriteException(ex, MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.Name);
                throw new Exception(string.Format(Messages.MsgExceptionOccured, guid));
            }
        }

        public Result AddEvents(Events objEvents, string siteUrl, string token)
        {
            Result res = new Result();
            try
            {
                res = CRUDOperations.AddListItem<Events>(siteUrl, token, Lists.FrciEvents, objEvents);

                res.StatusCode = StatusCode.Success;
                res.Message = Messages.MsgEventAddedSuccessfully;
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

        public Result UpdateEventByID(Events objEvents, string siteUrl, string token)
        {
            Result res = new Result();
            try
            {
                res = CRUDOperations.UpdateListItem<Events>(siteUrl, token, Lists.FrciEvents, objEvents);

                res.StatusCode = StatusCode.Success;
                res.Message = Messages.MsgEventAddedSuccessfully;
                return res;
            }
            catch (Exception ex)
            {
                string guid = CRUDOperations.WriteException(ex, MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.Name);
                res.Message = Messages.MsgEventUpdatedSuccessfully;
                res.StatusCode = StatusCode.Error;
                return res;
            }
        }

        public Result DeleteEventByID(string id, string siteUrl, string token)
        {
            Result res = new Result();
            try
            {
                res = CRUDOperations.DeleteListItem(siteUrl, Lists.FrciEvents, id);

                res.StatusCode = StatusCode.Success;
                res.Message = Messages.MsgEventDeletedSuccessfully;
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
    }
}
