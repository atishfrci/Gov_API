using ONLINEAPP.ANONYMOUS.INTERFACE;
using ONLINEAPP.ANONYMOUS.MODEL;
using ONLINEAPP.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace ONLINEAPP.ANS.Controllers
{
    public class EventsController : ApiController
    {
        private IEvents objEventsOperations { get; set; }
        public EventsController()
            : base()
        {
        }
        public EventsController(IEvents dep)
        {
            this.objEventsOperations = dep;
        }

        string token = "No Authorization";

        [HttpGet]
        public IHttpActionResult Get()
        {
            return Ok(objEventsOperations.GetEvents(Constants.EventSiteUrl, token));
        }

        [HttpGet]
        public IHttpActionResult EventsByID(string id)
        {
            return Ok(objEventsOperations.EventsByID(id, Constants.EventSiteUrl, token));
        }
        
    }
}
