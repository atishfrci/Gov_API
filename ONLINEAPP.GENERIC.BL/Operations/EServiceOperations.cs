
using ONLINEAPP.DAL;
using ONLINEAPP.GENERIC.INTERFACE;
using ONLINEAPP.GENERIC.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ONLINEAPP.GENERIC.BL.Operations
{
    public class EServiceOperations : IEService
    {
        public List<EService> GetAllEServices(string token)
        {
            try
            {
                return ((IEnumerable<EService>)CRUDOperations.GetListByRestURL<EService>(this.BuildRestUrl(), token)).ToList<EService>();
            }
            catch (Exception ex)
            {
                string name1 = MethodBase.GetCurrentMethod().Name;
                string name2 = MethodBase.GetCurrentMethod().DeclaringType.Name;
                throw new Exception(string.Format("An error occured while reading data. GUID: {0}", (object)CRUDOperations.WriteException(ex, name1, name2)));
            }
        }

        public string BuildRestUrl()
        {
            try
            {
                return Variables.EServiceUrl + "/" + string.Format("/_api/web/lists/GetByTitle('{0}')", (object)"EServices") + "/items?$select=EServiceName,EServiceUrl,EServiceListName,EServicePublicFormUrl,TableName,NewBackend";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public AdminDetails GetAdminDetails(string token)
        {
            AdminDetails adminDetails = new AdminDetails();
            adminDetails.UserName = (string)null;
            adminDetails.Password = (string)null;
            try
            {
                if (string.IsNullOrEmpty(token))
                    throw new UnauthorizedAccessException();
                if (!string.IsNullOrEmpty(Variables.NAFUserName))
                {
                    if (!string.IsNullOrEmpty(Variables.NAFPassword))
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
