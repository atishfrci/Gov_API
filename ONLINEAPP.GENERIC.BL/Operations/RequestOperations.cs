using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ONLINEAPP.DAL;
using ONLINEAPP.MODEL;
using ONLINEAPP.GENERIC.INTERFACE;
using ONLINEAPP.GENERIC.MODEL;
using ONLINEAPP.TRANSPORTS.MODEL;
using System.Reflection;

namespace ONLINEAPP.GENERIC.BL.Operations
{
    public class RequestOperations : IRequest
    {

        #region Methods for Workspace

        //ok 100%
        public List<Request> GetAllRequests(string token, string userName)
        {
            List<Request> lstAllRequest = new List<Request>();

            try
            {
                string restUrlEservices = BuildEServicesRestUrl();
                var lstEservices = CRUDOperations.GetListByRestURL<EService>(restUrlEservices, token).ToList();

                if(lstEservices != null)
                {
                    foreach (EService eservice in lstEservices)
                    {
                        if (!string.IsNullOrEmpty(eservice.EServiceListName)
                            && !string.IsNullOrEmpty(eservice.EServiceName)
                            && !string.IsNullOrEmpty(eservice.EServicePublicFormUrl)
                            && !string.IsNullOrEmpty(eservice.EServiceUrl)
                        )
                        {
                            string filterParam = string.Concat("&$filter=username eq '", userName, "'");
                            string restUrl = BuildRestUrl(eservice.EServiceUrl, eservice.EServiceListName, Variables.RequestFilter, filterParam);
                            var result = CRUDOperations.GetListByRestURL<Request>(restUrl, token);

                            foreach (Request req in result)
                            {
                                if (req != null)
                                {
                                    
                                    if (!string.IsNullOrEmpty(req.GenericId)
                                        && !string.IsNullOrEmpty(req.SID)
                                        && !string.IsNullOrEmpty(req.Status))
                                    {
                                        req.Created = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Parse(req.Created.ToString()), TimeZoneInfo.Local).ToString();
                                        req.EServiceName = eservice.EServiceName;
                                        req.EServicePublicFormUrl = eservice.EServicePublicFormUrl;

                                        lstAllRequest.Add(req);

                                    }
                                }
                            }

                        }
                    }
                }

            }
            catch (Exception ex)
            {
                string guid = CRUDOperations.WriteException(ex, MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.Name);
                throw new Exception(string.Format(Messages.MsgExceptionOccured, guid));
            }

            return lstAllRequest;

        }
    

        /* old working code
        public List<Request> GetAllRequests(string token, string subSiteName, string listName, string userName)
        {
            try
            {
                string filterParam = string.Concat("&$filter=username eq '", userName, "'");
                string restUrl = BuildRestUrl(subSiteName, listName, Variables.RequestFilter, filterParam);
                var result = CRUDOperations.GetListByRestURL<Request>(restUrl, token);

                foreach (Request rq in result)
                {
                    rq.Created = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Parse(rq.Created.ToString()), TimeZoneInfo.Local).ToString();
                }
                return result.ToList();

            }
            catch (Exception ex)
            {
                string guid = CRUDOperations.WriteException(ex, MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.Name);
                throw new Exception(string.Format(Messages.MsgExceptionOccured, guid));
            }
        }
        */

        #endregion


        #region Generic Methods

        //ok 100%
        public string BuildEServicesRestUrl()
        {
            try
            {

                return string.Concat(
                            string.Concat(Variables.EServiceUrl, "/"),
                            string.Format("/_api/web/lists/GetByTitle('{0}')", Variables.ListEServiceName),
                            Variables.EServiceFilter
                        );
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        //ok 100%
        public string BuildRestUrl(string subSiteName, string listName, string select, string filterParam = null)
        {
            try
            {

                return string.Concat(
                            string.Concat(Variables.EServiceUrl, "/", subSiteName),
                            string.Format("/_api/web/lists/GetByTitle('{0}')", listName),
                            select,
                            filterParam != null ? filterParam : ""
                        );
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        #endregion


        #region Methods for each EService


        #region Transport Service

        //100% ok
        public TransportList GetTransport(string requestId, string token)
        {

            try
            {
                string filterParam = string.Concat("&$filter=SID eq '", requestId, "'");
                string restUrl = BuildRestUrl(Variables.SubsiteTransport, Variables.ListTransportName, Variables.SelectAllFilter, filterParam);
                var result = CRUDOperations.GetListByRestURL<TransportList>(restUrl, token);
                return result.ToList().First();
            }
            catch (Exception ex)
            {
                string guid = CRUDOperations.WriteException(ex, MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.Name);
                throw new Exception(string.Format(Messages.MsgExceptionOccured, guid));
            }
        }

        //100% ok
        public Result CancelTransportRequest(TransportList objUpdateRequest, string siteUrl, string listName, string token)
        {
            Result res = new Result();
            try
            {
                res = CRUDOperations.UpdateListItem<TransportList>(siteUrl, token, listName, objUpdateRequest);
                res.StatusCode = StatusCode.Success;
                res.Message = Messages.MsgTransportUpdatedSuccessfully;
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

        #endregion



        #endregion


    }
}
