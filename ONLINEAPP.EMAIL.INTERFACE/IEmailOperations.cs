using ONLINEAPP.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ONLINEAPP.EMAIL.INTERFACE
{
    public interface IEmailOperations
    {
        Result Send(ONLINEAPP.EMAIL.MODEL.Email email);
    }
}
