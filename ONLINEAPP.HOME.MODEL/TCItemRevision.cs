using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ONLINEAPP.HOME.MODEL
{
    public class TCItemRevision
    {
        public string ItemId { get; set; }
        public string ItemRevision { get; set; }
        public string ItemMasters { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Object { get; set; }
        public string Owner { get; set; }
        public string OwningGroup { get; set; }
        public string ReleaseStatus { get; set; }
        public string BOMViewRevisions { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ReleasedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public string ObjectType { get; set; }
        public string AllWorkflow { get; set; }
        public string Design { get; set; }
        public string Category { get; set; }
        public string G_Weight { get; set; }
    }
}
