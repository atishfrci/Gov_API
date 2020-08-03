using ONLINEAPP.MODEL;
using ONLINEAPP.TRANSPORTS.INTERFACE;
using System;
using System.Web;
using System.Web.Http;

namespace ONLINEAPP.API.Controllers.Transport
{
    public class TransportController : ApiController
    {

        private ITransport objTransportOperations { get; set; }
        public TransportController()
            : base()
        {
        }
        public TransportController(ITransport dep)
        {
            this.objTransportOperations = dep;
        }

        string token = Convert.ToString(HttpContext.Current.Request.Headers["Authorization"]);

        [Authorize]
        [HttpGet]
        public IHttpActionResult Get()
        {
            return Ok(objTransportOperations.GetTransport(Constants.TransportSiteUrl, token));
        }

        [Authorize]
        [HttpGet]
        public IHttpActionResult TransportByID(string id)
        {
            return Ok(objTransportOperations.TransportByID(id, Constants.TransportSiteUrl, token));
        }

        [Authorize]
        [HttpGet]
        public IHttpActionResult TransportByRegistrationMark(string RegistrationMark)
        {
            return Ok(objTransportOperations.TransportByRegistrationMark(RegistrationMark, Constants.TransportSiteUrl, token));
        }

        [Authorize]
        [HttpGet]
        public IHttpActionResult TransportByRegistrationMarkByStatus(string RegistrationMark)
        {
            return Ok(objTransportOperations.TransportByRegistrationMarkByStatus(RegistrationMark, Constants.TransportSiteUrl, token));

        }

        [Authorize]
        [HttpGet]
        public IHttpActionResult IsRegistrationAvailable(string RegistrationMark, string Category, string Id)
        {
            return Ok(objTransportOperations.RegistrationAvailability(RegistrationMark, Constants.TransportSiteUrl, token, Category, Id));
        }

        [Authorize]
        [HttpGet]
        public IHttpActionResult TransportByStatus(string Status)
        {
            return Ok(objTransportOperations.TransportByStatus(Status, Constants.TransportSiteUrl, token));
        }

        [Authorize]
        [HttpGet]
        public IHttpActionResult TransportCountByStatus(string Status)
        {
            return Ok(objTransportOperations.TransportCountByStatus(Status, Constants.TransportSiteUrl, token));
        }

        [Authorize]
        [HttpGet]
        public IHttpActionResult Search(string Prefix, string FromNumber, string ToNumber)
        {
            return Ok(objTransportOperations.GetRegistrationMarks(Prefix, FromNumber, ToNumber, Constants.TransportSiteUrl, token));
        }

       
        //[Authorize]
        //[HttpGet]
        //public IHttpActionResult CheckRegistrationMark(string Status)
        //{
        //    return Ok(objTransportOperations.ValidateRegistrationNo(Status));
        //}

        [Authorize]
        [HttpPost]
        public IHttpActionResult Post(ONLINEAPP.TRANSPORTS.MODEL.TransportList ObjTransport)
        {
            return Ok(objTransportOperations.AddTransport(ObjTransport, Constants.TransportSiteUrl, token));
        }

        [Authorize]
        [HttpPost]
        public IHttpActionResult UpdateTransportByID(ONLINEAPP.TRANSPORTS.MODEL.TransportList ObjTransport)
        {
            return Ok(objTransportOperations.UpdateTransportByID(ObjTransport, Constants.TransportSiteUrl, token));
        }

        [Authorize]
        [HttpPost]
        public IHttpActionResult DeleteTransportByID(string id)
        {
            return Ok(objTransportOperations.DeleteTransportByID(id, Constants.TransportSiteUrl, token));
        }


        [Authorize]
        [HttpGet]
        public IHttpActionResult IsMVDCodeValid(string mvdCode)
        {
            //return Ok(objMVDealerOperations.GetMVDCode(Constants.TransportSiteUrl,token,mvdCode));
            return Ok(objTransportOperations.MvdAvailability(mvdCode, Constants.TransportSiteUrl, token)); 
        }

        [Authorize]
        [HttpGet]
        public IHttpActionResult IsApplicantValid(string NIC)
        {
            //return Ok(objMVDealerOperations.GetMVDCode(Constants.TransportSiteUrl,token,mvdCode));
            return Ok(objTransportOperations.ValidateNIC(NIC,token));
        }

        [Authorize]
        [HttpGet]
        public IHttpActionResult IsCompanyValid(string BRN)
        {
            //return Ok(objMVDealerOperations.GetMVDCode(Constants.TransportSiteUrl,token,mvdCode));
            return Ok(objTransportOperations.ValidateBRN(BRN, token));
        }

        /* COMMENTED BY HEMA SINCE NO NEED TO CALL FROM FE 
        [Authorize]
        [HttpGet]
        public IHttpActionResult IsMarkAvailableFromInfoHighway(string regMark)
        {
            //return Ok(objMVDealerOperations.GetMVDCode(Constants.TransportSiteUrl,token,mvdCode));
            return Ok(objTransportOperations.CheckAvailableMarkFromInfoHighway(regMark,token));
        }

        [Authorize]
        [HttpGet]
        public IHttpActionResult IsMarkSoldFromInfoHighway(string regMark)
        {
            //return Ok(objMVDealerOperations.GetMVDCode(Constants.TransportSiteUrl,token,mvdCode));
            return Ok(objTransportOperations.CheckSoldMarkFromInfoHighway(regMark,token));
        }
        */
        [Authorize]
        [HttpGet]
        public IHttpActionResult PaymentTypeFromPaymentGateway()
        {
            //return Ok(objMVDealerOperations.GetMVDCode(Constants.TransportSiteUrl,token,mvdCode));
            return Ok(objTransportOperations.getPaymentTypeFromPaymentGateway()); 
        }

        [Authorize]
        [HttpGet]
        public IHttpActionResult CurrenciesFromPaymentGateway(string selectedPaymentType)
        {
            //return Ok(objMVDealerOperations.GetMVDCode(Constants.TransportSiteUrl,token,mvdCode));
            return Ok(objTransportOperations.getCurrencyFromPaymentGateway(selectedPaymentType)); 
        }

        [Authorize]
        [HttpGet]
        public IHttpActionResult SimpleTransacToPaymentGateway(string Amount, string username, string userDisplayName, string requestId, string selectedCurrencyCode, string selectedPaymentTypeCode, string messageFromFE)
        {
            //return Ok(objMVDealerOperations.GetMVDCode(Constants.TransportSiteUrl,token,mvdCode));
            return Ok(objTransportOperations.addSimpleTransactionToPaymentGateway(Amount, username, userDisplayName, requestId, selectedCurrencyCode, selectedPaymentTypeCode, messageFromFE));
        }

        [Authorize]
        [HttpGet]
        public IHttpActionResult SbmPaymentGateway(string Url)
        {
            //return Ok(objMVDealerOperations.GetMVDCode(Constants.TransportSiteUrl,token,mvdCode));
            return Ok(objTransportOperations.callSbmPaymentGateway(Url)); 
        }


    }
}
