using ProSoft.EF.DTOs.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Accounts
{
    public class DisplayAccSubCodeEditDTO
    {
        [DisplayName("Account (Main)")]
        [Required(ErrorMessage = "The field is required")]
        public string? MainCode { get; set; }
        public List<BranchDTO>? branchs { get; set; }
        public List<AccMainCodeDTO>? accMainCodes { get; set; }
    }
}
