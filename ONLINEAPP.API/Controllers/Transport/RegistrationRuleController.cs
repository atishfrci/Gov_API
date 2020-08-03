using ONLINEAPP.MODEL;
using ONLINEAPP.TRANSPORTS.INTERFACE;
using System;
using System.Web;
using System.Web.Http;
namespace ONLINEAPP.API.Controllers.Transport
{
    public class RegistrationRuleController : ApiController
    {

        private IRegistrationRule objRegistrationRuleOperations { get; set; }
        public RegistrationRuleController()
            : base()
        {
        }
        public RegistrationRuleController(IRegistrationRule dep)
        {
            this.objRegistrationRuleOperations = dep;
        }

        string token = Convert.ToString(HttpContext.Current.Request.Headers["Authorization"]);

        [Authorize]
        [HttpGet]
        public IHttpActionResult Get()
        {
            return Ok(objRegistrationRuleOperations.GetRegistrationRules(Constants.TransportSiteUrl, token));
        }
        [Authorize]
        [HttpGet]
        public IHttpActionResult RuleByStatus(string Status)
        {
            return Ok(objRegistrationRuleOperations.RuleByStatus(Status, Constants.TransportSiteUrl, token));
        }
        [Authorize]
        [HttpGet]
        public IHttpActionResult RuleByStatusAndYear(string Status, string Year)
        {
            return Ok(objRegistrationRuleOperations.RuleByStatusAndYear(Status, Year, Constants.TransportSiteUrl, token));
        }
        [Authorize]
        [HttpGet]
        public IHttpActionResult RuleById(string id)
        {
            return Ok(objRegistrationRuleOperations.RuleByID(id, Constants.TransportSiteUrl, token));
        }
    }
}
