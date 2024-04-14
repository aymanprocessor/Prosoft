using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Medical.HospitalPatData
{
    public class DoctorPrecentEditAddDTO
    {
        public int DrPercent { get; set; }
        public int DrCode { get; set; }
        public int MainCode { get; set; }
        public string SClinicDesc { get; set; }
        public string ServDesc { get; set; }
        public int? DrValFlg { get; set; }
        public decimal DrVal { get; set; }
        public int DrPerc { get; set; }
        public decimal ValueService { get; set; }
        public decimal DrValContract { get; set; }
        public decimal? DrPercContract { get; set; }
        public int SrvFlag { get; set; }
        //list
        public List<DoctorViewDTO>? Doctors { get; set; }
        public List<SubClinicViewDTO>? subClinics { get; set; }
        public List<ServiceClinicViewDTO>? services { get; set; }
    }
}
