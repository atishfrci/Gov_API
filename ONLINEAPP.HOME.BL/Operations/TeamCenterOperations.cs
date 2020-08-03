using ONLINEAPP.DAL;
using ONLINEAPP.HOME.INTERFACE;
using ONLINEAPP.HOME.MODEL;
using ONLINEAPP.MODEL;
using ONLINEAPP.TEAMCENTER;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace ONLINEAPP.HOME.BL.Operations
{
    public class TeamCenterOperations : ITeamCenter
    {
        public string GetPartDescriptionByPartNumber(string partNumber)
        {
            string partDescription = string.Empty;

            try
            {
                return TCPartDetails.GetItemDescriptionByItemIDFromTeamCenter(partNumber);
            }
            catch (Exception ex)
            {
                string guid = CRUDOperations.WriteException(ex, MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.Name);
                throw new Exception(string.Format(Messages.MsgExceptionOccured, guid));
            }
        }

        public bool ValidateABReport(string ABReport, string TestType)
        {
            bool isValid = false;

            try
            {
                string itemid = string.Empty;
                string revisionID = string.Empty;

                if ((!(ABReport.Contains("*"))) && ((ABReport.Contains("/")) || (ABReport.Contains("\\"))))
                {
                    isValid = true;


                    if (ABReport.Contains("/"))
                    {
                        int a = (ABReport).IndexOf("/");

                        itemid = (ABReport).Substring(0, a);
                        a++;
                        revisionID = (ABReport).Substring(a);


                    }
                    else if (ABReport.Contains("\\"))
                    {
                        int a = (ABReport).IndexOf("\\");

                        itemid = (ABReport).Substring(0, a);
                        a++;
                        revisionID = (ABReport).Substring(a);

                    }
                }
                else
                {
                    isValid = false;
                }
                if (isValid)
                {
                    return TCPartDetails.ValidateABReportFromTeamCenter(itemid, revisionID, TestType);
                }
                //return TCPartDetails.ValidateABReportFromTeamCenter(ABReport, revisionID, TestType);

                return isValid;
            }
            catch (Exception ex)
            {
                string guid = CRUDOperations.WriteException(ex, MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.Name);
                throw new Exception(string.Format(Messages.MsgExceptionOccured, guid));
            }
        }

        public bool ValidatePartNumberAndRevisionID(string partNumber, string revisionID)
        {
            try
            {
                return TCPartDetails.ValidatePartNumberAndRevisionID(partNumber, revisionID);

            }
            catch (Exception ex)
            {
                string guid = CRUDOperations.WriteException(ex, MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.Name);
                throw new Exception(string.Format(Messages.MsgExceptionOccured, guid));
            }
        }

        public string GetPartDescriptionByPartNumberAndRevisionID(string partNumber, string revisionID)
        {
            try
            {
                return TCPartDetails.GetPartDescriptionByPartNumberAndRevisionID(partNumber, revisionID);

            }
            catch (Exception ex)
            {
                string guid = CRUDOperations.WriteException(ex, MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.Name);
                throw new Exception(string.Format(Messages.MsgExceptionOccured, guid));
            }
        }

        public List<ProjectDetails> GetProjects()
        {
            try
            {
                return TCPartDetails.GetAllActiveProjects();

            }
            catch (Exception ex)
            {
                string guid = CRUDOperations.WriteException(ex, MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.Name);
                throw new Exception(string.Format(Messages.MsgExceptionOccured, guid));
            }
        }

        public List<TCItemRevision> GetItemRevisionByProjectCode(string projectCode)
        {
            try
            {
                return TCPartDetails.GetItemRevisionByProjectCode(projectCode);

            }
            catch (Exception ex)
            {
                string guid = CRUDOperations.WriteException(ex, MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.Name);
                throw new Exception(string.Format(Messages.MsgExceptionOccured, guid));
            }
        }

        //input date string should be in dd-MMM-yyyy format
        public List<TCItemRevision> GetItemRevisionsByCreationDate(string startDate, string endDate)
        {
            try
            {
                return TCPartDetails.GetItemRevisionByCreationDate(startDate, endDate);

            }
            catch (Exception ex)
            {
                string guid = CRUDOperations.WriteException(ex, MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.Name);
                throw new Exception(string.Format(Messages.MsgExceptionOccured, guid));
            }
        }

        //input date string should be in dd-MMM-yyyy format
        public List<TCItemRevision> GetItemRevisionsByCreationDateAndProjectCode(string startDate, string endDate, string project)
        {
            try
            {
                return TCPartDetails.GetItemRevisionsByCreationDateAndProjectCode(startDate, endDate, project);

            }
            catch (Exception ex)
            {
                string guid = CRUDOperations.WriteException(ex, MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.Name);
                throw new Exception(string.Format(Messages.MsgExceptionOccured, guid));
            }
        }

        //input date string should be in dd-MMM-yyyy format
        public DateTime? GetSOPDateByItemID(string itemId)
        {
            try
            {
                return TCPartDetails.GetSOPDateByItemID(itemId);

            }
            catch (Exception ex)
            {
                string guid = CRUDOperations.WriteException(ex, MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.Name);
                throw new Exception(string.Format(Messages.MsgExceptionOccured, guid));
            }
        }

        //input date string should be in dd-MMM-yyyy format
        public List<TCItemRevision> GetItemRevisionsBySingleCreationDateAndProjectCode(string date, string project)
        {
            try
            {
                return TCPartDetails.GetItemRevisionsByCreationDateAndProjectCode(date, project);

            }
            catch (Exception ex)
            {
                string guid = CRUDOperations.WriteException(ex, MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.Name);
                throw new Exception(string.Format(Messages.MsgExceptionOccured, guid));
            }
        }

        //input date string should be in dd-MMM-yyyyhh:mm:ss format
        public List<TCItemRevision> GetItemRevisionsByCreationDateWithTimeAndProjectCode(string startDate, string endDate, string project)
        {
            try
            {
                return TCPartDetails.GetItemRevisionsByCreationDateWithTimeAndProjectCode(startDate, endDate, project);

            }
            catch (Exception ex)
            {
                string guid = CRUDOperations.WriteException(ex, MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.Name);
                throw new Exception(string.Format(Messages.MsgExceptionOccured, guid));
            }
        }

        //input date string should be in dd-MMM-yyyy format
        public List<TCItemRevision> GetItemRevisionsByModifiedDate(string modifiedDate)
        {
            try
            {
                return TCPartDetails.GetItemRevisionByModifiedDate(modifiedDate);

            }
            catch (Exception ex)
            {
                string guid = CRUDOperations.WriteException(ex, MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.Name);
                throw new Exception(string.Format(Messages.MsgExceptionOccured, guid));
            }
        }

        //input date string should be in dd-MMM-yyyy format
        public List<TCItemRevision> GetItemRevisionsByCreationDateAndEmptyProjectCode(string startDate, string endDate)
        {
            try
            {
                return TCPartDetails.GetItemRevisionsByCreationDateAndProjectCode(startDate, endDate, "\"\"");

            }
            catch (Exception ex)
            {
                string guid = CRUDOperations.WriteException(ex, MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.Name);
                throw new Exception(string.Format(Messages.MsgExceptionOccured, guid));
            }
        }

        //input date string should be in dd-MMM-yyyy format
        public List<TCItemRevision> GetItemRevisionsByModifiedDateAndProjectCode(string startDate, string endDate, string project)
        {
            try
            {
                return TCPartDetails.GetItemRevisionsByModiefiedDateAndProjectCode(startDate, endDate, project);

            }
            catch (Exception ex)
            {
                string guid = CRUDOperations.WriteException(ex, MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.Name);
                throw new Exception(string.Format(Messages.MsgExceptionOccured, guid));
            }
        }

        //input date string should be in dd-MMM-yyyy format
        public List<TCMFGComments> GetItemMfgCommentsByCreationDate(string startDate, string endDate)
        {
            try
            {
                return TCPartDetails.GetItemMfgCommentsByCreationDate(startDate, endDate);

            }
            catch (Exception ex)
            {
                string guid = CRUDOperations.WriteException(ex, MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.Name);
                throw new Exception(string.Format(Messages.MsgExceptionOccured, guid));
            }
        }

        //input date string should be in dd-MMM-yyyy format
        public List<TCMFGComments> GetItemMfgCommentsByModifiedDate(string startDate, string endDate)
        {
            try
            {
                return TCPartDetails.GetItemMfgCommentsByModifiedDate(startDate, endDate);

            }
            catch (Exception ex)
            {
                string guid = CRUDOperations.WriteException(ex, MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.Name);
                throw new Exception(string.Format(Messages.MsgExceptionOccured, guid));
            }
        }

        //input date string should be in dd-MMM-yyyy format
        public List<TCMFGComments> GetItemMfgCommentsByCreationDateAndProjectCode(string startDate, string endDate, string project)
        {
            try
            {
                return TCPartDetails.GetItemMfgCommentsByCreationDateAndProjectCode(startDate, endDate, project);

            }
            catch (Exception ex)
            {
                string guid = CRUDOperations.WriteException(ex, MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.Name);
                throw new Exception(string.Format(Messages.MsgExceptionOccured, guid));
            }
        }

        public TCItemComments GetItemCommentsByItemIDAndRevision(string itemId, string revision, string baselineName)
        {
            try
            {
                return TCPartDetails.GetItemCommentsByItemIDAndRevision(itemId, revision, baselineName);

            }
            catch (Exception ex)
            {
                string guid = CRUDOperations.WriteException(ex, MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.Name);
                throw new Exception(string.Format(Messages.MsgExceptionOccured, guid));
            }
        }

        //input date string should be in dd-MMM-yyyy format
        public List<ProblemReport> GetProblemReportByCreationDate(string startDate, string endDate)
        {
            try
            {
                return TCPartDetails.GetProblemReportByCreationDate(startDate, endDate);

            }
            catch (Exception ex)
            {
                string guid = CRUDOperations.WriteException(ex, MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.Name);
                throw new Exception(string.Format(Messages.MsgExceptionOccured, guid));
            }
        }

        //input date string should be in dd-MMM-yyyy format
        public List<ProblemReport> GetProblemReportByModifiedDate(string startDate, string endDate)
        {
            try
            {
                return TCPartDetails.GetProblemReportByModifiedDate(startDate, endDate);

            }
            catch (Exception ex)
            {
                string guid = CRUDOperations.WriteException(ex, MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.Name);
                throw new Exception(string.Format(Messages.MsgExceptionOccured, guid));
            }
        }

        public List<EmployeeInformation> GetEmployeeInformationByName(string name)
        {
            try
            {
                return TCPartDetails.GetEmployeeInformationByName(name);

            }
            catch (Exception ex)
            {
                string guid = CRUDOperations.WriteException(ex, MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.Name);
                throw new Exception(string.Format(Messages.MsgExceptionOccured, guid));
            }
        }
    }
}
