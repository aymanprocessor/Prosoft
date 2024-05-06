using ProSoft.EF.DTOs.Accounts;
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
        public string SupCode1 { get; set; }

        [DisplayName("Company Name")]
        [Required(ErrorMessage = "The field is required")]
        public string SupName { get; set; }

        [DisplayName("Address")]
        public string? SupAdd { get; set; }
        
        [DisplayName("Telephone Number")]
        public string? SupPhone1 { get; set; }
        
        [DisplayName("Mobile number")]
        public string? SupPhone2 { get; set; }

        [DisplayName("Fax Number")]
        public string? SupFax { get; set; }

        [DisplayName("Notes")]
        public string? Remarks { get; set; }
        
        [DisplayName("Supplier in Accounts")]
        [Required(ErrorMessage = "The field is required")]
        public string SubCode { get; set; }
        public string? MainCode { get; set; }
        
        [DisplayName("The Email")]
        public string? Email { get; set; }
        
        [DisplayName("The Governerate")]
        public int? CityCode { get; set; }

        [DisplayName("The City")]
        public int? AreaCode { get; set; }
        public int? BranchId { get; set; }

        [DisplayName("Responsible Name")]
        public string? PersonName { get; set; }

        public List<AccSubCodeDTO>? SubCodes { get; set; }
        public List<CityCodeDTO>? Cities { get; set; }
        public List<PlaceCodeDTO>? Places { get; set; }
    }
}
