using ProSoft.EF.DTOs.Calculus;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Stocks
{
    public class SupCodeEditAddDTO
    {
        [DisplayName("The Code")]
        public int Sup { get; set; }
        public string SupCode1 { get; set; }

        [DisplayName("Company Name")]
        [Required(ErrorMessage = "The field is required")]
        public string SupName { get; set; }

        [DisplayName("Address")]
        [Required(ErrorMessage = "The field is required")]
        public string SupAdd { get; set; }
        
        [DisplayName("Telephone Number")]
        [Required(ErrorMessage = "The field is required")]
        public string SupPhone1 { get; set; }
        
        [DisplayName("Mobile number")]
        [Required(ErrorMessage = "The field is required")]
        public string SupPhone2 { get; set; }

        [DisplayName("Fax Number")]
        [Required(ErrorMessage = "The field is required")]
        public string SupFax { get; set; }

        [DisplayName("Notes")]
        [Required(ErrorMessage = "The field is required")]
        public string Remarks { get; set; }
        public decimal ValDept { get; set; }
        public decimal ValCredit { get; set; }
        
        [DisplayName("Code in Calculus")]
        [Required(ErrorMessage = "The field is required")]
        public string SubCode { get; set; }
        public string? MainCode { get; set; }
        
        [DisplayName("The Email")]
        [Required(ErrorMessage = "The field is required")]
        public string Email { get; set; }
        
        [DisplayName("The Governerate")]
        [Required(ErrorMessage = "The field is required")]
        public int CityCode { get; set; }

        [DisplayName("The City")]
        [Required(ErrorMessage = "The field is required")]
        public int AreaCode { get; set; }
        public int BranchId { get; set; }

        [DisplayName("Person Name")]
        [Required(ErrorMessage = "The field is required")]
        public string PersonName { get; set; }
        public short SupType { get; set; }
        public int Replcate { get; set; }
        public string BrReplc { get; set; }

        public List<AccSubCodeDTO>? SubCodes { get; set; }
        public List<CityCodeDTO>? Cities { get; set; }
        public List<PlaceCodeDTO>? Places { get; set; }
    }
}
