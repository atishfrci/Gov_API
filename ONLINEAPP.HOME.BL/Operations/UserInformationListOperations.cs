using ONLINEAPP.DAL;
using ONLINEAPP.HOME.INTERFACE;
using ONLINEAPP.HOME.MODEL;
using ONLINEAPP.HOME.VIEWMODEL;
using ONLINEAPP.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ONLINEAPP.HOME.BL.Operations
{
    public class UserInformationListOperations : IUserInformationList
    {
        public List<UserInformationList> UserInformationList(string siteUrl, string token)
        {
            try
            {
                string RestUrl = string.Concat(siteUrl, ListURLs.RestUrlListItemWithQuery(Lists.UserInformationList, true),
                                                    string.Format(RESTFilters.topItems, GetTop._10000), string.Format(RESTFilters.orderByAscending, Fields.Title));

                return CRUDOperations.GetListByRestURL<UserInformationList>(RestUrl, token);
            }
            catch (Exception ex)
            {
                string guid = CRUDOperations.WriteException(ex, MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.Name);
                throw new Exception(string.Format(Messages.MsgExceptionOccured, guid));
            }
        }

        public UserInformationList GetUserInformationByID(string id, string siteUrl, string token)
        {
            try
            {
                string RestUrl = string.Concat(siteUrl, ListURLs.RestUrlListItemWithQuery(Lists.UserInformationList, true),
                                                    string.Format(RESTFilters.ByID, id));

                return CRUDOperations.GetListByRestURL<UserInformationList>(RestUrl, token).FirstOrDefault();
            }
            catch (Exception ex)
            {
                string guid = CRUDOperations.WriteException(ex, MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.Name);
                throw new Exception(string.Format(Messages.MsgExceptionOccured, guid));
            }
        }

        public List<UserInformationListViewModel> GetUserInformationListViewModel(string siteUrl, string token)
        {
            try
            {
                string RestUrl = string.Concat(siteUrl, ListURLs.RestUrlListItemWithQuery(Lists.UserInformationList, true),
                                                    string.Format(RESTFilters.topItems, GetTop._10000), string.Format(RESTFilters.orderByAscending, Fields.Title));

                return CRUDOperations.GetListByRestURL<UserInformationListViewModel>(RestUrl, token);
            }
            catch (Exception ex)
            {
                string guid = CRUDOperations.WriteException(ex, MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.Name);
                throw new Exception(string.Format(Messages.MsgExceptionOccured, guid));
            }
        }

        public List<UserInformationDomainIDListViewModel> GetUserInformationDomainIDListViewModel(string siteUrl, string token)
        {
            try
            {
                string RestUrl = string.Concat(siteUrl, ListURLs.RestUrlListItemWithQuery(Lists.UserInformationList, true),
                                                    string.Format(RESTFilters.topItems, GetTop._10000), string.Format(RESTFilters.orderByAscending, Fields.Title));

                return CRUDOperations.GetListByRestURL<UserInformationDomainIDListViewModel>(RestUrl, token);
            }
            catch (Exception ex)
            {
                string guid = CRUDOperations.WriteException(ex, MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.Name);
                throw new Exception(string.Format(Messages.MsgExceptionOccured, guid));
            }
        }

        public List<UserInformationEMailListViewModel> GetUserInformationEMailListViewModel(string siteUrl, string token)
        {
            try
            {
                string RestUrl = string.Concat(siteUrl, ListURLs.RestUrlListItemWithQuery(Lists.UserInformationList, true),
                                                    string.Format(RESTFilters.topItems, GetTop._10000), string.Format(RESTFilters.orderByAscending, Fields.Title));

                return CRUDOperations.GetListByRestURL<UserInformationEMailListViewModel>(RestUrl, token);
            }
            catch (Exception ex)
            {
                string guid = CRUDOperations.WriteException(ex, MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.Name);
                throw new Exception(string.Format(Messages.MsgExceptionOccured, guid));
            }
        }

        public List<UserInformationListViewModel> GetUserInformationListByName(string userName, string siteUrl, string token)
        {
            try
            {
                string RestUrl = string.Concat(siteUrl, ListURLs.RestUrlListItemWithQuery(Lists.UserInformationList, true),
                                                    string.Format(RESTFilters.ContainsByTitleOrEmail, userName),
                                                    string.Format(RESTFilters.topItems, GetTop._10000), string.Format(RESTFilters.orderByAscending, Fields.Title));

                List<UserInformationListNameEMailViewModel> lst = CRUDOperations.GetListByRestURL<UserInformationListNameEMailViewModel>(RestUrl, token);
                if (lst != null && lst.Count > 0)
                {
                    lst = lst.GroupBy(x => x.Title, (key, group) => (group.Count() == 1 ? group.First() : group.Where(i => !i.Name.Contains("i:0#.w")).Select(i => i).FirstOrDefault())).Cast<UserInformationListNameEMailViewModel>().ToList();

                    List<UserInformationListViewModel> lstFinal = lst.Where(i => i != null).Select(i => new UserInformationListViewModel() { ID = i.ID, Name = i.Title }).ToList();

                    return lstFinal;
                }
                else
                    return new List<UserInformationListViewModel>();
            }
            catch (Exception ex)
            {
                string guid = CRUDOperations.WriteException(ex, MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.Name);
                throw new Exception(string.Format(Messages.MsgExceptionOccured, guid));
            }
        }

        public List<UserInformationEMailListViewModel> GetUserInformationEMailListByEmail(string email, string siteUrl, string token)
        {
            try
            {
                string RestUrl = string.Concat(siteUrl, ListURLs.RestUrlListItemWithQuery(Lists.UserInformationList, true),
                                                    string.Format(RESTFilters.ContainsByEMail, email),
                                                    string.Format(RESTFilters.topItems, GetTop._10000), string.Format(RESTFilters.orderByAscending, Fields.Title));

                return CRUDOperations.GetListByRestURL<UserInformationEMailListViewModel>(RestUrl, token);
            }
            catch (Exception ex)
            {
                string guid = CRUDOperations.WriteException(ex, MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.Name);
                throw new Exception(string.Format(Messages.MsgExceptionOccured, guid));
            }
        }

        public List<UserInformationDomainIDListViewModel> GetUserInformationDomainIDListByUserName(string name, string siteUrl, string token)
        {
            try
            {
                string RestUrl = string.Concat(siteUrl, ListURLs.RestUrlListItemWithQuery(Lists.UserInformationList, true),
                                                    string.Format(RESTFilters.ContainsByUserName, name),
                                                    string.Format(RESTFilters.topItems, GetTop._10000), string.Format(RESTFilters.orderByAscending, Fields.Title));

                return CRUDOperations.GetListByRestURL<UserInformationDomainIDListViewModel>(RestUrl, token);
            }
            catch (Exception ex)
            {
                string guid = CRUDOperations.WriteException(ex, MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.Name);
                throw new Exception(string.Format(Messages.MsgExceptionOccured, guid));
            }
        }
    }
}
