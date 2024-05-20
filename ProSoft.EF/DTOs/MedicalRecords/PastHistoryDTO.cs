using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.MedicalRecords
{
    public class PastHistoryDTO
    {
        public string? PatName { get; set; }
        public int SerialHistory { get; set; }
        public string? PatSsn { get; set; }
        public DateTime? EntryDate { get; set; }
        public double? Diabetes { get; set; }
        public double? Htn { get; set; }
        public double? Somker { get; set; }
        public double? Csa { get; set; }
        public double? PriorCabg { get; set; }
        public double? PriorPci { get; set; }
        public double? PriorMi { get; set; }
        public double? Hfref { get; set; }
        public double? Hfpef { get; set; }
        public double? IschemicStroke { get; set; }
        public double? HemorthagicStroke { get; set; }
        public double? Tia { get; set; }
        public double? Dyslipidemia { get; set; }
        public double? ChronicAf { get; set; }
        public double? ProxysmaiAf { get; set; }
        public double? MalignantTumors { get; set; }
        public double? OsApnea { get; set; }
        public double? CkdOnMedical { get; set; }
        public double? CkdOnDialysis { get; set; }
        public double? IvAddict { get; set; }
        public double? HashishAddict { get; set; }
        public double? TramadolAddict { get; set; }
        public double? Pregnant { get; set; }
        public double? RheumaticHeatDisease { get; set; }
        public double? Hyperthyroidism { get; set; }
        public double? Hypothyroidism { get; set; }
        public double? ProstheticValve { get; set; }
        public int? PatId { get; set; }
        public int? Serial { get; set; }
    }
}
