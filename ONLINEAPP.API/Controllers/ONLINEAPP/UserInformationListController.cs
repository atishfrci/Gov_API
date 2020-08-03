using ONLINEAPP.HOME.INTERFACE;
using ONLINEAPP.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace ONLINEAPP.API.Controllers.Ahead
{
    public class UserInformationListController : ApiController
    {
        private IUserInformationList objUserInformationListOperations { get; set; }
        public UserInformationListController()
            : base()
        {
        }
        public UserInformationListController(IUserInformationList dep)
        {
            this.objUserInformationListOperations = dep;
        }

        string token = Convert.ToString(HttpContext.Current.Request.Headers["Authorization"]);

        [Authorize]
        [HttpGet]
        public IHttpActionResult Get()
        {
            return Ok(objUserInformationListOperations.UserInformationList(Constants.ROOTSiteUrl, token));
        }

        [Authorize]
        [HttpGet]
        public IHttpActionResult GetUserInformationByID(string id)
        {
            return Ok(objUserInformationListOperations.GetUserInformationByID(id, Constants.ROOTSiteUrl, token));
        }

        [Authorize]
        [HttpGet]
        public IHttpActionResult GetUserInformationListByName(string userName)
        {
            return Ok(objUserInformationListOperations.GetUserInformationListByName(userName, Constants.ROOTSiteUrl, token));
        }

        [Authorize]
        [HttpGet]
        public IHttpActionResult GetUserInformationListViewModel()
        {
            return Ok(objUserInformationListOperations.GetUserInformationListViewModel(Constants.ROOTSiteUrl, token));
        }

        [Authorize]
        [HttpGet]
        public IHttpActionResult GetUserInformationDomainIDListViewModel()
        {
            return Ok(objUserInformationListOperations.GetUserInformationDomainIDListViewModel(Constants.ROOTSiteUrl, token));
        }

        [Authorize]
        [HttpGet]
        public IHttpActionResult GetUserInformationEMailListViewModel()
        {
            return Ok(objUserInformationListOperations.GetUserInformationEMailListViewModel(Constants.ROOTSiteUrl, token));
        }

        [Authorize]
        [HttpGet]
        public IHttpActionResult GetUserInformationEMailListByEmail(string email)
        {
            return Ok(objUserInformationListOperations.GetUserInformationEMailListByEmail(email, Constants.ROOTSiteUrl, token));
        }

        [Authorize]
        [HttpGet]
        public IHttpActionResult GetUserInformationDomainIDListByUserName(string userName)
        {
            return Ok(objUserInformationListOperations.GetUserInformationDomainIDListByUserName(userName, Constants.ROOTSiteUrl, token));
        }
    }
}