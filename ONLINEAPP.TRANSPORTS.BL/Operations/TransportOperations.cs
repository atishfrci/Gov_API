using Newtonsoft.Json.Linq;
using ONLINEAPP.DAL;
using ONLINEAPP.MODEL;
using ONLINEAPP.TRANSPORTS.INTERFACE;
using ONLINEAPP.TRANSPORTS.MODEL;
using ONLINEAPP.TRANSPORTS.VIEWMODEL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ONLINEAPP.TRANSPORTS.BL.Operations
{
    public class TransportOperations : ITransport
    {

        //private CommonOperations objCommon { get; set; }

        //public TransportOperations()
        //{
        //    objCommon = new CommonOperations();
        //}

        public List<TransportList> GetTransport(string siteUrl, string token)
        {
            try
            {
                string RestUrl = string.Concat(siteUrl, ListURLs.RestUrlListItemWithQuery(typeof(TransportList).Name, true),
                                                    string.Format(RESTFilters.topItems, GetTop._500000), string.Format(RESTFilters.orderByDescending, Fields.Title));

                var _result = CRUDOperations.GetListByRestURL<TransportList>(RestUrl, token);

                return _result.ToList();
            }
            catch (Exception ex)
            {
                string guid = CRUDOperations.WriteException(ex, MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.Name);
                throw new Exception(string.Format(Messages.MsgExceptionOccured, guid));
            }
        }

        public List<string> GetUsedMarks(string siteUrl, string token, string prefix, int from , int to)
        {
            

            try
            {
                if (!string.IsNullOrEmpty(prefix))
                {

                    string RestUrl = string.Concat(siteUrl, ListURLs.RestUrlListItemWithQuery(typeof(TransportList).Name, true),
                                                    string.Format(RESTFilters.ByStatusNotExpiredAndContainRegMarkPrefix, prefix, "Expired", "Surrendered" ,"Cancelled"), string.Format(RESTFilters.topItems, GetTop._10000), string.Format(RESTFilters.orderByDescending, Fields.ID));

                    var _result = CRUDOperations.GetListByRestURL<TransportList>(RestUrl, token);

                    return _result.Select(t => t.RegistrationMark).ToList();
                }
                else
                {
                    string RestUrl = string.Concat(siteUrl, ListURLs.RestUrlListItemWithQuery(typeof(TransportList).Name, true),
                                                   string.Format(RESTFilters.ByStartWithEndWithAndStatusNotExpired, from , to , "Expired", "Surrendered" , "Cancelled"), string.Format(RESTFilters.topItems, Constants.TopChanging), string.Format(RESTFilters.orderByDescending, Fields.ID));

                    var _result = CRUDOperations.GetListByRestURL<TransportList>(RestUrl, token);

                    return _result.Select(t => t.RegistrationMark).ToList();
                }
            }
            catch (Exception ex)
            {
                string guid = CRUDOperations.WriteException(ex, MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.Name);
                throw new Exception(string.Format(Messages.MsgExceptionOccured, guid));
            }
        }

        public List<string> GetNumberOfMarksPerNIC(string siteUrl, string token, string nic)
        {
            try
            {              
                string RestUrl = string.Concat(siteUrl, ListURLs.RestUrlListItemWithQuery(typeof(TransportList).Name, true),
                                                  string.Format(RESTFilters.ByNIC, nic), string.Format(RESTFilters.topItems, GetTop._50000), string.Format(RESTFilters.orderByDescending, Fields.ID));

                var _result = CRUDOperations.GetListByRestURL<TransportList>(RestUrl, token);

                return _result.Select(t => t.RegistrationMark).ToList();
            
            }
            catch (Exception ex)
            {
                string guid = CRUDOperations.WriteException(ex, MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.Name);
                throw new Exception(string.Format(Messages.MsgExceptionOccured, guid));
            }
        }

        public List<string> GetNumberOfMarksPerBRN(string siteUrl, string token, string brn)
            {
            try
            {
                string RestUrl = string.Concat(siteUrl, ListURLs.RestUrlListItemWithQuery(typeof(TransportList).Name, true),
                                                  string.Format(RESTFilters.ByBRN, brn), string.Format(RESTFilters.topItems, Constants.TopChanging), string.Format(RESTFilters.orderByDescending, Fields.ID));

                var _result = CRUDOperations.GetListByRestURL<TransportList>(RestUrl, token);

                return _result.Select(t => t.RegistrationMark).ToList();

            }
            catch (Exception ex)
            {
                string guid = CRUDOperations.WriteException(ex, MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.Name);
                throw new Exception(string.Format(Messages.MsgExceptionOccured, guid));
            }
        }

        public TransportList TransportByID(string id, string siteUrl, string token)
        {
            try
            {
                string RestUrl = string.Concat(siteUrl, ListURLs.RestUrlListItemWithQuery(typeof(TransportList).Name, true),
                                                    string.Format(RESTFilters.ByID, id));

                return CRUDOperations.GetListByRestURL<TransportList>(RestUrl, token).FirstOrDefault();
            }
            catch (Exception ex)
            {
                string guid = CRUDOperations.WriteException(ex, MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.Name);
                throw new Exception(string.Format(Messages.MsgExceptionOccured, guid));
            }
        }

        public TransportList TransportByRegistrationMark(string RegistrationMark, string siteUrl, string token)
        {
            try
            {
                string RestUrl = string.Concat(siteUrl, ListURLs.RestUrlListItemWithQuery(typeof(TransportList).Name, true),
                                                    string.Format(RESTFilters.ByRegistrationMark, RegistrationMark));

                return CRUDOperations.GetListByRestURL<TransportList>(RestUrl, token).FirstOrDefault();
            }
            catch (Exception ex)
            {
                string guid = CRUDOperations.WriteException(ex, MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.Name);
                throw new Exception(string.Format(Messages.MsgExceptionOccured, guid));
            }
        }

        public TransportList TransportByRegistrationMarkByStatus(string RegistrationMark, string siteUrl, string token)
        {
            string prefix = Regex.Replace(RegistrationMark, "[^.0-9]", "");
            try
            {
                string RestUrl = string.Concat(siteUrl, ListURLs.RestUrlListItemWithQuery(typeof(TransportList).Name, true),
                                               string.Format(RESTFilters.ByContainPrefixRegistrationMarkAndStatusNotExpired,prefix, RegistrationMark, "Pending","Approved"), string.Format(RESTFilters.topItems, /*COMMENTED BY HEMA 22.01.2019 GetTop._1000*/ GetTop._10000), string.Format(RESTFilters.orderByDescending, Fields.ID));

                return CRUDOperations.GetListByRestURL<TransportList>(RestUrl, token).FirstOrDefault();
            }
            catch (Exception ex)
            {
                string guid = CRUDOperations.WriteException(ex, MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.Name);
                throw new Exception(string.Format(Messages.MsgExceptionOccured, guid));
            }
        }

        public Result AddTransport(TransportList objTransport, string siteUrl, string token)
        {
            Result res = new Result();
            PaymentGatewayOperation pg = new PaymentGatewayOperation(); 
            try
            {

                /*objTransport.SID = Constants.TransportSID;
                objTransport.GenericId = string.Concat(Constants.TransportGenericID, "-", objTransport.RegistrationMark, "-", DateTime.Now.ToString("ddMMyyHHmm")); // CODE ADDED BY HEMA 09.02.2019  COMMENTED BY HEMA 08.04.2019*/

                objTransport.SID = string.Concat(Constants.TransportGenericID, "-", objTransport.RegistrationMark, "-", DateTime.Now.ToString("ddMMyyHHmm"));
                objTransport.GenericId = objTransport.RegistrationMark; 

                res = CRUDOperations.AddListItem<TransportList>(siteUrl, token, typeof(TransportList).Name, objTransport);

                // PaymentGatewayOperation.buildUrlForSimpleTransaction(objTransport.Amount)
                res.RequestId = objTransport.SID; 
                res.StatusCode = StatusCode.Success;
                res.Message = Messages.MsgTransportAddedSuccessfully;
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

        public Result UpdateTransportByID(TransportList objTransport, string siteUrl, string token)
        {
            Result res = new Result();
            try
            {
                res = CRUDOperations.UpdateListItem<TransportList>(siteUrl, token, typeof(TransportList).Name, objTransport);

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

        public Result DeleteTransportByID(string id, string siteUrl, string token)
        {
            Result res = new Result();
            try
            {
                res = CRUDOperations.DeleteListItem(siteUrl, typeof(TransportList).Name, id);

                res.StatusCode = StatusCode.Success;
                res.Message = Messages.MsgTransportDeletedSuccessfully;
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

        public List<TransportList> TransportByStatus(string Status, string siteUrl, string token)
        {
            try
            {
                string RestUrl = string.Concat(siteUrl, ListURLs.RestUrlListItemWithQuery(typeof(TransportList).Name, true),
                                                    string.Format(RESTFilters.ByStatus, Status));

                return CRUDOperations.GetListByRestURL<TransportList>(RestUrl, token);
            }
            catch (Exception ex)
            {
                string guid = CRUDOperations.WriteException(ex, MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.Name);
                throw new Exception(string.Format(Messages.MsgExceptionOccured, guid));
            }
        }

        public int TransportCountByStatus(string Status, string siteUrl, string token)
        {
            try
            {
                List<TransportList> totalRecords = TransportByStatus(Status, siteUrl, token);
                return totalRecords.Count();
            }
            catch (Exception ex)
            {
                string guid = CRUDOperations.WriteException(ex, MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.Name);
                throw new Exception(string.Format(Messages.MsgExceptionOccured, guid));
            }
        }

        public string ValidateRegistrationNo(string RegistationNo)
        {
            try
            {
                string result = "No Data HIT";
                string from = "ABC";
                string to = "FMG";

                int digitIndex = 0;
                string totChar = string.Empty;
                GetCharOnly(RegistationNo, ref digitIndex, ref totChar);

                int digitIndexFrom = 0;
                string totCharFrom = string.Empty;
                GetCharOnly(from, ref digitIndexFrom, ref totCharFrom);

                int digitIndexTo = 0;
                string totCharTo = string.Empty;
                GetCharOnly(to, ref digitIndexTo, ref totCharTo);

                char[] _fromChar = totCharFrom.ToArray();
                char[] _toChar = totCharTo.ToArray();
                List<string> rs1 = new List<string>();
                for (int count = 0; count < _fromChar.Count(); count++)
                {
                    List<string> rsinner = new List<string>();
                    byte[] fromAsc = Encoding.ASCII.GetBytes(_fromChar);
                    byte[] toAsc = Encoding.ASCII.GetBytes(_toChar);
                    if (rs1.Count > 0)
                    {
                        int counter = 1;
                        foreach (var item in rs1)
                        {
                            if (count == 1 && counter == 1)
                            {
                                for (int innerCount = fromAsc[count]; innerCount <= 90; innerCount++)
                                {
                                    string test = Convert.ToString(Convert.ToChar(innerCount));
                                    rsinner.Add(string.Concat(item, test));
                                }
                            }
                            else if (counter == rs1.Count() && count < _fromChar.Count())
                            {
                                for (int innerCount = 65; innerCount <= toAsc[count]; innerCount++)
                                {
                                    string test = Convert.ToString(Convert.ToChar(innerCount));
                                    rsinner.Add(string.Concat(item, test));
                                }
                            }
                            else
                            {
                                for (int innerCount = 65; innerCount <= 90; innerCount++)
                                {
                                    string test = Convert.ToString(Convert.ToChar(innerCount));
                                    rsinner.Add(string.Concat(item, test));
                                }
                            }
                            //for (int innerCount = fromAsc[count]; innerCount <= toAsc[count]; innerCount++)
                            //{
                            //    string test = Convert.ToString(Convert.ToChar(innerCount));
                            //    rsinner.Add(string.Concat(item, test));
                            //}
                            counter++;
                        }
                    }
                    else
                    {
                        for (int innerCount = fromAsc[count]; innerCount <= toAsc[count]; innerCount++)
                        {
                            string test = Convert.ToString(Convert.ToChar(innerCount));
                            rsinner.Add(test);
                        }
                    }
                    rs1.AddRange(rsinner);
                }

                char txt1 = (char)65;
                //char txt2 = (char)66;
                //char txt3 = (char)67;
                //char txt4 = (char)68;
                //string[] arrstr;
                //byte[] asciiBytes = Encoding.ASCII.GetBytes(totChar.ToUpper());
                //byte[] asciiBytes12 = Encoding.ASCII.GetBytes(totChar.ToUpper(), 3, asciiBytes, 3);


                //string tempStr = string.Join("", asciiBytes);
                //long tempPrf = Convert.ToInt64(tempStr);

                //byte[] asciiBytesFrom = Encoding.ASCII.GetBytes("AA".ToUpper());
                //string tempStrFrom = string.Join("", asciiBytesFrom);
                //long tempPrfFrom = Convert.ToInt64(tempStrFrom);

                //byte[] asciiBytesTo = Encoding.ASCII.GetBytes("ee".ToUpper());
                //string tempStrTo = string.Join("", asciiBytesTo);
                //long tempPrfTo = Convert.ToInt64(tempStrTo);

                //long tempStrA = Convert.ToInt64(Encoding.ASCII.GetBytes('AAA'.ToUpper()).ToString());// Min Char  limit from SP
                //long tempStrD = Convert.ToInt64(Encoding.ASCII.GetBytes('DDD'.ToUpper()).ToString());;// Max Char  limit from SP
                //if (tempPrf >= tempPrfFrom && tempPrf < tempPrfTo)
                //{
                //    result = "Valid Mark";
                //}
                //else
                //{
                //    result = "invalid range!";
                //}

                return result;
            }
            catch (Exception ex)
            {
                string guid = CRUDOperations.WriteException(ex, MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.Name);
                throw new Exception(string.Format(Messages.MsgExceptionOccured, guid));
            }
        }

        public Result MvdAvailability(string mvdcode, string siteUrl, string token)
        {
            Result resp = new Result();

            try
            {

                MVDealerOpertations objMVD = new MVDealerOpertations();
                MVDealerList _MVDealers = objMVD.GetMVDCode(siteUrl, token, mvdcode).FirstOrDefault();

                if (_MVDealers != null)
                {
                    resp.StatusCode = StatusCode.Success;
                    resp.Message = Messages.MsgMVDValid;
                    return resp;
                }
                else
                {
                    resp.StatusCode = StatusCode.Error;
                    resp.Message = Messages.MsgMVDNotValid;
                    return resp;
                }
            }

            catch (Exception ex)
            {
                string guid = CRUDOperations.WriteException(ex, MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.Name);
                resp.StatusCode = StatusCode.Error;
                resp.Message = Messages.MsgSomethingWentWrong; 
                return resp;
            }
        }

        public HI_NIC ValidateNIC(string nic , string token)
        {
            InfoHighwayOperation InfoHighwayOperation = new InfoHighwayOperation();
            try
            {
                HI_NIC ApplicantObj = null;

                ApplicantObj = InfoHighwayOperation.ValidateNICwithInfoHighway(nic, token);

                return ApplicantObj; 
            }
            catch (Exception ex)
            {
                string guid = CRUDOperations.WriteException(ex, MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.Name);
                throw ex; 
            }
        }

        public HI_BRN ValidateBRN(string brn, string token)
        {
            InfoHighwayOperation InfoHighwayOperation = new InfoHighwayOperation();
            try
            {
                HI_BRN CompanyObj = null;

                CompanyObj = InfoHighwayOperation.ValidateBRNwithInfoHighway(brn, token);

                return CompanyObj;
            }
            catch (Exception ex)
            {
                string guid = CRUDOperations.WriteException(ex, MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.Name);
                throw ex;
            }
        }

        /*COMMENTED BY HEMA SINCE NO NEeD TO CALL FROM FE
        public HI_RegistrationMark CheckAvailableMarkFromInfoHighway(string regmark, string token)
        {
            InfoHighwayOperation InfoHighwayOperation = new InfoHighwayOperation();
            try
            {
                HI_RegistrationMark AvailableRegMark = null;

                AvailableRegMark = InfoHighwayOperation.ValidateWithListOfAvailableNumbersFromInfoHighway(regmark, token);

                return AvailableRegMark;
            }
            catch (Exception ex)
            {
                string guid = CRUDOperations.WriteException(ex, MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.Name);
                throw ex;
            }
        }

        public HI_RegistrationMark CheckSoldMarkFromInfoHighway(string regmark, string token)
        {
            InfoHighwayOperation InfoHighwayOperation = new InfoHighwayOperation();
            try
            {
                HI_RegistrationMark SoldRegMark = null;

                SoldRegMark = InfoHighwayOperation.ValidateWithListOfSoldNumbersFromInfoHighway(regmark, token);

                return SoldRegMark;
            }
            catch (Exception ex)
            {
                string guid = CRUDOperations.WriteException(ex, MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.Name);
                throw ex;
            }
        }


        */
        public List<PaymentType> getPaymentTypeFromPaymentGateway()
        {
            try
            {
                List<PaymentType> returnPaymentTypes = new List<PaymentType>();

                returnPaymentTypes = PaymentGatewayOperation.buildPaymentTypeUrl();

                return returnPaymentTypes; 
            }
            catch (Exception ex)
            {
                string guid = CRUDOperations.WriteException(ex, MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.Name);
                throw ex;
            }
        }

        public List<Currency> getCurrencyFromPaymentGateway(string selectedPaymentTypeCode)
        {
            try
            {
                List<Currency> returnCurrencies = new List<Currency>();

                returnCurrencies = PaymentGatewayOperation.buildCurrencyUrl(selectedPaymentTypeCode);

                return returnCurrencies; 
            }
            catch (Exception ex)
            {
                string guid = CRUDOperations.WriteException(ex, MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.Name);
                throw ex;
            }
        }

        public PG_Transactions addSimpleTransactionToPaymentGateway(string Amount, string username, string userDisplayName, string requestId, string selectedCurrencyCode, string selectedPaymentTypeCode, string messageFromFE)
        {
            try
            {
                PG_Transactions simpleTransacRespObj = new PG_Transactions();

                simpleTransacRespObj = PaymentGatewayOperation.buildUrlForSimpleTransaction(Amount, username, userDisplayName, requestId, selectedCurrencyCode, selectedPaymentTypeCode, messageFromFE);

                return simpleTransacRespObj;
            }
            catch (Exception ex)
            {
                string guid = CRUDOperations.WriteException(ex, MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.Name);
                throw ex;
            }
        }

        public  Task<JObject> callSbmPaymentGateway(string url)
        {
            try
            {
                //Task<PG_Transactions> sbmTransacRespObj = "";

                Task<JObject> sbmTransacRespObj = PaymentGatewayOperation.sbmGatewayTrasanction(url);

                return sbmTransacRespObj;
            }
            catch (Exception ex)
            {
                string guid = CRUDOperations.WriteException(ex, MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.Name);
                throw ex;
            }
        }

        public Result RegistrationAvailability(string RegistrationMark, string siteUrl, string token, string category, string id)
        {
            Result res = new Result();

            Boolean _matchresult = false;
            Boolean _reachlimit = false;  

            string RegistrationMarkPrefix = Regex.Replace(RegistrationMark, "[^.0-9]", "");
           //string identificationNum = 

            try
            {
                ///Get the transport record by registration mark if present
                // TransportList _transport = TransportByRegistrationMark(RegistrationMark, siteUrl, token);//COMMENTED BY HEMA 12.02.2019

                TransportList _transport = TransportByRegistrationMarkByStatus(RegistrationMark, siteUrl, token);
                ///Get the reserved mark record by registration mark if present
                CommonOperations objCommon = new CommonOperations();
                ReservedMark _reservedmark = objCommon.GetReservedMarksByMark(RegistrationMark, siteUrl, token, RegistrationMarkPrefix);

                //string _usedMark = GetUsedMarks(siteUrl, token).FirstOrDefault();

                if (_transport != null || _reservedmark != null)
                {
                     _matchresult = false;
                  
                    /* ADDED BY HEMA FOR TESTING PURPOSE 23.01.2019 string filePath = @"C:\EservicesONLINEAPILog.txt";

                    using (StreamWriter streamWriter = new StreamWriter(filePath))
                    { 
                        if (_reservedmark != null )
                        {
                            streamWriter.WriteLine("In IF " + _matchresult + " --  " + _reservedmark.Title + _reservedmark.RegistrationMark);
                            streamWriter.Close();
                        }
                        if (_transport != null)
                        {

                            streamWriter.WriteLine("In IF " + _matchresult + " --  " + _transport.Title + _transport.RegistrationMark);
                            streamWriter.Close();
                        }
                    }*/
                }
                else
                {
                    //Get all combinations and check is current present in it the return true else flase
                   List<string> allComb = objCommon.GetRegistrationMarks(string.Empty);
                    ////Get all marks exceptions
                   List<string> _exceptionalMarks = objCommon.GetReservedMasks(siteUrl, token , RegistrationMarkPrefix);
                    ////Get all used Marks
                   List<string> usedMarks = GetUsedMarks(siteUrl, token, RegistrationMarkPrefix, Convert.ToInt32("1"), Convert.ToInt32("9999"));

                   _exceptionalMarks.AddRange(usedMarks);

                   List<string> filtteredComb = allComb.Except(_exceptionalMarks).ToList();

                    //COMMENTED BY HEMA 23.01.2019 _matchresult = allComb.Exists(x => x == RegistrationMark);

                   _matchresult = filtteredComb.Exists(x => x == RegistrationMark);

                    if (_matchresult)
                    {
                        InfoHighwayOperation InfoHighwayOperation = new InfoHighwayOperation();
                         HI_RegistrationMark  availableregmark = InfoHighwayOperation.ValidateWithListOfAvailableNumbersFromInfoHighway(RegistrationMark);

                        if (!availableregmark.Equals(null))
                        {
                            if (!string.IsNullOrEmpty(availableregmark.RegistrationMark))
                            {
                                _matchresult = true;
                            }
                        }
                        else
                        {
                            HI_RegistrationMark soldmark = InfoHighwayOperation.ValidateWithListOfSoldNumbersFromInfoHighway(RegistrationMark);

                            if (!string.IsNullOrEmpty(soldmark.RegistrationMark))
                            {
                                _matchresult = false;
                            }
                            else
                            {
                                _matchresult = true;
                            }

                        }
                    }


                    /*ADDED BY HEMA TO CATER FOR NO APP PER NIC PER BRN*/
                    if (category == "Individual Applicant")
                    {
                        List<string> MarksPerNIC = GetNumberOfMarksPerNIC(siteUrl, token, id);

                        if (MarksPerNIC != null)
                        {
                           if (MarksPerNIC.Count >=2)
                           {
                                _reachlimit = true; 
                           }
                           else
                           {
                                _reachlimit = false; 
                           }
                        }
                    }

                    if (category == "Registered Companies")
                    {
                        List<string> MarksPerBRN = GetNumberOfMarksPerBRN(siteUrl, token, id);

                        if (MarksPerBRN != null)
                        {
                            if (MarksPerBRN.Count >= 3)
                            {
                                _reachlimit = true;
                            }
                            else
                            {
                                _reachlimit = false;
                            }
                        }

                    }


                    /*ADDED BY HEMA TO CHECK NUMBER OF APPLICATIONS PER USER */



                    /* ADDED BY HEMA FOR TESTING PURPOSE 23.01.2019  string filePath = @"C:\EservicesONLINEAPILog.txt";

                   using (StreamWriter streamWriter = new StreamWriter(filePath))
                   {
                       streamWriter.WriteLine("In Else " + _matchresult + " -- " + allComb.Count + " -- " + _exceptionalMarks.Count + " -- " + usedMarks.Count + " -- " + filtteredComb.Count);

                       foreach(string t in filtteredComb)
                       {
                           streamWriter.WriteLine(t);
                       }
                       streamWriter.Close();
                   }*/
                }
                if (_matchresult)
                {
                    if (!_reachlimit)
                    {
                        res.StatusCode = StatusCode.Success;
                        res.Message = Messages.MsgTransportMarkAvailableSuccessfully;
                        return res;
                    }
                    else
                    {
                        res.StatusCode = StatusCode.ExceedLimit;
                        res.Message = Messages.MsgTransportMarkOutLimit;
                        return res;
                    }
                   
                }
                else
                {
                     res.StatusCode = StatusCode.Error;
                     res.Message = Messages.MsgTransportMarkNoAvailableSuccessfully;
                     return res;
                }

            }
            catch (Exception ex)
            {
                string guid = CRUDOperations.WriteException(ex, MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.Name);
                throw new Exception(string.Format(Messages.MsgExceptionOccured, guid));
            }
        }

        private static void GetCharOnly(string RegistationNo, ref int digitIndex, ref string totChar)
        {
            int index = 0;
            foreach (char _char in RegistationNo)
            {
                if (char.IsLetter(_char))
                {
                    totChar += _char;
                }
                else if (char.IsDigit(_char))
                {
                    digitIndex = index;
                    break;
                    // byte[] asciiBytes = Encoding.ASCII.GetBytes(_char);
                }
                index++;
            }
        }

        public string GetMvdCode(string mvdcode)
        {
            string result = "";

            return result; 
        }

        public List<string> GetRegistrationMarks(string prefix, string from, string to, string siteUrl, string token)
        {
            CommonOperations objCommon = new CommonOperations();

            //Get All mark in given range
            List<string> _marksInRange = objCommon.GetRegMarksWithinRange(prefix, Convert.ToInt32(from), Convert.ToInt32(to));
            //Get all marks exceptions
            List<string> _exceptionalMarks = objCommon.GetReservedMasks(siteUrl, token , prefix);
            //Get all used Marks
            List<string> usedMarks = GetUsedMarks(siteUrl, token, prefix, Convert.ToInt32("1"), Convert.ToInt32("9999"));

            _exceptionalMarks.AddRange(usedMarks);

            List<string> filtteredMarks = _marksInRange.Except(_exceptionalMarks).ToList();
            return filtteredMarks;
            //return _exceptionalMarks; 
        }
    }
}
