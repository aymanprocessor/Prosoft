using ProSoft.EF.DTOs.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Accounts
{
    public class ReportExpenseAnalysisDTO
    {
        [DisplayName("Account (Main)")]
        [Required(ErrorMessage = "The field is required")]
        public string? MainCode { get; set; }

        [DisplayName("Year")]
        [Required(ErrorMessage = "The field is required")]
        public int? Year { get; set; }
        public List<BranchDTO>? branchs { get; set; }
        public List<JournalTypeDTO>? JournalTypes { get; set; }
        public List<AccMainCodeDTO>? accMainCodes { get; set; }
    }
}
