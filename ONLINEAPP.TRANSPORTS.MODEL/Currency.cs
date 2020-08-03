using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ONLINEAPP.TRANSPORTS.MODEL
{
    public class Currency
    {
        [JsonProperty("CurrencyId")]
        public string CurrencyId{ get; set; }

        [JsonProperty("CurrencyName")]
        public string CurrencyName { get; set; }

        [JsonProperty("CurrencyCode")]
        public string CurrencyCode { get; set; }
    }
}
