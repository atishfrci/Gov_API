using Newtonsoft.Json;
using System;

namespace ONLINEAPP.ANONYMOUS.MODEL
{
    public class News
    {
        [JsonProperty("Id")]
        public int Id { get; set; }

        [JsonProperty("Title")]
        public string Title { get; set; }

        [JsonProperty("newstitle")]
        public string newstitle { get; set; }

        [JsonProperty("news_photo")]
        public string Photo { get; set; }

        [JsonProperty("newsdescription")]
        public string Description { get; set; }

        [JsonProperty("news_multiphoto")]
        public string MultiplePhotos { get; set; }

        [JsonProperty("newsactiveon")]
        public DateTime ActiveOn { get; set; }

        [JsonProperty("newspinontop")]
        public string PinonTop { get; set; }
    }
}
