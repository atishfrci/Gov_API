using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ONLINEAPP.MODEL
{
    public class Result
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string ReasonCode { get; set; }
        public string ErrorMsg { get; set; }
        public string RequestId { get; set; }
    }
}
