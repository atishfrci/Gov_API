using ONLINEAPP.DAL;
using ONLINEAPP.MODEL;
using ONLINEAPP.TRANSPORTS.INTERFACE;
using ONLINEAPP.TRANSPORTS.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ONLINEAPP.TRANSPORTS.BL.Operations
{
    public class RegistrationRuleOperation : IRegistrationRule
    {
        public List<RegistrationRule> GetRegistrationRules(string siteUrl, string token)
        {
            try
            {
                string RestUrl = string.Concat(siteUrl, ListURLs.RestUrlListItemWithQuery(typeof(RegistrationRule).Name, true),
                                                    string.Format(RESTFilters.topItems, GetTop._1000), string.Format(RESTFilters.orderByDescending, Fields.ID));

                var _result = CRUDOperations.GetListByRestURL<RegistrationRule>(RestUrl, token);

                return _result.ToList();
            }
            catch (Exception ex)
            {
                string guid = CRUDOperations.WriteException(ex, MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.Name);
                throw new Exception(string.Format(Messages.MsgExceptionOccured, guid));
            }
        }

        public RegistrationRule RuleByID(string id, string siteUrl, string token)
        {
            try
            {
                string RestUrl = string.Concat(siteUrl, ListURLs.RestUrlListItemWithQuery(typeof(RegistrationRule).Name, true),
                                                    string.Format(RESTFilters.ByID, id));

                return CRUDOperations.GetListByRestURL<RegistrationRule>(RestUrl, token).FirstOrDefault();
            }
            catch (Exception ex)
            {
                string guid = CRUDOperations.WriteException(ex, MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.Name);
                throw new Exception(string.Format(Messages.MsgExceptionOccured, guid));
            }
        }

        public List<RegistrationRule> RuleByStatus(string Status, string siteUrl, string token)
        {
            try
            {
                string RestUrl = string.Concat(siteUrl, ListURLs.RestUrlListItemWithQuery(typeof(RegistrationRule).Name, true),
                                                    string.Format(RESTFilters.ByStatus, Status));

                return CRUDOperations.GetListByRestURL<RegistrationRule>(RestUrl, token);
            }
            catch (Exception ex)
            {
                string guid = CRUDOperations.WriteException(ex, MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.Name);
                throw new Exception(string.Format(Messages.MsgExceptionOccured, guid));
            }
        }
        
        public List<RegistrationRule> RuleByStatusAndYear(string Status, string Year, string siteUrl, string token)
        {
            try
            {
                string RestUrl = string.Concat(siteUrl, ListURLs.RestUrlListItemWithQuery(typeof(RegistrationRule).Name, true),
                                                    string.Format(RESTFilters.ByStatusAndYear, Status, Year));

                return CRUDOperations.GetListByRestURL<RegistrationRule>(RestUrl, token);
            }
            catch (Exception ex)
            {
                string guid = CRUDOperations.WriteException(ex, MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.Name);
                throw new Exception(string.Format(Messages.MsgExceptionOccured, guid));
            }
        }

        public Result AddRegistrationRule(RegistrationRule objRegistrationRule, string siteUrl, string token)
        {
            Result res = new Result();
            try
            {
                res = CRUDOperations.AddListItem<RegistrationRule>(siteUrl, token, typeof(RegistrationRule).Name, objRegistrationRule);

                res.StatusCode = StatusCode.Success;
                res.Message = Messages.MsgRegistrationRuleAddedSuccessfully;
                return res;
            }
            catch (Exception ex)
            {
                string guid = CRUDOperations.WriteException(ex, MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.Name);
                res.Message = Messages.MsgSomethingWentWrong;
                res.StatusCode = StatusCode.Error;
                return res;
            }
        }

        public Result UpdateRegistrationRuleByID(RegistrationRule objRegistrationRule, string siteUrl, string token)
        {
            Result res = new Result();
            try
            {
                res = CRUDOperations.UpdateListItem<RegistrationRule>(siteUrl, token, typeof(RegistrationRule).Name, objRegistrationRule);

                res.StatusCode = StatusCode.Success;
                res.Message = Messages.MsgRegistrationRuleUpdatedSuccessfully;
                return res;
            }
            catch (Exception ex)
            {
                string guid = CRUDOperations.WriteException(ex, MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.Name);
                res.Message = Messages.MsgSomethingWentWrong;
                res.StatusCode = StatusCode.Error;
                return res;
            }
        }

        public Result DeleteRegistrationRuleByID(string id, string siteUrl, string token)
        {
            Result res = new Result();
            try
            {
                res = CRUDOperations.DeleteListItem(siteUrl, typeof(RegistrationRule).Name, id);

                res.StatusCode = StatusCode.Success;
                res.Message = Messages.MsgRegistrationRuleDeletedSuccessfully;
                return res;
            }
            catch (Exception ex)
            {
                string guid = CRUDOperations.WriteException(ex, MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.Name);
                res.Message = Messages.MsgSomethingWentWrong;
                res.StatusCode = StatusCode.Error;
                return res;
            }
        }
    }
}
