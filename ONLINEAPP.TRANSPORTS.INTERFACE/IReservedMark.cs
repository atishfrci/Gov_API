using ONLINEAPP.MODEL;
using ONLINEAPP.TRANSPORTS.MODEL;
using System.Collections.Generic;

namespace ONLINEAPP.TRANSPORTS.INTERFACE
{
    public interface IReservedMark
    {
        List<ReservedMark> GetReservedMarks(string siteUrl, string token , string prefix);

        ReservedMark ReservedMarkByID(string id, string siteUrl, string token);

        ReservedMark ReservedRegistrationMark(string RegistrationMark, string siteUrl, string token, string prefix);

        Result AddReservedMark(ReservedMark objReservedMark, string siteUrl, string token);

        Result UpdateReservedMarkByID(ReservedMark objReservedMark, string siteUrl, string token);

        Result DeleteReservedMarkByID(string id, string siteUrl, string token);

        List<ReservedMark> ReservedMarkByStatus(string Status, string siteUrl, string token);
    }
}
