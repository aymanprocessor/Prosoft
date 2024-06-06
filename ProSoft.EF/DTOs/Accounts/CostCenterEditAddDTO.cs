using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Accounts
{
    public class CostCenterEditAddDTO
    {
        [DisplayName("Code")]
        [Required(ErrorMessage = "The field is required")]
        public int CostCode { get; set; }

        [DisplayName("Cost Name")]
        [Required(ErrorMessage = "The field is required")]
        public string CostDesc { get; set; }

        [DisplayName("Activation")]
        [Required(ErrorMessage = "The field is required")]
        public int? CostVisible { get; set; }
        public int? CostFlag { get; set; }
        public decimal? DepitVal { get; set; }
        public decimal? CreditVal { get; set; }
    }
}
