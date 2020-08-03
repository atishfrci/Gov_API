using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ONLINEAPP.MODEL
{
    public class StatusCode
    {
        public static int Success { get { return 1; } }
        public static int Error { get { return 2; } }
        public static int CannotDelete { get { return 3; } }
        public static int ExceedLimit { get { return 4; } }
    }
}
