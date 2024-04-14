using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Medical.HospitalPatData
{
    public class ServiceClinicViewDTO
    {
        public int ServId { get; set; }
        public string ServDesc { get; set; }
        public int? ServOnOff { get; set; }
        public decimal? PlValue { get; set; }
        public string? CostDesc { get; set; }
        public int? ServType { get; set; }
        public decimal? DrPerc { get; set; }
        public decimal? DrVal { get; set; }
        public int? ProtectId { get; set; }





    }
}
