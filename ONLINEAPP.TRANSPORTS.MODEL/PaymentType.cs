using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ONLINEAPP.TRANSPORTS.MODEL
{
    public class PaymentType
    {
        [JsonProperty("PaymentTypeName")]
        public string PaymentTypeName { get; set; }

        [JsonProperty("PaymentTypeCode")]
        public string PaymentTypeCode { get; set; }
    }
}
