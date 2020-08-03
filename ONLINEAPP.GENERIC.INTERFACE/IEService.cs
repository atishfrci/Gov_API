using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ONLINEAPP.GENERIC.MODEL;

namespace ONLINEAPP.GENERIC.INTERFACE
{

    public interface IEService
    {
        List<EService> GetAllEServices(string token);

        string BuildRestUrl();

        AdminDetails GetAdminDetails(string token);

    }
}
