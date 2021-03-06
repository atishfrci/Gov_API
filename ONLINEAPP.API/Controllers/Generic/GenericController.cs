﻿using System;
using System.Web;
using System.Web.Http;
using ONLINEAPP.GENERIC.INTERFACE;
using ONLINEAPP.MODEL;
using ONLINEAPP.GENERIC.MODEL;
using ONLINEAPP.TRANSPORTS.MODEL;

namespace ONLINEAPP.API.Controllers.Generic
{
    public class GenericController : ApiController
    {

        #region Fields and properties

        private IEService _eServiceOperations { get; set; }
        private IRequest _requestOperations { get; set; }

        #endregion


        #region Constructors and token


        public GenericController() : base()
        {
        }

        public GenericController(IEService dep, IRequest request)
        {
            this._eServiceOperations = dep;
            this._requestOperations = request;
        }

        string token = Convert.ToString(HttpContext.Current.Request.Headers["Authorization"]);


        #endregion



        #region Rest Methods


        #region EService Workspace Form

        /// <summary>
        /// Getting all eServices metadata
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        public IHttpActionResult GetAllEServices()
        {
            return Ok(_eServiceOperations.GetAllEServices(token));
        }


        /// <summary>
        /// Getting all user requests
        /// from passed subsite
        /// </summary>
        /// <param name="sub">subsite representing a service</param>
        /// <param name="lst">list that stores user request</param>
        /// <param name="usr">username</param>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        public IHttpActionResult GetAllRequests(string usr)
        {
           // usr = "Tester";
            return Ok(_requestOperations.GetAllRequests(token, usr));
        }




        /// <summary>
        /// Getting specific request data from Transport EService
        /// based on request id
        /// </summary>
        /// <param name="req">request id</param>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        public IHttpActionResult GetRequest(string req)
        {
            return Ok(_requestOperations.GetTransport(req, token));
        }

        #endregion
        /// <summary>
        /// Get id of request on frontend and eservice name and usr name to fetch fieldlist from front list to insert in db or sharepoint list
        /// </summary>
        /// <param name="id"></param>
        /// <param name="eserv"></param>
        /// <param name="usr"></param>
        /// <returns></returns>
        //[Authorize]
        [HttpGet]
        public IHttpActionResult EserviceCentralOperation(string id, string eserv, string usr)
        {
            // usr = "Tester";
            return Ok(_requestOperations.EserviceCentralOperation(token, id, eserv, usr));
        }


        #region EService Transport Workspace Form

        /// <summary>
        /// Getting complete transport item from sharepoint list
        /// based on the requestId
        /// </summary>
        /// <param name="req">RequestId of the Request</param>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        public IHttpActionResult GetTransport(string req)
        {
            return Ok(_requestOperations.GetTransport(req, token));
        }

        /// <summary>
        /// Cancelling request from eservice 
        /// transport workspace form
        /// </summary>
        /// <param name="updateRequest">complete list item with updated status</param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public IHttpActionResult CancelRequestTransport(TransportList updateRequest)
        {
            return Ok(_requestOperations.CancelTransportRequest(updateRequest, string.Concat(Variables.EServiceUrl, "/", Variables.SubsiteTransport), Variables.ListTransportName, token));
        }

        #endregion


        #region NAF

        /// <summary>
        /// Getting NAF Admin Credentials
        /// </summary>
        /// <returns>AdminDetails</returns>
        [Authorize]
        [HttpGet]
        public IHttpActionResult GetNAFAdminCredentials()
        {
            return Ok(_eServiceOperations.GetAdminDetails(token));
        }

        #endregion


        #endregion

    }
}