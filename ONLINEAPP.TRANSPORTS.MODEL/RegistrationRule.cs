using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ONLINEAPP.TRANSPORTS.MODEL
{
    public class RegistrationRule
    {
        [JsonProperty("Title")]
        public string Title { get; set; }

        [JsonProperty("MaxRange")]
        public string MaxRange { get; set; }

        [JsonProperty("MinRange")]
        public string MinRange { get; set; }

        [JsonProperty("MaxChar")]
        public string MaxChar { get; set; }

        [JsonProperty("MinChar")]
        public string MinChar { get; set; }

        [JsonProperty("Status")]
        public string Status { get; set; }
    }
}
