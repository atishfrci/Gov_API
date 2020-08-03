using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ONLINEAPP.HOME.MODEL
{
    public class SiteUsers
    {
        public int Id { get; set; }

        public string LoginName { get; set; }

        public string Title { get; set; }

        public int PrincipalType { get; set; }

        public string Email { get; set; }

        public bool IsSiteAdmin { get; set; }
    }
}
