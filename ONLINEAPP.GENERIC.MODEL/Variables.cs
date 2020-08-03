using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace ONLINEAPP.GENERIC.MODEL
{
    public static class Variables
    {
        #region Lists

        public const string ListEServiceName = "EServices";
        public const string ListTransportName = "TransportList";

        #endregion


        #region Subsites

        public const string SubsiteTransport = "transport";

        #endregion


        #region Urls

        public static string EServiceUrl = WebConfigurationManager.AppSettings["EServiceUrl"];

        #endregion


        #region Credentials

        public static string NAFUserName = WebConfigurationManager.AppSettings["NAFUserName"];
        public static string NAFPassword = WebConfigurationManager.AppSettings["NAFPassword"];

        #endregion


        #region Filters

        public const string EServiceFilter = "/items?$select=EServiceName,EServiceUrl,EServiceListName,EServicePublicFormUrl";
        public const string RequestFilter = "/items?$select=SID,GenericId,Created,Status";
        public const string SelectAllFilter = "/items?$select=*";
        #endregion

    }
}
