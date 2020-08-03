using ONLINEAPP.MODEL;
using ONLINEAPP.TRANSPORTS.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ONLINEAPP.TRANSPORTS.INTERFACE
{
    public interface IEvents
    {
        List<Events> GetEvents(string siteUrl, string token);

        Events EventsByID(string id, string siteUrl, string token);

        List<Events> EventsByStatus(string Status, string siteUrl, string token);

        Result AddEvents(Events objEvents, string siteUrl, string token);

        Result UpdateEventByID(Events objRegistrationRule, string siteUrl, string token);

        Result DeleteEventByID(string id, string siteUrl, string token);
    }
}
