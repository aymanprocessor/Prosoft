using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Medical.HospitalPatData
{
    public class TermsPriceListViewDTO
    {
        public int PLId { get; set; }
        public int PLDtlId { get; set; }
        public int? PLDetailCode { get; set; }
        public string ClinicDesc { get; set; }
        public string SClinicDesc { get; set; }
        public int ServId { get; set; }
        public string ServDesc { get; set; }
        public decimal? ServBefDesc { get; set; }
        public decimal? DiscoutComp { get; set; }
        public decimal? PlValue { get; set; }
        public decimal? CompCovPercentage { get; set; }
        public decimal? CompValue { get; set; }
        public decimal? PlValue2 { get; set; }
        public decimal? PlValue3 { get; set; }
        public decimal? ExtraVal { get; set; }
        public decimal? ExtraVal2 { get; set; }
        public int? Covered { get; set; }
        public int? ServOnOff { get; set; }

    }
}
