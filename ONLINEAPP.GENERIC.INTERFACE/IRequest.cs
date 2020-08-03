using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ONLINEAPP.GENERIC.MODEL;
using ONLINEAPP.TRANSPORTS.MODEL;
using ONLINEAPP.MODEL;

namespace ONLINEAPP.GENERIC.INTERFACE
{
    public interface IRequest
    {
        List<Request> GetAllRequests(string token, string subSiteName, string listName, string userName);

        string BuildRestUrl(string subSiteName, string listName, string filter, string userName = null);

        Result CancelTransportRequest(TransportList objUpdateRequest, string siteUrl, string listName, string token);

        TransportList GetTransport(string requestId, string token);

    }
}
