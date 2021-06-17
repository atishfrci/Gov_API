using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ONLINEAPP.EMAIL.MODEL
{
    public class Email
    {

        [JsonProperty("ListTo")]
        public List<string> ListTo { get; set; }

        [JsonProperty("ListCc")]
        public List<string> ListCc { get; set; }

        [JsonProperty("Subject")]
        public string Subject { get; set; }

        [JsonProperty("Body")]
        public string Body { get; set; }

        [JsonProperty("IsBodyHtml")]
        public bool IsBodyHtml { get; set; }

    }
}
