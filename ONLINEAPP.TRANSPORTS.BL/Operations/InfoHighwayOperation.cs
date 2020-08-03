using ONLINEAPP.MODEL;
using ONLINEAPP.TRANSPORTS.MODEL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using RestSharp; 

namespace ONLINEAPP.TRANSPORTS.BL.Operations
{
    public class InfoHighwayOperation
    {
        public HI_NIC ValidateNICwithInfoHighway(string nic, string token)
        {
            CommonInfoHighwayOperations CommonInfoHighwayOp = new CommonInfoHighwayOperations();

            HI_NIC ApplicantInfoDet = null;

            try
            {
                string nic_queryid = Constants.IH_NICParam;
                string nic_querypass = Constants.IH_NICQueryColumn;

                string infohighwayresults = CommonInfoHighwayOp.GetInfoHighwayData(nic, nic_queryid, nic_querypass);

                if (!string.IsNullOrEmpty(infohighwayresults))
                {
                    ApplicantInfoDet = ReadNICSoapResponse(infohighwayresults, out string faultError, out string statusError);   
                }

                return ApplicantInfoDet;
            }
            catch (Exception ex)
            {
                throw ex; 
            }
        }

        public HI_BRN ValidateBRNwithInfoHighway(string brn, string token)
        {
            CommonInfoHighwayOperations CommonInfoHighwayOp = new CommonInfoHighwayOperations();

            HI_BRN CompanyDet = null; 

            try
            {
                string brn_queryid = Constants.IH_BRNParam;
                string brn_querypass = Constants.IH_BRNQueryColumn;

                string infohighwayresults = CommonInfoHighwayOp.GetInfoHighwayData(brn, brn_queryid, brn_querypass);

                if (!string.IsNullOrEmpty(infohighwayresults))
                {
                    CompanyDet = ReadBRNSoapResponse(infohighwayresults, out string faultError, out string statusError);
                }

                return CompanyDet;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public HI_RegistrationMark ValidateWithListOfAvailableNumbersFromInfoHighway (string regnum)
        {
            CommonInfoHighwayOperations CommonInfoHighwayOp = new CommonInfoHighwayOperations();

            HI_RegistrationMark AvailableRegMark = null;

            try
            {
                string ntaavailable_queryid = Constants.IH_NTAAvailableMarkParam;
                string ntaavailable_querypass = Constants.IH_NTAAvailableMarkQueryColumn;

                string infohighwayresults = CommonInfoHighwayOp.GetInfoHighwayData(regnum, ntaavailable_queryid, ntaavailable_querypass);

                if (!string.IsNullOrEmpty(infohighwayresults))
                {
                   AvailableRegMark = ReadAvailableRegMarkSoapResponse(infohighwayresults, out string faultError, out string statusError);
                }

                return AvailableRegMark; 
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public HI_RegistrationMark ValidateWithListOfSoldNumbersFromInfoHighway(string regnum)
        {
            CommonInfoHighwayOperations CommonInfoHighwayOp = new CommonInfoHighwayOperations();

            HI_RegistrationMark SoldRegMark = null;

            try
            {
                string ntaused_queryid = Constants.IH_NTAUsedMarkParam;
                string ntaused_querypass = Constants.IH_NTAUsedMarkQueryColumn;

                string infohighwayresults = CommonInfoHighwayOp.GetInfoHighwayData(regnum, ntaused_queryid, ntaused_querypass);

                if (!string.IsNullOrEmpty(infohighwayresults))
                {
                    SoldRegMark = ReadUsedRegMarkSoapResponse(infohighwayresults, out string faultError, out string statusError);
                }

                return SoldRegMark;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        internal HI_NIC ReadNICSoapResponse(string NICsoapResponse, out string faultError, out string statusError)
        {
            statusError = "";
            faultError = "";
            HI_NIC ApplicantInfo = new HI_NIC();

            try
            {
                if (!string.IsNullOrEmpty(NICsoapResponse))
                {
                    StringReader stringReader = new StringReader(NICsoapResponse);
                    Dictionary<string, string> data = new Dictionary<string, string>();

                    using (XmlReader xmlReader = XmlReader.Create(stringReader, new XmlReaderSettings { IgnoreComments = true, IgnoreWhitespace = true }))
                    {
                        xmlReader.MoveToContent();
                        XElement xElement = (XElement)XElement.ReadFrom(xmlReader);

                        if (xElement.Descendants("faultDescription").First().Value.ToLower().Trim().Equals(Constants.IH_SuccessfulQuery.ToLower()))
                        {
                            if (xElement.Descendants("statusCode").First().Value.ToLower().Trim().Equals(Constants.IH_NICStatusCode.ToLower()))
                            {
                                IEnumerable<XElement> lstXFields = from field in xElement.Descendants("fields")
                                                                   select field;

                                if (lstXFields != null)
                                {
                                    IEnumerable<XElement> lstXValues = from value in xElement.Descendants("value")
                                                                       select value;

                                    if (lstXValues != null)
                                    {
                                        ApplicantInfo = new HI_NIC();

                                        //getting fields value
                                        foreach (XElement fieldValue in lstXFields)
                                        {
                                            data.Add(fieldValue.Value.Trim(), null);
                                        }

                                        //getting value
                                        int counter = 0;
                                        foreach (XElement value in lstXValues)
                                        {
                                            string key = data.ElementAt(counter).Key;
                                            data[key] = value.Value.Trim();
                                            counter++;
                                        }

                                        foreach (var z in data)
                                        {
                                            switch (z.Key)
                                            {
                                                case Constants.IH_NICColumn:
                                                    ApplicantInfo.NIC = data[Constants.IH_NICColumn];
                                                    break;
                                                case Constants.IH_FirstNameColumn:
                                                    ApplicantInfo.FirstName = data[Constants.IH_FirstNameColumn];
                                                    break;
                                                case Constants.IH_LastNameColumn:
                                                    ApplicantInfo.FamilyName = data[Constants.IH_LastNameColumn];
                                                    break;
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                statusError = xElement.Descendants("statusCode").First().Value.ToLower().Trim();
                            }
                        }
                        else
                        {
                            faultError = xElement.Descendants("faultDescription").First().Value.ToLower().Trim();
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return ApplicantInfo;
        }

        internal HI_BRN ReadBRNSoapResponse(string BRNsoapResponse, out string faultError, out string statusError)
        {
            statusError = "";
            faultError = "";
            HI_BRN CompanyInfo = new HI_BRN();

            try
            {
                if (!string.IsNullOrEmpty(BRNsoapResponse))
                {
                    StringReader stringReader = new StringReader(BRNsoapResponse);
                    Dictionary<string, string> data = new Dictionary<string, string>();

                    using (XmlReader xmlReader = XmlReader.Create(stringReader, new XmlReaderSettings { IgnoreComments = true, IgnoreWhitespace = true }))
                    {
                        xmlReader.MoveToContent();
                        XElement xElement = (XElement)XElement.ReadFrom(xmlReader);

                        if (xElement.Descendants("faultDescription").First().Value.ToLower().Trim().Equals(Constants.IH_SuccessfulQuery.ToLower()))
                        {
                            if (xElement.Descendants("statusCode").First().Value.ToLower().Trim().Equals(Constants.IH_BRNStatusCode.ToLower()))
                            {
                                IEnumerable<XElement> lstXFields = from field in xElement.Descendants("fields")
                                                                   select field;

                                if (lstXFields != null)
                                {
                                    IEnumerable<XElement> lstXValues = from value in xElement.Descendants("value")
                                                                       select value;

                                    if (lstXValues != null)
                                    {
                                        CompanyInfo = new HI_BRN();

                                        //getting fields value
                                        foreach (XElement fieldValue in lstXFields)
                                        {
                                            data.Add(fieldValue.Value.Trim(), null);
                                        }

                                        //getting value
                                        int counter = 0;
                                        foreach (XElement value in lstXValues)
                                        {
                                            string key = data.ElementAt(counter).Key;
                                            data[key] = value.Value.Trim();
                                            counter++;
                                        }

                                        foreach (var z in data)
                                        {
                                            switch (z.Key)
                                            {
                                                case Constants.IH_NICColumn:
                                                    CompanyInfo.BRN = data[Constants.IH_BRNColumn];
                                                    break;
                                                case Constants.IH_CompanyNameColumn:
                                                    CompanyInfo.CompanyName = data[Constants.IH_CompanyNameColumn];
                                                    break;    
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                statusError = xElement.Descendants("statusCode").First().Value.ToLower().Trim();
                            }
                        }
                        else
                        {
                            faultError = xElement.Descendants("faultDescription").First().Value.ToLower().Trim();
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return CompanyInfo;
        }

        internal HI_RegistrationMark ReadAvailableRegMarkSoapResponse(string ReadAvailablesoapResponse, out string faultError, out string statusError)
        {
            statusError = "";
            faultError = "";
            HI_RegistrationMark RegNumInfo = new HI_RegistrationMark();

            try
            {
                if (!string.IsNullOrEmpty(ReadAvailablesoapResponse))
                {
                    StringReader stringReader = new StringReader(ReadAvailablesoapResponse);
                    Dictionary<string, string> data = new Dictionary<string, string>();

                    using (XmlReader xmlReader = XmlReader.Create(stringReader, new XmlReaderSettings { IgnoreComments = true, IgnoreWhitespace = true }))
                    {
                        xmlReader.MoveToContent();
                        XElement xElement = (XElement)XElement.ReadFrom(xmlReader);

                        if (xElement.Descendants("faultDescription").First().Value.ToLower().Trim().Equals(Constants.IH_SuccessfulQuery.ToLower()))
                        {
                            if (xElement.Descendants("statusCode").First().Value.ToLower().Trim().Equals(Constants.IH_BRNStatusCode.ToLower()))
                            {
                                IEnumerable<XElement> lstXFields = from field in xElement.Descendants("fields")
                                                                   select field;

                                if (lstXFields != null)
                                {
                                    IEnumerable<XElement> lstXValues = from value in xElement.Descendants("value")
                                                                       select value;

                                    if (lstXValues != null)
                                    {
                                        RegNumInfo = new HI_RegistrationMark();

                                        //getting fields value
                                        foreach (XElement fieldValue in lstXFields)
                                        {
                                            data.Add(fieldValue.Value.Trim(), null);
                                        }

                                        //getting value
                                        int counter = 0;
                                        foreach (XElement value in lstXValues)
                                        {
                                            string key = data.ElementAt(counter).Key;
                                            data[key] = value.Value.Trim();
                                            counter++;
                                        }

                                        foreach (var z in data)
                                        {
                                            switch (z.Key)
                                            {
                                                case Constants.IH_RegistrationMarkAvailableColumn:
                                                    RegNumInfo.RegistrationMark= data[Constants.IH_RegistrationMarkAvailableColumn];
                                                    break;
                                              
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                statusError = xElement.Descendants("statusCode").First().Value.ToLower().Trim();
                            }
                        }
                        else
                        {
                            faultError = xElement.Descendants("faultDescription").First().Value.ToLower().Trim();
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return RegNumInfo;
        }

        internal HI_RegistrationMark ReadUsedRegMarkSoapResponse(string ReadUsedsoapResponse, out string faultError, out string statusError)
        {
            statusError = "";
            faultError = "";
            HI_RegistrationMark RegNumInfo = new HI_RegistrationMark();

            try
            {
                if (!string.IsNullOrEmpty(ReadUsedsoapResponse))
                {
                    StringReader stringReader = new StringReader(ReadUsedsoapResponse);
                    Dictionary<string, string> data = new Dictionary<string, string>();

                    using (XmlReader xmlReader = XmlReader.Create(stringReader, new XmlReaderSettings { IgnoreComments = true, IgnoreWhitespace = true }))
                    {
                        xmlReader.MoveToContent();
                        XElement xElement = (XElement)XElement.ReadFrom(xmlReader);

                        if (xElement.Descendants("faultDescription").First().Value.ToLower().Trim().Equals(Constants.IH_SuccessfulQuery.ToLower()))
                        {
                            if (xElement.Descendants("statusCode").First().Value.ToLower().Trim().Equals(Constants.IH_BRNStatusCode.ToLower()))
                            {
                                IEnumerable<XElement> lstXFields = from field in xElement.Descendants("fields")
                                                                   select field;

                                if (lstXFields != null)
                                {
                                    IEnumerable<XElement> lstXValues = from value in xElement.Descendants("value")
                                                                       select value;

                                    if (lstXValues != null)
                                    {
                                        RegNumInfo = new HI_RegistrationMark();

                                        //getting fields value
                                        foreach (XElement fieldValue in lstXFields)
                                        {
                                            data.Add(fieldValue.Value.Trim(), null);
                                        }

                                        //getting value
                                        int counter = 0;
                                        foreach (XElement value in lstXValues)
                                        {
                                            string key = data.ElementAt(counter).Key;
                                            data[key] = value.Value.Trim();
                                            counter++;
                                        }

                                        foreach (var z in data)
                                        {
                                            switch (z.Key)
                                            {
                                                case Constants.IH_RegistrationMarkUsedColumn:
                                                    RegNumInfo.RegistrationMark = data[Constants.IH_RegistrationMarkUsedColumn];
                                                    break;

                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                statusError = xElement.Descendants("statusCode").First().Value.ToLower().Trim();
                            }
                        }
                        else
                        {
                            faultError = xElement.Descendants("faultDescription").First().Value.ToLower().Trim();
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return RegNumInfo;
        }
    }
}
