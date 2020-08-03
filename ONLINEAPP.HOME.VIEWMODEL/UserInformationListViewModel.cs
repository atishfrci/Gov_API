using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ONLINEAPP.HOME.VIEWMODEL
{
    public class UserInformationListViewModel
    {
        [JsonProperty("ID")]
        public int ID { get; set; }

        [JsonProperty("Title")]
        public string _Name { set { Name = value; } }
        public string Name { get; set; }
    }

    public class UserInformationDomainIDListViewModel
    {
        [JsonProperty("ID")]
        public int ID { get; set; }

        [JsonProperty("UserName")]
        public string UserName { get; set; }
    }

    public class UserInformationEMailListViewModel
    {
        [JsonProperty("ID")]
        public int ID { get; set; }

        [JsonProperty("EMail")]
        public string EMail { get; set; }
    }

    public class UserInformationListNameEMailViewModel
    {
        [JsonProperty("ID")]
        public int ID { get; set; }

        [JsonProperty("Title")]
        public string Title { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("EMail")]
        public string EMail { get; set; }

    }
}
