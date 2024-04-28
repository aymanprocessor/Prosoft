using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Medical.HospitalPatData
{
    public class CompanyDtlEditAddDTO
    {
        [DisplayName("Code")]
        [Required(ErrorMessage = "The field is required")]
        public int CompIdDtl { get; set; }

        public int CompId { get; set; }

        [DisplayName("Company Name")]
        [Required(ErrorMessage = "The field is required")]
        public string CompNameDtl { get; set; }

        [DisplayName("Tax Number")]
        [Required(ErrorMessage = "The field is required")]
        public string TaxNo { get; set; }

        [DisplayName("Sub Accounting Code")]
        [Required(ErrorMessage = "The field is required")]
        public string SubCode { get; set; }

        [DisplayName("Main Accounting Code")]
        [Required(ErrorMessage = "The field is required")]
        public string MainCode { get; set; }

        [DisplayName("Accounting Code")]
        [Required(ErrorMessage = "The field is required")]
        public int EinvType { get; set; }

        [DisplayName("Invoice on Level Items")]
        [Required(ErrorMessage = "The field is required")]
        public string WinvItemsFlg { get; set; }

        [DisplayName("Activation")]
        [Required(ErrorMessage = "The field is required")]
        public int CompIdDtlOnOff { get; set; }

    }
}
