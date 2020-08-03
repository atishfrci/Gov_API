using ONLINEAPP.HOME.MODEL;
using ONLINEAPP.HOME.VIEWMODEL;
using ONLINEAPP.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ONLINEAPP.HOME.INTERFACE
{
    public interface IUserInformationList
    {
        #region User Information List Operations - Interfaces

        List<UserInformationList> UserInformationList(string siteUrl,string token);

        UserInformationList GetUserInformationByID(string id, string siteUrl, string token);

        List<UserInformationListViewModel> GetUserInformationListByName(string userName, string siteUrl, string token);

        List<UserInformationListViewModel> GetUserInformationListViewModel(string siteUrl, string token);

        List<UserInformationDomainIDListViewModel> GetUserInformationDomainIDListViewModel(string siteUrl, string token);

        List<UserInformationEMailListViewModel> GetUserInformationEMailListViewModel(string siteUrl, string token);

        List<UserInformationEMailListViewModel> GetUserInformationEMailListByEmail(string email, string siteUrl, string token);

        List<UserInformationDomainIDListViewModel> GetUserInformationDomainIDListByUserName(string name, string siteUrl, string token);

        #endregion
    }
}
