using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ONLINEAPP.HOME.MODEL
{
    public class UserInformationList
    {
        [JsonProperty("ID")]
        public int ID { get; set; }

        [JsonProperty("Title")]
        public string Title { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("EMail")]
        public string EMail { get; set; }

        [JsonProperty("FirstName")]
        public string FirstName { get; set; }

        [JsonProperty("LastName")]
        public string LastName { get; set; }

        [JsonProperty("Department")]
        public string Department { get; set; }

        [JsonProperty("JobTitle")]
        public string JobTitle { get; set; }

        [JsonProperty("WorkPhone")]
        public string WorkPhone { get; set; }

        [JsonProperty("Notes")]
        public string Notes { get; set; }

        [JsonProperty("SipAddress")]
        public string SipAddress { get; set; }

        [JsonProperty("IsSiteAdmin")]
        public bool IsSiteAdmin { get; set; }

        [JsonProperty("Deleted")]
        public bool Deleted { get; set; }

        [JsonProperty("Picture")]
        public string Picture { get; set; }

        [JsonProperty("Office")]
        public string Office { get; set; }

        [JsonProperty("UserName")]
        public string UserName { get; set; }

        [JsonProperty("WebSite")]
        public string WebSite { get; set; }
       
        [JsonProperty("MobilePhone")]
        public string MobilePhone { get; set; }

        //[JsonProperty("SPSResponsibility")]
        //public object SPSResponsibility { get; set; }

        //[JsonProperty("UserInfoHidden")]
        //public bool UserInfoHidden { get; set; }

        //[JsonProperty("SPSPictureTimestamp")]
        //public object SPSPictureTimestamp { get; set; }

        //[JsonProperty("SPSPicturePlaceholderState")]
        //public object SPSPicturePlaceholderState { get; set; }

        //[JsonProperty("SPSPictureExchangeSyncState")]
        //public object SPSPictureExchangeSyncState { get; set; }
        
    }
    
}
