using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Stocks
{
    public class ClassificationViewDTO
    {
        [DisplayName("Department Code")]
        public int ClassificationCust1 { get; set; }

        [DisplayName("Department Description")]
        [Required(ErrorMessage = "The field is required")]
        public string ClassificationDesc { get; set; }
    }
}
