using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.MedicalRecords
{
    public class LabReportDTO
    {
        public string? PatName { get; set; }

        public int SerialHistory { get; set; }
        public string? PatSsn { get; set; }
        public DateTime? EntryDate { get; set; }
        public string? Trop { get; set; }
        public decimal? Ldh { get; set; }
        public decimal? Platelet { get; set; }
        public decimal? Bil { get; set; }
        public decimal? Ast { get; set; }
        public decimal? Ldl { get; set; }
        public decimal? Tg { get; set; }
        public decimal? CrBeforeAngioplasty { get; set; }
        public decimal? Urea { get; set; }
        public decimal? K { get; set; }
        public decimal? Mg { get; set; }
        public string? HcvAb { get; set; }
        public decimal? HcvA1c { get; set; }
        public string? Notes { get; set; }
        public decimal? Ckmb { get; set; }
        public decimal? Hb { get; set; }
        public decimal? Wbcs { get; set; }
        public decimal? Alt { get; set; }
        public decimal? Alb { get; set; }
        public decimal? Hdl { get; set; }
        public decimal? Cholesterol { get; set; }
        public decimal? Cr48HAfterAngioplasty { get; set; }
        public decimal? Inr { get; set; }
        public decimal? Na { get; set; }
        public decimal? Ca { get; set; }
        public string? Hbvsag { get; set; }
        public decimal? Tsh { get; set; }
        public int? PatId { get; set; }
        public int? Serial { get; set; }
    }
}
