using ONLINEAPP.SP.RESTSERVICES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ONLINEAPP.DAL
{
    public class GetUserInfo
    {
        public static string LoginName
        {
            get { return Convert.ToString(RESTAPI.TryGetClaim("LoginName").Value); }
        }

        public static string UserName
        {
            get { return Convert.ToString(RESTAPI.TryGetClaim("UserName").Value); }
        }

        public static string FirstName
        {
            get { return Convert.ToString(RESTAPI.TryGetClaim("FirstName").Value); }
        }

        public static string MiddleName
        {
            get { return Convert.ToString(RESTAPI.TryGetClaim("MiddleName").Value); }
        }

        public static string LastName
        {
            get { return Convert.ToString(RESTAPI.TryGetClaim("LastName").Value); }
        }

        public static string Email
        {
            get { return Convert.ToString(RESTAPI.TryGetClaim("Email").Value); }
        }

        //public static string EmployeeID
        //{
        //    get { return Convert.ToString(RESTAPI.TryGetClaim("TicketNumber").Value); }
        //}

        //public static string UserRole
        //{
        //    get { return Convert.ToString(RESTAPI.TryGetClaim("Department").Value); }
        //}

        //public static string Approvers
        //{
        //    get { return RESTAPI.TryGetClaim("Approvers").Value; }
        //}

        //public static string Department
        //{
        //    get { return RESTAPI.TryGetClaim("Department").Value; }
        //}
    }
}
