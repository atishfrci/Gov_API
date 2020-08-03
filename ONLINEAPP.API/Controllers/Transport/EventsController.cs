using ONLINEAPP.MODEL;
using ONLINEAPP.TRANSPORTS.INTERFACE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace ONLINEAPP.API.Controllers.Transport
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

        string token = Convert.ToString(HttpContext.Current.Request.Headers["Authorization"]);

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
        
        [Authorize]
        [HttpPost]
        public IHttpActionResult Post(ONLINEAPP.TRANSPORTS.MODEL.Events ObjEvents)
        {
            return Ok(objEventsOperations.AddEvents(ObjEvents, Constants.EventSiteUrl, token));
        }

        [Authorize]
        [HttpPost]
        public IHttpActionResult UpdateEventsByID(ONLINEAPP.TRANSPORTS.MODEL.Events ObjEvents)
        {
            return Ok(objEventsOperations.UpdateEventByID(ObjEvents, Constants.EventSiteUrl, token));
        }

        [Authorize]
        [HttpPost]
        public IHttpActionResult DeleteEventsByID(string id)
        {
            return Ok(objEventsOperations.DeleteEventByID(id, Constants.EventSiteUrl, token));
        }

    }
}
