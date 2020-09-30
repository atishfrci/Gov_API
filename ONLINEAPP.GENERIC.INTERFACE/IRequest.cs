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
        #region Generic

        //commented by atish before naf to make single call in workspace
        //List<Request> GetAllRequests(string token, string subSiteName, string listName, string userName);
        List<Request> GetAllRequests(string token, string userName);

        string BuildRestUrl(string subSiteName, string listName, string filter, string userName = null);

        #endregion

        #region Transport

        Result CancelTransportRequest(TransportList objUpdateRequest, string siteUrl, string listName, string token);

        TransportList GetTransport(string requestId, string token);

        #endregion

    }
}
