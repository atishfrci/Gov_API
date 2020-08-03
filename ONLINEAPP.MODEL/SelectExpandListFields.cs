using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ONLINEAPP.MODEL
{
    public class SelectExpandListFields
    {
        public static string BAL_RMS_UGP = "?$select=domainid/Id,domainid/UserName,emailid/Id,emailid/EMail,GLdomainid/Id,GLdomainid/Title,GLdomainid/UserName,GLdomainid/EMail,OData__x004c_1/Id,OData__x004c_1/Title,OData__x004c_1/EMail,User_x002e_/Id,User_x002e_/Title,User_x002e_/EMail,*&$expand=domainid,emailid,GLdomainid,OData__x004c_1,User_x002e_";
        public static string TR = "?$select=EncodedAbsUrl";
        public static string HolidayWorking = "?$select=AttachmentFiles,GL/Id,GL/Title,GL/EMail,OData__x004c_1/Id,OData__x004c_1/Title,OData__x004c_1/EMail,Plant_x0020_Head/Id,Plant_x0020_Head/Title,Plant_x0020_Head/EMail,Author/Id,Author/Title,RaisedBy/Id,RaisedBy/Title,RaisedBy/EMail,*&$expand=GL,OData__x004c_1,Plant_x0020_Head,Author,RaisedBy,AttachmentFiles";
        public static string FarmAdminUsers = "?$select=Admin/Id,Admin/Title,*&$expand=Admin";
        public static string RndUsers = "?$select=Reporting_x0020_To/Id,Reporting_x0020_To/Title,AheadUser/Id,AheadUser/Title,*&$expand=Reporting_x0020_To,AheadUser";
        public static string PlantHead = "?$select=PlantHead/Id,PlantHead/Title,*&$expand=PlantHead";
        public static string CashPurchaseNew = "?$select=AttachmentFiles,*&$expand=AttachmentFiles";
        public static string StoreMaterialReceived = "?$select=Requester/Id,Requester/Title,Author/Id,Author/Title,*&$expand=Requester,Author";
        public static string Admins = "?$select=Admin1/Id,Admin1/Title,Admin2/Id,Admin2/Title,Admin3/Id,Admin3/Title,Admin4/Id,Admin4/Title,*&$expand=Admin1,Admin2,Admin3,Admin4";
        public static string Approvers = "?$select=Approver/Id,Approver/Title,Approver/EMail,*&$expand=Approver";
        public static string Security = "?$select=SecurityUser/Id,SecurityUser/Title,SecurityUser/EMail,*&$expand=SecurityUser";
        public static string RMGP = "?$select=AttachmentFiles,RMGP_x0020_Domain_x0020_Approver/Id,RMGP_x0020_Domain_x0020_Approver/Title,GL/Id,GL/Title,OData__x004c_1/Id,OData__x004c_1/Title,Admin1/Id,Admin1/Title,Admin2/Id,Admin2/Title,Admin3/Id,Admin3/Title,Admin4/Id,Admin4/Title,Author/Id,Author/Title,RaisedBy/Id,RaisedBy/Title,*&$expand=RMGP_x0020_Domain_x0020_Approver,GL,OData__x004c_1,Admin1,Admin2,Admin3,Admin4,Author,RaisedBy,AttachmentFiles";
        public static string DomesticDispatchRequest = "?$select=AttachmentFiles,GL_x0020_Name/Id,GL_x0020_Name/Title,L1_x0020_Name/Id,L1_x0020_Name/Title,GM_x0020_Name/Id,GM_x0020_Name/Title,OData__x0050_M1/Id,OData__x0050_M1/Title,OData__x0050_M2/Id,OData__x0050_M2/Title,OData__x0050_M3/Id,OData__x0050_M3/Title,OData__x0050_M4/Id,OData__x0050_M4/Title,Author/Id,Author/Title,Editor/Id,Editor/Title,*&$expand=GL_x0020_Name,L1_x0020_Name,GM_x0020_Name,OData__x0050_M1,OData__x0050_M2,OData__x0050_M3,OData__x0050_M4,Author,Editor,AttachmentFiles";
        public static string ExportDispatchRequest = "?$select=AttachmentFiles,GL_x0020_Name/Id,GL_x0020_Name/Title,L1_x0020_Name/Id,L1_x0020_Name/Title,GM_x0020_Name/Id,GM_x0020_Name/Title,OData__x0050_M1/Id,OData__x0050_M1/Title,OData__x0050_M2/Id,OData__x0050_M2/Title,OData__x0050_M3/Id,OData__x0050_M3/Title,OData__x0050_M4/Id,OData__x0050_M4/Title,Author/Id,Author/Title,Editor/Id,Editor/Title,*&$expand=GL_x0020_Name,L1_x0020_Name,GM_x0020_Name,OData__x0050_M1,OData__x0050_M2,OData__x0050_M3,OData__x0050_M4,Author,Editor,AttachmentFiles";
        public static string RoadTestForm2Wh = "?$select=TR_x0020_Mark_x0020_No/Id,TR_x0020_Mark_x0020_No/Title,Author/Id,Author/Title,Editor/Id,Editor/Title,*&$expand=TR_x0020_Mark_x0020_No,Author,Editor";
        public static string RoadTestForm4Wh = "?$select=TR_x0020_Mark_x0020_No/Id,TR_x0020_Mark_x0020_No/Title,Author/Id,Author/Title,Editor/Id,Editor/Title,*&$expand=TR_x0020_Mark_x0020_No,Author,Editor";
        public static string GM = "?$select=GM/Id,GM/Title,GM/EMail,*&$expand=GM";
        public static string PM = "?$select=Brand_x0020_Owner/Id,Brand_x0020_Owner/Title,Brand_x0020_Owner/EMail,*&$expand=Brand_x0020_Owner";
        public static string DispatchGroup = "?$select=DispatchUsers/Id,DispatchUsers/Title,DispatchUsers/EMail,*&$expand=DispatchUsers";
        public static string OvertimeApprovals = "?$select=Group_x0020_Leader/Id,Group_x0020_Leader/Title,Group_x0020_Leader/EMail,OData__x0047_L2/Id,OData__x0047_L2/Title,OData__x0047_L2/EMail,Author/Id,Author/Title,Author/EMail,Editor/Id,Editor/Title,Editor/EMail,*&$expand=Group_x0020_Leader,OData__x0047_L2,Author,Editor";
        public static string Issues = "?$select=Assigned_x0020_To/Id,Assigned_x0020_To/Title,ITAdmin/Id,ITAdmin/Title,Logged_x0020_By/Id,Logged_x0020_By/Title,Logged_x0020_By/EMail,Author/Id,Author/Title,Author/EMail,Editor/Id,Editor/Title,Editor/EMail,*&$expand=Assigned_x0020_To,ITAdmin,Logged_x0020_By,Author,Editor";
        public static string IssuesReportView = "?$select=Assigned_x0020_To/Id,Assigned_x0020_To/Title,Group_x0020_Name,Status,Issue_x0020_Category,Created&$expand=Assigned_x0020_To";
        public static string OverAllReportView = "?$select=ID,Created";
        public static string BAL_VMS_CoreMaster_list = "?$select=AttachmentFiles,*&$expand=AttachmentFiles";
        public static string BAL_VMS_VehicleMaster_listViewModel = "?$select=ID,Title,ParentTitle,Brand,Status,Date,Group,Project,Batch_x0020_Type,Required_x0020_Date,Purpose,Comments,Returnable,Issued_x0020_Date,Return_x0020_Date,ParentId/ID&$expand=ParentId";
        public static string BAL_VMS_VehicleMaster_list = "?$select=ParentId/ID,AttachmentFiles,*&$expand=ParentId,AttachmentFiles";
        public static string VC = string.Concat("?$select=GL_x0020_Name/ID,GL_x0020_Name/Title,CD_x0020_Name/ID,CD_x0020_Name/Title,",
                                                "Vendor_x0020_Name/ID,Vendor_x0020_Name/Title,Vendor_x0020_Name2/ID,Vendor_x0020_Name2/Title,Vendor_x0020_Name3/ID,Vendor_x0020_Name3/Title,Vendor_x0020_Name4/ID,Vendor_x0020_Name4/Title,Vendor_x0020_Name5/ID,Vendor_x0020_Name5/Title,",
                                                "OData__x0063_d1/ID,OData__x0063_d1/Title,OData__x0063_d2/ID,OData__x0063_d2/Title,OData__x0063_d3/ID,OData__x0063_d3/Title,OData__x0063_d4/ID,OData__x0063_d4/Title,",
                                                "Notify1/ID,Notify1/Title,Notify2/ID,Notify2/Title,Notify3/ID,Notify3/Title,Notify4/ID,Notify4/Title,",
                                                "Author/ID,Author/Title,Editor/ID,Editor/Title,AttachmentFiles,*&",
                                                "$expand=GL_x0020_Name,CD_x0020_Name,Vendor_x0020_Name,Vendor_x0020_Name2,Vendor_x0020_Name3,Vendor_x0020_Name4,Vendor_x0020_Name5,",
                                                "OData__x0063_d1,OData__x0063_d2,OData__x0063_d3,OData__x0063_d4,",
                                                "Notify1,Notify2,Notify3,Notify4,",
                                                "Author,Editor,AttachmentFiles");
        public static string Test = "?$select=AttachmentFiles,*&$expand=AttachmentFiles";
        public static string BrandNotify = "?$select=Notify1/ID,Notify1/Title,Notify2/ID,Notify2/Title,Notify3/ID,Notify3/Title,Notify4/ID,Notify4/Title,Notify5/ID,Notify5/Title,Notify6/ID,Notify6/Title,Notify7/ID,Notify7/Title,Notify8/ID,Notify8/Title,Notify9/ID,Notify9/Title,*&$expand=Notify1,Notify2,Notify3,Notify4,Notify5,Notify6,Notify7,Notify8,Notify9";
        public static string VCUserGL = "?$select=User_x002e_/ID,User_x002e_/Title,GLdomainid/Id,GLdomainid/Title,GLdomainid/UserName,GLdomainid/EMail,*&$expand=User_x002e_,GLdomainid";
        public static string BAL_AGT_AnnualGetTogether = "?$select=Group_x0020_Lead/Id,Group_x0020_Lead/Title,ADMIN1/Id,ADMIN1/Title,ADMIN2/Id,ADMIN2/Title,ADMIN3/Id,ADMIN3/Title,*&$expand=Group_x0020_Lead,ADMIN1,ADMIN2,ADMIN3";
        public static string SPGroups = "?$select=User_x002e_/ID,User_x002e_/Title,GLdomainid/Id,GLdomainid/Title,GLdomainid/UserName,GLdomainid/EMail,*&$expand=User_x002e_,GLdomainid";
        public static string VendorDiscussion = "?$select=OData__x0043_D1/ID,OData__x0043_D1/Title,OData__x0043_D2/ID,OData__x0043_D2/Title,OData__x0043_D3/ID,OData__x0043_D3/Title,OData__x0043_D4/ID,OData__x0043_D4/Title,OData__x0043_D5/ID,OData__x0043_D5/Title, Vendor_x0020_CP1/ID,Vendor_x0020_CP1/Title,Vendor_x0020_CP2/ID,Vendor_x0020_CP2/Title,Vendor_x0020_CP3/ID,Vendor_x0020_CP3/Title,Vendor_x0020_CP4/ID,Vendor_x0020_CP4/Title,Vendor_x0020_CP5/ID,Vendor_x0020_CP5/Title,GL_x0020_Name/ID,GL_x0020_Name/Title,AttachmentFiles,*&$expand=OData__x0043_D1,OData__x0043_D2,OData__x0043_D3,OData__x0043_D4,OData__x0043_D5,Vendor_x0020_CP1,Vendor_x0020_CP2,Vendor_x0020_CP3,Vendor_x0020_CP4,Vendor_x0020_CP5,GL_x0020_Name,AttachmentFiles";
        public static string Vendor_Mapping = "?$select=OData__x0063_d1/ID,OData__x0063_d1/Title,ID,OData__x0063_d1/EMail,OData__x0063_d2/ID,OData__x0063_d2/Title,OData__x0063_d2/EMail,OData__x0063_d3/ID,OData__x0063_d3/Title,OData__x0063_d3/EMail,OData__x0063_d4/ID,OData__x0063_d4/Title,OData__x0063_d4/EMail,Vendor_CP2/ID,Vendor_CP2/Title,%20Vendor_CP3/ID,Vendor_CP3/Title,Vendor_CP4/ID,Vendor_CP4/Title,Vendor_CP1/ID,Vendor_CP1/Title,CD_x0020_Name/ID,CD_x0020_Name/Title,AttachmentFiles,*&$expand=OData__x0063_d1,OData__x0063_d2,OData__x0063_d3,OData__x0063_d4,Vendor_CP1,Vendor_CP2,Vendor_CP3,Vendor_CP4,CD_x0020_Name";
        public static string TeamDiscussion = "?$select=ParentItemEditor/ID,ParentItemEditor/Title,Author/Id,Author/Title,Editor/Id,Editor/Title,*&$expand=ParentItemEditor,Author,Editor";
        public static string BAL_KC_GroupMaster = "?$select=MainGroup/ID,MainGroup/Title,*&$expand=MainGroup";
        public static string BAL_KC_MainGroupMaster = "?$select=MainGroupLeader/ID,MainGroupLeader/EmployeeName,*&$expand=MainGroupLeader";
        public static string BAL_KC_EmployeeMaster = "?$select=Level/ID,Level/Level,Grade/ID,Grade/Grade,Designation/ID,Designation/Designation,Group/ID,Group/Group,ReportingManagerName/ID,ReportingManagerName/EmployeeName,ChartLevel/ID,ChartLevel/ChartLevel,MainGroup/ID,MainGroup/MainGroup,KPALevel/ID,KPALevel/Level,*&$expand=Level,Grade,Designation,Group,ReportingManagerName,ChartLevel,MainGroup,KPALevel";
        public static string BAL_KC_Ratings = "?$select=Level/ID,Level/Level,Group/ID,Group/Group,EmployeeName/ID,EmployeeName/EmployeeName,MainGroup/ID,MainGroup/MainGroup,KPALevel/ID,KPALevel/Level,*&$expand=Level,Group,EmployeeName,MainGroup,KPALevel";
        public static string DCMaster = "?$select=AttachmentFiles,CreatedBy1/ID,CreatedBy1/Title,ModifiedBy1/ID,ModifiedBy1/Title,*&$expand=AttachmentFiles,CreatedBy1,ModifiedBy1";
    }
}
