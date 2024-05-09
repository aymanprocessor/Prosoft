using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
