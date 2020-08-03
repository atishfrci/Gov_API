using ONLINEAPP.TRANSPORTS.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ONLINEAPP.TRANSPORTS.INTERFACE
{
    public interface IRegistrationRule
    {
        List<RegistrationRule> GetRegistrationRules(string siteUrl, string token);

        RegistrationRule RuleByID(string id, string siteUrl, string token);

        List<RegistrationRule> RuleByStatus(string Status, string siteUrl, string token);

        List<RegistrationRule> RuleByStatusAndYear(string Status, string Year, string siteUrl, string token);
    }
}
