using ONLINEAPP.HOME.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ONLINEAPP.HOME.INTERFACE
{
    public interface ISiteGroup
    {
        List<SiteGroup> SiteGroups(string siteUrl, string token);

        SiteGroup GetSiteGroupByID(string id, string siteUrl, string token);

        SiteGroup GetSiteGroupByGroupName(string groupName, string siteUrl, string token);

        List<SiteGroup> GetSiteGroupByUserId(string id, string siteUrl, string token);

        List<SiteUsers> GetUsersFromGroup(string group, string siteUrl, string token);
    }
}
