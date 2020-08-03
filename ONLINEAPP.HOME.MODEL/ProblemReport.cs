using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ONLINEAPP.HOME.MODEL
{
    public class ProblemReport
    {
        public string PRNumber { get; set; }
        public string PartNumber { get; set; }
        public string PRType { get; set; }
        public string Synopsis { get; set; }
        public string PRBrand { get; set; }
        public string System { get; set; }
        public string SubSystem { get; set; }
        public string DevelopmentStage { get; set; }
        public string IssueSubType { get; set; }
        public string ModelsApplicable { get; set; }
        public string ClosureComments { get; set; }
        public string DesignActionRequired { get; set; }
        public string GroupID { get; set; }
        public string Owner { get; set; }
        public string CategoryOfComponent { get; set; }
        public string ProposalType { get; set; }
        public string CurrentDetails { get; set; }
        public string ProposalTheme { get; set; }
        public string ProposedDetails { get; set; }
        public string ProposedEvaluation { get; set; }
        public string CurrentCostPerVehicle { get; set; }
        public string SavingsPerVehicle { get; set; }
        public string AnnualCostSaving { get; set; }
        public string TotalSavings { get; set; }
        public string SavingShared { get; set; }
        public string ActualSavings { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public Nullable<System.DateTime> CommentsDate { get; set; }
        public string OpenWithDept { get; set; }
        public string ReleaseStatus { get; set; }
        
    }
}
