using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Medical.HospitalPatData
{
    public class AnalysisDtlEditDTO
    {
        public DateTime Headdate { get; set; }
        public double PatId { get; set; }
        public string Codeanalcode { get; set; }     
        public int Itemanalcode { get; set; }   
        public string Itemanalname { get; set; }
        public string Itemrate { get; set; }
        public long MasterId { get; set; }
        public double BranchId { get; set; }
        public string MainCode { get; set; }
        public double FYear { get; set; }
        public string SubCode { get; set; }
    }
}
