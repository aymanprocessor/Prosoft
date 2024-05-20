using ProSoft.EF.DTOs.Accounts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Treasury
{
    public class CustCollectionsDiscountEditAddDTO
    {
        [DisplayName("Code")]
        [Required(ErrorMessage = "The field is required")]
        public int? DiscountCode { get; set; }
        public int? ReceiptNo { get; set; }
        public DateTime? ReceiptDate { get; set; }
        public string? DocType { get; set; }
        
        [DisplayName("Discount Percentage")]
        [Required(ErrorMessage = "The field is required")]
        public decimal? DiscPrc { get; set; }

        [DisplayName("Discount Value")]
        [Required(ErrorMessage = "The field is required")]
        public decimal? DiscValue { get; set; }

        [DisplayName("Credit Account (Main)")]
        [Required(ErrorMessage = "The field is required")]
        public string? MainCode { get; set; }

        [DisplayName("Credit Account (Sub)")]
        public string? SubCode { get; set; }
        public int? FYear { get; set; }
        public int? SafeCode { get; set; }

        //Lists
        public List<AccMainCodeDTO>? accMainCodes { get; set; }
        public List<AccSubCodeDTO>? accSubCodes { get; set; }

    }
}
