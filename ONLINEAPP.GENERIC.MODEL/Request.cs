using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ONLINEAPP.GENERIC.MODEL
{
    public class Request
    {
        [JsonProperty("SID")]
        public string SID { get; set; }

        [JsonProperty("GenericId")]
        public string GenericId { get; set; }

        [JsonProperty("Created")]
        public string Created { get; set; }

        [JsonProperty("Status")]
        public string Status { get; set; }
    }
}
