using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ONLINEAPP.GENERIC.MODEL
{
    public class AdminDetails
    {
        [JsonProperty("UserName")]
        public string UserName { get; set; }

        [JsonProperty("Password")]
        public string Password { get; set; }
    }
}
