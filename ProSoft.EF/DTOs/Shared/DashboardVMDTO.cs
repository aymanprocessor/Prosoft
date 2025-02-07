using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Shared
{
    public class DashboardVMDTO
    {
        public int? PatientCounts { get; set; }
        public int? PatientCountsDaily { get; set; }
        public int? PatientCountsWeekly { get; set; }
        public int? ClinicTransCounts { get; set; }
        public int? ClinicTransCountsDaily { get; set; }
        public int? ClinicTransCountsWeekly { get; set; }
    }
}
