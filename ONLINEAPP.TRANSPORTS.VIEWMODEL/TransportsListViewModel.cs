using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ONLINEAPP.TRANSPORTS.VIEWMODEL
{
    public class TransportsListViewModel
    {
        [JsonProperty("ID")]
        public string ID { get; set; }

        [JsonProperty("Status")]
        public string Status { get; set; }

        [JsonProperty("RegistrationMark")]
        public string RegistrationMark { get; set; }
    }
}
