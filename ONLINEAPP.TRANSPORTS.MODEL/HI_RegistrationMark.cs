using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ONLINEAPP.TRANSPORTS.MODEL
{
    public class HI_RegistrationMark
    {
        [JsonProperty("RegistrationMark")]
        public string RegistrationMark { get; set; }
    }
}
