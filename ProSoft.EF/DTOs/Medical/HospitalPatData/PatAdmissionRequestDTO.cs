using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Medical.HospitalPatData
{
    public class PatAdmissionRequestDTO
    {
        public int? BranchId { get; set; } = 1;
        public int? ExYear { get; set; } = 2025;

        public string? PatName { get; set; }
        public int? patId { get; set; }
        public int? MasterId { get; set; }
        public DateTime PatAdDate { get; set; } = DateTime.Now;
        public decimal Deal { get; set; }
        public int CompId { get; set; }
        public int CompIdDtl { get; set; }
        public int BrnachInitial { get; set; }
        public int SendFr { get; set; }
        public int DrCode { get; set; }
        public DateTime? PatDateExit { get; set; }
        public int SendTo { get; set; }
        public decimal PatientValue { get; set; }
        public decimal Prepaid { get; set; }
        public int MainInvNo { get; set; }
        public int SessionNo { get; set; }
    }
}
