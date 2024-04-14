using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace ProSoft.EF.DTOs.Stocks
{
    public class UnitCodeDTO
    {
        [DisplayName("UnitCode Id")]
        public int Code { get; set; }

        [DisplayName("UnitCode Name")]
        [Required(ErrorMessage = "The field is required")]
        public string Names { get; set; }

        [DisplayName("Number of Particles")]
        [Required(ErrorMessage = "The field is required")]
        public decimal ItemQty { get; set; }

        public int? Flag1 { get; set; }
    }
}
