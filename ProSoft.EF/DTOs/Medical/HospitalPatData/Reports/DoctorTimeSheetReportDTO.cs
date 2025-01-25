using ProSoft.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Medical.HospitalPatData.Reports
{
    public class DoctorTimeSheetReportDTO
    {
        public string? Day { get; set; }
        public string? ExPeriod { get; set; }
        public string? TimeFrom { get; set; }
        public string? TimeTo { get; set; }
    }
}
