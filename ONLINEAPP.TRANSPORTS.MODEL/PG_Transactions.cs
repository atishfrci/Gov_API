using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ONLINEAPP.TRANSPORTS.MODEL
{
    public class PG_Transactions
    {
        [JsonProperty("TransactionGuid")]
        public string TransactionGuid { get; set; }

        [JsonProperty("ReceiptNo")]
        public string ReceiptNo { get; set; }

        [JsonProperty("Status")]
        public string Status { get; set; }

        [JsonProperty("RedirectUrl")]
        public string RedirectUrl { get; set; }

        [JsonProperty("Amount")]
        public string Amount { get; set; }

        [JsonProperty("TransactionID")]
        public string TransactionID { get; set; }

        [JsonProperty("SecureHash")]
        public string SecureHash { get; set; }

        [JsonProperty("Message")]
        public string Message { get; set; }
    }
}
