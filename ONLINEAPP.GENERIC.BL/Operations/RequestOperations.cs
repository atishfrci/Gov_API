using Newtonsoft.Json.Linq;
using ONLINEAPP.DAL;
using ONLINEAPP.GENERIC.INTERFACE;
using ONLINEAPP.GENERIC.MODEL;
using ONLINEAPP.MODEL;
using ONLINEAPP.TRANSPORTS.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;


namespace ONLINEAPP.GENERIC.BL.Operations
{
    public class RequestOperations : IRequest
    {
        public string EserviceCentralOperation(string token,string id,string eserv,string usr)
        {
            List<Request> requestList1 = new List<Request>();
            List<EService> list = new List<EService>();
            list = ((IEnumerable<EService>)CRUDOperations.GetListByRestURL<EService>(this.BuildEServicesRestUrl(), token)).ToList<EService>();
            if (list.Count > 0)
            {
                foreach (EService eservice in list)
                {
                    if (eservice != null)
                    {
                        if (eserv.ToLower().Equals(eservice.EServiceName?.ToLower()))
                        {

                            if (!string.IsNullOrEmpty(eservice.EServiceListName) && !string.IsNullOrEmpty(eservice.EServiceName) && (!string.IsNullOrEmpty(eservice.EServicePublicFormUrl) && !string.IsNullOrEmpty(eservice.EServiceUrl)) && (!string.IsNullOrEmpty(eservice.EServiceTableName)))
                            {
                                List<FieldsList> fieldlist = CRUDOperations.GetListByRestURL<FieldsList>(this.BuildRestUrl(eservice.EServiceUrl, eservice.EServiceFieldsList, "/items?$select=Title,VisibleInListView,InternalName", ""), token);
                                string filter = "";
                                foreach (FieldsList field in fieldlist)
                                {
                                    if(!field.InternalName.ToLower().Contains("requestid") && !field.InternalName.ToLower().Contains("status") && !field.InternalName.ToLower().Contains("internalstatus"))
                                    filter = string.Concat(filter+",", field.InternalName);
                                }

                                filter = filter.Substring(1);

                                JArray jarr = CRUDOperations.GetListByRestURL(this.BuildRestUrl(eservice.EServiceFrontEndSubsite, eservice.EServiceFrontEndList, "/items?$select="+ filter + "&$filter=ID eq "+ id), token);
                        
                                DBOperation dbOperation = new DBOperation();
                                requestList1 = dbOperation.InsertEservicesApplicationInDB(token, jarr, eservice.EServiceTableName, fieldlist, id, usr);
                            }   
                        }
                    }
                }
            }

             return requestList1[0].SID??"";
        }

        private T GetObject<T>(params object[] lstArgument)
        {
            return (T)Activator.CreateInstance(typeof(T), lstArgument);
        }

        public List<Request> GetAllRequests(string token, string userName)
        {
            List<Request> requestList1 = new List<Request>();
            List<Request> requestList2 = new List<Request>();
            List<EService> list = new List<EService>();
            List<Request> first = new List<Request>();
            try
            {
                
                list = ((IEnumerable<EService>)CRUDOperations.GetListByRestURL<EService>(this.BuildEServicesRestUrl(), token)).ToList<EService>();
                if (list.Count > 0)
                {
                    foreach (EService eservice in list)
                    {
                        if (eservice != null)
                        {
                            if (eservice.SQLOrSP.ToLower().Equals("sp"))
                            {
                                if (!string.IsNullOrEmpty(eservice.EServiceListName) && !string.IsNullOrEmpty(eservice.EServiceName) && (!string.IsNullOrEmpty(eservice.EServicePublicFormUrl) && !string.IsNullOrEmpty(eservice.EServiceUrl)) && (string.IsNullOrEmpty(eservice.EServiceTableName)))
                                {
                                    string filterParam = "&$filter=username eq '" + userName + "'";
                                    foreach (Request request in (List<Request>)CRUDOperations.GetListByRestURL<Request>(this.BuildRestUrl(eservice.EServiceUrl, eservice.EServiceListName, "/items?$select=SID,GenericId,Created,Status", filterParam), token))
                                    {
                                        if (request != null && !string.IsNullOrEmpty(request.GenericId) && (!string.IsNullOrEmpty(request.SID) && !string.IsNullOrEmpty(request.Status)))
                                        {
                                            request.Created = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Parse(request.Created.ToString()), TimeZoneInfo.Local).ToString("dd/MM/yyyy hh:mm tt");
                                            request.EServiceName = eservice.EServiceName;
                                            request.EServicePublicFormUrl = eservice.EServicePublicFormUrl;
                                            first.Add(request);
                                        }
                                    }
                                }
                            }
                            if (eservice.SQLOrSP.ToLower().Equals("sql"))
                            {
                                if (!string.IsNullOrEmpty(eservice.EServiceTableName) && !string.IsNullOrEmpty(eservice.EServiceName) && (!string.IsNullOrEmpty(eservice.EServicePublicFormUrl) && !string.IsNullOrEmpty(eservice.EServiceUrl)))
                                {
                                    DBOperation dbOperation = new DBOperation();
                                    string str = eservice.EServiceTableName.ToString();
                                    char[] chArray = new char[1] { ',' };
                                    foreach (string tableName in str.Split(chArray))
                                    {
                                        List<Request> requestList3 = new List<Request>();
                                        foreach (Request request in dbOperation.GetAllRequestDB(token, userName, tableName))
                                        {
                                            request.EServiceName = eservice.EServiceName;
                                            request.EServiceName = eservice.EServiceName;
                                            request.EServicePublicFormUrl = eservice.EServicePublicFormUrl;
                                            requestList2.Add(request);
                                        }
                                    }
                                }
                            }


                        }

                    }
                    requestList1 = first.Concat<Request>((IEnumerable<Request>)requestList2).ToList<Request>();
                }
            }
            catch (Exception ex)
            {
                string name1 = MethodBase.GetCurrentMethod().Name;
                string name2 = MethodBase.GetCurrentMethod().DeclaringType.Name;
                //throw new Exception(string.Format("An error occured while reading data. GUID: {0}", (object)CRUDOperations.WriteException(ex, name1, name2)));
                throw new Exception(string.Format("listeservices.count {1}, An error occured while reading data. GUID: {0}", ex.ToString(),list[1].EServiceTableName));

            }
            return requestList1;
        }

        public string BuildEServicesRestUrl()
        {
            try
            {
                return Variables.EServiceUrl + "/" + string.Format("/_api/web/lists/GetByTitle('{0}')", (object)"EServices") + "/items?$select=EServiceName,EServiceUrl,EServiceListName,EServicePublicFormUrl,TableName,NewBackend,FrontEndSubsite,StatusList,Subsite,FieldsList,FrontEndList,DBConnStrName";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string BuildRestUrl(
          string subSiteName,
          string listName,
          string select,
          string filterParam = null)
        {
            try
            {
                return Variables.EServiceUrl + "/" + subSiteName + string.Format("/_api/web/lists/GetByTitle('{0}')", (object)listName) + select + (filterParam != null ? filterParam : "");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string BuildRestUrl(
          string subSiteName,
          string listName,
          string select
          )
        {
            try
            {
                return  subSiteName + string.Format("/_api/web/lists/GetByTitle('{0}')", (object)listName) + select;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TransportList GetTransport(string requestId, string token)
        {
            try
            {
                return ((IEnumerable<TransportList>)((IEnumerable<TransportList>)CRUDOperations.GetListByRestURL<TransportList>(this.BuildRestUrl("transport", "TransportList", "/items?$select=*", "&$filter=SID eq '" + requestId + "'"), token)).ToList<TransportList>()).First<TransportList>();
            }
            catch (Exception ex)
            {
                string name1 = MethodBase.GetCurrentMethod().Name;
                string name2 = MethodBase.GetCurrentMethod().DeclaringType.Name;
                throw new Exception(string.Format("An error occured while reading data. GUID: {0}", (object)CRUDOperations.WriteException(ex, name1, name2)));
            }
        }

        public Result CancelTransportRequest(
          TransportList objUpdateRequest,
          string siteUrl,
          string listName,
          string token)
        {
            Result result = new Result();
            try
            {
                result = CRUDOperations.UpdateListItem<TransportList>(siteUrl, token, listName, objUpdateRequest);
                result.StatusCode = StatusCode.Success;
                result.Message = "Your Registration Mark data has beenupdated successfully";
                return result;
            }
            catch (Exception ex)
            {
                string name1 = MethodBase.GetCurrentMethod().Name;
                string name2 = MethodBase.GetCurrentMethod().DeclaringType.Name;
                CRUDOperations.WriteException(ex, name1, name2);
                result.Message = "Something went wrong";
                result.StatusCode = StatusCode.Error;
                return result;
            }
        }
    }
}
