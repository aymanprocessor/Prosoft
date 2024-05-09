using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProSoft.EF.DTOs.Stocks;
using ProSoft.EF.DTOs.Accounts;

namespace ProSoft.EF.DTOs.Treasury
{
    public class TreasuryNameEditAddDTO
    {
        [DisplayName("Code")]
        [Required(ErrorMessage = "The field is required")]
        public int SafeCode { get; set; }

        [DisplayName("Treasury Name")]
        [Required(ErrorMessage = "The field is required")]
        public string? SafeNames { get; set; }

        [DisplayName("Show")]
        [Required(ErrorMessage = "The field is required")]
        public int? Flag1 { get; set; }

        [DisplayName("Posting To Accounts")]
        [Required(ErrorMessage = "The field is required")]
        public int? PostAccount { get; set; }

        [DisplayName("Journal Type")]
        [Required(ErrorMessage = "The field is required")]
        public string? JeDocTyp { get; set; }

        //Lists
        public List<JournalTypeDTO>? JournalTypes { get; set; }


    }
}
