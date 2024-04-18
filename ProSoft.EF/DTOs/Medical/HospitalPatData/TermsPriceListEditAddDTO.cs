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
        [Required(ErrorMessage = "The field is required")]
        public int PLDetailCode { get; set; }

        [DisplayName("Level 1")]
        [Required(ErrorMessage = "The field is required")]
        public int? ClinicId { get; set; }

        [DisplayName("Level 2")]
        [Required(ErrorMessage = "The field is required")]
        public int? SClinicId { get; set; }

        [DisplayName("Level 3")]
        [Required(ErrorMessage = "The field is required")]
        public int ServId { get; set; }

        [DisplayName("Service Value")]
        [Required(ErrorMessage = "The field is required")]
        public decimal ServBefDesc { get; set; }

        [DisplayName("Service Discount Percentage")]
        [Required(ErrorMessage = "The field is required")]
        public decimal DiscoutComp { get; set; }

        [DisplayName("Service Value After Discount")]
        [Required(ErrorMessage = "The field is required")]
        public decimal PlValue { get; set; }

        [DisplayName("Company Percentage")]
        [Required(ErrorMessage = "The field is required")]
        public decimal CompCovPercentage { get; set; }

        [DisplayName("Company Cost Value")]
        [Required(ErrorMessage = "The field is required")]
        public decimal CompValue { get; set; }

        [DisplayName("Member Cost Value")]
        [Required(ErrorMessage = "The field is required")]
        public decimal PlValue2 { get; set; }

        [DisplayName("Realitve Cost Value")]
        [Required(ErrorMessage = "The field is required")]
        public decimal PlValue3 { get; set; }

        [DisplayName("Patient Cost Requirment")]
        [Required(ErrorMessage = "The field is required")]
        public decimal ExtraVal { get; set; }

        [DisplayName("Patient Cost Maintainace")]
        [Required(ErrorMessage = "The field is required")]
        public decimal ExtraVal2 { get; set; }

        [DisplayName("Cover Service")]
        [Required(ErrorMessage = "The field is required")]
        public int Covered { get; set; }

        [DisplayName("Activation")]
        [Required(ErrorMessage = "The field is required")]
        public int ServOnOff { get; set; }
        ////////////
        //Lists

        public List<MainClinicViewDTO>? MainClinics { get; set; }
        public List<SubClinicViewDTO>? SubClinics { get; set; }
        public List<ServiceClinicViewDTO>? ServiceClinics { get; set; }


    }
}
