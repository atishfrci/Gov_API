using ONLINEAPP.HOME.INTERFACE;
using ONLINEAPP.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace ONLINEAPP.API.Controllers.Ahead
{
    public class TeamCenterController : ApiController
    {
        private ITeamCenter objITeamCenterOperations { get; set; }
        public TeamCenterController()
            : base()
        {
        }
        public TeamCenterController(ITeamCenter dep)
        {
            this.objITeamCenterOperations = dep;
        }

        string token = Convert.ToString(HttpContext.Current.Request.Headers["Authorization"]);

        [Authorize]
        [HttpGet]
        public IHttpActionResult GetPartDescriptionByPartNumber(string partNumber)
        {
            return Ok(objITeamCenterOperations.GetPartDescriptionByPartNumber(partNumber));
        }

        [Authorize]
        [HttpGet]
        public IHttpActionResult ValidateABReport(string ABReport, string TestType)
        {
            return Ok(objITeamCenterOperations.ValidateABReport(ABReport, TestType));
        }

        [Authorize]
        [HttpGet]
        public IHttpActionResult ValidatePartNumberAndRevisionID(string partNumber, string revisionID)
        {
            return Ok(objITeamCenterOperations.ValidatePartNumberAndRevisionID(partNumber, revisionID));
        }

        [Authorize]
        [HttpGet]
        public IHttpActionResult GetPartDescriptionByPartNumberAndRevisionID(string partNumber, string revisionID)
        {
            return Ok(objITeamCenterOperations.GetPartDescriptionByPartNumberAndRevisionID(partNumber, revisionID));
        }

        [Authorize]
        [HttpGet]
        public IHttpActionResult GetProjects()
        {
            return Ok(objITeamCenterOperations.GetProjects());
        }

        [Authorize]
        [HttpGet]
        public IHttpActionResult GetItemRevisionByProjectCode(string projectCode)
        {
            return Ok(objITeamCenterOperations.GetItemRevisionByProjectCode(projectCode));
        }

        [Authorize]
        [HttpGet]
        public IHttpActionResult GetSOPDateByItemID(string itemId)
        {
            return Ok(objITeamCenterOperations.GetSOPDateByItemID(itemId));
        }

        [Authorize]
        [HttpGet]
        //input date string should be in dd-MMM-yyyy format
        public IHttpActionResult GetItemRevisionsByCreationDate(string startDate, string endDate)
        {
            return Ok(objITeamCenterOperations.GetItemRevisionsByCreationDate(startDate, endDate));
        }

        [Authorize]
        [HttpGet]
        //input date string should be in dd-MMM-yyyy format
        public IHttpActionResult GetItemRevisionsByCreationDateAndProjectCode(string startDate, string endDate, string project)
        {
            return Ok(objITeamCenterOperations.GetItemRevisionsByCreationDateAndProjectCode(startDate, endDate, project));
        }

        [Authorize]
        [HttpGet]
        //input date string should be in dd-MMM-yyyy format
        public IHttpActionResult GetItemRevisionsBySingleCreationDateAndProjectCode(string date, string project)
        {
            return Ok(objITeamCenterOperations.GetItemRevisionsBySingleCreationDateAndProjectCode(date, project));
        }

        [Authorize]
        [HttpGet]
        //input date string should be in dd-MMM-yyyyhh:mm:ss format
        public IHttpActionResult GetItemRevisionsByCreationDateWithTimeAndProjectCode(string startDate, string endDate, string project)
        {
            return Ok(objITeamCenterOperations.GetItemRevisionsByCreationDateWithTimeAndProjectCode(startDate, endDate, project));
        }

        [Authorize]
        [HttpGet]
        //input date string should be in dd-MMM-yyyy format
        public IHttpActionResult GetItemRevisionsByModifiedDate(string modifiedDate)
        {
            return Ok(objITeamCenterOperations.GetItemRevisionsByModifiedDate(modifiedDate));
        }

        [Authorize]
        [HttpGet]
        //input date string should be in dd-MMM-yyyy format
        public IHttpActionResult GetItemRevisionsByCreationDateAndEmptyProjectCode(string startDate, string endDate)
        {
            return Ok(objITeamCenterOperations.GetItemRevisionsByCreationDateAndEmptyProjectCode(startDate, endDate));
        }

        [Authorize]
        [HttpGet]
        //input date string should be in dd-MMM-yyyy format
        public IHttpActionResult GetItemRevisionsByModifiedDateAndProjectCode(string startDate, string endDate, string project)
        {
            return Ok(objITeamCenterOperations.GetItemRevisionsByModifiedDateAndProjectCode(startDate, endDate, project));
        }

        [Authorize]
        [HttpGet]
        //input date string should be in dd-MMM-yyyy format
        public IHttpActionResult GetItemMfgCommentsByCreationDate(string startDate, string endDate)
        {
            return Ok(objITeamCenterOperations.GetItemMfgCommentsByCreationDate(startDate, endDate));
        }

        [Authorize]
        [HttpGet]
        //input date string should be in dd-MMM-yyyy format
        public IHttpActionResult GetItemMfgCommentsByModifiedDate(string startDate, string endDate)
        {
            return Ok(objITeamCenterOperations.GetItemMfgCommentsByModifiedDate(startDate, endDate));
        }

        [Authorize]
        [HttpGet]
        //input date string should be in dd-MMM-yyyy format
        public IHttpActionResult GetItemMfgCommentsByCreationDateAndProjectCode(string startDate, string endDate, string project)
        {
            return Ok(objITeamCenterOperations.GetItemMfgCommentsByCreationDateAndProjectCode(startDate, endDate, project));
        }

        [Authorize]
        [HttpGet]
        public IHttpActionResult GetItemCommentsByItemIDAndRevision(string itemId, string revision, string baselineName)
        {
            return Ok(objITeamCenterOperations.GetItemCommentsByItemIDAndRevision(itemId, revision, baselineName));
        }

        [Authorize]
        [HttpGet]
        //input date string should be in dd-MMM-yyyy format
        public IHttpActionResult GetProblemReportByCreationDate(string startDate, string endDate)
        {
            return Ok(objITeamCenterOperations.GetProblemReportByCreationDate(startDate, endDate));
        }

        [Authorize]
        [HttpGet]
        //input date string should be in dd-MMM-yyyy format
        public IHttpActionResult GetProblemReportByModifiedDate(string startDate, string endDate)
        {
            return Ok(objITeamCenterOperations.GetProblemReportByModifiedDate(startDate, endDate));
        }

        [Authorize]
        [HttpGet]
        //input date string should be in dd-MMM-yyyy format
        public IHttpActionResult GetEmployeeInformationByName(string name)
        {
            return Ok(objITeamCenterOperations.GetEmployeeInformationByName(name));
        }
    }
}