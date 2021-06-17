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

        [JsonProperty("TableName")]
        public string EServiceTableName { get; set; }

        [JsonProperty("NewBackend")]
        public string EServiceNewBackEnd { get; set; }

        [JsonProperty("StatusList")]
        public string EServiceStatusList { get; set; }

        [JsonProperty("Subsite")]
        public string EServiceSubsite { get; set; }

        [JsonProperty("FieldsList")]
        public string EServiceFieldsList { get; set; }

        [JsonProperty("SQLOrSP")]
        public string SQLOrSP { get; set; } = "SQL";

        [JsonProperty("FrontEndSubsite")]
        public string EServiceFrontEndSubsite { get; set; }

        [JsonProperty("FrontEndList")]
        public string EServiceFrontEndList { get; set; }

        [JsonProperty("DBConnStrName")]
        public string EServiceDBConnStrName { get; set; }

    }
}
