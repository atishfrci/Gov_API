using ONLINEAPP.MODEL;
using ONLINEAPP.SP.RESTSERVICES;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Threading;
using System.IO;
using Newtonsoft.Json;

namespace ONLINEAPP.DAL
{
    public class CRUDOperations
    {
        public static List<T> GetListByRestURL<T>(string RestUrl, string token)
        {
            List<T> lst = new List<T>();
            try
            {
                if (!string.IsNullOrEmpty(token))
                {
                    //JObject jobj = RESTAPI.GetResponseFromEndPointRequestAccessToken(RestUrl, token.Split(' ')[1]);
                    JObject jobj = RESTAPI.GetResponseFromEndPointRequest(RestUrl, System.Net.CredentialCache.DefaultCredentials);
                    JArray jarr = (JArray)jobj["d"]["results"];
                    if (jarr.Count > 0)
                    {              
                        lst = jarr.ToObject<List<T>>();

                    }
                }
                else
                {
                    JObject jobj = RESTAPI.GetResponseFromEndPointRequest(RestUrl, System.Net.CredentialCache.DefaultCredentials);
                    JArray jarr = (JArray)jobj["d"]["results"];
                    if (jarr.Count > 0)
                    {
                        lst = jarr.ToObject<List<T>>();
                    }
                   
                }


                return lst;
            }
            catch (Exception ex)
            {
                string method = string.Concat(MethodBase.GetCurrentMethod().Name, " - Url - ", RestUrl);
                string guid = WriteException(ex, method, MethodBase.GetCurrentMethod().DeclaringType.Name);
                throw new Exception(string.Format(Messages.MsgExceptionOccured, guid));
            }
        }

        public static T GetPropertiesByRestURL<T>(string RestUrl, string token)
        {
            List<T> lst = new List<T>();
            try
            {
                if (!string.IsNullOrEmpty(token))
                {
                    JObject jobj = RESTAPI.GetResponseFromEndPointRequest(RestUrl, System.Net.CredentialCache.DefaultCredentials);
                    T result = jobj["d"].ToObject<T>();
                    return result;
                }
                else
                {
                    JObject jobj = RESTAPI.GetResponseFromEndPointRequest(RestUrl, System.Net.CredentialCache.DefaultCredentials);
                    T result = jobj["d"].ToObject<T>();
                    return result;
                }
            }
            catch (Exception ex)
            {
                string method = string.Concat(MethodBase.GetCurrentMethod().Name, " - Url - ", RestUrl);
                string guid = WriteException(ex, method, MethodBase.GetCurrentMethod().DeclaringType.Name);
                throw new Exception(string.Format(Messages.MsgExceptionOccured, guid));
            }
        }

        public static List<T> GetListOfChoicesByRestURL<T>(string RestUrl, string token)
        {
            List<T> lst = new List<T>();
            try
            {
                if (!string.IsNullOrEmpty(token))
                {
                    //JObject jobj = RESTAPI.GetResponseFromEndPointRequestAccessToken(RestUrl, token.Split(' ')[1]);
                    JObject jobj = RESTAPI.GetResponseFromEndPointRequest(RestUrl, System.Net.CredentialCache.DefaultCredentials);
                    JArray jarr = (JArray)jobj["d"]["results"];
                    if (jarr.Count > 0)
                    {
                        JArray jrr = (JArray)jarr[0]["Choices"]["results"];
                        lst = jrr.ToObject<List<T>>();
                    }
                    return lst;
                }
                else
                {
                    throw new ArgumentException("Invalid Token or Token is not available!");
                }
            }
            catch (Exception ex)
            {
                string method = string.Concat(MethodBase.GetCurrentMethod().Name, " - Url - ", RestUrl);
                string guid = WriteException(ex, method, MethodBase.GetCurrentMethod().DeclaringType.Name);
                throw new Exception(string.Format(Messages.MsgExceptionOccured, guid));
            }
        }

        public static async Task<List<T>> GetListByRestURLAsync<T>(string RestUrl, string token)
        {
            List<T> lst = new List<T>();
            try
            {
                if (!string.IsNullOrEmpty(token))
                {
                    //JObject jobj = RESTAPI.GetResponseFromEndPointRequestAccessToken(RestUrl, token.Split(' ')[1]);
                    JObject jobj = await RESTAPI.GetResponseFromEndPointRequestAsync(RestUrl, System.Net.CredentialCache.DefaultCredentials);
                    JArray jarr = (JArray)jobj["d"]["results"];
                    if (jarr.Count > 0)
                    {
                        lst = jarr.ToObject<List<T>>();
                    }
                    return lst;
                }
                else
                {
                    JObject jobj = await RESTAPI.GetResponseFromEndPointRequestAsync(RestUrl, System.Net.CredentialCache.DefaultCredentials);
                    JArray jarr = (JArray)jobj["d"]["results"];
                    if (jarr.Count > 0)
                    {
                        lst = jarr.ToObject<List<T>>();
                    }
                    return lst;
                }
            }
            catch (Exception ex)
            {
                string method = string.Concat(MethodBase.GetCurrentMethod().Name, " - Url - ", RestUrl);
                string guid = WriteException(ex, method, MethodBase.GetCurrentMethod().DeclaringType.Name);
                throw new Exception(string.Format(Messages.MsgExceptionOccured, guid));
            }
        }

        public static T GetObjectByRestURL<T>(string RestUrl, string token)
        {
            try
            {
                if (!string.IsNullOrEmpty(token))
                {
                    //JObject jobj = RESTAPI.GetResponseFromEndPointRequestAccessToken(RestUrl, token.Split(' ')[1]);
                    JObject jobj = RESTAPI.GetResponseFromEndPointRequest(RestUrl, System.Net.CredentialCache.DefaultCredentials);

                    var str = JsonConvert.SerializeObject(jobj["d"]);
                    T lst = JsonConvert.DeserializeObject<T>(str);

                    return lst;
                }
                else
                {
                    JObject jobj = RESTAPI.GetResponseFromEndPointRequest(RestUrl, System.Net.CredentialCache.DefaultCredentials);

                    var str = JsonConvert.SerializeObject(jobj["d"]);
                    T lst = JsonConvert.DeserializeObject<T>(str);

                    return lst;
                }
            }
            catch (Exception ex)
            {
                string method = string.Concat(MethodBase.GetCurrentMethod().Name, " - Url - ", RestUrl);
                string guid = WriteException(ex, method, MethodBase.GetCurrentMethod().DeclaringType.Name);
                throw new Exception(string.Format(Messages.MsgExceptionOccured, guid));
            }
        }

        public static Result AddListItem(string SiteUrl, string token, string listName, string itemPostBody)
        {

            Result res = new Result();
            try
            {
                if (!string.IsNullOrEmpty(token))
                {
                    string accessToken = token.Split(' ')[1];

                    //res.ReasonCode =RESTAPI.AddListItemsByAccessToken(SiteUrl, accessToken, errorLogPath, ListURLs.RestUrlList(listName),
                    //                   ListURLs.RestUrlListItemWithQuery(listName, false), itemPostBody);
                    res.ReasonCode = RESTAPI.AddListItemsByDefaultAdmin(SiteUrl, Constants.ErrorLogPath, ListURLs.RestUrlList(listName),
                                         ListURLs.RestUrlListItemWithQuery(listName, false), itemPostBody);
                }
                else
                {

                    res.ReasonCode = RESTAPI.AddListItemsByDefaultAdmin(SiteUrl, Constants.ErrorLogPath, ListURLs.RestUrlList(listName),
                                           ListURLs.RestUrlListItemWithQuery(listName, false), itemPostBody);
                }

                res.StatusCode = StatusCode.Success;
                res.Message = Messages.MsgSuccessAdd;

                return res;
            }
            catch (Exception ex)
            {
                string method = string.Concat(MethodBase.GetCurrentMethod().Name, " - Url - ", SiteUrl, ", ListName - ", listName, ", ItempostBody - ", itemPostBody);
                string guid = WriteException(ex, method, MethodBase.GetCurrentMethod().DeclaringType.Name);
                throw new Exception(string.Format(Messages.MsgExceptionOccured, guid));

            }
        }

        public static async Task<Result> AddListItemAsync(string SiteUrl, string token, string listName, string itemPostBody)
        {

            Result res = new Result();
            try
            {
                if (!string.IsNullOrEmpty(token))
                {
                    string accessToken = token.Split(' ')[1];

                    //res.ReasonCode =RESTAPI.AddListItemsByAccessToken(SiteUrl, accessToken, errorLogPath, ListURLs.RestUrlList(listName),
                    //                   ListURLs.RestUrlListItemWithQuery(listName, false), itemPostBody);
                    res.ReasonCode = await RESTAPI.AddListItemsByDefaultAdminAsync(SiteUrl, Constants.ErrorLogPath, ListURLs.RestUrlList(listName),
                                         ListURLs.RestUrlListItemWithQuery(listName, false), itemPostBody);
                }
                else
                {

                    res.ReasonCode = await RESTAPI.AddListItemsByDefaultAdminAsync(SiteUrl, Constants.ErrorLogPath, ListURLs.RestUrlList(listName),
                                           ListURLs.RestUrlListItemWithQuery(listName, false), itemPostBody);
                }

                res.StatusCode = StatusCode.Success;
                res.Message = Messages.MsgSuccessAdd;

                return res;
            }
            catch (Exception ex)
            {
                string method = string.Concat(MethodBase.GetCurrentMethod().Name, " - Url - ", SiteUrl, ", ListName - ", listName, ", ItempostBody - ", itemPostBody);
                string guid = WriteException(ex, method, MethodBase.GetCurrentMethod().DeclaringType.Name);
                throw new Exception(string.Format(Messages.MsgExceptionOccured, guid));

            }
        }

        public static Result AddListItem<T>(string SiteUrl, string token, string listName, T obj)
        {
            Result res = new Result();
            try
            {
                string id = string.Empty;
                string itemPostBody = GenerateJsonFromObject.GenerateJson<T>(obj, out id);

                return AddListItem(SiteUrl, token, listName, itemPostBody);
            }
            catch (Exception ex)
            {
                string method = string.Concat(MethodBase.GetCurrentMethod().Name, " - Url - ", SiteUrl, ", ListName - ", listName, ", ItempostBody - ", JsonConvert.SerializeObject(obj));
                string guid = WriteException(ex, method, MethodBase.GetCurrentMethod().DeclaringType.Name);
                throw new Exception(string.Format(Messages.MsgExceptionOccured, guid));

            }
        }

        public static Result AddDiscussionListItem<T>(string SiteUrl, string token, string listName, T obj, string ParentId)
        {
            Result res = new Result();
            try
            {
                string id = string.Empty;
                var messagePayload = "{'__metadata': { 'type': 'SP.Data.DiscussionsListItem' }, 'Body': 'Thanks for the information',  'FileSystemObjectType': 0, 'ContentTypeId': '0x0107008822E9328717EB48B3B665EE2266388E', 'ParentItemID': " + ParentId + "  }";
                string itemPostBody = GenerateJsonFromObject.GenerateJson<T>(obj, out id);

                return AddListItem(SiteUrl, token, listName, itemPostBody);
            }
            catch (Exception ex)
            {
                string method = string.Concat(MethodBase.GetCurrentMethod().Name, " - Url - ", SiteUrl, ", ListName - ", listName, ", ItempostBody - ", JsonConvert.SerializeObject(obj));
                string guid = WriteException(ex, method, MethodBase.GetCurrentMethod().DeclaringType.Name);
                throw new Exception(string.Format(Messages.MsgExceptionOccured, guid));
            }
        }

        public static async Task<Result> AddListItemAsync<T>(string SiteUrl, string token, string listName, T obj)
        {
            Result res = new Result();
            try
            {
                string id = string.Empty;
                string itemPostBody = GenerateJsonFromObject.GenerateJson<T>(obj, out id);

                return await AddListItemAsync(SiteUrl, token, listName, itemPostBody);
            }
            catch (Exception ex)
            {
                string method = string.Concat(MethodBase.GetCurrentMethod().Name, " - Url - ", SiteUrl, ", ListName - ", listName, ", ItempostBody - ", JsonConvert.SerializeObject(obj));
                string guid = WriteException(ex, method, MethodBase.GetCurrentMethod().DeclaringType.Name);
                throw new Exception(string.Format(Messages.MsgExceptionOccured, guid));

            }
        }

        public static Result AddListItemAttachments(string SiteUrl, string token, string listName, string itemID, HttpFileCollection attachedFiles)
        {

            Result res = new Result();
            try
            {
                if (!string.IsNullOrEmpty(token))
                {
                    string accessToken = token.Split(' ')[1];

                    res.ReasonCode = RESTAPI.AttachFilesToListItemByAccessToken(attachedFiles, SiteUrl, Constants.ErrorLogPath, accessToken, listName, itemID, true);
                }
                else
                {

                    res.ReasonCode = RESTAPI.AttachFilesToListItemByDefaultAdmin(attachedFiles, SiteUrl, Constants.ErrorLogPath, listName, itemID, true);
                }

                res.StatusCode = StatusCode.Success;
                res.Message = Messages.MsgSuccessAdd;

                return res;
            }
            catch (Exception ex)
            {
                string method = string.Concat(MethodBase.GetCurrentMethod().Name, " - Url - ", SiteUrl, ", ListName - ", listName, ", ItemID - ", itemID);
                string guid = WriteException(ex, method, MethodBase.GetCurrentMethod().DeclaringType.Name);
                throw new Exception(string.Format(Messages.MsgExceptionOccured, guid));

            }
        }

        public static async Task<Result> AddListItemAttachmentsAsync(string SiteUrl, string token, string listName, string itemID, HttpFileCollection attachedFiles)
        {

            Result res = new Result();
            try
            {
                if (!string.IsNullOrEmpty(token))
                {
                    string accessToken = token.Split(' ')[1];

                    res.ReasonCode = await RESTAPI.AttachFilesToListItemByAccessTokenAsync(attachedFiles, SiteUrl, Constants.ErrorLogPath, accessToken, listName, itemID, true);
                }
                else
                {

                    res.ReasonCode = await RESTAPI.AttachFilesToListItemByDefaultAdminAsync(attachedFiles, SiteUrl, Constants.ErrorLogPath, listName, itemID, true);
                }

                res.StatusCode = StatusCode.Success;
                res.Message = Messages.MsgSuccessAdd;

                return res;
            }
            catch (Exception ex)
            {
                string method = string.Concat(MethodBase.GetCurrentMethod().Name, " - Url - ", SiteUrl, ", ListName - ", listName, ", ItemID - ", itemID);
                string guid = WriteException(ex, method, MethodBase.GetCurrentMethod().DeclaringType.Name);
                throw new Exception(string.Format(Messages.MsgExceptionOccured, guid));

            }
        }

        public static Result AddListItemAttachments(string SiteUrl, string token, string listName, string itemID, byte[] byteArray, string fileName)
        {

            Result res = new Result();
            try
            {
                if (!string.IsNullOrEmpty(token))
                {
                    string accessToken = token.Split(' ')[1];

                    res.ReasonCode = RESTAPI.AttachFilesToListItemByDefaultAdmin(byteArray, fileName, SiteUrl, Constants.ErrorLogPath, listName, itemID, true);
                }
                else
                {
                    res.ReasonCode = RESTAPI.AttachFilesToListItemByDefaultAdmin(byteArray, fileName, SiteUrl, Constants.ErrorLogPath, listName, itemID, true);
                }

                res.StatusCode = StatusCode.Success;
                res.Message = Messages.MsgSuccessAdd;

                return res;
            }
            catch (Exception ex)
            {
                string method = string.Concat(MethodBase.GetCurrentMethod().Name, " - Url - ", SiteUrl, ", ListName - ", listName, ", ItemID - ", itemID, ", FileName - ", fileName);
                string guid = WriteException(ex, method, MethodBase.GetCurrentMethod().DeclaringType.Name);
                throw new Exception(string.Format(Messages.MsgExceptionOccured, guid));

            }
        }

        public static async Task<Result> AddListItemAttachmentsAsync(string SiteUrl, string token, string listName, string itemID, byte[] byteArray, string fileName)
        {

            Result res = new Result();
            try
            {
                if (!string.IsNullOrEmpty(token))
                {
                    string accessToken = token.Split(' ')[1];

                    res.ReasonCode = await RESTAPI.AttachFilesToListItemByDefaultAdminAsync(byteArray, fileName, SiteUrl, Constants.ErrorLogPath, listName, itemID, true);
                }
                else
                {

                    res.ReasonCode = await RESTAPI.AttachFilesToListItemByDefaultAdminAsync(byteArray, fileName, SiteUrl, Constants.ErrorLogPath, listName, itemID, true);
                }

                res.StatusCode = StatusCode.Success;
                res.Message = Messages.MsgSuccessAdd;

                return res;
            }
            catch (Exception ex)
            {
                string method = string.Concat(MethodBase.GetCurrentMethod().Name, " - Url - ", SiteUrl, ", ListName - ", listName, ", ItemID - ", itemID, ", FileName - ", fileName);
                string guid = WriteException(ex, method, MethodBase.GetCurrentMethod().DeclaringType.Name);
                throw new Exception(string.Format(Messages.MsgExceptionOccured, guid));

            }
        }

        public static async Task<Result> DeleteAttachmentFromListByDefaultAdminAsync(string SiteUrl, string relativeUrl, string token, string listName, string itemID, string fileName)
        {

            Result res = new Result();
            try
            {
                if (!string.IsNullOrEmpty(token))
                {
                    string accessToken = token.Split(' ')[1];

                    res.ReasonCode = await RESTAPI.DeleteAttachmentFromListByDefaultAdminAsync(fileName, SiteUrl, relativeUrl, Constants.ErrorLogPath, listName, itemID);
                }
                else
                {

                    res.ReasonCode = await RESTAPI.DeleteAttachmentFromListByDefaultAdminAsync(fileName, SiteUrl, relativeUrl, Constants.ErrorLogPath, listName, itemID);
                }

                res.StatusCode = StatusCode.Success;
                res.Message = Messages.MsgSuccessAdd;

                return res;
            }
            catch (Exception ex)
            {
                string method = string.Concat(MethodBase.GetCurrentMethod().Name, " - Url - ", SiteUrl, ", ListName - ", listName, ", ItemID - ", itemID, ", FileName - ", fileName);
                string guid = WriteException(ex, method, MethodBase.GetCurrentMethod().DeclaringType.Name);
                throw new Exception(string.Format(Messages.MsgExceptionOccured, guid));

            }
        }

        public static Result DeleteAttachmentFromListByDefaultAdmin(string SiteUrl, string relativeUrl, string token, string listName, string itemID, string fileName)
        {

            Result res = new Result();
            try
            {
                if (!string.IsNullOrEmpty(token))
                {
                    string accessToken = token.Split(' ')[1];

                    res.ReasonCode = RESTAPI.DeleteAttachmentFromListByDefaultAdmin(fileName, SiteUrl, relativeUrl, Constants.ErrorLogPath, listName, itemID);
                }
                else
                {

                    res.ReasonCode = RESTAPI.DeleteAttachmentFromListByDefaultAdmin(fileName, SiteUrl, relativeUrl, Constants.ErrorLogPath, listName, itemID);
                }

                res.StatusCode = StatusCode.Success;
                res.Message = Messages.MsgSuccessAdd;

                return res;
            }
            catch (Exception ex)
            {
                string method = string.Concat(MethodBase.GetCurrentMethod().Name, " - Url - ", SiteUrl, ", ListName - ", listName, ", ItemID - ", itemID, ", FileName - ", fileName);
                string guid = WriteException(ex, method, MethodBase.GetCurrentMethod().DeclaringType.Name);
                throw new Exception(string.Format(Messages.MsgExceptionOccured, guid));

            }
        }

        public static Result UpdateListItem(string SiteUrl, string token, string listName, string itemPostBody, string id)
        {
            Result res = new Result();
            try
            {
                if (!string.IsNullOrEmpty(token))
                {
                    string accessToken = token.Split(' ')[1];

                    //RESTAPI.UpdateListItemsByAccessToken(SiteUrl, accessToken, Constants.ErrorLogPath, ListURLs.RestUrlList(listName),
                    //                   ListURLs.RestUrlListItemWithQuery(listName, false), itemPostBody, id);
                    RESTAPI.UpdateListItemsByDefaultAdmin(SiteUrl, Constants.ErrorLogPath, ListURLs.RestUrlList(listName),
                                    ListURLs.RestUrlListItemWithQuery(listName, false), itemPostBody, id);

                }
                else
                {

                    RESTAPI.UpdateListItemsByDefaultAdmin(SiteUrl, Constants.ErrorLogPath, ListURLs.RestUrlList(listName),
                                        ListURLs.RestUrlListItemWithQuery(listName, false), itemPostBody, id);
                }

                res.StatusCode = StatusCode.Success;
                res.Message = Messages.MsgSuccessUpdate;

                return res;
            }
            catch (Exception ex)
            {
                string method = string.Concat(MethodBase.GetCurrentMethod().Name, " - Url - ", SiteUrl, ", ListName - ", listName, ", ItemPostBody - ", itemPostBody, ", Id - ", id);
                string guid = WriteException(ex, method, MethodBase.GetCurrentMethod().DeclaringType.Name);
                throw new Exception(string.Format(Messages.MsgExceptionOccured, guid));

            }
        }

        public static Result MoveItemFolder(string SiteUrl, string token, string FileServerRelativeUrl, string MoveToUrl)
        {
            Result res = new Result();
            try
            {
                bool isSuccess = false;
                if (!string.IsNullOrEmpty(token))
                {
                    string accessToken = token.Split(' ')[1];

                    isSuccess = RESTAPI.MoveFolder(SiteUrl, token, FileServerRelativeUrl, MoveToUrl);
                    //RESTAPI.UpdateListItemsByDefaultAdmin(SiteUrl, Constants.ErrorLogPath, ListURLs.RestUrlList(listName),
                    //                ListURLs.RestUrlListItemWithQuery(listName, false), itemPostBody, id);

                }
                else
                {

                    //RESTAPI.UpdateListItemsByDefaultAdmin(SiteUrl, Constants.ErrorLogPath, ListURLs.RestUrlList(listName),
                    //                    ListURLs.RestUrlListItemWithQuery(listName, false), itemPostBody, id);
                }
                if (isSuccess)
                {
                    res.StatusCode = StatusCode.Success;
                    //res.Message = Messages.MsgSuccessUpdate;
                }
                else
                {
                    res.StatusCode = StatusCode.Error;
                    //res.Message = Messages.MsgSuccessUpdate;
                }


                return res;
            }
            catch (Exception ex)
            {
                string method = string.Concat(MethodBase.GetCurrentMethod().Name, " - Url - ", SiteUrl);
                string guid = WriteException(ex, method, MethodBase.GetCurrentMethod().DeclaringType.Name);
                throw new Exception(string.Format(Messages.MsgExceptionOccured, guid));

            }
        }

        public static List<T> GetPublishingImages<T>(string SiteUrl, string token, string FolderServerRelativeUrl)
        {
            List<T> lst = new List<T>();
            try
            {
                if (!string.IsNullOrEmpty(token))
                {
                    JObject jobj = RESTAPI.GetPublishingImages(SiteUrl, token, FolderServerRelativeUrl);
                    if (jobj == null) { return null; }
                    JArray jarr = (JArray)jobj["d"]["Files"]["results"];
                    if (jarr.Count > 0)
                    {
                        lst = jarr.ToObject<List<T>>();
                    }
                    return lst;
                }
                else
                {
                    JObject jobj = RESTAPI.GetPublishingImages(SiteUrl, "token", FolderServerRelativeUrl);
                    if (jobj == null) { return null; }
                    JArray jarr = (JArray)jobj["d"]["Files"]["results"];
                    if (jarr.Count > 0)
                    {
                        lst = jarr.ToObject<List<T>>();
                    }
                    return lst;
                }
            }
            catch (Exception ex)
            {
                string method = string.Concat(MethodBase.GetCurrentMethod().Name, " - Url - ", SiteUrl);
                string guid = WriteException(ex, method, MethodBase.GetCurrentMethod().DeclaringType.Name);
                throw new Exception(string.Format(Messages.MsgExceptionOccured, guid));
            }
        }

        public static async Task<Result> UpdateListItemAsync(string SiteUrl, string token, string listName, string itemPostBody, string id)
        {
            Result res = new Result();
            try
            {
                if (!string.IsNullOrEmpty(token))
                {
                    string accessToken = token.Split(' ')[1];

                    //RESTAPI.UpdateListItemsByAccessToken(SiteUrl, accessToken, errorLogPath, ListURLs.RestUrlList(listName),
                    //                   ListURLs.RestUrlListItemWithQuery(listName, false), itemPostBody, id);
                    res.ReasonCode = await RESTAPI.UpdateListItemsByDefaultAdminAsync(SiteUrl, Constants.ErrorLogPath, ListURLs.RestUrlList(listName),
                                    ListURLs.RestUrlListItemWithQuery(listName, false), itemPostBody, id);

                }
                else
                {

                    res.ReasonCode = await RESTAPI.UpdateListItemsByDefaultAdminAsync(SiteUrl, Constants.ErrorLogPath, ListURLs.RestUrlList(listName),
                                        ListURLs.RestUrlListItemWithQuery(listName, false), itemPostBody, id);
                }

                res.StatusCode = StatusCode.Success;
                res.Message = Messages.MsgSuccessUpdate;

                return res;
            }
            catch (Exception ex)
            {
                string method = string.Concat(MethodBase.GetCurrentMethod().Name, " - Url - ", SiteUrl, ", ListName - ", listName, ", ItemPostBody - ", itemPostBody, ", Id - ", id);
                string guid = WriteException(ex, method, MethodBase.GetCurrentMethod().DeclaringType.Name);
                throw new Exception(string.Format(Messages.MsgExceptionOccured, guid));

            }
        }

        public static Result UpdateListItem<T>(string SiteUrl, string token, string listName, T obj)
        {
            Result res = new Result();

            try
            {
                string id = string.Empty;
                string itemPostBody = GenerateJsonFromObject.GenerateJson<T>(obj, out id);
                return UpdateListItem(SiteUrl, token, listName, itemPostBody, id);
            }
            catch (Exception ex)
            {
                string method = string.Concat(MethodBase.GetCurrentMethod().Name, " - Url - ", SiteUrl, ", ListName - ", listName, ", ItemPostBody - ", JsonConvert.SerializeObject(obj));
                string guid = WriteException(ex, method, MethodBase.GetCurrentMethod().DeclaringType.Name);
                throw new Exception(string.Format(Messages.MsgExceptionOccured, guid));
            }
        }

        public static async Task<Result> UpdateListItemAsync<T>(string SiteUrl, string token, string listName, T obj)
        {
            Result res = new Result();
            try
            {
                string id = string.Empty;
                string itemPostBody = GenerateJsonFromObject.GenerateJson<T>(obj, out id);
                return await UpdateListItemAsync(SiteUrl, token, listName, itemPostBody, id);
            }
            catch (Exception ex)
            {
                string method = string.Concat(MethodBase.GetCurrentMethod().Name, " - Url - ", SiteUrl, ", ListName - ", listName, ", ItemPostBody - ", JsonConvert.SerializeObject(obj));
                string guid = WriteException(ex, method, MethodBase.GetCurrentMethod().DeclaringType.Name);
                throw new Exception(string.Format(Messages.MsgExceptionOccured, guid));
            }
        }

        public static Result DeleteListItem(string SiteUrl, string listName, string id)
        {
            Result res = new Result();
            try
            {
                RESTAPI.DeleteListItems(SiteUrl, Constants.ErrorLogPath, ListURLs.RestUrlList(listName),
                                        ListURLs.RestUrlListItemWithQuery(listName, false), id);

                res.StatusCode = StatusCode.Success;
                res.Message = Messages.MsgSuccessDelete;

                return res;
            }
            catch (Exception ex)
            {
                string method = string.Concat(MethodBase.GetCurrentMethod().Name, " - Url - ", SiteUrl, ", ListName - ", listName, ", Id - ", id);
                string guid = WriteException(ex, method, MethodBase.GetCurrentMethod().DeclaringType.Name);
                throw new Exception(string.Format(Messages.MsgExceptionOccured, guid));

            }
        }

        public static async Task<Result> DeleteListItemAsync(string SiteUrl, string listName, string id)
        {
            Result res = new Result();
            try
            {
                await RESTAPI.DeleteListItemsAsync(SiteUrl, Constants.ErrorLogPath, ListURLs.RestUrlList(listName),
                                        ListURLs.RestUrlListItemWithQuery(listName, false), id);

                res.StatusCode = StatusCode.Success;
                res.Message = Messages.MsgSuccessDelete;

                return res;
            }
            catch (Exception ex)
            {
                string method = string.Concat(MethodBase.GetCurrentMethod().Name, " - Url - ", SiteUrl, ", ListName - ", listName, ", Id - ", id);
                string guid = WriteException(ex, method, MethodBase.GetCurrentMethod().DeclaringType.Name);
                throw new Exception(string.Format(Messages.MsgExceptionOccured, guid));

            }
        }

        //public static string UploadFilesDirectlyInLibrary(HttpFileCollection uploadedFiles, string siteUrl, string errorLogPath, string accessToken, string documentLibUrl, string documentLibListUrl, string folderName, bool overwrite, string itemPostBody, out string fileName, out int id)
        //{
        //    string filePath = string.Empty;
        //    fileName = string.Empty;
        //    int fileId = 0;
        //    try
        //    {

        //        filePath = RESTAPI.UploadFilesDirectlyInLibrary(uploadedFiles, siteUrl, string.Empty, string.Empty, Lists.TRDocuments,
        //                                                        ListURLs.RestUrlList(Lists.TRDocuments), string.Empty,
        //                                                        Convert.ToBoolean(HttpContext.Current.Request.Form["Overwrite"]),
        //                                                        itemPostBody, out fileName, out id);
        //        fileId = id;
        //    }
        //    catch (Exception ex)
        //    {
        //        string method = string.Concat(MethodBase.GetCurrentMethod().Name, " - Library Url - ", documentLibUrl, ", List Url - ", documentLibListUrl, ", ItemPostBody - ", itemPostBody, ", folderName - ", folderName);
        //        string guid = WriteException(ex, method, MethodBase.GetCurrentMethod().DeclaringType.Name);
        //        throw new Exception(string.Format("An error occured while reading data. GUID: {0}", guid));
        //    }
        //    return filePath;

        //}

        public static string RetrieveFileBinary(string fileUrl)
        {
            try
            {
                return RESTAPI.RetrieveFileBinary(fileUrl);
            }
            catch (Exception ex)
            {
                string method = string.Concat(MethodBase.GetCurrentMethod().Name, " - Url - ", fileUrl);
                string guid = WriteException(ex, method, MethodBase.GetCurrentMethod().DeclaringType.Name);
                throw new Exception(string.Format("An error occured while reading data. GUID: {0}", guid));
            }

        }

        public static byte[] RetrieveFileByteViaREST(string fileUrl)
        {

            try
            {
                return RESTAPI.RetrieveFileByteViaREST(fileUrl);
            }
            catch (Exception ex)
            {
                string method = string.Concat(MethodBase.GetCurrentMethod().Name, " - Url - ", fileUrl);
                string guid = WriteException(ex, method, MethodBase.GetCurrentMethod().DeclaringType.Name);
                throw new Exception(string.Format("An error occured while reading data. GUID: {0}", guid));
            }

        }

        public static List<T> GetDataFromCAMLQueryPOST<T>(string RestUrl, string itempostBody, string siteUrl, string token)
        {
            List<T> lst = new List<T>();
            try
            {
                if (!string.IsNullOrEmpty(token))
                {
                    //JObject jobj = RESTAPI.GetListItemsUsingPost(siteUrl, Constants.ErrorLogPath,token.Split(' ')[1],RestUrl,itempostBody);
                    JObject jobj = RESTAPI.GetListItemsUsingPost(siteUrl, Constants.ErrorLogPath, RestUrl, itempostBody);
                    JArray jarr = (JArray)jobj["d"]["results"];
                    if (jarr.Count > 0)
                    {
                        lst = jarr.ToObject<List<T>>();
                    }
                    return lst;
                }
                else
                {
                    JObject jobj = RESTAPI.GetListItemsUsingPost(siteUrl, Constants.ErrorLogPath, RestUrl, itempostBody);
                    JArray jarr = (JArray)jobj["d"]["results"];
                    if (jarr.Count > 0)
                    {
                        lst = jarr.ToObject<List<T>>();
                    }
                    return lst;
                }
            }
            catch (Exception ex)
            {
                string method = string.Concat(MethodBase.GetCurrentMethod().Name, " - Url - ", RestUrl, ", PostBodyData - ", itempostBody, ", SiteUrl - ", siteUrl);
                string guid = WriteException(ex, method, MethodBase.GetCurrentMethod().DeclaringType.Name);
                throw new Exception(string.Format("An error occured while reading data. GUID: {0}", guid));
            }
        }

        public static string WriteException(Exception ex, string methodName, string className)
        {
            try
            {
                string userName = string.Empty;
                try
                {
                    userName = GetUserInfo.UserName;
                }
                catch (Exception ex1)
                {
                }

                return RESTAPI.WriteException(ex, methodName, className, userName);
            }
            catch (Exception ex2)
            {
                throw;
            }
        }

    }
}
