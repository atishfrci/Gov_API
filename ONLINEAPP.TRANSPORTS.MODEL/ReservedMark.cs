using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ONLINEAPP.TRANSPORTS.MODEL
{
    public class ReservedMark
    {
        [JsonProperty("Title")]
        public string Title { get; set; }

        [JsonProperty("RegistrationMark")]
        public string RegistrationMark { get; set; }

        [JsonProperty("Status")]
        public string Status { get; set; }
    }
}
