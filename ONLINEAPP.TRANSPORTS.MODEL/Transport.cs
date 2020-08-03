using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ONLINEAPP.TRANSPORTS.MODEL
{
    public class TransportList
    {
        [JsonProperty("Id")]
        public int Id { get; set; }

        [JsonProperty("Title")]
        public string Title { get; set; }

        [JsonProperty("ApplicantType")]
        public string ApplicantType { get; set; }

        [JsonProperty("OnBehalf")]
        public string OnBehalf { get; set; }

        [JsonProperty("CompanyName")]
        public string CompanyName { get; set; }

        [JsonProperty("FirstName")]
        public string FirstName { get; set; }

        [JsonProperty("MiddleName")]
        public string MiddleName { get; set; }

        [JsonProperty("FamilyName")]
        public string FamilyName { get; set; }

        [JsonProperty("RegisteredCoNumber")]
        public string RegisteredCoNumber { get; set; }

        [JsonProperty("Address")]
        public string Address { get; set; }

        [JsonProperty("State")]
        public string State { get; set; }

        [JsonProperty("Town")]
        public string Town { get; set; }

        [JsonProperty("City")]
        public string City { get; set; }

        [JsonProperty("NIC")]
        public string NIC { get; set; }

        [JsonProperty("SID")]
        public string SID { get; set; }

        [JsonProperty("CountryOfResidence")]
        public string CountryOfResidence { get; set; }

        [JsonProperty("PhoneNumber")]
        public string PhoneNumber { get; set; }

        [JsonProperty("MobileNumber")]
        public string MobileNumber { get; set; }

        [JsonProperty("RegistrationMark")]
        public string RegistrationMark { get; set; }

        [JsonProperty("NYP")]
        public string NYP { get; set; }

        [JsonProperty("ID")]
        public int ID { get; set; }

        [JsonProperty("Modified")]
        public DateTime Modified { get; set; }

        [JsonProperty("Created")]
        public DateTime Created { get; set; }

        [JsonProperty("Status")]
        public string Status { get; set; }

        [JsonProperty("username")]
        public string username { get; set; }

        [JsonProperty("useremail")]
        public string useremail { get; set; }
      
        [JsonProperty("reservedtill")]
        public string reservedtill { get; set; }

        [JsonProperty("GenericId")]
        public string GenericId { get; set; }

        [JsonProperty("mvdcode")]
        public string mvdcode { get; set; }

        [JsonProperty("mvdname")]
        public string mvdname { get; set; }

        [JsonProperty("TransactionGuid")]
        public string TransactionGuid { get; set; }

        [JsonProperty("TransactionID")]
        public string TransactionID { get; set; }

        [JsonProperty("IncludePayment")]
        public string IncludePayment { get; set; }

        [JsonProperty("Amount")]
        public string Amount { get; set; }

        [JsonProperty("PurchaseMethod")]
        public string PurchaseMethod { get; set; }

        [JsonProperty("workspacestatus")]
        public string workspacestatus { get; set; }

    }
}
