using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ONLINEAPP.MODEL
{
    public class BaseID
    {
        [JsonProperty("ID")]
        public string ID { get; set; }
    }
}
