using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ONLINEAPP.DAL;
using ONLINEAPP.MODEL;
using ONLINEAPP.GENERIC.INTERFACE;
using ONLINEAPP.GENERIC.MODEL;
using System.Reflection;

namespace ONLINEAPP.GENERIC.BL.Operations
{
    public class EServiceOperations : IEService
    {

        //ok 100%
        public List<EService> GetAllEServices(string token)
        {
            try
            {

                string restUrl = BuildRestUrl();
                var result = CRUDOperations.GetListByRestURL<EService>(restUrl, token);

                return result.ToList();

            }
            catch (Exception ex)
            {
                string guid = CRUDOperations.WriteException(ex, MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.Name);
                throw new Exception(string.Format(Messages.MsgExceptionOccured, guid));
            }
        }

        //ok 100%
        public string BuildRestUrl()
        {
            try
            {

                return string.Concat(
                            string.Concat(Variables.EServiceUrl, "/"),
                            string.Format("/_api/web/lists/GetByTitle('{0}')", Variables.ListEServiceName),
                            Variables.EServiceFilter
                        );
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public AdminDetails GetAdminDetails(string token)
        {
            AdminDetails adminDetails = new AdminDetails();
            adminDetails.UserName = null;
            adminDetails.Password = null;
            
            try
            {
                if (!string.IsNullOrEmpty(token))
                {
                    if (!string.IsNullOrEmpty(Variables.NAFUserName) && !string.IsNullOrEmpty(Variables.NAFPassword))
                    {
                        adminDetails.UserName = Variables.NAFUserName;
                        adminDetails.Password = Variables.NAFPassword;
                    }
                }                
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return adminDetails;

        }

    }
}
