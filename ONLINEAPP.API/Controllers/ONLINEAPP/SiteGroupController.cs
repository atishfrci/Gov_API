using ONLINEAPP.HOME.INTERFACE;
using ONLINEAPP.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace ONLINEAPP.API.Controllers.Ahead
{
    public class SiteGroupController : ApiController
    {
        private ISiteGroup objSiteGroupOperations { get; set; }
        public SiteGroupController()
            : base()
        {
        }
        public SiteGroupController(ISiteGroup dep)
        {
            this.objSiteGroupOperations = dep;
        }

        string token = Convert.ToString(HttpContext.Current.Request.Headers["Authorization"]);

        [Authorize]
        [HttpGet]
        public IHttpActionResult Get()
        {
            return Ok(objSiteGroupOperations.SiteGroups(Constants.ROOTSiteUrl, token));
        }

        [Authorize]
        [HttpGet]
        public IHttpActionResult GetSiteGroupByID(string id)
        {
            return Ok(objSiteGroupOperations.GetSiteGroupByID(id, Constants.ROOTSiteUrl, token));
        }

        [Authorize]
        [HttpGet]
        public IHttpActionResult GetSiteGroupByGroupName(string groupName)
        {
            return Ok(objSiteGroupOperations.GetSiteGroupByGroupName(groupName, Constants.ROOTSiteUrl, token));
        }

        [Authorize]
        [HttpGet]
        public IHttpActionResult GetSiteGroupByUserId(string id)
        {
            return Ok(objSiteGroupOperations.GetSiteGroupByUserId(id, Constants.ROOTSiteUrl, token));
        }

        [Authorize]
        [HttpGet]
        public IHttpActionResult GetUsersFromGroup(string groupName)
        {
            return Ok(objSiteGroupOperations.GetUsersFromGroup(groupName, Constants.ROOTSiteUrl, token));
        }

    }
}