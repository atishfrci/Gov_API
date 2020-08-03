using ONLINEAPP.ANONYMOUS.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ONLINEAPP.ANONYMOUS.VIEWMODEL
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
