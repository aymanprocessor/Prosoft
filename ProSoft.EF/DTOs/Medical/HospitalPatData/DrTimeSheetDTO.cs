using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Medical.HospitalPatData
{
    public class DrTimeSheetDTO
    {
        public int? DrTimId { get; set; }
        public int? BranchId { get; set; }
        public int? DrId { get; set; }

        [DisplayName("Day")]
        [Required(ErrorMessage = "The field is required")]
        public int? DayNumber { get; set; }

        [DisplayName("Period")]
        [Required(ErrorMessage = "The field is required")]
        public int? ExPeriod { get; set; }

        [DisplayName("From")]
        [Required(ErrorMessage = "The field is required")]
        public DateTime? Timfrom { get; set; }

        [DisplayName("To")]
        [Required(ErrorMessage = "The field is required")]
        public DateTime? Timto { get; set; }

    }
}
