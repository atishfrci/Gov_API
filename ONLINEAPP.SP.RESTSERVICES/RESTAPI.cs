//using BAL.LOGGERANDMAILER;
//using BAL.LOGGERANDMAILER;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml;

namespace ONLINEAPP.SP.RESTSERVICES
{
    public static class RESTAPI
    {
        /// <summary>
        /// Add items to list
        /// </summary>
        /// <param name="siteUrl"></param>
        /// <param name="errorLogPath"></param>
        /// <param name="accessToken"></param>
        /// <param name="ListUrl"></param>
        /// <param name="ListItemUrl"></param>
        /// <param name="itemPostBody"></param>
        /// <param name="xmlnspm"></param>
        /// <param name="formDigestNode"></param>
        /// <returns></returns>
        public static string AddListItemsByAccessToken(string siteUrl, string accessToken, string errorLogPath, string listUrl, string listItemUrl, string itemPostBody)
        {
            string ID = string.Empty;
            try
            {
                XmlNamespaceManager xmlnspm = AddXmlNameSpaces();
                var formDigestNode = GetFormDigest(siteUrl, accessToken, xmlnspm);

                //itemPostBody = itemPostBody.Remove(itemPostBody.LastIndexOf('}'));
                //itemPostBody = string.Concat(itemPostBody, ",'CreatedById':", userID, ",'ModifiedById':", userID, "}");
                string FormDigest = formDigestNode.InnerXml;
                string EntityTypeName = GetEntityTypeName(siteUrl, listUrl, xmlnspm);
                itemPostBody = itemPostBody.Replace("{0}", EntityTypeName);
                //Byte[] itemPostData = System.Text.Encoding.ASCII.GetBytes(itemPostBody);
                Byte[] itemPostData = System.Text.Encoding.UTF8.GetBytes(itemPostBody);
                HttpWebRequest ItemRequest;
                ItemRequest = (HttpWebRequest)HttpWebRequest.Create(string.Concat(siteUrl, listItemUrl));
                ItemRequest.Method = "POST";
                // ItemRequest.ContentLength = itemPostBody.Length;
                ItemRequest.ContentLength = itemPostData.Length;
                ItemRequest.ContentType = "application/json;odata=verbose;charset=utf-8";
                ItemRequest.Accept = "application/json;odata=verbose";
                ItemRequest.Headers.Add("Authorization", "Bearer " + accessToken);
                //ItemRequest.Credentials = System.Net.CredentialCache.DefaultCredentials;

                ItemRequest.Headers.Add("X-RequestDigest", FormDigest);
                using (Stream itemRequestStream = ItemRequest.GetRequestStream())
                {
                    itemRequestStream.Write(itemPostData, 0, itemPostData.Length);
                    itemRequestStream.Close();
                }
                using (HttpWebResponse itemResponse = (HttpWebResponse)ItemRequest.GetResponse())
                {
                    using (StreamReader responseReader = new StreamReader(itemResponse.GetResponseStream()))
                    {
                        string response = responseReader.ReadToEnd();
                        JObject jobj = JObject.Parse(response);
                        ID = Convert.ToString(jobj["d"]["Id"]);
                    }
                }
                //ItemRequest.KeepAlive = false;
                return ID;
            }
            catch (Exception ex)
            {
                string method = string.Concat(MethodBase.GetCurrentMethod().Name, " - Url - ", siteUrl, ", ListName - ", listUrl, ", ItemPostBody - ", itemPostBody);
                string guid = WriteException(ex, method, MethodBase.GetCurrentMethod().DeclaringType.Name, string.Empty);
                throw new Exception(string.Format("An error occured while reading data. GUID: {0}", guid));
            }
        }

        public static string AddListItemsByDefaultAdmin(string siteUrl, string errorLogPath, string listUrl, string listItemUrl, string itemPostBody)
        {
            string ID = string.Empty;
            try
            {
                XmlNamespaceManager xmlnspm = AddXmlNameSpaces();
                var formDigestNode = GetFormDigest(siteUrl, xmlnspm);
                string FormDigest = formDigestNode.InnerXml;
                string EntityTypeName = GetEntityTypeName(siteUrl, listUrl, xmlnspm);
                itemPostBody = itemPostBody.Replace("{0}", EntityTypeName);
                Byte[] itemPostData = System.Text.Encoding.UTF8.GetBytes(itemPostBody);
                HttpWebRequest ItemRequest;
                ItemRequest = (HttpWebRequest)HttpWebRequest.Create(string.Concat(siteUrl, listItemUrl));
                ItemRequest.Method = "POST";
                //ItemRequest.ContentLength = itemPostBody.Length;
                ItemRequest.ContentLength = itemPostData.Length;
                ItemRequest.ContentType = "application/json;odata=verbose;charset=UTF-8";
                ItemRequest.Accept = "application/json;odata=verbose";
                //ItemRequest.Headers.Add("Authorization", "Bearer " + accessToken);
                ItemRequest.Credentials = System.Net.CredentialCache.DefaultCredentials;
                ItemRequest.Headers.Add("X-RequestDigest", FormDigest);
                using (Stream itemRequestStream = ItemRequest.GetRequestStream())
                {
                    itemRequestStream.Write(itemPostData, 0, itemPostData.Length);
                    itemRequestStream.Close();
                }
                using (HttpWebResponse itemResponse = (HttpWebResponse)ItemRequest.GetResponse())
                {
                    using (StreamReader responseReader = new StreamReader(itemResponse.GetResponseStream()))
                    {
                        string response = responseReader.ReadToEnd();
                        JObject jobj = JObject.Parse(response);
                        ID = Convert.ToString(jobj["d"]["Id"]);
                    }
                }
                return ID;
            }
            catch (WebException ex)
            {
                string method = string.Concat(MethodBase.GetCurrentMethod().Name, " - Url - ", siteUrl, ", ListName - ", listUrl, ", ItemPostBody - ", itemPostBody);
                string guid = WebException(ex, method);
                throw new Exception(string.Format("An error occured while reading data. GUID: {0}", guid));
            }
            catch (Exception ex)
            {
                string method = string.Concat(MethodBase.GetCurrentMethod().Name, " - Url - ", siteUrl, ", ListName - ", listUrl, ", ItemPostBody - ", itemPostBody);
                string guid = WriteException(ex, method, MethodBase.GetCurrentMethod().DeclaringType.Name, string.Empty);
                throw new Exception(string.Format("An error occured while reading data. GUID: {0}", guid));
            }
        }

        //protected override System.Net.WebRequest GetWebRequest(Uri uri)
        //{
        //    System.Net.HttpWebRequest webRequest = (System.Net.HttpWebRequest)base.GetWebRequest(uri);
        //    webRequest.KeepAlive = false;
        //    return webRequest;
        //}


        public static async Task<string> AddListItemsByDefaultAdminAsync(string siteUrl, string errorLogPath, string listUrl, string listItemUrl, string itemPostBody)
        {
            string ID = string.Empty;
            try
            {
                XmlNamespaceManager xmlnspm = AddXmlNameSpaces();
                var formDigestNode = GetFormDigest(siteUrl, xmlnspm);

                //itemPostBody = itemPostBody.Remove(itemPostBody.LastIndexOf('}'));
                //itemPostBody = string.Concat(itemPostBody, ",'CreatedById':", userID, ",'ModifiedById':", userID, "}");
                string FormDigest = formDigestNode.InnerXml;
                string EntityTypeName = GetEntityTypeName(siteUrl, listUrl, xmlnspm);
                itemPostBody = itemPostBody.Replace("{0}", EntityTypeName);
                Byte[] itemPostData = System.Text.Encoding.ASCII.GetBytes(itemPostBody);
                HttpWebRequest ItemRequest;
                ItemRequest = (HttpWebRequest)HttpWebRequest.Create(string.Concat(siteUrl, listItemUrl));
                ItemRequest.Method = "POST";
                ItemRequest.ContentLength = itemPostBody.Length;
                ItemRequest.ContentType = "application/json;odata=verbose;charset=utf-8";
                ItemRequest.Accept = "application/json;odata=verbose";
                //ItemRequest.Headers.Add("Authorization", "Bearer " + accessToken);
                ItemRequest.Credentials = System.Net.CredentialCache.DefaultCredentials;

                ItemRequest.Headers.Add("X-RequestDigest", FormDigest);
                using (Stream itemRequestStream = ItemRequest.GetRequestStream())
                {
                    itemRequestStream.Write(itemPostData, 0, itemPostData.Length);
                    itemRequestStream.Close();
                }
                using (HttpWebResponse itemResponse = (HttpWebResponse)ItemRequest.GetResponse())
                {
                    using (StreamReader responseReader = new StreamReader(itemResponse.GetResponseStream()))
                    {
                        string response = responseReader.ReadToEnd();
                        JObject jobj = JObject.Parse(response);
                        ID = Convert.ToString(jobj["d"]["Id"]);
                    }
                }
                //ItemRequest.KeepAlive = false;
                return ID;
            }
            catch (Exception ex)
            {
                string method = string.Concat(MethodBase.GetCurrentMethod().Name, " - Url - ", siteUrl, ", ListName - ", listUrl, ", ItemPostBody - ", itemPostBody);
                string guid = WriteException(ex, method, MethodBase.GetCurrentMethod().DeclaringType.Name, string.Empty);
                throw new Exception(string.Format("An error occured while reading data. GUID: {0}", guid));
            }
        }

        public static string AddListItemsByRequestDigest(string siteUrl, string requestDigest, string errorLogPath, string listUrl, string listItemUrl, string itemPostBody)
        {
            string ID = string.Empty;
            try
            {
                XmlNamespaceManager xmlnspm = AddXmlNameSpaces();
                //var formDigestNode = GetFormDigest(siteUrl, xmlnspm);

                //itemPostBody = itemPostBody.Remove(itemPostBody.LastIndexOf('}'));
                //itemPostBody = string.Concat(itemPostBody, ",'CreatedById':", userID, ",'ModifiedById':", userID, "}");
                string FormDigest = requestDigest;
                string EntityTypeName = GetEntityTypeName(siteUrl, listUrl, xmlnspm);
                itemPostBody = itemPostBody.Replace("{0}", EntityTypeName);
                Byte[] itemPostData = System.Text.Encoding.ASCII.GetBytes(itemPostBody);
                HttpWebRequest ItemRequest;
                ItemRequest = (HttpWebRequest)HttpWebRequest.Create(string.Concat(siteUrl, listItemUrl));
                ItemRequest.Method = "POST";
                ItemRequest.Headers.Add("X-HTTP-Method", "MERGE");
                ItemRequest.Headers.Add("If-Match", "*");
                ItemRequest.ContentLength = itemPostBody.Length;
                ItemRequest.ContentType = "application/json;odata=verbose;charset=UTF-8";
                ItemRequest.Accept = "application/json;odata=verbose";
                //ItemRequest.Headers.Add("Authorization", "Bearer " + accessToken);
                //ItemRequest.Credentials = System.Net.CredentialCache.DefaultCredentials;

                ItemRequest.Headers.Add("X-RequestDigest", FormDigest);
                using (Stream itemRequestStream = ItemRequest.GetRequestStream())
                {
                    itemRequestStream.Write(itemPostData, 0, itemPostData.Length);
                    itemRequestStream.Close();
                }
                using (HttpWebResponse itemResponse = (HttpWebResponse)ItemRequest.GetResponse())
                {
                    using (StreamReader responseReader = new StreamReader(itemResponse.GetResponseStream()))
                    {
                        string response = responseReader.ReadToEnd();
                        JObject jobj = JObject.Parse(response);
                        ID = Convert.ToString(jobj["d"]["Id"]);
                    }
                }
                //ItemRequest.KeepAlive = false;
                return ID;
            }
            catch (WebException ex)
            {
                string method = string.Concat(MethodBase.GetCurrentMethod().Name, " - Url - ", siteUrl, ", ListName - ", listUrl, ", ItemPostBody - ", itemPostBody);
                string guid = WebException(ex, method);
                throw new Exception(string.Format("An error occured while reading data. GUID: {0}", guid));
            }
            catch (Exception ex)
            {
                string method = string.Concat(MethodBase.GetCurrentMethod().Name, " - Url - ", siteUrl, ", ListName - ", listUrl, ", ItemPostBody - ", itemPostBody);
                string guid = WriteException(ex, method, MethodBase.GetCurrentMethod().DeclaringType.Name, string.Empty);
                throw new Exception(string.Format("An error occured while reading data. GUID: {0}", guid));
            }
        }

        public static string UpdateListItemsByRequestDigest(string siteUrl, string requestDigest, string errorLogPath, string listUrl, string listItemUrl, string itemPostBody, string id)
        {
            string ID = string.Empty;
            try
            {
                XmlNamespaceManager xmlnspm = AddXmlNameSpaces();
                //var formDigestNode = GetFormDigest(siteUrl, accessToken, xmlnspm);
                //itemPostBody = itemPostBody.Remove(itemPostBody.LastIndexOf('}'));
                //itemPostBody = string.Concat(itemPostBody, ",'ModifiedById':", userID, "}");
                string FormDigest = requestDigest;
                string EntityTypeName = GetEntityTypeName(siteUrl, listUrl, xmlnspm);

                itemPostBody = itemPostBody.Replace("{0}", EntityTypeName);
                Byte[] itemPostData = System.Text.Encoding.ASCII.GetBytes(itemPostBody);

                HttpWebRequest ItemRequest =
                        (HttpWebRequest)HttpWebRequest.Create(string.Concat(siteUrl, listItemUrl, "(", id, ")"));
                ItemRequest.Method = "POST";
                ItemRequest.ContentLength = itemPostBody.Length;
                ItemRequest.Headers.Add("X-HTTP-Method", "MERGE");
                ItemRequest.Headers.Add("IF-MATCH", "*");
                ItemRequest.ContentType = "application/json;odata=verbose;charset=utf-8";
                ItemRequest.Accept = "application/json;odata=verbose";
                //ItemRequest.Headers.Add("Authorization", "Bearer " + accessToken);
                //ItemRequest.Credentials = System.Net.CredentialCache.DefaultCredentials;

                ItemRequest.Headers.Add("X-RequestDigest", FormDigest);
                using (Stream itemRequestStream = ItemRequest.GetRequestStream())
                {
                    itemRequestStream.Write(itemPostData, 0, itemPostData.Length);
                    itemRequestStream.Close();
                }
                using (HttpWebResponse itemResponse = (HttpWebResponse)ItemRequest.GetResponse()) ;
                //ItemRequest.KeepAlive = false;
                return null;
            }
            catch (WebException ex)
            {
                string method = string.Concat(MethodBase.GetCurrentMethod().Name, " - Url - ", siteUrl, ", ListName - ", listUrl, ", ItemPostBody - ", itemPostBody);
                string guid = WebException(ex, method);
                throw new Exception(string.Format("An error occured while reading data. GUID: {0}", guid));
            }
            catch (Exception ex)
            {
                string method = string.Concat(MethodBase.GetCurrentMethod().Name, " - Url - ", siteUrl, ", ListName - ", listUrl, ", ItemPostBody - ", itemPostBody);
                string guid = WriteException(ex, method, MethodBase.GetCurrentMethod().DeclaringType.Name, string.Empty);
                throw new Exception(string.Format("An error occured while reading data. GUID: {0}", guid));
            }
        }

        public static string UpdateListItemsByAccessToken(string siteUrl, string accessToken, string errorLogPath, string listUrl, string listItemUrl, string itemPostBody, string id)
        {
            try
            {
                XmlNamespaceManager xmlnspm = AddXmlNameSpaces();
                var formDigestNode = GetFormDigest(siteUrl, accessToken, xmlnspm);
                //itemPostBody = itemPostBody.Remove(itemPostBody.LastIndexOf('}'));
                //itemPostBody = string.Concat(itemPostBody, ",'ModifiedById':", userID, "}");
                string FormDigest = formDigestNode.InnerXml;
                string EntityTypeName = GetEntityTypeName(siteUrl, listUrl, xmlnspm);

                itemPostBody = itemPostBody.Replace("{0}", EntityTypeName);
                Byte[] itemPostData = System.Text.Encoding.ASCII.GetBytes(itemPostBody);

                HttpWebRequest ItemRequest =
                        (HttpWebRequest)HttpWebRequest.Create(string.Concat(siteUrl, listItemUrl, "(", id, ")"));
                ItemRequest.Method = "POST";
                ItemRequest.ContentLength = itemPostBody.Length;
                ItemRequest.Headers.Add("X-HTTP-Method", "MERGE");
                ItemRequest.Headers.Add("IF-MATCH", "*");
                ItemRequest.ContentType = "application/json;odata=verbose;charset=utf-8";
                ItemRequest.Accept = "application/json;odata=verbose";
                ItemRequest.Headers.Add("Authorization", "Bearer " + accessToken);
                //ItemRequest.Credentials = System.Net.CredentialCache.DefaultCredentials;

                ItemRequest.Headers.Add("X-RequestDigest", FormDigest);
                using (Stream itemRequestStream = ItemRequest.GetRequestStream())
                {
                    itemRequestStream.Write(itemPostData, 0, itemPostData.Length);
                    itemRequestStream.Close();
                }
                using (HttpWebResponse itemResponse = (HttpWebResponse)ItemRequest.GetResponse()) ;
                //ItemRequest.KeepAlive = false;
                return null;
            }
            catch (WebException ex)
            {
                string method = string.Concat(MethodBase.GetCurrentMethod().Name, " - Url - ", siteUrl, ", ListName - ", listUrl, ", ItemPostBody - ", itemPostBody);
                string guid = WebException(ex, method);
                throw new Exception(string.Format("An error occured while reading data. GUID: {0}", guid));
            }
            catch (Exception ex)
            {
                string method = string.Concat(MethodBase.GetCurrentMethod().Name, " - Url - ", siteUrl, ", ListName - ", listUrl, ", ItemPostBody - ", itemPostBody);
                string guid = WriteException(ex, method, MethodBase.GetCurrentMethod().DeclaringType.Name, string.Empty);
                throw new Exception(string.Format("An error occured while reading data. GUID: {0}", guid));
            }
        }

        public static string UpdateListItemsByDefaultAdmin(string siteUrl, string errorLogPath, string listUrl, string listItemUrl, string itemPostBody, string id)
        {
            try
            {
                XmlNamespaceManager xmlnspm = AddXmlNameSpaces();
                var formDigestNode = GetFormDigest(siteUrl, xmlnspm);

                string FormDigest = formDigestNode.InnerXml;
                string EntityTypeName = GetEntityTypeName(siteUrl, listUrl, xmlnspm);

                itemPostBody = itemPostBody.Replace("{0}", EntityTypeName);
                Byte[] itemPostData = System.Text.Encoding.ASCII.GetBytes(itemPostBody);

                HttpWebRequest ItemRequest =
                        (HttpWebRequest)HttpWebRequest.Create(string.Concat(siteUrl, listItemUrl, "(", id, ")"));
                ItemRequest.Method = "POST";
                ItemRequest.Headers.Add("Cache-Control", "no-cache");
                ItemRequest.ContentLength = itemPostBody.Length;
                ItemRequest.Headers.Add("X-HTTP-Method", "MERGE");
                ItemRequest.Headers.Add("IF-MATCH", "*");
                ItemRequest.ContentType = "application/json;odata=verbose;charset=utf-8";
                ItemRequest.Accept = "application/json;odata=verbose";
                //ItemRequest.Headers.Add("Authorization", "Bearer " + accessToken);
                ItemRequest.Credentials = System.Net.CredentialCache.DefaultCredentials;

                ItemRequest.Headers.Add("X-RequestDigest", FormDigest);
                using (Stream itemRequestStream = ItemRequest.GetRequestStream())
                {
                    itemRequestStream.Write(itemPostData, 0, itemPostData.Length);
                    itemRequestStream.Close();
                }
                using (HttpWebResponse itemResponse = (HttpWebResponse)ItemRequest.GetResponse()) ;
                //ItemRequest.KeepAlive = false;
                return null;
            }
            catch (WebException ex)
            {
                string method = string.Concat(MethodBase.GetCurrentMethod().Name, " - Url - ", siteUrl, ", ListName - ", listUrl, ", ItemPostBody - ", itemPostBody);
                string guid = WebException(ex, method);
                throw new Exception(string.Format("An error occured while reading data. GUID: {0}", guid));
            }
            catch (Exception ex)
            {
                string method = string.Concat(MethodBase.GetCurrentMethod().Name, " - Url - ", siteUrl, ", ListName - ", listUrl, ", ItemPostBody - ", itemPostBody);
                string guid = WriteException(ex, method, MethodBase.GetCurrentMethod().DeclaringType.Name, string.Empty);
                throw new Exception(string.Format("An error occured while reading data. GUID: {0}", guid));
            }
        }

        public static async Task<string> UpdateListItemsByDefaultAdminAsync(string siteUrl, string errorLogPath, string listUrl, string listItemUrl, string itemPostBody, string id)
        {
            try
            {
                XmlNamespaceManager xmlnspm = AddXmlNameSpaces();
                var formDigestNode = GetFormDigest(siteUrl, xmlnspm);

                string FormDigest = formDigestNode.InnerXml;
                string EntityTypeName = GetEntityTypeName(siteUrl, listUrl, xmlnspm);

                itemPostBody = itemPostBody.Replace("{0}", EntityTypeName);
                Byte[] itemPostData = System.Text.Encoding.ASCII.GetBytes(itemPostBody);

                HttpWebRequest ItemRequest =
                        (HttpWebRequest)HttpWebRequest.Create(string.Concat(siteUrl, listItemUrl, "(", id, ")"));
                ItemRequest.Method = "POST";
                ItemRequest.ContentLength = itemPostBody.Length;
                ItemRequest.Headers.Add("X-HTTP-Method", "MERGE");
                ItemRequest.Headers.Add("IF-MATCH", "*");
                ItemRequest.ContentType = "application/json;odata=verbose;charset=utf-8";
                ItemRequest.Accept = "application/json;odata=verbose";
                //ItemRequest.Headers.Add("Authorization", "Bearer " + accessToken);
                ItemRequest.Credentials = System.Net.CredentialCache.DefaultCredentials;

                ItemRequest.Headers.Add("X-RequestDigest", FormDigest);
                using (Stream itemRequestStream = ItemRequest.GetRequestStream())
                {
                    itemRequestStream.Write(itemPostData, 0, itemPostData.Length);
                    itemRequestStream.Close();
                }
                using (HttpWebResponse itemResponse = (HttpWebResponse)ItemRequest.GetResponse()) ;
                //ItemRequest.KeepAlive = false;
                return null;
            }
            catch (WebException ex)
            {
                string method = string.Concat(MethodBase.GetCurrentMethod().Name, " - Url - ", siteUrl, ", ListName - ", listUrl, ", ItemPostBody - ", itemPostBody);
                string guid = WebException(ex, method);
                throw new Exception(string.Format("An error occured while reading data. GUID: {0}", guid));
            }
            catch (Exception ex)
            {
                string method = string.Concat(MethodBase.GetCurrentMethod().Name, " - Url - ", siteUrl, ", ListName - ", listUrl, ", ItemPostBody - ", itemPostBody);
                string guid = WriteException(ex, method, MethodBase.GetCurrentMethod().DeclaringType.Name, string.Empty);
                throw new Exception(string.Format("An error occured while reading data. GUID: {0}", guid));
            }
        }

        public static JObject GetListItemsUsingPost(string siteUrl, string errorLogPath, string accessToken, string RestUrl, string itemPostBody)
        {
            JObject jobj;
            try
            {
                XmlNamespaceManager xmlnspm = AddXmlNameSpaces();
                var formDigestNode = GetFormDigest(siteUrl, accessToken, xmlnspm);

                string FormDigest = formDigestNode.InnerXml;
                Byte[] itemPostData = System.Text.Encoding.ASCII.GetBytes(itemPostBody);

                HttpWebRequest ItemRequest = (HttpWebRequest)HttpWebRequest.Create(RestUrl);
                ItemRequest.Method = "POST";
                ItemRequest.ContentLength = itemPostBody.Length;

                ItemRequest.ContentType = "application/json;odata=verbose;charset=utf-8";
                ItemRequest.Accept = "application/json;odata=verbose";
                ItemRequest.Headers.Add("Authorization", "Bearer " + accessToken);
                //ItemRequest.Credentials = System.Net.CredentialCache.DefaultCredentials;

                ItemRequest.Headers.Add("X-RequestDigest", FormDigest);
                using (Stream itemRequestStream = ItemRequest.GetRequestStream())
                {
                    itemRequestStream.Write(itemPostData, 0, itemPostData.Length);
                    itemRequestStream.Close();
                }
                using (HttpWebResponse itemResponse = (HttpWebResponse)ItemRequest.GetResponse())
                {
                    using (StreamReader responseReader = new StreamReader(itemResponse.GetResponseStream()))
                    {
                        string response = responseReader.ReadToEnd();
                        jobj = JObject.Parse(response);
                    }
                }
                //ItemRequest.KeepAlive = false;
                return jobj;
            }
            catch (WebException ex)
            {
                string method = string.Concat(MethodBase.GetCurrentMethod().Name, " - RestUrl - ", RestUrl, ", ItemPostBody - ", itemPostBody);
                string guid = WebException(ex, method);
                throw new Exception(string.Format("An error occured while reading data. GUID: {0}", guid));
            }
            catch (Exception ex)
            {
                string method = string.Concat(MethodBase.GetCurrentMethod().Name, " - RestUrl - ", RestUrl, ", ItemPostBody - ", itemPostBody);
                string guid = WriteException(ex, method, MethodBase.GetCurrentMethod().DeclaringType.Name, string.Empty);
                throw new Exception(string.Format("An error occured while reading data. GUID: {0}", guid));
            }

        }

        public static JObject GetListItemsUsingPost(string siteUrl, string errorLogPath, string RestUrl, string itemPostBody)
        {
            JObject jobj;
            try
            {
                XmlNamespaceManager xmlnspm = AddXmlNameSpaces();
                var formDigestNode = GetFormDigest(siteUrl, xmlnspm);

                string FormDigest = formDigestNode.InnerXml;
                Byte[] itemPostData = System.Text.Encoding.ASCII.GetBytes(itemPostBody);

                HttpWebRequest ItemRequest = (HttpWebRequest)HttpWebRequest.Create(RestUrl);
                ItemRequest.Method = "POST";
                ItemRequest.ContentLength = itemPostBody.Length;

                ItemRequest.ContentType = "application/json;odata=verbose;charset=utf-8";
                ItemRequest.Accept = "application/json;odata=verbose";
                //ItemRequest.Headers.Add("Authorization", "Bearer " + accessToken);
                ItemRequest.Credentials = System.Net.CredentialCache.DefaultCredentials;

                ItemRequest.Headers.Add("X-RequestDigest", FormDigest);
                using (Stream itemRequestStream = ItemRequest.GetRequestStream())
                {
                    itemRequestStream.Write(itemPostData, 0, itemPostData.Length);
                    itemRequestStream.Close();
                }
                using (HttpWebResponse itemResponse = (HttpWebResponse)ItemRequest.GetResponse())
                {
                    using (StreamReader responseReader = new StreamReader(itemResponse.GetResponseStream()))
                    {
                        string response = responseReader.ReadToEnd();
                        jobj = JObject.Parse(response);
                    }
                }
                //ItemRequest.KeepAlive = false;
                return jobj;
            }
            catch (WebException ex)
            {
                string method = string.Concat(MethodBase.GetCurrentMethod().Name, " - RestUrl - ", RestUrl, ", ItemPostBody - ", itemPostBody);
                string guid = WebException(ex, method);
                throw new Exception(string.Format("An error occured while reading data. GUID: {0}", guid));
            }
            catch (Exception ex)
            {
                string method = string.Concat(MethodBase.GetCurrentMethod().Name, " - RestUrl - ", RestUrl, ", ItemPostBody - ", itemPostBody);
                string guid = WriteException(ex, method, MethodBase.GetCurrentMethod().DeclaringType.Name, string.Empty);
                throw new Exception(string.Format("An error occured while reading data. GUID: {0}", guid));
            }

        }

        /// <summary>
        /// Overloaded Method to return stream from HttpWebRequest endPointRequest by passing default credentials
        /// </summary>
        /// <param name="restURL"></param>
        /// <param name="credentials"></param>
        /// <returns></returns>
        public static JObject GetResponseFromEndPointRequest(string restURL, ICredentials credentials)
        {
            JObject jobj;
            try
            {
                HttpWebRequest endpointRequest = (HttpWebRequest)HttpWebRequest.Create(restURL);
                endpointRequest.Method = "GET";
                endpointRequest.Accept = "application/json;odata=verbose";
                endpointRequest.ContentType = "application/json;charset=utf-8";
                endpointRequest.Credentials = credentials;
                //endpointRequest.Headers.Add("X-RequestDigest", FormDigest);
                using (HttpWebResponse itemResponse = (HttpWebResponse)endpointRequest.GetResponse())
                {
                    using (StreamReader responseReader = new StreamReader(itemResponse.GetResponseStream()))
                    {
                        string response = responseReader.ReadToEnd();
                        jobj = JObject.Parse(response);
                    }
                }
                //endpointRequest.KeepAlive = false;
                return jobj;
            }
            catch (WebException ex)
            {
                string method = string.Concat(MethodBase.GetCurrentMethod().Name, " - RestUrl - ", restURL);
                string guid = WebException(ex, method);
                throw new Exception(string.Format("An error occured while reading data. GUID: {0}", guid));
            }
            catch (Exception ex)
            {
                string method = string.Concat(MethodBase.GetCurrentMethod().Name, " - RestUrl - ", restURL);
                string guid = WriteException(ex, method, MethodBase.GetCurrentMethod().DeclaringType.Name, string.Empty);
                throw new Exception(string.Format("An error occured while reading data. GUID: {0}", guid));
            }
        }

        /// <summary>
        /// Overloaded Method to return stream from HttpWebRequest endPointRequest by passing default credentials
        /// </summary>
        /// <param name="restURL"></param>
        /// <param name="credentials"></param>
        /// <returns></returns>
        public static async Task<JObject> GetResponseFromEndPointRequestAsync(string restURL, ICredentials credentials)
        {
            JObject jobj;
            try
            {
                HttpWebRequest endpointRequest = (HttpWebRequest)HttpWebRequest.Create(restURL);
                endpointRequest.Method = "GET";
                endpointRequest.Accept = "application/json;odata=verbose";
                endpointRequest.ContentType = "application/json;charset=utf-8";
                endpointRequest.Credentials = credentials;
                //endpointRequest.Headers.Add("X-RequestDigest", FormDigest);
                using (HttpWebResponse itemResponse = (HttpWebResponse)endpointRequest.GetResponse())
                {
                    using (StreamReader responseReader = new StreamReader(itemResponse.GetResponseStream()))
                    {
                        string response = responseReader.ReadToEnd();
                        jobj = JObject.Parse(response);
                    }
                }
                //endpointRequest.KeepAlive = false;
                return jobj;
            }
            catch (WebException ex)
            {
                string method = string.Concat(MethodBase.GetCurrentMethod().Name, " - RestUrl - ", restURL);
                string guid = WebException(ex, method);
                throw new Exception(string.Format("An error occured while reading data. GUID: {0}", guid));
            }
            catch (Exception ex)
            {
                string method = string.Concat(MethodBase.GetCurrentMethod().Name, " - RestUrl - ", restURL);
                string guid = WriteException(ex, method, MethodBase.GetCurrentMethod().DeclaringType.Name, string.Empty);
                throw new Exception(string.Format("An error occured while reading data. GUID: {0}", guid));
            }
        }

        /// <summary>
        /// Overloaded Method to return stream from HttpWebRequest endPointRequest by passing default credentials
        /// </summary>
        /// <param name="restURL"></param>
        /// <param name="credentials"></param>
        /// <returns></returns>
        public static JObject GetResponseFromEndPointRequestFormDigest(string restURL, string formDisest)
        {
            JObject jobj;
            try
            {
                HttpWebRequest endpointRequest = (HttpWebRequest)HttpWebRequest.Create(restURL);
                endpointRequest.Method = "GET";
                endpointRequest.Accept = "application/json;odata=verbose";
                endpointRequest.ContentType = "application/json;charset=utf-8";
                endpointRequest.Headers.Add("X-RequestDigest", formDisest);
                using (HttpWebResponse itemResponse = (HttpWebResponse)endpointRequest.GetResponse())
                {
                    using (StreamReader responseReader = new StreamReader(itemResponse.GetResponseStream()))
                    {
                        string response = responseReader.ReadToEnd();
                        jobj = JObject.Parse(response);
                    }
                }
                //endpointRequest.KeepAlive = false;
                return jobj;
            }
            catch (WebException ex)
            {
                string method = string.Concat(MethodBase.GetCurrentMethod().Name, " - RestUrl - ", restURL);
                string guid = WebException(ex, method);
                throw new Exception(string.Format("An error occured while reading data. GUID: {0}", guid));
            }
            catch (Exception ex)
            {
                string method = string.Concat(MethodBase.GetCurrentMethod().Name, " - RestUrl - ", restURL);
                string guid = WriteException(ex, method, MethodBase.GetCurrentMethod().DeclaringType.Name, string.Empty);
                throw new Exception(string.Format("An error occured while reading data. GUID: {0}", guid));
            }
        }

        /// <summary>
        /// Overloaded Method to return stream from HttpWebRequest endPointRequest by passing username and password in network credentials 
        /// </summary>
        /// <param name="restURL"></param>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static JObject GetResponseFromEndPointRequest(string restURL, string userName, string password)
        {
            JObject jobj;
            try
            {
                HttpWebRequest endpointRequest = (HttpWebRequest)HttpWebRequest.Create(restURL);
                endpointRequest.Method = "GET";
                endpointRequest.Accept = "application/json;odata=verbose";
                endpointRequest.ContentType = "application/json;charset=utf-8";
                endpointRequest.Credentials = new NetworkCredential(userName, password);
                using (HttpWebResponse itemResponse = (HttpWebResponse)endpointRequest.GetResponse())
                {
                    using (StreamReader responseReader = new StreamReader(itemResponse.GetResponseStream()))
                    {
                        string response = responseReader.ReadToEnd();
                        jobj = JObject.Parse(response);
                    }
                }
                //endpointRequest.KeepAlive = false;
                return jobj;
            }
            catch (WebException ex)
            {
                string method = string.Concat(MethodBase.GetCurrentMethod().Name, " - RestUrl - ", restURL);
                string guid = WebException(ex, method);
                throw new Exception(string.Format("An error occured while reading data. GUID: {0}", guid));
            }
            catch (Exception ex)
            {
                string method = string.Concat(MethodBase.GetCurrentMethod().Name, " - RestUrl - ", restURL);
                string guid = WriteException(ex, method, MethodBase.GetCurrentMethod().DeclaringType.Name, string.Empty);
                throw new Exception(string.Format("An error occured while reading data. GUID: {0}", guid));
            }
        }

        /// <summary>
        /// Overloaded Method to return stream from HttpWebRequest endPointRequest by passing asccess token in headers
        /// </summary>
        /// <param name="restURL"></param>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public static JObject GetResponseFromEndPointRequestAccessToken(string restURL, string accessToken)
        {
            JObject jobj;
            try
            {
                HttpWebRequest endpointRequest = (HttpWebRequest)HttpWebRequest.Create(restURL);
                endpointRequest.Method = "GET";
                endpointRequest.Accept = "application/json;odata=verbose";
                endpointRequest.ContentType = "application/json;charset=utf-8";
                endpointRequest.Headers.Add("Authorization", "Bearer " + accessToken);
                using (HttpWebResponse itemResponse = (HttpWebResponse)endpointRequest.GetResponse())
                {
                    using (StreamReader responseReader = new StreamReader(itemResponse.GetResponseStream()))
                    {
                        string response = responseReader.ReadToEnd();
                        jobj = JObject.Parse(response);
                    }
                }
                //endpointRequest.KeepAlive = false;
                return jobj;
            }
            catch (WebException ex)
            {
                string method = string.Concat(MethodBase.GetCurrentMethod().Name, " - RestUrl - ", restURL);
                string guid = WebException(ex, method);
                throw new Exception(string.Format("An error occured while reading data. GUID: {0}", guid));
            }
            catch (Exception ex)
            {
                string method = string.Concat(MethodBase.GetCurrentMethod().Name, " - RestUrl - ", restURL);
                string guid = WriteException(ex, method, MethodBase.GetCurrentMethod().DeclaringType.Name, string.Empty);
                throw new Exception(string.Format("An error occured while reading data. GUID: {0}", guid));
            }
        }

        public static string WebException(WebException ex, string methodName)
        {
            string guid = Convert.ToString(Guid.NewGuid());
            ErrorLogger errorLogger = new ErrorLogger();
            if (ex.Status == WebExceptionStatus.ProtocolError)
            {
                var resp = new StreamReader(ex.Response.GetResponseStream()).ReadToEnd();
                dynamic obj = JsonConvert.DeserializeObject(resp);
                string errorMessage = Convert.ToString(obj.error.message.value);
                string stackTrace = Convert.ToString(obj.error.innererror.stacktrace);
                string innerException = Convert.ToString(ex.Message);

                //errorLogger.Log(errorMessage, stackTrace, "", methodName, "Error in SharePoint REST Service", innerException, guid);
                errorLogger.Log(errorMessage, methodName, guid, ex.Message, stackTrace, ex.StackTrace, innerException);
            }
            errorLogger.Log("", methodName, guid, ex.Message, MethodBase.GetCurrentMethod().DeclaringType.Name, ex.StackTrace, Convert.ToString(ex.InnerException));
            return guid;
        }

        public static string WriteException(Exception ex, string methodName, string className, string userName)
        {
            ErrorLogger errorLogger = new ErrorLogger();
            string guid = Convert.ToString(Guid.NewGuid());
            errorLogger.Log(userName, methodName, guid, ex.Message, className, ex.StackTrace, Convert.ToString(ex.InnerException));
            return guid;
        }

        /// <summary>
        /// Generate Unique ID's
        /// </summary>
        /// <param name="Value"></param>
        /// <returns></returns>
        public static string generateUniqueID(string Value)
        {
            return Value + System.DateTime.Now.Ticks.ToString().Replace(".", "").Replace(":", "").Replace("/", "").Replace("PM", "").Replace(" ", "").Remove(1, 8);
        }

        /// <summary>
        /// Get Form Digest value
        /// </summary>
        /// <param name="siteUrl"></param>
        /// <param name="accessToken"></param>
        /// <param name="xmlnspm"></param>
        /// <returns></returns>
        public static XmlNode GetFormDigest(string siteUrl, string accessToken, XmlNamespaceManager xmlnspm)
        {
            try
            {
                var formDigestXML = new XmlDocument();
                HttpWebRequest contextinfoRequest =
                    (HttpWebRequest)HttpWebRequest.Create(siteUrl + "/_api/contextinfo");
                contextinfoRequest.Method = "POST";
                contextinfoRequest.ContentType = "application/json;charset=utf-8";
                contextinfoRequest.ContentLength = 0;
                contextinfoRequest.Headers.Set(HttpRequestHeader.AcceptLanguage, "en-US");
                contextinfoRequest.Headers.Add("Authorization", "Bearer " + accessToken);
                CredentialCache myCache = new CredentialCache();

                contextinfoRequest.Credentials = System.Net.CredentialCache.DefaultCredentials;
                HttpWebResponse contextinfoResponse = (HttpWebResponse)contextinfoRequest.GetResponse();

                using (StreamReader contextinfoReader = new StreamReader(contextinfoResponse.GetResponseStream(), System.Text.Encoding.UTF8))
                {
                    formDigestXML.LoadXml(contextinfoReader.ReadToEnd());
                }
                //contextinfoRequest.KeepAlive = false;

                var formDigestNode = formDigestXML.SelectSingleNode("//d:FormDigestValue", xmlnspm);
                return formDigestNode;
            }
            catch (WebException ex)
            {
                string guid = WebException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw new Exception(string.Format("An error occured while reading data. GUID: {0}", guid));
            }
            catch (Exception ex)
            {
                string guid = WriteException(ex, MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.Name, string.Empty);
                throw new Exception(string.Format("An error occured while reading data. GUID: {0}", guid));
            }

        }

        public static XmlNode GetFormDigest(string siteUrl, XmlNamespaceManager xmlnspm)
        {
            try
            {
                var formDigestXML = new XmlDocument();
                HttpWebRequest contextinfoRequest =
                    (HttpWebRequest)HttpWebRequest.Create(siteUrl + "/_api/contextinfo");
                //System.Threading.Thread.Sleep(10000);
                contextinfoRequest.Method = "POST";
                //contextinfoRequest.Headers.Add("Cache-Control", "no-cache");
                //contextinfoRequest.Headers.Add("X-HTTP-Method", "MERGE");
                //contextinfoRequest.Headers.Add("IF-MATCH", "*");
                contextinfoRequest.ContentType = "application/json;odata=verbose;charset=utf-8";
                //contextinfoRequest.Accept = "application/json;odata=verbose";
                contextinfoRequest.ContentLength = 0;
                contextinfoRequest.Headers.Set(HttpRequestHeader.AcceptLanguage, "en-US");
                //contextinfoRequest.Headers.Add("Authorization", "Bearer " + accessToken);
                CredentialCache myCache = new CredentialCache();

                contextinfoRequest.Credentials = System.Net.CredentialCache.DefaultCredentials;
                HttpWebResponse contextinfoResponse = (HttpWebResponse)contextinfoRequest.GetResponse();

                using (StreamReader contextinfoReader = new StreamReader(contextinfoResponse.GetResponseStream(), System.Text.Encoding.UTF8))
                {
                    formDigestXML.LoadXml(contextinfoReader.ReadToEnd());
                }
                //contextinfoRequest.KeepAlive = false;
                var formDigestNode = formDigestXML.SelectSingleNode("//d:FormDigestValue", xmlnspm);
                return formDigestNode;
            }
            catch (WebException ex)
            {
                string guid = WebException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw new Exception(string.Format("An error occured while reading data. GUID: {0}", guid));
            }
            catch (Exception ex)
            {
                string guid = WriteException(ex, MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.Name, string.Empty);
                throw new Exception(string.Format("An error occured while reading data. GUID: {0}", guid));
            }
        }

        /// <summary>
        /// Upload Files to Pofile Bank
        /// </summary>
        /// <param name="UploadedFiles"></param>
        /// <param name="siteUrl"></param>
        /// <param name="errorLogPath"></param>
        /// <param name="accessToken"></param>
        /// <param name="DocumentLibUrl"></param>
        /// <param name="DocumentLibListUrl"></param>
        /// <param name="FolderName"></param>
        /// <param name="Overwrite"></param>
        /// <param name="itemPostBody"></param>
        /// <param name="xmlnspm"></param>
        /// <param name="formDigestNode"></param>
        /// <returns></returns>
        public static string UploadFilesDirectlyInLibrary(HttpFileCollection uploadedFiles, string siteUrl, string errorLogPath, string accessToken, string documentLibUrl, string documentLibListUrl, string folderName, bool overwrite, string itemPostBody, out string fileName, out int id)
        {
            string filePath = string.Empty;
            int fileId = 0;
            try
            {
                XmlNamespaceManager xmlnspm = AddXmlNameSpaces();
                var formDigestNode = GetFormDigest(siteUrl, xmlnspm);

                string formDigest = formDigestNode.InnerXml;
                id = 0;
                fileName = string.Empty;
                if (uploadedFiles.Count > 0)
                {
                    string NewFileWithExtention = string.Empty;
                    int Counter = 0;
                    foreach (string file in uploadedFiles)
                    {
                        var postedFile = uploadedFiles[Counter];
                        var FileName = new FileInfo(postedFile.FileName).Name;

                        byte[] fileData = null;
                        using (var binaryReader = new BinaryReader(postedFile.InputStream))
                        {
                            fileData = binaryReader.ReadBytes(postedFile.ContentLength);
                        }
                        //string NewFileName = Convert.ToString(Guid.NewGuid());
                        string filename = string.Concat(FileName.Substring(0, FileName.Length - Path.GetExtension(FileName).Length), "_", DateTime.Now.ToString("yyyyMMddHHmmss"));
                        NewFileWithExtention = string.Concat(filename, Path.GetExtension(FileName));
                        HttpWebRequest endpointRequest = (HttpWebRequest)HttpWebRequest.Create(string.Concat(siteUrl, "/_api/web/GetFolderByServerRelativeUrl('",
                                                                                                documentLibUrl, "/')/Files/add(url='", NewFileWithExtention,
                                                                                                "',overwrite=", Convert.ToString(overwrite).ToLower(), ")?$expand=ListItemAllFields"));
                        endpointRequest.Method = "POST";
                        endpointRequest.Accept = "application/json;odata=verbose";
                        endpointRequest.ContentType = "application/json;odata=verbose";
                        endpointRequest.Headers.Add("binaryStringRequestBody", "true");
                        endpointRequest.Headers.Add("Authorization", "Bearer " + accessToken);
                        endpointRequest.Headers.Add("X-RequestDigest", formDigest);
                        endpointRequest.Credentials = System.Net.CredentialCache.DefaultCredentials;
                        endpointRequest.GetRequestStream().Write(fileData, 0, fileData.Length);

                        try
                        {
                            HttpWebResponse endpointresponse = (HttpWebResponse)endpointRequest.GetResponse();
                            using (StreamReader reader = new StreamReader(endpointresponse.GetResponseStream()))
                            {
                                string response = reader.ReadToEnd();
                                JObject jobj = JObject.Parse(response);
                                if (!string.IsNullOrEmpty(Convert.ToString(jobj)))
                                    fileId = Convert.ToInt32(Convert.ToString(jobj["d"]["ListItemAllFields"]["ID"]));

                            }
                            //endpointRequest.KeepAlive = false;
                            UpdateMetadataWithoutFolderStructure(siteUrl, errorLogPath, accessToken, documentLibUrl, FileName, NewFileWithExtention, itemPostBody, documentLibListUrl, xmlnspm, formDigestNode);

                        }
                        catch (WebException ex)
                        {
                            HttpWebResponse errorResponse = ex.Response as HttpWebResponse;

                            if (errorResponse.StatusCode == HttpStatusCode.NotFound)
                            {

                            }

                        }
                        catch (Exception ex)
                        {
                            string guid = WriteException(ex, MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.Name, string.Empty);

                            throw new Exception(string.Format("An error occured while reading data. GUID: {0}", guid));
                        }
                        Counter++;
                    }
                    if (Counter > 0)
                    {
                        filePath = string.Concat(siteUrl, "/", documentLibUrl, "/", NewFileWithExtention);
                        fileName = NewFileWithExtention;
                        id = fileId;
                    }
                }
            }
            catch (Exception ex)
            {
                string guid = WriteException(ex, MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.Name, string.Empty);
                throw new Exception(string.Format("An error occured while reading data. GUID: {0}", guid));
            }
            return filePath;

        }


        public static string AttachFilesToListItemByAccessToken(HttpFileCollection attachedFiles, string siteUrl, string errorLogPath, string accessToken, string listName, string itemID, bool overwrite)
        {
            string ID = string.Empty;
            try
            {
                XmlNamespaceManager xmlnspm = AddXmlNameSpaces();
                var formDigestNode = GetFormDigest(siteUrl, xmlnspm);

                string formDigest = formDigestNode.InnerXml;

                if (attachedFiles.Count > 0)
                {
                    string NewFileWithExtention = string.Empty;
                    int Counter = 0;
                    foreach (string file in attachedFiles)
                    {
                        var postedFile = attachedFiles[Counter];
                        var FileName = new FileInfo(postedFile.FileName).Name;

                        byte[] fileData = null;
                        using (var binaryReader = new BinaryReader(postedFile.InputStream))
                        {
                            fileData = binaryReader.ReadBytes(postedFile.ContentLength);
                        }
                        //string NewFileName = Convert.ToString(Guid.NewGuid());
                        string filename = string.Concat(FileName.Substring(0, FileName.Length - Path.GetExtension(FileName).Length), "_", DateTime.Now.ToString("yyyyMMddHHmmss"));
                        NewFileWithExtention = string.Concat(filename, Path.GetExtension(FileName));
                        HttpWebRequest endpointRequest = (HttpWebRequest)HttpWebRequest.Create(string.Format("{0}/_api/web/lists/GetByTitle('{1}')/items({2})/AttachmentFiles/add(FileName='{3}')", siteUrl, listName, itemID, NewFileWithExtention));
                        endpointRequest.Method = "POST";
                        endpointRequest.Accept = "application/json;odata=verbose";
                        endpointRequest.ContentType = "application/json;odata=verbose;charset=utf-8";
                        endpointRequest.Headers.Add("binaryStringRequestBody", "true");
                        endpointRequest.Headers.Add("Authorization", "Bearer " + accessToken);
                        endpointRequest.Headers.Add("X-RequestDigest", formDigest);
                        endpointRequest.GetRequestStream().Write(fileData, 0, fileData.Length);
                        try
                        {
                            HttpWebResponse endpointresponse = (HttpWebResponse)endpointRequest.GetResponse();
                            using (StreamReader responseReader = new StreamReader(endpointresponse.GetResponseStream()))
                            {
                                string response = responseReader.ReadToEnd();
                                JObject jobj = JObject.Parse(response);
                                ID = Convert.ToString(jobj["d"]["Id"]);
                            }
                            //endpointRequest.KeepAlive = false;
                        }
                        catch (WebException ex)
                        {
                            HttpWebResponse errorResponse = ex.Response as HttpWebResponse;
                            if (errorResponse.StatusCode == HttpStatusCode.NotFound)
                            {
                            }
                        }
                        catch (Exception ex)
                        {
                            string guid = WriteException(ex, MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.Name, string.Empty);
                            throw new Exception(string.Format("An error occured while reading data. GUID: {0}", guid));
                        }
                        Counter++;
                    }
                }
                return ID;

            }
            catch (Exception ex)
            {
                string guid = WriteException(ex, MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.Name, string.Empty);
                throw new Exception(string.Format("An error occured while reading data. GUID: {0}", guid));
            }
        }

        public static async Task<string> AttachFilesToListItemByAccessTokenAsync(HttpFileCollection attachedFiles, string siteUrl, string errorLogPath, string accessToken, string listName, string itemID, bool overwrite)
        {
            string ID = string.Empty;
            try
            {
                XmlNamespaceManager xmlnspm = AddXmlNameSpaces();
                var formDigestNode = GetFormDigest(siteUrl, xmlnspm);

                string formDigest = formDigestNode.InnerXml;

                if (attachedFiles.Count > 0)
                {
                    string NewFileWithExtention = string.Empty;
                    int Counter = 0;
                    foreach (string file in attachedFiles)
                    {
                        var postedFile = attachedFiles[Counter];
                        var FileName = new FileInfo(postedFile.FileName).Name;

                        byte[] fileData = null;
                        using (var binaryReader = new BinaryReader(postedFile.InputStream))
                        {
                            fileData = binaryReader.ReadBytes(postedFile.ContentLength);
                        }
                        //string NewFileName = Convert.ToString(Guid.NewGuid());
                        string filename = string.Concat(FileName.Substring(0, FileName.Length - Path.GetExtension(FileName).Length), "_", DateTime.Now.ToString("yyyyMMddHHmmss"));
                        NewFileWithExtention = string.Concat(filename, Path.GetExtension(FileName));
                        HttpWebRequest endpointRequest = (HttpWebRequest)HttpWebRequest.Create(string.Format("{0}/_api/web/lists/GetByTitle('{1}')/items({2})/AttachmentFiles/add(FileName='{3}')", siteUrl, listName, itemID, NewFileWithExtention));
                        endpointRequest.Method = "POST";
                        endpointRequest.Accept = "application/json;odata=verbose";
                        endpointRequest.ContentType = "application/json;odata=verbose;charset=utf-8";
                        endpointRequest.Headers.Add("binaryStringRequestBody", "true");
                        endpointRequest.Headers.Add("Authorization", "Bearer " + accessToken);
                        endpointRequest.Headers.Add("X-RequestDigest", formDigest);
                        endpointRequest.GetRequestStream().Write(fileData, 0, fileData.Length);
                        try
                        {
                            HttpWebResponse endpointresponse = (HttpWebResponse)endpointRequest.GetResponse();
                            using (StreamReader responseReader = new StreamReader(endpointresponse.GetResponseStream()))
                            {
                                string response = responseReader.ReadToEnd();
                                JObject jobj = JObject.Parse(response);
                                ID = Convert.ToString(jobj["d"]["Id"]);
                            }
                            //endpointRequest.KeepAlive = false;
                        }
                        catch (WebException ex)
                        {
                            HttpWebResponse errorResponse = ex.Response as HttpWebResponse;
                            if (errorResponse.StatusCode == HttpStatusCode.NotFound)
                            {
                            }
                        }
                        catch (Exception ex)
                        {
                            string guid = WriteException(ex, MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.Name, string.Empty);
                            throw new Exception(string.Format("An error occured while reading data. GUID: {0}", guid));
                        }
                        Counter++;
                    }
                }
                return ID;

            }
            catch (Exception ex)
            {
                string guid = WriteException(ex, MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.Name, string.Empty);
                throw new Exception(string.Format("An error occured while reading data. GUID: {0}", guid));
            }
        }

        public static string AttachFilesToListItemByDefaultAdmin(HttpFileCollection attachedFiles, string siteUrl, string errorLogPath, string listName, string itemID, bool overwrite)
        {
            string ID = string.Empty;
            try
            {
                XmlNamespaceManager xmlnspm = AddXmlNameSpaces();
                var formDigestNode = GetFormDigest(siteUrl, xmlnspm);
                string formDigest = formDigestNode.InnerXml;
                if (attachedFiles.Count > 0)
                {
                    string NewFileWithExtention = string.Empty;
                    int Counter = 0;
                    foreach (string file in attachedFiles)
                    {
                        var postedFile = attachedFiles[Counter];
                        var FileName = new FileInfo(postedFile.FileName).Name;
                        byte[] fileData = null;
                        using (var binaryReader = new BinaryReader(postedFile.InputStream))
                        {
                            fileData = binaryReader.ReadBytes(postedFile.ContentLength);
                        }
                        //string NewFileName = Convert.ToString(Guid.NewGuid());
                        string filename = string.Concat(FileName.Substring(0, FileName.Length - Path.GetExtension(FileName).Length), "_", DateTime.Now.ToString("yyyyMMddHHmmss"));
                        NewFileWithExtention = string.Concat(filename, Path.GetExtension(FileName));
                        HttpWebRequest endpointRequest = (HttpWebRequest)HttpWebRequest.Create(string.Format("{0}/_api/web/lists/GetByTitle('{1}')/items({2})/AttachmentFiles/add(FileName='{3}')", siteUrl, listName, itemID, NewFileWithExtention));
                        endpointRequest.Method = "POST";
                        endpointRequest.Accept = "application/json;odata=verbose";
                        endpointRequest.ContentType = "application/json;odata=verbose;charset=utf-8";
                        endpointRequest.Headers.Add("binaryStringRequestBody", "true");
                        endpointRequest.Headers.Add("X-RequestDigest", formDigest);
                        endpointRequest.Credentials = System.Net.CredentialCache.DefaultCredentials;
                        endpointRequest.GetRequestStream().Write(fileData, 0, fileData.Length);
                        try
                        {
                            HttpWebResponse endpointresponse = (HttpWebResponse)endpointRequest.GetResponse();
                            using (StreamReader responseReader = new StreamReader(endpointresponse.GetResponseStream()))
                            {
                                string response = responseReader.ReadToEnd();
                                JObject jobj = JObject.Parse(response);
                                ID = Convert.ToString(jobj["d"]["Id"]);
                            }
                            //endpointRequest.KeepAlive = false;
                        }
                        catch (WebException ex)
                        {
                            HttpWebResponse errorResponse = ex.Response as HttpWebResponse;
                            if (errorResponse.StatusCode == HttpStatusCode.NotFound)
                            {
                            }
                        }
                        catch (Exception ex)
                        {
                            string guid = WriteException(ex, MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.Name, string.Empty);
                            throw new Exception(string.Format("An error occured while reading data. GUID: {0}", guid));
                        }
                        Counter++;
                    }
                }
                return ID;
            }
            catch (Exception ex)
            {
                string guid = WriteException(ex, MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.Name, string.Empty);
                throw new Exception(string.Format("An error occured while reading data. GUID: {0}", guid));
            }
        }

        public static async Task<string> AttachFilesToListItemByDefaultAdminAsync(HttpFileCollection attachedFiles, string siteUrl, string errorLogPath, string listName, string itemID, bool overwrite)
        {
            string ID = string.Empty;
            try
            {
                XmlNamespaceManager xmlnspm = AddXmlNameSpaces();
                var formDigestNode = GetFormDigest(siteUrl, xmlnspm);
                string formDigest = formDigestNode.InnerXml;
                if (attachedFiles.Count > 0)
                {
                    string NewFileWithExtention = string.Empty;
                    int Counter = 0;
                    foreach (string file in attachedFiles)
                    {
                        var postedFile = attachedFiles[Counter];
                        var FileName = new FileInfo(postedFile.FileName).Name;
                        byte[] fileData = null;
                        using (var binaryReader = new BinaryReader(postedFile.InputStream))
                        {
                            fileData = binaryReader.ReadBytes(postedFile.ContentLength);
                        }
                        //string NewFileName = Convert.ToString(Guid.NewGuid());
                        string filename = string.Concat(FileName.Substring(0, FileName.Length - Path.GetExtension(FileName).Length), "_", DateTime.Now.ToString("yyyyMMddHHmmss"));
                        NewFileWithExtention = string.Concat(filename, Path.GetExtension(FileName));
                        HttpWebRequest endpointRequest = (HttpWebRequest)HttpWebRequest.Create(string.Format("{0}/_api/web/lists/GetByTitle('{1}')/items({2})/AttachmentFiles/add(FileName='{3}')", siteUrl, listName, itemID, NewFileWithExtention));
                        endpointRequest.Method = "POST";
                        endpointRequest.Accept = "application/json;odata=verbose";
                        endpointRequest.ContentType = "application/json;odata=verbose";
                        endpointRequest.Headers.Add("binaryStringRequestBody", "true");
                        endpointRequest.Headers.Add("X-RequestDigest", formDigest);
                        endpointRequest.Credentials = System.Net.CredentialCache.DefaultCredentials;
                        endpointRequest.GetRequestStream().Write(fileData, 0, fileData.Length);
                        try
                        {
                            HttpWebResponse endpointresponse = (HttpWebResponse)endpointRequest.GetResponse();
                            using (StreamReader responseReader = new StreamReader(endpointresponse.GetResponseStream()))
                            {
                                string response = responseReader.ReadToEnd();
                                JObject jobj = JObject.Parse(response);
                                ID = Convert.ToString(jobj["d"]["Id"]);
                            }
                            //endpointRequest.KeepAlive = false;
                        }
                        catch (WebException ex)
                        {
                            HttpWebResponse errorResponse = ex.Response as HttpWebResponse;
                            if (errorResponse.StatusCode == HttpStatusCode.NotFound)
                            {
                            }
                        }
                        catch (Exception ex)
                        {
                            string guid = WriteException(ex, MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.Name, string.Empty);
                            throw new Exception(string.Format("An error occured while reading data. GUID: {0}", guid));
                        }
                        Counter++;
                    }
                }
                return ID;
            }
            catch (Exception ex)
            {
                string guid = WriteException(ex, MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.Name, string.Empty);
                throw new Exception(string.Format("An error occured while reading data. GUID: {0}", guid));
            }
        }

        public static string AttachFilesToListItemByDefaultAdmin(byte[] byteArray, string fileName, string siteUrl, string errorLogPath, string listName, string itemID, bool overwrite)
        {
            string FileName = string.Empty;
            try
            {
                XmlNamespaceManager xmlnspm = AddXmlNameSpaces();
                var formDigestNode = GetFormDigest(siteUrl, xmlnspm);
                string formDigest = formDigestNode.InnerXml;

                HttpWebRequest endpointRequest = (HttpWebRequest)HttpWebRequest.Create(string.Format("{0}/_api/web/lists/GetByTitle('{1}')/items({2})/AttachmentFiles/add(FileName='{3}')", siteUrl, listName, itemID, fileName));

                endpointRequest.Method = "POST";
                endpointRequest.Accept = "application/json;odata=verbose";
                endpointRequest.ContentType = "application/json;odata=verbose";
                endpointRequest.Headers.Add("binaryStringRequestBody", "true");
                endpointRequest.Headers.Add("X-RequestDigest", formDigest);
                endpointRequest.Credentials = System.Net.CredentialCache.DefaultCredentials;
                endpointRequest.GetRequestStream().Write(byteArray, 0, byteArray.Length);
                try
                {
                    HttpWebResponse endpointresponse = (HttpWebResponse)endpointRequest.GetResponse();
                    using (StreamReader responseReader = new StreamReader(endpointresponse.GetResponseStream()))
                    {
                        string response = responseReader.ReadToEnd();
                        JObject jobj = JObject.Parse(response);
                        FileName = Convert.ToString(jobj["d"]["FileName"]);
                    }
                    //endpointRequest.KeepAlive = false;
                }
                catch (WebException ex)
                {
                    HttpWebResponse errorResponse = ex.Response as HttpWebResponse;
                    if (errorResponse.StatusCode == HttpStatusCode.NotFound)
                    {
                    }
                }
                catch (Exception ex)
                {
                    string guid = WriteException(ex, MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.Name, string.Empty);
                    throw new Exception(string.Format("An error occured while reading data. GUID: {0}", guid));
                }

                return FileName;
            }
            catch (Exception ex)
            {
                string guid = WriteException(ex, MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.Name, string.Empty);
                throw new Exception(string.Format("An error occured while reading data. GUID: {0}", guid));
            }
        }

        public static async Task<string> AttachFilesToListItemByDefaultAdminAsync(byte[] byteArray, string fileName, string siteUrl, string errorLogPath, string listName, string itemID, bool overwrite)
        {
            string FileName = string.Empty;
            try
            {
                XmlNamespaceManager xmlnspm = AddXmlNameSpaces();
                var formDigestNode = GetFormDigest(siteUrl, xmlnspm);
                string formDigest = formDigestNode.InnerXml;

                HttpWebRequest endpointRequest = (HttpWebRequest)HttpWebRequest.Create(string.Format("{0}/_api/web/lists/GetByTitle('{1}')/items({2})/AttachmentFiles/add(FileName='{3}')", siteUrl, listName, itemID, fileName));

                endpointRequest.Method = "POST";
                endpointRequest.Accept = "application/json;odata=verbose";
                endpointRequest.ContentType = "application/json;odata=verbose";
                endpointRequest.Headers.Add("binaryStringRequestBody", "true");
                endpointRequest.Headers.Add("X-RequestDigest", formDigest);
                endpointRequest.Credentials = System.Net.CredentialCache.DefaultCredentials;
                endpointRequest.GetRequestStream().Write(byteArray, 0, byteArray.Length);
                try
                {
                    HttpWebResponse endpointresponse = (HttpWebResponse)endpointRequest.GetResponse();
                    using (StreamReader responseReader = new StreamReader(endpointresponse.GetResponseStream()))
                    {
                        string response = responseReader.ReadToEnd();
                        JObject jobj = JObject.Parse(response);
                        FileName = Convert.ToString(jobj["d"]["FileName"]);
                    }
                    //endpointRequest.KeepAlive = false;
                }
                catch (WebException ex)
                {
                    HttpWebResponse errorResponse = ex.Response as HttpWebResponse;
                    string method = string.Concat(MethodBase.GetCurrentMethod().Name, " - Url - ", siteUrl, ", ListName - ", listName, ", itemID - ", itemID);
                    string guid = WebException(ex, method);
                    throw new Exception(string.Format("An error occured while reading data. GUID: {0}", guid));
                    if (errorResponse.StatusCode == HttpStatusCode.NotFound)
                    {
                    }
                }
                catch (Exception ex)
                {
                    string guid = WriteException(ex, MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.Name, string.Empty);
                    throw new Exception(string.Format("An error occured while reading data. GUID: {0}", guid));
                }

                return FileName;
            }
            catch (Exception ex)
            {
                string guid = WriteException(ex, MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.Name, string.Empty);
                throw new Exception(string.Format("An error occured while reading data. GUID: {0}", guid));
            }
        }

        public static string DeleteAttachmentFromListByDefaultAdmin(string fileName, string siteUrl, string relativeUrl, string errorLogPath, string listName, string itemID)
        {
            string FileName = string.Empty;
            try
            {
                XmlNamespaceManager xmlnspm = AddXmlNameSpaces();
                var formDigestNode = GetFormDigest(siteUrl, xmlnspm);
                string formDigest = formDigestNode.InnerXml;


                HttpWebRequest endpointRequest = (HttpWebRequest)HttpWebRequest.Create(string.Format("{0}/_api/web/getFileByServerRelativeUrl('/{1}/Lists/{2}/Attachments/{3}/{4}')", siteUrl, relativeUrl, listName, itemID, fileName));

                endpointRequest.Method = "DELETE";
                endpointRequest.Accept = "application/json;odata=verbose";
                endpointRequest.ContentType = "application/json;odata=verbose";
                endpointRequest.Headers.Add("X-RequestDigest", formDigest);
                endpointRequest.Credentials = System.Net.CredentialCache.DefaultCredentials;
                try
                {
                    using (HttpWebResponse itemResponse = (HttpWebResponse)endpointRequest.GetResponse())
                    {
                        using (StreamReader responseReader = new StreamReader(itemResponse.GetResponseStream()))
                        {
                            string response = responseReader.ReadToEnd();
                        }
                    }
                }
                catch (WebException ex)
                {
                    HttpWebResponse errorResponse = ex.Response as HttpWebResponse;
                    string method = string.Concat(MethodBase.GetCurrentMethod().Name, " - Url - ", siteUrl, ", ListName - ", listName, ", itemID - ", itemID);
                    string guid = WebException(ex, method);
                    throw new Exception(string.Format("An error occured while reading data. GUID: {0}", guid));
                }
                catch (Exception ex)
                {
                    string guid = WriteException(ex, MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.Name, string.Empty);
                    throw new Exception(string.Format("An error occured while reading data. GUID: {0}", guid));
                }

                return FileName;
            }
            catch (Exception ex)
            {
                string guid = WriteException(ex, MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.Name, string.Empty);
                throw new Exception(string.Format("An error occured while reading data. GUID: {0}", guid));
            }
        }


        public static async Task<string> DeleteAttachmentFromListByDefaultAdminAsync(string fileName, string siteUrl, string relativeUrl, string errorLogPath, string listName, string itemID)
        {
            string FileName = string.Empty;
            try
            {
                XmlNamespaceManager xmlnspm = AddXmlNameSpaces();
                var formDigestNode = GetFormDigest(siteUrl, xmlnspm);
                string formDigest = formDigestNode.InnerXml;


                HttpWebRequest endpointRequest = (HttpWebRequest)HttpWebRequest.Create(string.Format("{0}/_api/web/getFileByServerRelativeUrl('/{1}/Lists/{2}/Attachments/{3}/{4}')", siteUrl, relativeUrl, listName, itemID, fileName));

                endpointRequest.Method = "DELETE";
                endpointRequest.Accept = "application/json;odata=verbose";
                endpointRequest.ContentType = "application/json;odata=verbose";
                endpointRequest.Headers.Add("X-RequestDigest", formDigest);
                endpointRequest.Credentials = System.Net.CredentialCache.DefaultCredentials;
                try
                {
                    using (HttpWebResponse itemResponse = (HttpWebResponse)endpointRequest.GetResponse())
                    {
                        using (StreamReader responseReader = new StreamReader(itemResponse.GetResponseStream()))
                        {
                            string response = responseReader.ReadToEnd();
                        }
                    }
                }
                catch (WebException ex)
                {
                    HttpWebResponse errorResponse = ex.Response as HttpWebResponse;
                    string method = string.Concat(MethodBase.GetCurrentMethod().Name, " - Url - ", siteUrl, ", ListName - ", listName, ", itemID - ", itemID);
                    string guid = WebException(ex, method);
                    throw new Exception(string.Format("An error occured while reading data. GUID: {0}", guid));
                }
                catch (Exception ex)
                {
                    string guid = WriteException(ex, MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.Name, string.Empty);
                    throw new Exception(string.Format("An error occured while reading data. GUID: {0}", guid));
                }

                return FileName;
            }
            catch (Exception ex)
            {
                string guid = WriteException(ex, MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.Name, string.Empty);
                throw new Exception(string.Format("An error occured while reading data. GUID: {0}", guid));
            }
        }

        /// <summary>
        /// Upload Files to Pofile Bank
        /// </summary>
        /// <param name="UploadedFiles"></param>
        /// <param name="siteUrl"></param>
        /// <param name="errorLogPath"></param>
        /// <param name="accessToken"></param>
        /// <param name="DocumentLibUrl"></param>
        /// <param name="DocumentLibListUrl"></param>
        /// <param name="FolderName"></param>
        /// <param name="Overwrite"></param>
        /// <param name="itemPostBody"></param>
        /// <param name="xmlnspm"></param>
        /// <param name="formDigestNode"></param>
        /// <returns></returns>
        public static string UploadFilesInFolder(HttpFileCollection uploadedFiles, string siteUrl, string errorLogPath, string accessToken, string documentLibUrl, string documentLibListUrl, string folderName, bool overwrite, string itemPostBody, out string fileName)
        {
            string filePath = string.Empty;
            try
            {
                XmlNamespaceManager xmlnspm = AddXmlNameSpaces();
                var formDigestNode = GetFormDigest(siteUrl, xmlnspm);

                string formDigest = formDigestNode.InnerXml;
                //string entityTypeName = GetEntityTypeName(siteUrl, documentLibListUrl, xmlnspm);
                fileName = string.Empty;
                if (uploadedFiles.Count > 0)
                {
                    string MonthFolder = Convert.ToString(DateTime.Now.ToString("MMMM"));
                    string Year_MonthFolder = Convert.ToString(DateTime.Now.Year) + "_" + MonthFolder;
                    CreateFolder(siteUrl, errorLogPath, accessToken, documentLibUrl, "", Year_MonthFolder, false, xmlnspm, formDigestNode);
                    //CommonEntities.CreateFolder(siteUrl, errorLogPath, accessToken, documentLibUrl, YearFolder, MonthFolder, false, xmlnspm, formDigestNode);
                    string NewFileWithExtention = string.Empty;
                    int Counter = 0;
                    foreach (string file in uploadedFiles)
                    {
                        var postedFile = uploadedFiles[Counter];
                        var FileName = postedFile.FileName;
                        var FileExtention = Path.GetExtension(FileName);
                        byte[] fileData = null;
                        using (var binaryReader = new BinaryReader(postedFile.InputStream))
                        {
                            fileData = binaryReader.ReadBytes(postedFile.ContentLength);
                        }
                        string NewFileName = Convert.ToString(Guid.NewGuid());
                        NewFileWithExtention = NewFileName + FileExtention;
                        HttpWebRequest endpointRequest = (HttpWebRequest)HttpWebRequest.Create(siteUrl + "/_api/web/GetFolderByServerRelativeUrl('" + documentLibUrl + "/" + Year_MonthFolder + "')/Files/add(url='" + NewFileWithExtention + "',overwrite=" + Convert.ToString(overwrite).ToLower() + ")");
                        endpointRequest.Method = "POST";
                        endpointRequest.Headers.Add("binaryStringRequestBody", "true");
                        endpointRequest.Headers.Add("Authorization", "Bearer " + accessToken);
                        endpointRequest.Headers.Add("X-RequestDigest", formDigest);
                        endpointRequest.Credentials = System.Net.CredentialCache.DefaultCredentials;
                        endpointRequest.GetRequestStream().Write(fileData, 0, fileData.Length);

                        try
                        {
                            HttpWebResponse endpointresponse = (HttpWebResponse)endpointRequest.GetResponse();
                            //endpointRequest.KeepAlive = false;
                            UpdateMetadata(siteUrl, errorLogPath, accessToken, documentLibUrl, Year_MonthFolder, FileName, NewFileWithExtention, itemPostBody, documentLibListUrl, xmlnspm, formDigestNode);

                        }
                        catch (WebException ex)
                        {
                            HttpWebResponse errorResponse = ex.Response as HttpWebResponse;

                            if (errorResponse.StatusCode == HttpStatusCode.NotFound)
                            {

                            }

                        }
                        catch (Exception ex)
                        {
                            string guid = WriteException(ex, MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.Name, string.Empty);

                            throw new Exception(string.Format("An error occured while reading data. GUID: {0}", guid));
                        }
                        Counter++;
                    }
                    if (Counter > 0)
                    {
                        filePath = string.Concat(siteUrl, "/", documentLibUrl, "/", Year_MonthFolder, "/", NewFileWithExtention);
                        fileName = NewFileWithExtention;
                    }
                }
            }
            catch (Exception ex)
            {
                string guid = WriteException(ex, MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.Name, string.Empty);
                throw new Exception(string.Format("An error occured while reading data. GUID: {0}", guid));
            }
            return filePath;

        }

        /// <summary>
        /// Create Folders and SubFolders in Document Library
        /// </summary>
        /// <param name="siteUrl"></param>
        /// <param name="errorLogPath"></param>
        /// <param name="accessToken"></param>
        /// <param name="listUrl"></param>
        /// <param name="rootFolderName"></param>
        /// <param name="SubFolderName"></param>
        /// <param name="Overwrite"></param>
        /// <returns></returns>
        public static bool CreateFolder(string siteUrl, string errorLogPath, string accessToken, string listUrl, string rootFolderName, string subFolderName, bool overwrite, XmlNamespaceManager xmlnspm, XmlNode formDigestNode)
        {
            bool isFileUploaded = false;
            try
            {
                string formDigest = formDigestNode.InnerXml;
                //string entityTypeName = GetEntityTypeName(siteUrl, listUrl, xmlnspm);

                HttpWebRequest endpointRequest;
                if (!string.IsNullOrEmpty(rootFolderName))
                    endpointRequest = (HttpWebRequest)HttpWebRequest.Create(siteUrl + "/_api/web/GetFolderByServerRelativeUrl('" + listUrl + "/" + rootFolderName + "')" + "/folders/add(url=\'" + subFolderName + "\')");
                else
                    endpointRequest = (HttpWebRequest)HttpWebRequest.Create(siteUrl + "/_api/web/Lists/GetByTitle('" + listUrl + "')" + "/rootfolder/folders/add(url=\'" + subFolderName + "\')");
                endpointRequest.Method = "POST";
                //endpointRequest.Headers.Add("binaryStringRequestBody", "true");
                endpointRequest.Headers.Add("Authorization", "Bearer " + accessToken);
                endpointRequest.Headers.Add("X-RequestDigest", formDigest);
                endpointRequest.ContentType = "application/json; charset=utf-8";

                endpointRequest.ContentLength = 0;
                endpointRequest.Credentials = System.Net.CredentialCache.DefaultCredentials;

                //endpointRequest.GetRequestStream().Write(fileData, 0, fileData.Length);

                using (HttpWebResponse endpointresponse = (HttpWebResponse)endpointRequest.GetResponse()) ;
                //endpointRequest.KeepAlive = false;

                isFileUploaded = true;

            }
            catch (Exception ex)
            {
                isFileUploaded = false;
                string guid = WriteException(ex, MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.Name, string.Empty);
                throw new Exception(string.Format("An error occured while reading data. GUID: {0}", guid));
            }

            return isFileUploaded;

        }
        public static bool MoveFolder(string siteUrl, string accessToken, string FileServerRelativeUrl, string MoveToFileUrl)
        {
            bool isFileUploaded = false;
            try
            {
                XmlNamespaceManager xmlnspm = AddXmlNameSpaces();
                var formDigestNode = GetFormDigest(siteUrl, xmlnspm);

                string formDigest = formDigestNode.InnerXml;
                HttpWebRequest endpointRequest;
                endpointRequest = (HttpWebRequest)HttpWebRequest.Create(siteUrl + "/_api/web/getfilebyserverrelativeurl('" + FileServerRelativeUrl + "')/moveto(newurl='" + MoveToFileUrl + "',flags=1)");
                endpointRequest.Method = "POST";
                endpointRequest.Headers.Add("Authorization", "Bearer " + accessToken);
                endpointRequest.Headers.Add("X-RequestDigest", formDigest);
                endpointRequest.ContentType = "application/json; charset=utf-8";

                endpointRequest.ContentLength = 0;
                endpointRequest.Credentials = System.Net.CredentialCache.DefaultCredentials;
                using (HttpWebResponse endpointresponse = (HttpWebResponse)endpointRequest.GetResponse()) ;

                isFileUploaded = true;

            }
            catch (Exception ex)
            {
                isFileUploaded = false;
                string guid = WriteException(ex, MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.Name, string.Empty);
                throw new Exception(string.Format("An error occured while reading data. GUID: {0}", guid));
            }

            return isFileUploaded;

        }

        public static bool CreateListFolder(string siteUrl, string errorLogPath, string accessToken, string listUrl, string rootFolderName, string subFolderName, bool overwrite, XmlNamespaceManager xmlnspm, XmlNode formDigestNode)
        {
            bool isFileUploaded = false;
            try
            {
                string formDigest = formDigestNode.InnerXml;
                //string entityTypeName = GetEntityTypeName(siteUrl, listUrl, xmlnspm);
                ///string entityTypeName = "{ '__metadata': { 'type': 'SP.Folder' }, 'ServerRelativeUrl': '/Candidate Master/folderq'}";

                HttpWebRequest endpointRequest;
                endpointRequest = (HttpWebRequest)HttpWebRequest.Create(siteUrl + "/_api/web/Lists/GetByTitle('" + "Candidate Master" + "')" + "/rootfolder/folders/add(url=\'" + subFolderName + "\')");
                endpointRequest.Method = "POST";
                endpointRequest.Headers.Add("Authorization", "Bearer " + accessToken);
                endpointRequest.Headers.Add("X-RequestDigest", formDigest);
                endpointRequest.ContentType = "application/json;odata=verbose";
                endpointRequest.Accept = "application/json;odata=verbose";

                endpointRequest.ContentLength = 0;
                endpointRequest.Credentials = System.Net.CredentialCache.DefaultCredentials;

                using (HttpWebResponse endpointresponse = (HttpWebResponse)endpointRequest.GetResponse()) ;
                //endpointRequest.KeepAlive = false;
                isFileUploaded = true;

            }
            catch (Exception ex)
            {
                isFileUploaded = false;
                string guid = WriteException(ex, MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.Name, string.Empty);
                throw new Exception(string.Format("An error occured while reading data. GUID: {0}", guid));
            }

            return isFileUploaded;

        }

        public static string AddListItemsFolder(string siteUrl, string errorLogPath, string accessToken, string listUrl, string listItemUrl, string itemPostBody, XmlNamespaceManager xmlnspm, XmlNode formDigestNode)
        {
            try
            {
                string FormDigest = formDigestNode.InnerXml;
                string EntityTypeName = GetEntityTypeName(siteUrl, listUrl, xmlnspm);
                itemPostBody = "{'__metadata':{'type':'SP.Data.Candidate_x0020_MasterListItem'},'Title':'C6145441003','FolderServerRelativeUrl': '/sites/rms/Lists/Candidate%20Master/2016_May'}";
                //itemPostBody = itemPostBody.Replace("{0}", EntityTypeName);
                Byte[] itemPostData = System.Text.Encoding.ASCII.GetBytes(itemPostBody);

                HttpWebRequest ItemRequest =
                        (HttpWebRequest)HttpWebRequest.Create(siteUrl + listItemUrl);
                ItemRequest.Method = "POST";
                ItemRequest.ContentLength = itemPostBody.Length;
                ItemRequest.ContentType = "application/json;odata=verbose";
                ItemRequest.Accept = "application/json;odata=verbose";
                ItemRequest.Headers.Add("Authorization", "Bearer " + accessToken);
                ItemRequest.Credentials = System.Net.CredentialCache.DefaultCredentials;

                ItemRequest.Headers.Add("X-RequestDigest", FormDigest);
                using (Stream itemRequestStream = ItemRequest.GetRequestStream())
                {
                    itemRequestStream.Write(itemPostData, 0, itemPostData.Length);
                    itemRequestStream.Close();
                }

                using (HttpWebResponse itemResponse = (HttpWebResponse)ItemRequest.GetResponse()) ;
                //ItemRequest.KeepAlive = false;
                return null;
            }
            catch (Exception ex)
            {
                string guid = WriteException(ex, MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.Name, string.Empty);
                throw new Exception(string.Format("An error occured while reading data. GUID: {0}", guid));
            }

        }

        /// <summary>
        /// Update Metadata of Files in Document library
        /// </summary>
        /// <param name="siteUrl"></param>
        /// <param name="errorLogPath"></param>
        /// <param name="accessToken"></param>
        /// <param name="listUrl"></param>
        /// <param name="rootFolderName"></param>
        /// <param name="subFolderName"></param>
        /// <param name="FileName"></param>
        /// <param name="itemPostBody"></param>
        /// <param name="docLibraryUrl"></param>
        /// <returns></returns>
        public static bool UpdateMetadata(string siteUrl, string errorLogPath, string accessToken, string listUrl, string rootFolderName, string fileName, string internalFileName, string itemPostBody, string docLibraryUrl, XmlNamespaceManager xmlnspm, XmlNode formDigestNode)
        {
            bool isFileUploaded = false;
            try
            {

                string formDigest = formDigestNode.InnerXml;
                string entityTypeName = GetEntityTypeName(siteUrl, docLibraryUrl, xmlnspm);
                itemPostBody = itemPostBody.Replace("{0}", entityTypeName);
                itemPostBody = itemPostBody.Replace("{1}", fileName);
                Byte[] itemPostData = System.Text.Encoding.ASCII.GetBytes(itemPostBody);


                HttpWebRequest endpointRequest;
                endpointRequest = (HttpWebRequest)HttpWebRequest.Create(siteUrl + "/_api/web/GetFolderByServerRelativeUrl('" + listUrl + "/" + rootFolderName + "')/Files('" + internalFileName + "')/ListItemAllFields");
                endpointRequest.Method = "POST";

                endpointRequest.Headers.Add("X-HTTP-Method", "MERGE");
                endpointRequest.Headers.Add("IF-MATCH", "*");
                endpointRequest.ContentLength = itemPostBody.Length;
                endpointRequest.ContentType = "application/json;odata=verbose";
                endpointRequest.Accept = "application/json;odata=verbose";
                endpointRequest.Headers.Add("Authorization", "Bearer " + accessToken);
                endpointRequest.Credentials = System.Net.CredentialCache.DefaultCredentials;
                endpointRequest.Headers.Add("X-RequestDigest", formDigest);
                using (Stream itemRequestStream = endpointRequest.GetRequestStream())
                {
                    itemRequestStream.Write(itemPostData, 0, itemPostData.Length);
                    itemRequestStream.Close();
                }
                using (HttpWebResponse itemResponse = (HttpWebResponse)endpointRequest.GetResponse()) ;
                //endpointRequest.KeepAlive = false;
                //Update Folder Path in Candidate Master
                isFileUploaded = true;

            }
            catch (Exception ex)
            {
                isFileUploaded = false;
                string guid = WriteException(ex, MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.Name, string.Empty);
                throw new Exception(string.Format("An error occured while reading data. GUID: {0}", guid));
            }

            return isFileUploaded;

        }

        /// <summary>
        /// Update Metadata of Files in Document library
        /// </summary>
        /// <param name="siteUrl"></param>
        /// <param name="errorLogPath"></param>
        /// <param name="accessToken"></param>
        /// <param name="listUrl"></param>
        /// <param name="rootFolderName"></param>
        /// <param name="subFolderName"></param>
        /// <param name="FileName"></param>
        /// <param name="itemPostBody"></param>
        /// <param name="docLibraryUrl"></param>
        /// <returns></returns>
        public static bool UpdateMetadataWithoutFolderStructure(string siteUrl, string errorLogPath, string accessToken, string listUrl, string fileName, string internalFileName, string itemPostBody, string docLibraryUrl, XmlNamespaceManager xmlnspm, XmlNode formDigestNode)
        {
            bool isFileUploaded = false;
            try
            {

                string formDigest = formDigestNode.InnerXml;
                string entityTypeName = GetEntityTypeName(siteUrl, docLibraryUrl, xmlnspm);
                itemPostBody = itemPostBody.Replace("{0}", entityTypeName);
                itemPostBody = itemPostBody.Replace("{1}", fileName);
                Byte[] itemPostData = System.Text.Encoding.ASCII.GetBytes(itemPostBody);


                HttpWebRequest endpointRequest;
                endpointRequest = (HttpWebRequest)HttpWebRequest.Create(siteUrl + "/_api/web/GetFolderByServerRelativeUrl('" + listUrl + "')/Files('" + internalFileName + "')/ListItemAllFields");
                endpointRequest.Method = "POST";

                endpointRequest.Headers.Add("X-HTTP-Method", "MERGE");
                endpointRequest.Headers.Add("IF-MATCH", "*");
                endpointRequest.ContentLength = itemPostBody.Length;
                endpointRequest.ContentType = "application/json;odata=verbose";
                endpointRequest.Accept = "application/json;odata=verbose";
                endpointRequest.Headers.Add("Authorization", "Bearer " + accessToken);
                endpointRequest.Credentials = System.Net.CredentialCache.DefaultCredentials;
                endpointRequest.Headers.Add("X-RequestDigest", formDigest);
                using (Stream itemRequestStream = endpointRequest.GetRequestStream())
                {
                    itemRequestStream.Write(itemPostData, 0, itemPostData.Length);
                    itemRequestStream.Close();
                }
                using (HttpWebResponse itemResponse = (HttpWebResponse)endpointRequest.GetResponse()) ;
                //endpointRequest.KeepAlive = false;
                //Update Folder Path in Candidate Master
                isFileUploaded = true;

            }
            catch (Exception ex)
            {
                isFileUploaded = false;
                string guid = WriteException(ex, MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.Name, string.Empty);
                throw new Exception(string.Format("An error occured while reading data. GUID: {0}", guid));
            }

            return isFileUploaded;

        }


        public static Stream RetrieveFileViaREST(string fileUrl)
        {
            HttpWebRequest request = null;
            string commandString = string.Empty;
            commandString = fileUrl;
            Stream strm;
            Uri uri = new Uri(commandString);

            //Set up the HTTP Request -- ASSUMING YOU ARE USING NTLM -- IF NOT THEN PASS IN THE CORRECT CREDENTIALS
            request = (HttpWebRequest)WebRequest.Create(uri);
            request.Credentials = CredentialCache.DefaultCredentials;
            request.Method = WebRequestMethods.Http.Get;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                //if (response.ContentLength > 0) response.ContentLength = response.ContentLength;
                strm = response.GetResponseStream();
            }
            //request.KeepAlive = false;

            return strm;

        }

        public static StreamReader RetrieveFileStreamReaderViaREST(string fileUrl)
        {
            HttpWebRequest request = null;
            string commandString = string.Empty;
            commandString = fileUrl;

            Uri uri = new Uri(commandString);

            //Set up the HTTP Request -- ASSUMING YOU ARE USING NTLM -- IF NOT THEN PASS IN THE CORRECT CREDENTIALS
            request = (HttpWebRequest)WebRequest.Create(uri);
            request.Credentials = CredentialCache.DefaultCredentials;
            request.Method = WebRequestMethods.Http.Get;

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            if (response.ContentLength > 0) response.ContentLength = response.ContentLength;
            return new StreamReader(response.GetResponseStream());
        }

        public static string RetrieveFileBinary(string fileUrl)
        {
            string base64String = string.Empty;
            try
            {
                Uri uri = new Uri(fileUrl);

                //Set up the HTTP Request -- ASSUMING YOU ARE USING NTLM -- IF NOT THEN PASS IN THE CORRECT CREDENTIALS
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
                request.Credentials = CredentialCache.DefaultCredentials;
                request.Method = WebRequestMethods.Http.Get;
                request.Accept = "application/json;odata=verbose";
                request.ContentType = "application/json; charset=utf-8";

                HttpWebResponse webResponse = (HttpWebResponse)request.GetResponse();

                //if (webResponse.ContentLength > 0)
                //    webResponse.ContentLength = webResponse.ContentLength;
                Stream documentStream = webResponse.GetResponseStream();
                MemoryStream memoryStream = new MemoryStream();
                using (Stream input = documentStream)
                {
                    input.CopyTo(memoryStream);
                }
                memoryStream.Position = 0;
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StreamContent(memoryStream);

                byte[] imageBytes = memoryStream.ToArray();
                base64String = Convert.ToBase64String(imageBytes);

                return base64String;
            }
            catch (Exception ex)
            {
                string method = string.Concat(MethodBase.GetCurrentMethod().Name, " FileUrl - ", fileUrl);
                string guid = WriteException(ex, method, MethodBase.GetCurrentMethod().DeclaringType.Name, string.Empty);
                throw new Exception(string.Format("An error occured while reading data. GUID: {0}", guid));
            }

        }

        public static byte[] RetrieveFileByteViaREST(string fileUrl)
        {
            try
            {
                byte[] buffer = new byte[2048];
                byte[] data;
                Uri uri = new Uri(fileUrl);

                WebRequest request = WebRequest.Create(uri);
                request.Credentials = CredentialCache.DefaultCredentials;
                using (WebResponse response = request.GetResponse())
                {
                    using (Stream responseStream = response.GetResponseStream())
                    {
                        using (MemoryStream ms = new MemoryStream())
                        {
                            int count = 0;
                            do
                            {
                                count = responseStream.Read(buffer, 0, buffer.Length);
                                ms.Write(buffer, 0, count);
                            } while (count != 0);
                            data = ms.ToArray();
                        }
                    }
                }

                return data;
            }
            catch (Exception ex)
            {
                string method = string.Concat(MethodBase.GetCurrentMethod().Name, " FileUrl - ", fileUrl);
                string guid = WriteException(ex, method, MethodBase.GetCurrentMethod().DeclaringType.Name, string.Empty);
                throw new Exception(string.Format("An error occured while reading data. GUID: {0}", guid));
            }

        }

        public static string DeleteListItems(string siteUrl, string errorLogPath, string listUrl, string listItemUrl, string id)
        {
            try
            {
                XmlNamespaceManager xmlnspm = AddXmlNameSpaces();
                var formDigestNode = GetFormDigest(siteUrl, xmlnspm);

                string FormDigest = formDigestNode.InnerXml;
                // string EntityTypeName = GetEntityTypeName(siteUrl, listUrl, xmlnspm);
                HttpWebRequest ItemRequest =
                        (HttpWebRequest)HttpWebRequest.Create(siteUrl + listItemUrl + "(" + id + ")");
                ItemRequest.Method = "POST";
                ItemRequest.Headers.Add("X-HTTP-Method", "DELETE");
                ItemRequest.Headers.Add("IF-MATCH", "*");
                //ItemRequest.Headers.Add("Authorization", "Bearer " + accessToken);
                ItemRequest.Credentials = System.Net.CredentialCache.DefaultCredentials;

                ItemRequest.Headers.Add("X-RequestDigest", FormDigest);
                using (Stream itemRequestStream = ItemRequest.GetRequestStream())
                {
                    itemRequestStream.Close();
                }
                using (HttpWebResponse itemResponse = (HttpWebResponse)ItemRequest.GetResponse()) ;
                //ItemRequest.KeepAlive = false;
                return null;
            }
            catch (Exception ex)
            {
                string method = string.Concat(MethodBase.GetCurrentMethod().Name, " SiteUrl - ", siteUrl, ", ListUrl - ", listUrl, ", ID - ", id);
                string guid = WriteException(ex, method, MethodBase.GetCurrentMethod().DeclaringType.Name, string.Empty);
                throw new Exception(string.Format("An error occured while reading data. GUID: {0}", guid));
            }

        }


        public static async Task<string> DeleteListItemsAsync(string siteUrl, string errorLogPath, string listUrl, string listItemUrl, string id)
        {
            try
            {
                XmlNamespaceManager xmlnspm = AddXmlNameSpaces();
                var formDigestNode = GetFormDigest(siteUrl, xmlnspm);

                string FormDigest = formDigestNode.InnerXml;
                // string EntityTypeName = GetEntityTypeName(siteUrl, listUrl, xmlnspm);
                HttpWebRequest ItemRequest = (HttpWebRequest)HttpWebRequest.Create(siteUrl + listItemUrl + "(" + id + ")");
                ItemRequest.Method = "POST";
                ItemRequest.Headers.Add("X-HTTP-Method", "DELETE");
                ItemRequest.Headers.Add("IF-MATCH", "*");
                //ItemRequest.Headers.Add("Authorization", "Bearer " + accessToken);
                ItemRequest.Credentials = System.Net.CredentialCache.DefaultCredentials;

                ItemRequest.Headers.Add("X-RequestDigest", FormDigest);
                using (Stream itemRequestStream = ItemRequest.GetRequestStream())
                {
                    itemRequestStream.Close();
                }
                using (HttpWebResponse itemResponse = (HttpWebResponse)ItemRequest.GetResponse()) ;
                //ItemRequest.KeepAlive = false;
                return null;
            }
            catch (Exception ex)
            {
                string method = string.Concat(MethodBase.GetCurrentMethod().Name, " SiteUrl - ", siteUrl, ", ListUrl - ", listUrl, ", ID - ", id);
                string guid = WriteException(ex, method, MethodBase.GetCurrentMethod().DeclaringType.Name, string.Empty);
                throw new Exception(string.Format("An error occured while reading data. GUID: {0}", guid));
            }

        }

        /// <summary>
        /// Add Namespaces to Xmlnode
        /// </summary>
        /// <returns></returns>
        public static XmlNamespaceManager AddXmlNameSpaces()
        {

            try
            {
                XmlNamespaceManager xmlnspm = new XmlNamespaceManager(new NameTable());

                xmlnspm.AddNamespace("atom", "http://www.w3.org/2005/Atom");
                xmlnspm.AddNamespace("d", "http://schemas.microsoft.com/ado/2007/08/dataservices");
                xmlnspm.AddNamespace("m", "http://schemas.microsoft.com/ado/2007/08/dataservices/metadata");
                return xmlnspm;

            }
            catch (Exception ex)
            {
                string guid = WriteException(ex, MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.Name, string.Empty);
                throw new Exception(string.Format("An error occured while reading data. GUID: {0}", guid));
            }
        }

        /// <summary>
        /// Get entity type name 
        /// </summary>
        /// <param name="siteUrl"></param>
        /// <param name="listUrl"></param>
        /// <param name="xmlnspm"></param>
        /// <returns></returns>
        public static string GetEntityTypeName(string siteUrl, string listUrl, XmlNamespaceManager xmlnspm)
        {
            string entitytypeName = string.Empty;
            try
            {
                HttpWebRequest listRequest = (HttpWebRequest)HttpWebRequest.Create(siteUrl + listUrl);
                listRequest.Method = "GET";
                listRequest.Accept = "application/atom+xml";
                listRequest.ContentType = "application/atom+xml;type=entry";
                listRequest.Credentials = System.Net.CredentialCache.DefaultCredentials;
                HttpWebResponse listResponse = (HttpWebResponse)listRequest.GetResponse();
                using (StreamReader listReader = new StreamReader(listResponse.GetResponseStream()))
                {
                    var listXml = new XmlDocument();
                    listXml.LoadXml(listReader.ReadToEnd());
                    var entityTypeNode = listXml.SelectSingleNode("//atom:entry/atom:content/m:properties/d:ListItemEntityTypeFullName", xmlnspm);
                    var listNameNode = listXml.SelectSingleNode("//atom:entry/atom:content/m:properties/d:Title", xmlnspm);
                    entitytypeName = entityTypeNode.InnerXml;
                }
                //listRequest.KeepAlive = false;

            }
            catch (Exception ex)
            {
                string method = string.Concat(MethodBase.GetCurrentMethod().Name, " - SiteUrl - ", siteUrl, " - ListUrl - ", listUrl);
                string guid = WriteException(ex, method, MethodBase.GetCurrentMethod().DeclaringType.Name, string.Empty);
                throw new Exception(string.Format("An error occured while reading data. GUID: {0}", guid));
            }
            return entitytypeName;
        }

        public static Claim TryGetClaim(string key)
        {
            try
            {
                var identity = HttpContext.Current.Request.RequestContext.HttpContext.User.Identity as ClaimsIdentity;
                if (identity != null)
                    return identity.Claims.FirstOrDefault(o => o.Type.Equals(key));
                else
                    return null;
            }
            catch (Exception ex)
            {

                string guid = WriteException(ex, MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.Name, string.Empty);
                throw new Exception(string.Format("An error occured while reading data. GUID: {0}", guid));
            }
        }

        public static JObject GetPublishingImages(string siteUrl, string accessToken, string FolderServerRelativeUrl)
        {
            JObject jobj;
            try
            {
                HttpWebRequest endpointRequest;
                endpointRequest = (HttpWebRequest)HttpWebRequest.Create(siteUrl + "/_api/Web/GetFolderByServerRelativeUrl('" + FolderServerRelativeUrl + "')?$selecte=*&$expand=Folders,Files");
                endpointRequest.Method = "GET";
                endpointRequest.Accept = "application/json;odata=verbose";
                endpointRequest.ContentType = "application/json; charset=utf-8";
                endpointRequest.Credentials = System.Net.CredentialCache.DefaultCredentials;

                using (HttpWebResponse endpointresponse = (HttpWebResponse)endpointRequest.GetResponse())
                {
                    using (StreamReader responseReader = new StreamReader(endpointresponse.GetResponseStream()))
                    {
                        string response = responseReader.ReadToEnd();
                        jobj = JObject.Parse(response);
                    }
                };
                return jobj;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }

    public class ErrorLogger
    {
        public void Log(String errorMessage, string MethodName, String GUID, string exceptionMessage, string exceptionStack, string innerException)
        {
            string filePath = @"C:\EservicesONLINEAPIErrorLog.txt";

            using (StreamWriter streamWriter = new StreamWriter(filePath))
            {
                streamWriter.WriteLine(errorMessage + " - " + MethodName + " - " + GUID + " - " + exceptionMessage +  " - " + exceptionStack + " - " + innerException);
                streamWriter.Close();
            }

        }
        public void Log(String errorMessage, string MethodName, String GUID, string exceptionMessage, string UserName, string exceptionStack, string innerException)
        {
            string filePath = @"C:\EservicesONLINEAPIErrorLog.txt";
        
            using (StreamWriter streamWriter = new StreamWriter(filePath))
            {
                streamWriter.WriteLine(errorMessage + " - " + MethodName + " - " + GUID + " - " + exceptionMessage + " - " + UserName + " - " + exceptionStack + " - " + innerException);
                streamWriter.Close();
            }
        
    }
    }
}
