using ProSoft.EF.DTOs.Accounts;
using ProSoft.EF.DTOs.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Stocks
{
    public class StockEditAddDTO
    {
        [DisplayName("Stock ID")]
        [Required(ErrorMessage = "The field is required")]
        public int Stkcod { get; set; }

        [DisplayName("Stock Name")]
        [Required(ErrorMessage = "The field is required")]
        public string Stknam { get; set; }

        [DisplayName("Stock Type")]
        [Required(ErrorMessage = "The field is required")]
        public int Flag1 { get; set; }

        [DisplayName("Branch")]
        [Required(ErrorMessage = "The field is required")]
        public int BranchId { get; set; }

        [DisplayName("System Code")]
        [Required(ErrorMessage = "The field is required")]
        public int StockType { get; set; }

        public int? StkDefult { get; set; }

        [DisplayName("Stock Kind")]
        [Required(ErrorMessage = "The field is required")]
        public int StockPurchOnshelf { get; set; }

        [DisplayName("Jornal Code")]
        [Required(ErrorMessage = "The field is required")]
        public string JornalCode { get; set; }

        [DisplayName("Is Active")]
        [Required(ErrorMessage = "The field is required")]
        public int StkOnOff { get; set; }

        public List<BranchDTO>? Branches { get; set; }
        public List<KindStoreDTO>? StocksTypes { get; set; }
        public List<JournalTypeDTO>? CalculusJournals { get; set; }
    }
}
