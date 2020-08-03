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
    public class NewsController : ApiController
    {
        private INews objNewsOperations { get; set; }
        public NewsController()
            : base()
        {
        }
        public NewsController(INews dep)
        {
            this.objNewsOperations = dep;
        }

        string token = "No Authorization";

        [HttpGet]
        public IHttpActionResult Get()
        {
            return Ok(objNewsOperations.GetNews(Constants.NewsSiteUrl, token));
        }

        [HttpGet]
        public IHttpActionResult NewsByID(string id)
        {
            return Ok(objNewsOperations.GetNewsByID(id, Constants.NewsSiteUrl, token));
        }

    }
}
