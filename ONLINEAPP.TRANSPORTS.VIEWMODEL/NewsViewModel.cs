using ONLINEAPP.TRANSPORTS.MODEL;
using System;
using System.Collections.Generic;

namespace ONLINEAPP.TRANSPORTS.VIEWMODEL
{
    public class NewsViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string newstitle { get; set; }

        public List<File> Photos { get; set; }

        public string Description { get; set; }

        public DateTime ActiveOn { get; set; }

        public string PinonTop { get; set; }
    }
   
}
