using ONLINEAPP.EMAIL.INTERFACE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace ONLINEAPP.API.Controllers.Email
{
    public class EmailController : ApiController
    {
        private IEmailOperations objEmailOperations { get; set; }

        public EmailController()
            : base()
        {
        }

        public EmailController(IEmailOperations emailOperations)
        {
            this.objEmailOperations = emailOperations;
        }


        [HttpPost]
        public IHttpActionResult Send(ONLINEAPP.EMAIL.MODEL.Email email)
        {
            return Ok(objEmailOperations.Send(email));
        }
    }
}