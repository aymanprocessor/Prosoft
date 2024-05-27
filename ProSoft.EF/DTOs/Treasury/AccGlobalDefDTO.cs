using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Treasury
{
    public class AccGlobalDefDTO
    {
        [DisplayName("Code")]
        [Required(ErrorMessage = "The field is required")]
        public int CodeNo { get; set; }

        [DisplayName("Currency Name")]
        [Required(ErrorMessage = "The field is required")]
        public string? CodeDesc { get; set; }

        public string? TableCode { get; set; }

        [DisplayName("Currency Symbol")]
        public string? Symbol { get; set; }

    }
}
