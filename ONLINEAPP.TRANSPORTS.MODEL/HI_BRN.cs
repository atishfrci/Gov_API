using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ONLINEAPP.TRANSPORTS.MODEL
{
    public class HI_BRN
    {
        [JsonProperty("BRN")]
        public string BRN { get; set; }

        [JsonProperty("CompanyName")]
        public string CompanyName { get; set; }
    }
}
