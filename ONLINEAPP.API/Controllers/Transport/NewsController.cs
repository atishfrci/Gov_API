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

        string token = Convert.ToString(HttpContext.Current.Request.Headers["Authorization"]);

        // [Authorize]
        [HttpGet]
        public IHttpActionResult Get()
        {
            return Ok(objNewsOperations.GetNews(Constants.NewsSiteUrl, token));
        }

        // [Authorize]
        [HttpGet]
        public IHttpActionResult NewsByID(string id)
        {
            return Ok(objNewsOperations.GetNewsByID(id, Constants.NewsSiteUrl, token));
        }
        
        [Authorize]
        [HttpPost]
        public IHttpActionResult Post(ONLINEAPP.TRANSPORTS.MODEL.NewsModel ObjNews)
        {
            return Ok(objNewsOperations.AddNews(ObjNews, Constants.NewsSiteUrl, token));
        }

        [Authorize]
        [HttpPost]
        public IHttpActionResult UpdateNewsByID(ONLINEAPP.TRANSPORTS.MODEL.NewsModel ObjNews)
        {
            return Ok(objNewsOperations.UpdateNewsByID(ObjNews, Constants.NewsSiteUrl, token));
        }

        [Authorize]
        [HttpPost]
        public IHttpActionResult DeleteNewsByID(string id)
        {
            return Ok(objNewsOperations.DeleteNewsByID(id, Constants.NewsSiteUrl, token));
        }

    }
}
