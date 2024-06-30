using ProSoft.EF.DTOs.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Medical.HospitalPatData
{
    public class ReportDoctorFeesDTO
    {
        public List<DoctorViewDTO>? Doctors { get; set; }
    }
}
