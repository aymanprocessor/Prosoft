using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Medical.HospitalPatData
{
    public class PatEditAddDTO
    {
        public int? PatId {  get; set; }
        [DisplayName("Patient SSN")]
        [RegularExpression("^[0-9]{14}$", ErrorMessage = "National Id must be 14 digit")]
        public double PatIdCard { get; set; }

        [DisplayName("Identity Type")]
        public double IdType { get; set; }

        [DisplayName("Patient Name")]
        public string PatName { get; set; }

        [DisplayName("Entry Number")]
        public double EntryNo { get; set; }

        [DisplayName("Entry Date")]
        public DateTime? EntryDate { get; set; }

        [DisplayName("Entry Time")]
        public DateTime? EntryTime { get; set; }

        [DisplayName("Birth Date")]
        public DateTime? BirthDate { get; set; }

        [DisplayName("Age In Years")]
        public double NewOld { get; set; }

        [DisplayName("Marital Status")]
        public int MaritalStatus { get; set; }

        [DisplayName("Gender")]
        public double PersonKind { get; set; }

        [DisplayName("Address")]
        public string PatAddress { get; set; }

        [DisplayName("Mobile number")]
        public string PatMobile { get; set; }

        [DisplayName("Job")]
        public string PatJob { get; set; }


        [DisplayName("Hospital")]
        public string? PatHospital { get; set; }

        [DisplayName("Department")]
        public string? PDep { get; set; }

        [DisplayName("Sector")]
        public string? PatSector { get; set; }
    }
}
