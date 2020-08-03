using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ONLINEAPP.GENERIC.MODEL
{
    public class EService
    {
        [JsonProperty("EServiceListName")]
        public string EServiceListName { get; set; }

        [JsonProperty("EServiceName")]
        public string EServiceName { get; set; }

        [JsonProperty("EServicePublicFormUrl")]
        public string EServicePublicFormUrl { get; set; }

        [JsonProperty("EServiceUrl")]
        public string EServiceUrl { get; set; }
    }
}
