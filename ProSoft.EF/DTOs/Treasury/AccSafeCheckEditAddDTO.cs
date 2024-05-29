using ProSoft.EF.DTOs.Accounts;
using ProSoft.EF.DTOs.Stocks;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Treasury
{
    public class AccSafeCheckEditAddDTO
    {
        [DisplayName("Code")]
        [Required(ErrorMessage = "The field is required")]
        public int SafeCeckId { get; set; }

        public int? Flag { get; set; } //deafult 1

        [DisplayName("Journal")]
        [Required(ErrorMessage = "The field is required")]
        public int? AccTransType { get; set; }

        [DisplayName("Check Number")]
        [Required(ErrorMessage = "The field is required")]
        public string? ChekNo { get; set; }    

        [DisplayName("Due Date")]
        [Required(ErrorMessage = "The field is required")]
        public DateTime? SattlDate { get; set; }

        [DisplayName("Payment Ways")]
        //[Required(ErrorMessage = "The field is required")]
        public string? FlagPay { get; set; }

        [DisplayName("Cost Center")]
        [Required(ErrorMessage = "The field is required")]
        public int? CostCenterCode { get; set; }

        [DisplayName("Receipt No")]
        [Required(ErrorMessage = "The field is required")]
        public int? DocNo { get; set; }
        public int? FYear { get; set; }
        public double? FMonth { get; set; }

        [DisplayName("User Dependence")]
        [Required(ErrorMessage = "The field is required")]
        public string? AprovedFlag { get; set; }

        [DisplayName("Treasury")]
        [Required(ErrorMessage = "The field is required")]
        public int? SafeCode { get; set; }

        [DisplayName("Treasury")]
        public int? SafeCode2 { get; set; }

        [DisplayName("Receipt date")]
        [Required(ErrorMessage = "The field is required")]
        public DateTime? DocDate { get; set; }//تاريخ الايصال

        [DisplayName("Statement")]
        [Required(ErrorMessage = "The field is required")]
        public string? Commentt { get; set; }
        public int? CurSer { get; set; }

        [DisplayName("Rate Currency")]
        public decimal? Rate1 { get; set; }

        [DisplayName("Currency")]
        //  [Required(ErrorMessage = "The field is required")]
        public int? CurCode { get; set; }//Acc Global Def

        [DisplayName("Debit Account (Main)")]
        [Required(ErrorMessage = "The field is required")]
        public string? MainCode { get; set; }

        [DisplayName("Debit Account (Sub)")]
        public string? SubCode { get; set; }
        public string? TranType { get; set; }
        public int? AccTransNo { get; set; }

        public int? MCodeDtl { get; set; }

        [DisplayName("Drawee Bank")]
        [Required(ErrorMessage = "The field is required")]
        public string? PersonName { get; set; }

        [DisplayName("Amount Deposited")]
        [Required(ErrorMessage = "The field is required")]
        public decimal? ValuePay { get; set; }


        //set in add deafults
        public string? FlagS { get; set; }
        public decimal? ProfitTax { get; set; }
        public decimal? DiscountVal { get; set; }
        public string? FlagPayStatus { get; set; }
        public string? FlagApr { get; set; }

        [DisplayName("Drawee Bank")]
        public string? SubCodeBank { get; set; }

        [DisplayName("Check Type")]
        public int? CheckType { get; set; }

        [DisplayName("Credit Account")]
        public string? SubCode2 { get; set; }

        //for text wirte front on credit account
        public string? mainName13 { get; set; }
        public string? mainName17 { get; set; }




        //Lists
        public List<JournalTypeDTO>? journalTypes { get; set; }
        public List<GTablelDTO>? gTablels { get; set; }
        public List<CostCenterViewDTO>? costCenters { get; set; }
        public List<TreasuryNameViewDTO>? treasuryNames { get; set; }
        public List<AccGlobalDefDTO>? accGlobalDefs { get; set; }
        public List<AccMainCodeDTO>? accMainCodes { get; set; }
        public List<AccSubCodeDTO>? accSubCodes { get; set; }
        public List<AccSubCodeDTO>? banks { get; set; }
    }
}
