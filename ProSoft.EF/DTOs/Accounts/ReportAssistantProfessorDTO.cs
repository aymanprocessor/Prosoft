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
    public class ReportAssistantProfessorDTO
    {
        [DisplayName("Account (Main)")]
        [Required(ErrorMessage = "The field is required")]
        public string? MainCode { get; set; }

        [DisplayName("Account (Sub)")]
        public string? SubCode { get; set; }

        public List<BranchDTO>? branchs { get; set; }
        public List<JournalTypeDTO>? JournalTypes { get; set; }
        public List<AccMainCodeDTO>? accMainCodes { get; set; }
        public List<AccSubCodeDTO>? accSubCodes { get; set; }
        public List<CostCenterViewDTO>? costCenters { get; set; }

    }
}
