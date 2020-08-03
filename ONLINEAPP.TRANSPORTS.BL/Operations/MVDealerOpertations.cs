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
    public class MVDealerOpertations : IMVDealer
    {
        public List<MVDealerList> GetMVDCode(string siteUrl, string token, string mvdcode)
        {
            try
            {
                string RestUrl = string.Concat(siteUrl, ListURLs.RestUrlListItemWithQuery(typeof(MVDealerList).Name, true),
                                                   string.Format(RESTFilters.ByMVDCode, mvdcode), string.Format(RESTFilters.topItems, GetTop._1), string.Format(RESTFilters.orderByDescending, Fields.ID));

                var _result = CRUDOperations.GetListByRestURL<MVDealerList>(RestUrl, token);

                return _result.ToList();
            }
            catch (Exception ex)
            {
                string guid = CRUDOperations.WriteException(ex, MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.Name);
                throw new Exception(string.Format(Messages.MsgExceptionOccured, guid));
            }
        }
    }
}
