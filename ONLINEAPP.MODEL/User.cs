using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ONLINEAPP.MODEL
{
    /// <summary>
    /// This model holds properties required from sharepoint user.
    /// </summary>
    public class User : BaseID
    {
        [JsonProperty("Title")]
        public string _Name { set { Name = value; } }
        [JsonProperty("Name")]
        public string Name { get; set; }
    }

    /// <summary>
    /// This model holds properties required from sharepoint user.
    /// </summary>
    public class UserEmail : BaseID
    {
        [JsonProperty("EMail")]
        public string EMail { get; set; }
    }

    /// <summary>
    /// This model holds properties required from sharepoint user.
    /// </summary>
    public class UserLoginName : BaseID
    {
        [JsonProperty("UserName")]
        public string UserName { get; set; }
    }

    public class UserAndEmail : BaseID
    {
        [JsonProperty("Title")]
        public string _Name { set { Name = value; } }
        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("EMail")]
        public string EMail { get; set; }
    }

    public class UserEmployeeName : BaseID
    {
        [JsonProperty("EmployeeName")]
        public string _Name { set { Name = value; } }
        [JsonProperty("Name")]
        public string Name { get; set; }
    }
}
