using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProSoft.EF.DTOs.Treasury;

namespace ProSoft.EF.DTOs.Accounts
{
    public class AccTransMasterEditAddDTO
    {
        public int TransId { get; set; }

        [DisplayName("Voucher No")]
        [Required(ErrorMessage = "The field is required")]
        public int? TransNo { get; set; }

        [DisplayName("Voucher Date")]
        [Required(ErrorMessage = "The field is required")]
        public DateTime? TransDate { get; set; }

        [DisplayName("Total Voucher")]
        public decimal? TotalTrans { get; set; }

        [DisplayName("Notes")]
        [Required(ErrorMessage = "The field is required")]
        public string? TransDesc { get; set; }

        [DisplayName("Voucher Type")]
        [Required(ErrorMessage = "The field is required")]
        public string? OkPost { get; set; }

        [DisplayName("Come From")]
        public int? MCode { get; set; }
        public DateTime? EntryDate { get; set; }
        public string? YearTransNo { get; set; }
        public int? DocStatus { get; set; }
        public int? MCodeDtl { get; set; }

        [DisplayName("Currency")]
        public string? CurCode { get; set; }

        [DisplayName("Rate Currency")]
        public decimal? CurRate { get; set; }
        public string? TransType { get; set; }

        public string? JournalName { get; set; }
        public int? JournalCode { get; set; }
        public int? UserCodeModify { get; set; }
        public int? CoCode { get; set; }
        public int? FYear { get; set; }



        //Lists
        public List<AccGlobalDefDTO>? accGlobalDefs { get; set; }



    }
}
