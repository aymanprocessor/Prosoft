using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.MedicalRecords
{
    public class CoronaryDTO
    {
        public string? PatName { get; set; }
        public int SerialHistory { get; set; }
        public string? PatSsn { get; set; }
        public DateTime? EntryDate { get; set; }
        public string? FirstOperator { get; set; }
        public string? SecondOperator { get; set; }
        public string? CatheterForRca { get; set; }
        public string? CatheterForLeftCoronary { get; set; }
        public string? Access { get; set; }
        public string? AmountOfContrastMl { get; set; }
        public double? PreoprativeAssessment { get; set; }
        public string? LeftMain { get; set; }
        public string? Lad { get; set; }
        public string? Lcx { get; set; }
        public string? Rca { get; set; }
        public double? HematomaD { get; set; }
        public double? CinDiagnostic { get; set; }
        public double? PreoprativeAssessment1 { get; set; }
        public string? LeftMainCoronary { get; set; }
        public string? LeftAnteriorDescending { get; set; }
        public string? LeftCircumflex { get; set; }
        public string? RightCoronaryArtery { get; set; }
        public string? OtherVessels { get; set; }
        public string? Diagnosis { get; set; }
        public string? Recomendation { get; set; }
        public int? PatId { get; set; }
        public int? Serial { get; set; }
    }
}
