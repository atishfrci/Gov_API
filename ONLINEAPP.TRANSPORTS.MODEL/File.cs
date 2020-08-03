using System;
using Newtonsoft.Json;

namespace ONLINEAPP.TRANSPORTS.MODEL
{
    public class File
    {
        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("ServerRelativeUrl")]
        public string ServerRelativeUrl { get; set; }

        public static void AppendAllText(object p, string v)
        {
            throw new NotImplementedException();
        }
    }
}
