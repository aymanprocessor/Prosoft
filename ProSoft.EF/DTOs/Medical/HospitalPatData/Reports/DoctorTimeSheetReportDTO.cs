using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Medical.HospitalPatData.Reports
{
    public class DoctorTimeSheetReportDTO
    {
        public int? Day { get; set; }
        public int? ExPeriod { get; set; }
        public string? TimeFrom { get; set; }
        public string? TimeTo { get; set; }
    }
}
