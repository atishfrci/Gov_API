using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ONLINEAPP.MODEL
{
    public class Fields
    {
        public const string ID = "ID";
        public const string Title = "Title";
        public const string Name = "Name";
        public const string Group = "Group";
        public const string SubGroup = "SubGroup";
        public const string TestType = "TestType";
        public const string TestSubType = "TestSubType";
        public const string Model = "Model";
        public const string Project = "Project";
        public const string Email = "Email";
        public const string Created = "Created";
        public const string PlantHead = "PlantHead/Title";
        public const string CPCreatedBy = "CPCreatedBy";
        public const string GL = "GL";
        public const string BINNO = "BINNO";
        public const string LogDate = "LogDate";
        public const string PurposeDescription = "PurposeDescription";
        public const string PlantName = "Plant_x0020_name";
        public const string MainGroup = "MainGroup";
        public const string Modified = "Modified";
        public const string Sequence = "Sequence";
    }

    public class FieldsList
    {
       
        public string Title { get; set; }
        public string VisibleInListView { get; set; }
        public string InternalName { get; set; }
       
    }
}
