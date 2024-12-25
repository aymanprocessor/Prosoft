using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Medical.HospitalPatData
{
    public class DrDiscountViewDTO
    {
        public int? Id { get; set; }
        public int? DrId { get; set; }
        public string? DoctorName { get; set; }
        public decimal? DrPercentage { get; set; }
        public decimal? DrPercentageContract { get; set; }
        public int? FlagDisc { get; set; }
    }
}
