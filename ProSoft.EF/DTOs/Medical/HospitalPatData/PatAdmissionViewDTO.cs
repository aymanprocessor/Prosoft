using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Medical.HospitalPatData
{
    public class PatAdmissionViewDTO
    {
        public int MasterId { get; set; }
        public int PatId { get; set; }
        public string? PatName { get; set; }
        public DateTime? PatAdDate { get; set; }
        
        public decimal? Deal { get; set; }
        public int CompId { get; set; }
        public int? CompIdDtl { get; set; }
        public int? BrnachInitial { get; set; }
        public string CompName { get; set; }
        public string CompNameDtl { get; set; }
        public string ClassificationDesc { get; set; }
        public string RegionDesc { get; set; }
        public string DrDesc { get; set; }
        public decimal? PatientValue { get; set; }
        public decimal? CompValue { get; set; }
        public decimal? Prepaid { get; set; }
        public int? MainInvNo { get; set; }
        public int? SessionNo { get; set; }

        public int? SendFr { get; set; }
        public int? DrCode { get; set; }
        public DateTime? PatDateExit { get; set; }
    }
}
