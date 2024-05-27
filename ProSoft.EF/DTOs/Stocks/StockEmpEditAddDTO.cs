using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProSoft.EF.DTOs.Shared;
using ProSoft.EF.DTOs.Accounts;

namespace ProSoft.EF.DTOs.Stocks
{
    public class StockEmpEditAddDTO
    {
        public int StockEmpID { get; set; }
        public int UserId { get; set; }

        [DisplayName("Stock")]
        [Required(ErrorMessage = "The field is required")]
        public int Stkcod { get; set; }

        [DisplayName("Permission Type")]
        [Required(ErrorMessage = "The field is required")]
        public int TransType { get; set; }
        public int? BranchId { get; set; }

        [DisplayName("Stock Is Default")]
        public int StockDef { get; set; }

        [DisplayName("Show Trans Price")]
        public int ShowPrice { get; set; }

        [DisplayName("Debit Account (Main)")]
        [Required(ErrorMessage = "The field is required")]
        public string MainCodeStk { get; set; }

        [DisplayName("Debit Account (Sub)")]
        public string? SubCodeStk { get; set; }

        [DisplayName("Credit Account (Main)")]
        [Required(ErrorMessage = "The field is required")]
        public string MainCodeAcc { get; set; }

        [DisplayName("Credit Account (Sub)")]
        public string? SubCodeAcc { get; set; }

        [DisplayName("Journal Type")]
        [Required(ErrorMessage = "The field is required")]
        public string JornalCode { get; set; }

        public List<PermissionDefViewDTO>? Permissions { get; set; }
        public List<StockViewDTO>? Stocks { get; set; }
        public List<AccMainCodeDTO>? MainAccCodes { get; set; }
        public List<AccSubCodeDTO>? SubAccCodes { get; set; }
        public List<JournalTypeDTO>? JournalTypes { get; set; }
    }
}
