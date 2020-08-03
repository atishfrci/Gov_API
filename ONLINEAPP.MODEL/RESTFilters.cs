using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ONLINEAPP.MODEL
{
    public class RESTFilters
    {
        public const string topItems = "&$top={0}";
        public const string orderByDescending = "&$orderby={0} desc";
        public const string orderByAscending = "&$orderby={0}";
        public const string ByID = "&$filter=ID eq '{0}'";
        public const string ByNIC = "&$filter=(NIC eq '{0}') and (Status ne  'Expired' and Status ne 'Surrendered' and Status ne 'Cancelled') and (IncludePayment eq 'Yes') and (ApplicantType ne 'Registered Companies')";
        public const string ByBRN = "&$filter=(RegisteredCoNumber eq '{0}') and (Status ne  'Expired' and Status ne 'Surrendered' and Status ne 'Cancelled') and (IncludePayment eq 'Yes')";
        public const string ByGroupName = "&$filter=Group eq '{0}'";
        public const string ByGroupAndSubGroupName = "&$filter=Group eq '{0}' and SubGroup eq '{1}'";
        public const string ByTestType = "&$filter=TestType eq '{0}'";
        public const string ContainsByTitle = "&$filter=substringof('{0}',Title)";
        public const string ContainsByEMail = "&$filter=substringof('{0}',EMail)";
        public const string ContainsByTitleOrEmail = "&$filter=substringof('{0}',Title) or substringof('{0}',EMail)";
        public const string ContainsByUserName = "&$filter=substringof('{0}',UserName)";
        public const string ContainsByTitleOrPersonNameAndActive = "&$filter=(substringof('{0}',Title) or substringof('{0}',Person_x0020_Name)) and Status eq 'Active'";
        public const string ContainsByTitleOrUserName = "&$filter=(substringof('{0}',Title) or substringof('{0}',UserName))";
        public const string ContainsByPersonName = "&$filter=substringof('{0}',Person_x0020_Name)";
        public const string ByTitle = "&$filter=Title eq '{0}'";
        public const string ByGLName = "&$filter=Group_x0020_Leader eq '{0}'";
        public const string ByUserName = "&$filter=UserName eq '{0}'";
        public const string ByCreatedDateRange = "&$filter=Created ge datetime'{0}' and Created le datetime'{1}'";
        public const string ByFromAndToDate = "&$filter=TestCreated ge datetime'{0}' and TestCreated le datetime'{1}'";
        public const string ByStatus = "&$filter=Status eq '{0}'";
        public const string ByStatusAndYear = "&$filter=Status eq '{0}' and Title eq '{1}'";
        public const string ContainsByTitleAndStatus = "&$filter=substringof('{0}',Title) and substringof('{1}',Status)";
        public const string ByIsActive = "&$filter=IsActive eq '{0}'";
        public const string ByRegistrationMark = "&$filter=RegistrationMark eq '{0}'";
        public const string ByContainPrefixAndRegistrationMark = "&$filter=substringof('{0}',RegistrationMark) and RegistrationMark eq '{1}'";
        public const string ByStatusNotExpired = "&$filter=Status eq '{0}' or Status eq '{1}'";
        public const string ByStatusNotExpiredAndContainRegMarkPrefix = "&$filter=substringof('{0}',RegistrationMark) and (Status ne '{1}' or Status ne '{2}' or Status ne '{3}')";
        public const string ByContainRegMarkPrefix = "&$filter=substringof('{0}',RegistrationMark)";
        public const string ByRegistrationMarkAndStatusNotExpired = "&$filter=RegistrationMark eq '{0}'and (Status eq '{1}' or Status eq '{2}')";
        public const string ByContainPrefixRegistrationMarkAndStatusNotExpired = "&$filter=substringof('{0}',RegistrationMark) and RegistrationMark eq '{1}'and (Status eq '{2}' or Status eq '{3}')";
        public const string ByStartWithEndWithAndStatusNotExpired = "&$filter=(RegistrationMark ge '{0}') and (RegistrationMark le '{1}') and (Status ne '{2}' or Status ne '{3}' or Status ne '{4}')";
        public const string ByMVDCode = "&$filter=Code eq '{0}'";

    }
}
