using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Medical.HospitalPatData
{
    public class SubClinicViewDTO
    {
        public int SClinicId { get; set; }
        public string SClinicDesc { get; set; }
        public int? SOnOff { get; set; }
        public string? TypDesc { get; set; }
        public string? CostDesc { get; set; }
        public int? MedicalFlag { get; set; }
        public string? EinvItem { get; set; }
        public string? SrvInvShowFlg { get; set; }
        public string? Stknam { get; set; }

    }
}
