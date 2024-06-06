using ProSoft.EF.DTOs.Accounts;
using ProSoft.EF.DTOs.Stocks;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Medical.HospitalPatData
{
    public class SubClinicEditAddDTO
    {
        [DisplayName("Code")]
        public int SClinicId { get; set; }

        [DisplayName("SubClinic Name")]
        public string SClinicDesc { get; set; }

        [DisplayName("Activation")]
        public int SOnOff { get; set; }

        [DisplayName("Service Type")]
        public int TypId { get; set; }

        [DisplayName("Cost Name")]
        public int CostCode { get; set; }

        public int? MedicalFlag { get; set; }

        [DisplayName("Tax Category")]
        public string? EinvItem { get; set; }

        [DisplayName("Level Invoice")]
        public string? SrvInvShowFlg { get; set; }

        [DisplayName("Stock Name")]
        public int Stkcod { get; set; }
        //list
        public List<StockViewDTO>? stocks { get; set; }
        public List<CostCenterViewDTO>?  costs { get; set; }
        public List<ServiceTypeViewDTO>?  servesTypes { get; set; }










    }
}
