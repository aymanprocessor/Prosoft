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
        public int CompId { get; set; }
        public string CompName { get; set; }
        public string CompNameDtl { get; set; }
        public string ClassificationDesc { get; set; }
        public string RegionDesc { get; set; }
        public string DrDesc { get; set; }
    }
}
