using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ONLINEAPP.MODEL;
using ONLINEAPP.TRANSPORTS.MODEL;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace ONLINEAPP.TRANSPORTS.BL.Operations
{
    public class PaymentGatewayOperation
    {
        // get Payment Type 
        //get Currency 
        //Add Simple Transaction

        public static List<PaymentType> buildPaymentTypeUrl()
        {
            try
            {
                string paymentmessage = "";
                string paymentTypeUrl = "";
                List<PaymentType> paymentTypesfrompg = new List<PaymentType>(); 

                CommonPaymentGatewayOperation commonPgOp = new CommonPaymentGatewayOperation();

                string builtMessage = "1033&NationalRegmark&NTA&NTR";
                string[] paymentTypeMessage = { Constants.PG_AccessCode, Constants.PG_SiteCode, Constants.PG_OperationCode, "1033" };

                Array.Sort(paymentTypeMessage);

                foreach (string payval in paymentTypeMessage)
                {

                    if (payval == paymentTypeMessage.Last())
                    {
                        paymentmessage = paymentmessage + payval;
                    }
                    else
                    {
                        paymentmessage = paymentmessage + payval + "&";
                    }
                }

                if (paymentmessage != null || paymentmessage != "")
                {

                    string HMACresult = commonPgOp.generateSecureHash(Constants.PG_SecureKey, paymentmessage);

                    paymentTypeUrl = string.Concat(Constants.PG_Url, Constants.PG_PaymentTypeUrl, "?AccessCode=", Constants.PG_AccessCode, "&SiteCode=", Constants.PG_SiteCode, "&OperationCode=", Constants.PG_OperationCode, "&LCID=", "1033", "&SecureHash=", HMACresult);
                }

                if (paymentTypeUrl != "")
                {

                     paymentTypesfrompg = getPaymentTypes(paymentTypeUrl);

                }

                return paymentTypesfrompg; 
            }
            catch(Exception ex)
            {
                throw ex; 
            }
        }

         
        public static List<Currency> buildCurrencyUrl(string paymentTypeCode)
        {
            try
            {
                string currencymessage = "";
                string currencyUrl = "";

                List<Currency> CurrencyFrompg = new List<Currency>(); 

                CommonPaymentGatewayOperation commonPgOp = new CommonPaymentGatewayOperation();

                string builtMessageCurr = "1033&NationalRegmark&NTA&PYT2";

                string[] currencyMessage = { Constants.PG_AccessCode, Constants.PG_SiteCode, paymentTypeCode, "1033" };

                Array.Sort(currencyMessage);

                foreach (string curval in currencyMessage)
                {

                    if (curval == currencyMessage.Last())
                    {
                        currencymessage = currencymessage + curval;
                    }
                    else
                    {
                        currencymessage = currencymessage + curval + "&";
                    }
                }

                if (currencymessage != null || currencymessage != "")
                {

                    string HMACresult = commonPgOp.generateSecureHash(Constants.PG_SecureKey, currencymessage);

                    currencyUrl = string.Concat(Constants.PG_Url, Constants.PG_CurrencyUrl, "?AccessCode=", Constants.PG_AccessCode, "&SiteCode=", Constants.PG_SiteCode, "&PaymentCode=", paymentTypeCode, "&LCID=", "1033", "&SecureHash=", HMACresult);
                }

                if (currencyUrl != "")
                {

                    CurrencyFrompg = getCurrencies(currencyUrl); 

                }

                return CurrencyFrompg;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        

        public static List<PaymentType> getPaymentTypes(string paymentTypeUrl)
        {
            List<PaymentType> AllPaymentTypes = new List<PaymentType>();
            try
            {
                var client = new RestClient(paymentTypeUrl);
                IRestResponse response = client.Execute(new RestRequest());
                var PaymentTypeResponses = response.Content;

                JObject JsonObjects = new JObject();
                JsonObjects = JObject.Parse(PaymentTypeResponses);

                JArray JsonObjectscount = (JArray)JsonObjects["PaymentTypes"];
                int JsonObjectCount = JsonObjectscount.Count;

                for (int i = 0; i < JsonObjectCount; i++)
                {
                    PaymentType paymentTypeObj = new PaymentType();

                    paymentTypeObj.PaymentTypeName = JsonObjects["PaymentTypes"][i]["Name"].ToString();
                    paymentTypeObj.PaymentTypeCode = JsonObjects["PaymentTypes"][i]["Code"].ToString();

                    AllPaymentTypes.Add(paymentTypeObj);

                }

                return AllPaymentTypes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public static List<Currency> getCurrencies(string CurrencyUrl)
        {
            List<Currency> AllCurrencies = new List<Currency>();
            try
            {
                var client = new RestClient(CurrencyUrl);
                IRestResponse response = client.Execute(new RestRequest());
                var currencyResponses = response.Content;

                JObject JsonObjects = new JObject();
                JsonObjects = JObject.Parse(currencyResponses);

                JArray JsonObjectscount = (JArray)JsonObjects["Currencies"];
                int JsonObjectCount = JsonObjectscount.Count;

                for (int i = 0; i < JsonObjectCount; i++)
                {
                    Currency currencyObj = new Currency();

                    currencyObj.CurrencyId = JsonObjects["Currencies"][i]["CurrencyID"].ToString();
                    currencyObj.CurrencyName = JsonObjects["Currencies"][i]["Name"].ToString();
                    currencyObj.CurrencyCode = JsonObjects["Currencies"][i]["Code"].ToString();

                    AllCurrencies.Add(currencyObj); 
                }

                return AllCurrencies; 
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public static PG_Transactions buildUrlForSimpleTransaction(string Amount, string username , string userDisplayName, string requestId, string selectedCurrencyCode, string selectedPaymentTypeCode, string messageFromFE)
        {
            try
            {
                string simpleTransacMsg = "";
                string simpleTransacUrl = "";

                PG_Transactions simpletransacObj = new PG_Transactions(); 

                CommonPaymentGatewayOperation commonPgOp = new CommonPaymentGatewayOperation();

                string[] simpleTransacMsgArr = { "",Constants.PG_OperationCode , "Request Info", Amount , username ,userDisplayName ,Constants.PG_SimpleTransac_ReturnUrl , selectedCurrencyCode ,selectedPaymentTypeCode , "1033", Constants.PG_SiteCode, Constants.PG_AccessCode};
                Array.Sort(simpleTransacMsgArr);

                foreach (string simpletransacval in simpleTransacMsgArr)
                {

                    if (simpletransacval == simpleTransacMsgArr.Last())
                    {
                        simpleTransacMsg = simpleTransacMsg + simpletransacval;
                    }
                    else
                    {
                        simpleTransacMsg = simpleTransacMsg + simpletransacval + "&";
                    }
                }

                if (simpleTransacMsg != null || simpleTransacMsg != "")
                {
                   // simpleTransacMsg = "1033&2000&https://OnlineServ.govmu.org:3001&MUR&NationalRegmark&NTA&NTR&hemat&hematahadil&PYT2&Request Info";

                    string HMACresult = commonPgOp.generateSecureHash(Constants.PG_SecureKey, simpleTransacMsg);

                    simpleTransacUrl = string.Concat(Constants.PG_Url, Constants.PG_SimpleTransacUrl, "?OperationCode=", Constants.PG_OperationCode, "&OperationInfo=", "Request Info", "&Amount=", Amount, "&UserIdentity=", username , "&DisplayUserName=", userDisplayName , "&ReturnURL=" , Constants.PG_SimpleTransac_ReturnUrl, "&UserMessage=", "&CurrencyCode=", selectedCurrencyCode , "&PaymentCode=",  selectedPaymentTypeCode , "&LCID=", "1033", "&SiteCode=" , Constants.PG_SiteCode , "&AccessCode=", Constants.PG_AccessCode ,  "&SecureHash=", HMACresult);
                }

                if (simpleTransacUrl != "")
                {

                    simpletransacObj = addSimpleTransactions(simpleTransacUrl);

                }

                return simpletransacObj;
            }
            catch(Exception ex)
            {
                throw ex;  
            }

        }


        public static PG_Transactions addSimpleTransactions(string SimpleTransacUrl)
        {
            try
            {
                PG_Transactions paymentTransacObj = new PG_Transactions();

                var client = new RestClient(SimpleTransacUrl);
                IRestResponse response = client.Execute(new RestRequest());
                var simpleTransacResponses = response.Content;

                JObject JsonObjects = new JObject();
                JsonObjects = JObject.Parse(simpleTransacResponses);

                paymentTransacObj.TransactionGuid = JsonObjects["TransactionGuid"].ToString();
                paymentTransacObj.TransactionID = JsonObjects["TransactionID"].ToString();
                paymentTransacObj.RedirectUrl = JsonObjects["RedirectUrl"].ToString();
                paymentTransacObj.Status = JsonObjects["Status"].ToString();

                //var response22 = Response.redirect(JsonObjects["RedirectUrl"].ToString());

                return paymentTransacObj;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public static async Task<JObject> sbmGatewayTrasanction(string SbmPaymentUrl)
        {
            HttpClientHandler handler = new HttpClientHandler();
            handler.AllowAutoRedirect = true;
            HttpClient _httpClient = new HttpClient(handler);
            HttpResponseMessage response = await _httpClient.GetAsync(SbmPaymentUrl);
            JObject jobj;
            try
            {
                HttpWebRequest endpointRequest = (HttpWebRequest)HttpWebRequest.Create(SbmPaymentUrl);
                endpointRequest.Method = "GET";
                endpointRequest.Accept = "application/json;odata=verbose";
                endpointRequest.ContentType = "application/json;charset=utf-8";
                //endpointRequest.Credentials = credentials;
                //endpointRequest.Headers.Add("X-RequestDigest", FormDigest);
                using (HttpWebResponse itemResponse = (HttpWebResponse)endpointRequest.GetResponse())
                {
                    using (StreamReader responseReader = new StreamReader(itemResponse.GetResponseStream()))
                    {
                        string response1 = responseReader.ReadToEnd();
                        jobj = JObject.Parse(response1);
                    }
                }
                //endpointRequest.KeepAlive = false;
                return jobj;
            }
            catch (WebException ex)
            {
                string method = string.Concat(MethodBase.GetCurrentMethod().Name, " - RestUrl - ", SbmPaymentUrl);
               // string guid = WebException(ex, method);
                throw new Exception(string.Format("An error occured while reading data. GUID: {0}"));
            }
            catch (Exception ex)
            {
                string method = string.Concat(MethodBase.GetCurrentMethod().Name, " - RestUrl - ", SbmPaymentUrl);
                //string guid = WriteException(ex, method, MethodBase.GetCurrentMethod().DeclaringType.Name, string.Empty);
                throw new Exception(string.Format("An error occured while reading data. GUID: {0}"));
            }


        }



    }
}
