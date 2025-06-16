using ProSoft.EF.Models.Medical.HospitalPatData;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Medical.HospitalPatData
{
    public class TermsPriceListEditAddDTO
    {
        public int PLDtlId { get; set; }
        public int? PLId { get; set; }

        [DisplayName("Sort")]
        public int PLDetailCode { get; set; }

        [DisplayName("Level 1")]
        public int? ClinicId { get; set; }

        [DisplayName("Level 2")]
        public int? SClinicId { get; set; }

        [DisplayName("Level 3")]
        public int ServId { get; set; }

        [DisplayName("Service Value")]
        public decimal ServBefDesc { get; set; }

        [DisplayName("Service Discount Percentage")]
        public decimal DiscoutComp { get; set; }

        [DisplayName("Service Value After Discount")]
        public decimal PlValue { get; set; }

        [DisplayName("Company Percentage")]
        public decimal CompCovPercentage { get; set; }

        [DisplayName("Company Cost Value")]
        public decimal CompValue { get; set; }

        [DisplayName("Member Cost Value")]
        public decimal PlValue2 { get; set; }

        [DisplayName("Realitve Cost Value")]
        public decimal PlValue3 { get; set; }

        [DisplayName("Patient Cost Requirment")]
        public decimal ExtraVal { get; set; }

        [DisplayName("Patient Cost Maintainace")]
        public decimal ExtraVal2 { get; set; }

        [DisplayName("Cover Service")]
        public int Covered { get; set; }

        [DisplayName("Activation")]
        public int ServOnOff { get; set; }
        ////////////
        //Lists

        //public List<MainClinicViewDTO>? MainClinics { get; set; }
        //public List<SubClinicViewDTO>? SubClinics { get; set; }
        //public List<ServiceClinicViewDTO>? ServiceClinics { get; set; }


    }
}
