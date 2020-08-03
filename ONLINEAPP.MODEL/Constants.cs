using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace ONLINEAPP.MODEL
{
    public class Constants
    {
        public static string DomainName = Convert.ToString(WebConfigurationManager.AppSettings["DomainName"]);
        public static string QualifiedDomainName = Convert.ToString(WebConfigurationManager.AppSettings["QualifiedDomainName"]);
        public static string ROOTSiteUrl = Convert.ToString(WebConfigurationManager.AppSettings["WEBAPPSiteUrl"]);
        public static string TransportSiteUrl = Convert.ToString(WebConfigurationManager.AppSettings["TransportSiteUrl"]);
        public static string ErrorLogPath = Convert.ToString(WebConfigurationManager.AppSettings["ErrorLogPath"]);
        public static string TransportSID = Convert.ToString(WebConfigurationManager.AppSettings["TransportSID"]);
        public static string TransportGenericID = Convert.ToString(WebConfigurationManager.AppSettings["TransportGenericID"]); // CODE ADDED BY HEMA 09.02.2019
        public static string TopChanging = Convert.ToString(WebConfigurationManager.AppSettings["GetTopUsed"]);

        public static string EventSiteUrl = Convert.ToString(WebConfigurationManager.AppSettings["EventSiteUrl"]);
        public static string NewsSiteUrl = Convert.ToString(WebConfigurationManager.AppSettings["NewsSiteUrl"]);
        public static string NewsPublisingImgRelativeUrl = Convert.ToString(WebConfigurationManager.AppSettings["NewsPublisingImgRelativeUrl"]);


        public static string OverallReportYearsCount = Convert.ToString(WebConfigurationManager.AppSettings["OverallReportYearsCount"]);

        public const string Q1 = "Q1";
        public const string Q2 = "Q2";
        public const string Q3 = "Q3";
        public const string Q4 = "Q4";
        public const string Quarterly = "Quarterly";
        public const string Lookup = "Lookup";
        public const string User = "User";

        public static string PrimaryADServer = Convert.ToString(WebConfigurationManager.AppSettings["PrimaryADServer"]);
        public static string SecondaryADServer = Convert.ToString(WebConfigurationManager.AppSettings["SecondaryADServer"]);
        public static string DecryptKey = Convert.ToString(WebConfigurationManager.AppSettings["DecryptKey"]);

        /*limit for BRN and NIC*/

        public static string LimitForNIC = Convert.ToString(WebConfigurationManager.AppSettings["limitforNIC"]);
        public static string LimitForBRN = Convert.ToString(WebConfigurationManager.AppSettings["limitforBRN"]);

        /*InfoHighway Parameters*/

        public static string IH_NICParam = "NTA031";
        public const string IH_NICQueryColumn = "CITIZEN.NIC_NUMBER";

        public const string IH_NICColumn = "NIC_NUMBER";                
        public const string IH_FirstNameColumn = "FIRST_NAME";
        public const string IH_LastNameColumn = "PRINTED_SURNAME";
        public const string IH_MaidenNameColumn = "SURNAME";

        public static string IH_BRNParam = "NTA033";
        public static string IH_BRNQueryColumn = "VOS_BRN";

        public const string IH_BRNColumn = "BRN_NUMBER";
        public const string IH_CompanyNameColumn = "COMPANY_NAME";
        
        public static string IH_NTAAvailableMarkParam = "NTA035";
        public static string IH_NTAAvailableMarkQueryColumn = "REGMARK";

        public const string IH_RegistrationMarkAvailableColumn = "REGMARK";

        public const string IH_RegistrationMarkUsedColumn = "REGISTRATION_MARK_NUMBER";
        public const string IH_VehicleChassisUsedColumn = "VEHICLE_CHASSIS";

        public static string IH_NTAUsedMarkParam = "NTA036";
        public static string IH_NTAUsedMarkQueryColumn = "VEH_REG_MARK";

        public static string IH_NTAUsername = Convert.ToString(WebConfigurationManager.AppSettings["NTA_userId"]);
        public static string IH_NTAPassword = Convert.ToString(WebConfigurationManager.AppSettings["NTA_password"]);

        public const string IH_SuccessfulQuery = "The transaction is executed successfully";
        public const string IH_NICStatusCode = "WQ000";

        public const string IH_BRNStatusCode = "WQ000";


        /*Payment Gateway Parameters*/

        public static string PG_Url = Convert.ToString(WebConfigurationManager.AppSettings["PG_NTA_Url"]);

        public static string PG_PaymentTypeUrl = Convert.ToString(WebConfigurationManager.AppSettings["PG_NTA_PaymentTypeUrl"]);
        public static string PG_CurrencyUrl = Convert.ToString(WebConfigurationManager.AppSettings["PG_NTA_CurrencyUrl"]);
        public static string PG_SimpleTransacUrl = Convert.ToString(WebConfigurationManager.AppSettings["PG_NTA_SimpleTransacUrl"]);

        public static string PG_SiteCode = Convert.ToString(WebConfigurationManager.AppSettings["PG_NTA_SiteCode"]);
        public static string PG_AccessCode = Convert.ToString(WebConfigurationManager.AppSettings["PG_NTA_AccessCode"]);
        public static string PG_OperationCode = Convert.ToString(WebConfigurationManager.AppSettings["PG_NTA_OperationCode"]);
        public static string PG_SecureKey = Convert.ToString(WebConfigurationManager.AppSettings["PG_NTA_SecureKey"]);

        public static string PG_SimpleTransac_ReturnUrl = Convert.ToString(WebConfigurationManager.AppSettings["PG_NTA_SimpleTransac_ReturnUrl"]);
    }
}
