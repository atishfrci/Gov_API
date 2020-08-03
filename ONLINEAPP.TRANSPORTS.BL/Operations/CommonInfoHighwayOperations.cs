using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using ONLINEAPP.MODEL;
using ONLINEAPP.TRANSPORTS.MODEL;
using RestSharp;

namespace ONLINEAPP.TRANSPORTS.BL.Operations
{
    public class CommonInfoHighwayOperations
    {

        internal string GetInfoHighwayData(string identificationNum, string queryId, string queryName)
        {
            string soapResponse = null;
            try
            {
                string url = "https://infohighway.govmu.org/ih-webservice/soap/query";
                var client = new RestClient(url);
                var request = new RestRequest(Method.POST);
                string body = BuildSoapBody(identificationNum, queryId, queryName);
                request.AddHeader("Cache-Control", "no-cache");
                request.AddHeader("Content-Type", "text/xml");
                request.AddParameter("undefined", body, ParameterType.RequestBody);

                IRestResponse response = client.Execute(request);
                if (response != null)
                {
                    soapResponse = response.Content;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return soapResponse;
        }


        internal string BuildSoapBody(string identificationNum, string queryId, string queryName)
        {
            string soapBody = null;
            try
            {
                string userId = Constants.IH_NTAUsername;
                string password = Constants.IH_NTAPassword;
                //string queryId = "NTA031";

                soapBody = "<soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:ws=\"http://ws.server.mhaccess.crimsonlogic.com/\">\r\n<soapenv:Header/>\r\n<soapenv:Body>\r\n<ws:query>\r\n<queryInput>\r\n<userId>" + userId + "</userId>\r\n<pass>" + password + "</pass>\r\n<queryId>" + queryId + "</queryId>\r\n<qwsInputParams>\r\n<field>" + queryName + "</field> \r\n<values>" + identificationNum + "</values>\r\n</qwsInputParams>\r\n</queryInput>\r\n</ws:query>\r\n</soapenv:Body>\r\n</soapenv:Envelope>";
            }
            catch (Exception)
            {
                throw;
            }
            return soapBody;
        }

        

    }
}
