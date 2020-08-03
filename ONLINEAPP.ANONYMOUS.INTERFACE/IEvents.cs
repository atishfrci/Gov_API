using ONLINEAPP.ANONYMOUS.MODEL;
using ONLINEAPP.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ONLINEAPP.ANONYMOUS.INTERFACE
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
