using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ONLINEAPP.MODEL
{
    public class LookupValue : BaseID
    {
        [JsonProperty("Title")]
        public string Title { get; set; }

    }

    public class LookupDesignationValue : BaseID
    {
        [JsonProperty("Designation")]
        public string _Title { set { Title = value; } }
        [JsonProperty("Title")]
        public string Title { get; set; }
    }

    public class LookupGradeValue : BaseID
    {
        [JsonProperty("Grade")]
        public string _Title { set { Title = value; } }
        [JsonProperty("Title")]
        public string Title { get; set; }
    }

    public class LookupLevelValue : BaseID
    {
        [JsonProperty("Level")]
        public string _Title { set { Title = value; } }
        [JsonProperty("Title")]
        public string Title { get; set; }
    }

    public class LookupGroupValue : BaseID
    {
        [JsonProperty("Group")]
        public string _Title { set { Title = value; } }
        [JsonProperty("Title")]
        public string Title { get; set; }
    }

    public class LookupChartLevelValue : BaseID
    {
        [JsonProperty("ChartLevel")]
        public string _Title { set { Title = value; } }
        [JsonProperty("Title")]
        public string Title { get; set; }
    }

    public class LookupMainGroupValue : BaseID
    {
        [JsonProperty("MainGroup")]
        public string _Title { set { Title = value; } }
        [JsonProperty("Title")]
        public string Title { get; set; }
    }
}
