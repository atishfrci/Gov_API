using ONLINEAPP.TRANSPORTS.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ONLINEAPP.TRANSPORTS.INTERFACE
{
    public interface IMVDealer
    {
        List<MVDealerList> GetMVDCode(string siteUrl, string token, string mvdcode);
    }
}
