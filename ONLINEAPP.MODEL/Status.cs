using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ONLINEAPP.MODEL
{
    public class Status
    {
        public const string Approved = "Approved";
        public const string Rejected = "Rejected";
        public const string Pending = "Pending";
        public const string RequestClosed = "Request Closed";
        public const string Closed = "Closed";
        public const string Started = "Started";
        public const string Suspended = "Suspended";
        public const string Abort = "Abort";
        public const string Demote = "Demote";
        public const string Promote = "Promote";
        public const string NeedInfo = "Need Info";
        public const string RequestCreated = "Request Created";
        public const string Created = "Created";
        public const string Updated = "Updated";
        public const string Cancelled = "Cancelled";
        public const string ApprovedByITTeam = "Approved by IT Team";
        public const string RejectedByITTeam = "Rejected by IT Team";
        public const string PendingGLApprovalNumOne = "1. Pending for GL Approval";
        public const string PendingPlantApprovalNumTwo = "2. Pending for Plant Approval";
        public const string ApprovedNumThree = "3. Approved";
        public const string RejectedByGLNumFour = "4. Rejected by GL";
        public const string RejectedByGLNumFive = "5. Rejected by GL";
        public const string RejectedByPlantHeadNumFive = "5. Rejected by Plant Head";
        public const string PendingNumOne = "1. Pending";
        public const string ApprovedNumTwo = "2. Approved";
        public const string RejectedNumThree = "3. Rejected";

        public const string Active = "Active";
        public const string InActive = "InActive";

        public const string Yes = "1";
        public const string No = "0";

        public const string Obsolete = "Obsolete";
        public const string Open = "Open";
        public const string RequestRejected = "Request Rejected";
        public const string PRRaised = "PR Raised";
        public const string MaterialReceived = "Material Received";
        public const string MaterialPartiallyReceived = "Material Partially Received";
        public const string MaterialUnavailable = "Material Unavailable";
        public const string RequestDropped = "Request Dropped";
        public const string MaterialAvailableInRnD = "Material Available in R&D";

        public const string PendingForApproval = "Pending for Approval";
        public const string PendingForMaterialOut = "Pending for Material Out";
        public const string PendingForMaterialIn = "Pending for Material In";
        public const string PendingForClosure = "Pending for Requester Closure";
        public const string CancelledByRequester = "Cancelled by Requester";


        #region ** DC Status (RMGP new)
        public const string dcPendingWithStores = "Pending with Stores";
        public const string dcPendingForApproval = "Pending for Approval";
        public const string dcPendingForMaterialOut = "Pending for Material Out";
        public const string dcPendingForMaterialIn = "Pending for Material In";
        public const string dcRejectedbyL1 = "Rejected by L1";
        public const string dcClosed = "Closed";
        public const string dcCancelled = "Cancelled";
        //public const string dcCancelledByRequester = "Cancelled by Requester";
        public const string dcPendingForClosure = "Pending for Requester Closure";
        public const string dcPendingForReview = "Pending for Review";
        #endregion


        public const string PendingWithGLOne = "1. Pending with GL";
        public const string PendingWithStoresTwo = "2. Pending with Stores";
        public const string DispatchedThree = "3. Dispatched";
        public const string DispatchedFour = "4. Dispatched";
        public const string DispatchedPartiallyFive = "5. Dispatched Partially";
        public const string DispatchedPartiallySeven = "7. Dispatched Partially";
        public const string PendingWithRequesterSix = "6. Pending with Requester";
        public const string PendingWithRequesterEight = "8. Pending with Requester";
        public const string PendingWithGMSeven = "7. Pending with GM/VP";
        public const string PendingWithGMThree = "3. Pending with GM/VP";
        public const string RejectedByGMEight = "8. Rejected by GM/VP";
        public const string RejectedByGMSix = "6. Rejected by GM/VP";
        public const string PendingWithSAPTeamNine = "9. Pending with SAP Team";
        public const string PendingForMaterialInTen = "10. Pending for Material In";
        public const string MaterialReceivedRequestClosedEleven = "11. Material Received-Request Closed";
        public const string ReviewRequestTwelve = "12. Review Request";
        public const string CancelRequestThirteen = "13. Cancel Request";
        public const string ContinueRequestFourteen = "14. Continue Request";
        public const string GMApproved = "GMApproved";
        public const string GMRejected = "GMRejected";
        public const string NAFour = "4. NA";
        public const string PendingWithL1Twelve = "12. Pending with L1";
        public const string RejectedByL1Thirteen = "13. Rejected by L1";

        public const string InformationNeededfromRequester = "1. Information Needed from Requester";
        public const string InformationUpdatedbyRequester = "2. Information Updated by Requester";
        public const string NotApplicable = "3. Not Applicable";
        public const string NotCharged = "2. Not Charged";

        public const string Received = "Received";
        public const string Unavailable = "Unavailable";


        public const string WorkflowNotInitiated = "Workflow not initiated";
        public const string InProgress = "InProgress";

        //*****************VMS Status***********************

        public const string VehicleIssued = "Vehicle Issued";
        public const string PendingWithStores = "Pending with Stores";
        public const string PendingForCapitalizationApproval = "Pending for Capitalization Approval";
        public const string RequestedFromPlant = "Requested from Plant";
        public const string VehicleReceivedByStores = "Vehicle received by Stores";
        public const string VehiclesToBeReturnedIn15Days = "Vehicles To Be Returned In 15 Days";
        public const string VehiclesOverdue = "Vehicles Overdue";
        public const string VehiclesToBeCapitalized = "Vehicles To Be Capitalized";
        public const string VehiclesConvertedToCapitalized = "Vehicles Converted To Capitalized";
        public const string ApprovedForCapitalizationApproval = "Approved for Capitalization Approval";
        public const string RejectedForCapitalizationApproval = "Rejected by Capitalization Approver";
        public const string PendingGLApproval = "Pending GL Approval";
        public const string PendingForCapitalizationFinanceApproval = "Pending for Capitalization Finance Approval";
        public const string ApprovedForCapitalizationFinanceApproval = "Approved for Capitalization Finance Approval";
        public const string ApprovedByCapitalizationFinanceApprover = "Approved by Capitalization Finance Approver";
        public const string VehiclePendingForScrapping = "Vehicle Pending for Scrapping";
        public const string VehicleReturnedFromDesigner = "Vehicle returned from designer";
        public const string RequestCancelled = "Request Cancelled";
        public const string RejectedRequestsByGL = "Rejected Requests by GL";
        public const string PendingWithPurchase = "Pending With Purchase";
        public const string PendingForReview = "Pending for Review";
        public const string VehicleReturnedToPlant = "Vehicle returned to Plant";
        public const string AssetTransferredToPlant = "Asset Transferred to Plant";
        public const string VehicleScrapped = "Vehicle Scrapped";
        public const string VehicleOrdered = "Vehicle Ordered";
    }


}
