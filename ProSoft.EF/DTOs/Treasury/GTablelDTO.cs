using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace ProSoft.EF.DTOs.Treasury
{
    public class GTablelDTO
    {
        [DisplayName("Code")]
        [Required(ErrorMessage = "The field is required")]
        public int GCode { get; set; }

        [DisplayName("Cost Center Name")]
        [Required(ErrorMessage = "The field is required")]
        public string? GDesc { get; set; }

        public int? Flag { get; set; }

    }
}
