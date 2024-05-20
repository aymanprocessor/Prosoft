using Microsoft.EntityFrameworkCore;
using ProSoft.EF.Models.Medical.HospitalPatData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.MedicalRecords
{
    public class DailyFollowUpDTO
    {
        public string? PatName { get; set; }
        public int SerialHistory { get; set; }
        public string? PatSsn { get; set; }
        public DateTime? EntryDate { get; set; }
        public string? CurrentCO { get; set; }
        public string? Blpr { get; set; }
        public string? ConsciousLevel { get; set; }
        public string? LowerLimbEdema { get; set; }
        public string? UnineOutPut { get; set; }
        public string? ChestCondition { get; set; }
        public string? Ecg { get; set; }
        public string? O2Sat { get; set; }
        public string? Temp { get; set; }
        public string? Rbs { get; set; }
        public string? InsulinGiven { get; set; }
        public string? Cnp { get; set; }
        public string? Infusions { get; set; }
        public string? UpdatesInMedication { get; set; }
        public string? InvestigasionsOrderd { get; set; } 
        public string? ConsultantVisitRecommendati { get; set; }
        public int? PatId { get; set; }
        public int? Serial { get; set; }
    }
}
