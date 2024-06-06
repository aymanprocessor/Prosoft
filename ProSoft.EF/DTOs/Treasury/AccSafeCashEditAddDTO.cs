using ProSoft.EF.Models.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProSoft.EF.DTOs.Accounts;

namespace ProSoft.EF.DTOs.Treasury
{
    public class AccSafeCashEditAddDTO
    {
        [DisplayName("Code")]
        [Required(ErrorMessage = "The field is required")]
        public int SafeCashId { get; set; }

        [DisplayName("Posting")]
        [Required(ErrorMessage = "The field is required")]
        public int? Flag { get; set; }

        [DisplayName("Registration No")]
        public long? AccTransNo { get; set; }


        [DisplayName("Journal")]
        [Required(ErrorMessage = "The field is required")]
        public int? AccTransType { get; set; }

        public int? SerId { get; set; }

        [DisplayName("Treasury Type")]
        //[Required(ErrorMessage = "The field is required")]
        public int? EntryType { get; set; }

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

        [DisplayName("Credit Account (Main)")]
        [Required(ErrorMessage = "The field is required")]
        public string? MainCode { get; set; }

        [DisplayName("Credit Account (Sub)")]
        public string? SubCode { get; set; }
        public string? DocType { get; set; }
        public int? MCodeDtl { get; set; }

        [DisplayName("Depositor Name")]
        [Required(ErrorMessage = "The field is required")]
        public string? PersonName { get; set; }

        [DisplayName("Amount Deposited")]
        [Required(ErrorMessage = "The field is required")]
        public decimal? ValuePay { get; set; }
        public decimal? ProfitTax { get; set; }
        public int? CshOrdNum { get; set; }
        public int? UserCode { get; set; }
        public int? BranchId { get; set; }




        //Lists
        public List<JournalTypeDTO>? journalTypes { get; set; }
        public List<GTablelDTO>? gTablels { get; set; }
        public List<CostCenterViewDTO>? costCenters { get; set; }
        public List<TreasuryNameViewDTO>? treasuryNames { get; set; }
        public List<AccGlobalDefDTO>? accGlobalDefs { get; set; }
        public List<AccMainCodeDTO>? accMainCodes { get; set; }
        public List<AccSubCodeDTO>? accSubCodes { get; set; }
    }
}
