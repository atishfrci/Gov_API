using ONLINEAPP.HOME.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ONLINEAPP.HOME.INTERFACE
{
    public interface ITeamCenter
    {
        string GetPartDescriptionByPartNumber(string partNumber);

        bool ValidateABReport(string ABReport, string TestType);

        bool ValidatePartNumberAndRevisionID(string partNumber, string revisionID);

        string GetPartDescriptionByPartNumberAndRevisionID(string partNumber, string revisionID);

        List<ProjectDetails> GetProjects();

        List<TCItemRevision> GetItemRevisionByProjectCode(string projectCode);

        List<TCItemRevision> GetItemRevisionsByCreationDate(string startDate, string endDate);

        DateTime? GetSOPDateByItemID(string itemId);

        List<TCItemRevision> GetItemRevisionsByCreationDateAndProjectCode(string startDate, string endDate, string project);

        List<TCItemRevision> GetItemRevisionsBySingleCreationDateAndProjectCode(string date, string project);

        List<TCItemRevision> GetItemRevisionsByCreationDateWithTimeAndProjectCode(string startDate, string endDate, string project);

        List<TCItemRevision> GetItemRevisionsByModifiedDate(string modifiedDate);

        List<TCItemRevision> GetItemRevisionsByCreationDateAndEmptyProjectCode(string startDate, string endDate);

        List<TCItemRevision> GetItemRevisionsByModifiedDateAndProjectCode(string startDate, string endDate, string project);

        List<TCMFGComments> GetItemMfgCommentsByCreationDate(string startDate, string endDate);

        List<TCMFGComments> GetItemMfgCommentsByModifiedDate(string startDate, string endDate);

        TCItemComments GetItemCommentsByItemIDAndRevision(string itemId, string revision, string baselineName);

        List<TCMFGComments> GetItemMfgCommentsByCreationDateAndProjectCode(string startDate, string endDate, string project);

        List<ProblemReport> GetProblemReportByCreationDate(string startDate, string endDate);

        List<ProblemReport> GetProblemReportByModifiedDate(string startDate, string endDate);

        List<EmployeeInformation> GetEmployeeInformationByName(string name);
    }
}
