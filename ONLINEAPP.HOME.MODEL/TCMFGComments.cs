using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ONLINEAPP.HOME.MODEL
{
    public class TCMFGComments : TCItemRevision
    {
        public string TEMfgComments { get; set; }
        public string MEMfgComments { get; set; }
        public string CDMfgComments { get; set; }
        public string PTDMfgComments { get; set; }
        public string MTDMfgComments { get; set; }
        public DateTime? CommentsDate { get; set; }
        public string OpenWithDept { get; set; }
    }
}
