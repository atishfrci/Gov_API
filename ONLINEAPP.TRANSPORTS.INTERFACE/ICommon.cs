using ONLINEAPP.TRANSPORTS.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ONLINEAPP.TRANSPORTS.INTERFACE
{
    public interface ICommon
    {
        ReservedMark GetReservedMarksByMark(string RegistrationMark, string siteUrl, string token);
    }
}
