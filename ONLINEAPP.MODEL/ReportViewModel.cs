using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ONLINEAPP.MODEL
{
    public class ReportViewModel
    {
        [JsonProperty("ID")]
        public int ID { get; set; }

        [JsonProperty("Created")]
        public DateTime Created { get; set; }
    }
}
