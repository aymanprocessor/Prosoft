using System;
using System.Linq;

namespace ProSoft.EF.DTOs.Medical.HospitalPatData
{
    public class PatRequestDTO
    {
        public double PatIdCard { get; set; }

        public double IdType { get; set; }

        public string PatName { get; set; }

        public double EntryNo { get; set; }

        public DateTime? EntryDate { get; set; }

        public DateTime? EntryTime { get; set; }

        public DateTime? BirthDate { get; set; }

        public double NewOld { get; set; }

        public int MaritalStatus { get; set; }

        public double PersonKind { get; set; }

        public string PatAddress { get; set; }

        public string PatMobile { get; set; }

        public string PatJob { get; set; }


        public string? PatHospital { get; set; }

        public string? PDep { get; set; }

        public string? PatSector { get; set; }
    }
}
