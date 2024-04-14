using ProSoft.EF.DTOs.Stocks;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Medical.HospitalPatData
{
    public class ServClinicEditAddDTO
    {
        [DisplayName("Code")]
        public int ServId { get; set; }

        [DisplayName("ServClinc Name")]
        public string? ServDesc { get; set; }

        [DisplayName("Activation")]
        public int? ServOnOff { get; set; }
        [DisplayName("Service Value")]
        public decimal? PlValue { get; set; }

        [DisplayName("Cost Name")]
        public int CostCode { get; set; }

        [DisplayName("Service Type")]
        public int? ServType { get; set; }

        [DisplayName("Dr Ratio")]
        public decimal? DrPerc { get; set; }
         
        //ممكن محتاجهاش
       // public decimal? DrVal { get; set; }
       
        [DisplayName("Protect")]
        public int? ProtectId { get; set; }
        //list
        public List<CostCenterViewDTO>? costs { get; set; }
    }
}
