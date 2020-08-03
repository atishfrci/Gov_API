using Newtonsoft.Json;
using System;


namespace ONLINEAPP.TRANSPORTS.MODEL
{
    public class Events
    {
        [JsonProperty("Id")]
        public int Id { get; set; }

        [JsonProperty("Title")]
        public string Title { get; set; }

        [JsonProperty("eventtitle")]
        public string EventTitle { get; set; }

        [JsonProperty("eventdescription")]
        public string EventDescription { get; set; }

        [JsonProperty("eventdate")]
        public string EventDate { get; set; }

        [JsonProperty("eventvenue")]
        public string EventVenue { get; set; }

        [JsonProperty("eventcategory")]
        public string EventCategory { get; set; }
    }
}
