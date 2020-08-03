using Newtonsoft.Json.Linq;
using ONLINEAPP.DAL;
using ONLINEAPP.MODEL;
using ONLINEAPP.TRANSPORTS.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ONLINEAPP.TRANSPORTS.INTERFACE
{
    public interface ITransport
    {
        List<TransportList> GetTransport(string siteUrl, string token);

        TransportList TransportByID(string id, string siteUrl, string token);

        TransportList TransportByRegistrationMark(string RegistrationMark, string siteUrl, string token);

        TransportList TransportByRegistrationMarkByStatus(string RegistrationMark, string siteUrl, string token);

        Result AddTransport(TransportList objTransport, string siteUrl, string token);

        Result UpdateTransportByID(TransportList objTransport, string siteUrl, string token);

        Result DeleteTransportByID(string id, string siteUrl, string token);

        List<TransportList> TransportByStatus(string Status, string siteUrl, string token);

        int TransportCountByStatus(string Status, string siteUrl, string token);

        Result RegistrationAvailability(string RegistrationMark, string siteUrl, string token, string category, string id);

        Result MvdAvailability(string mvdcode, string siteUrl, string token);

        string ValidateRegistrationNo(string RegistationNo);

        List<string> GetRegistrationMarks(string prefix, string from, string to, string siteUrl, string token);

        List<string> GetNumberOfMarksPerNIC(string siteUrl, string token, string nic);

        List<string> GetNumberOfMarksPerBRN(string siteUrl, string token, string brn);

        HI_NIC ValidateNIC(string nic, string token);

        HI_BRN ValidateBRN(string brn, string token);

        /*COMMENTED BY HEMA SINCE NO NEeD TO CALL FROM FE
        HI_RegistrationMark CheckAvailableMarkFromInfoHighway(string regmark, string token);

        HI_RegistrationMark CheckSoldMarkFromInfoHighway(string regmark, string token);
        */

        List<PaymentType> getPaymentTypeFromPaymentGateway();

        List<Currency> getCurrencyFromPaymentGateway(string selectedPaymentTypeCode);

        PG_Transactions addSimpleTransactionToPaymentGateway(string Amount, string username, string userDisplayName, string requestId, string selectedCurrencyCode, string selectedPaymentTypeCode, string messageFromFE);

        Task<JObject> callSbmPaymentGateway(string url);

    }
}
