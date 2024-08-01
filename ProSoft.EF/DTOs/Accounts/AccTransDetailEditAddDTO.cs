using ProSoft.EF.DTOs.Treasury;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Accounts
{
    public class AccTransDetailEditAddDTO
    {
        public int? TransDtlId { get; set; }
        public int TransId { get; set; }

        [DisplayName("Account (Main)")]
        [Required(ErrorMessage = "The field is required")]
        public string? MainCode { get; set; }

        [DisplayName("Account (Sub)")]
        public string? SubCode { get; set; }

        [DisplayName("EG Debit")]
        [Required(ErrorMessage = "The field is required")]
        public decimal? ValDep { get; set; }

        [DisplayName("EG Credit")]
        [Required(ErrorMessage = "The field is required")]
        public decimal? ValCredit { get; set; }

        [DisplayName("Cur Debit")]
        [Required(ErrorMessage = "The field is required")]
        public decimal? ValDepCur { get; set; }

        [DisplayName("Cur Credit")]
        [Required(ErrorMessage = "The field is required")]
        public decimal? ValCreditCur { get; set; }

        [DisplayName("File No")]
        public string? DocNo { get; set; }

        [DisplayName("File Date")]
        public DateTime? DocDate { get; set; }

        [DisplayName("Notes")]
        [Required(ErrorMessage = "The field is required")]
        public string? LineDesc { get; set; }

        [DisplayName("Cost Center")]
        public string? CostCenterCode { get; set; }

        public DateTime? EntryDate { get; set; }
        public int? FYear { get; set; }
        public string? TransType { get; set; }
        public string? CurCode { get; set; }
        public DateTime? TransDate { get; set; }
        public int? TransNo { get; set; }
        public string? YearTransNo { get; set; }
        public int? UserCode { get; set; }
        public int? UserCodeModify { get; set; }
        public int? CoCode { get; set; }
        public long? TransSerial { get; set; }






        //Lists
        public List<CostCenterViewDTO>? CostCenters { get; set; }
        public List<AccMainCodeDTO>? MainAccCodes { get; set; }
        public List<AccSubCodeDTO>? SubAccCodes { get; set; }
    }
}
