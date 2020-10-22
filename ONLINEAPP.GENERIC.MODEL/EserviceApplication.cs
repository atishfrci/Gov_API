using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace ONLINEAPP.GENERIC.MODEL
{
    /// <summary>
    /// Eservices Application
    /// </summary>
    public class EserviceApplication
    {
        public int RequestId { get; set; }

        public string Username { get; set; }

        public string Title { get; set; }

        public string Surname { get; set; }

        public string OtherName { get; set; }

        public string MaidenName { get; set; }

        public string CitizenOfMauritius { get; set; }

        public string NIC { get; set; }

        public string Country { get; set; }

        public string PassportNumber { get; set; }

        public DateTime PassportDateOfIssue { get; set; }

        public DateTime PassportDateOfExpiry { get; set; }

        public string OccupationalPermitNumber { get; set; }

        public DateTime OccupationalPermitDateOfIssue { get; set; }

        public DateTime OccupationalPermitDateOfExpiry { get; set; }

        public DateTime DateofBirth{ get; set; } 

        public int Age { get; set; }

        public string Address { get; set; }

        public int PhoneNumber { get; set;}

        
        public string PlaceOfBirth { get; set; }

        public string EmailAddress { get; set; }

        public string TypeOfLicense { get; set; }

        public DateTime CreatedDate { get; set; }

        public string NameOfMinistry { get; set; }

        public string ClassOfMotorVehicle { get; set; }

        public string Others { get; set; }

        public string LicenseNumber { get; set; }

        public DateTime LicenseDAteOfIssue { get; set; }

        public DateTime DatePassedTest { get; set; }

        public DateTime DateForPhysicalAppointment { get; set; }

        public string TimeSlot { get; set; }
    }
}
