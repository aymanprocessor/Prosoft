using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Accounts
{
    public class AccStartBalEditAddDTO
    {
        public int StartBalId { get; set; }

        [DisplayName("Debit")]
        [Required(ErrorMessage = "The field is required")]
        public decimal? FDepOr { get; set; }

        [DisplayName("Credit")]
        [Required(ErrorMessage = "The field is required")]
        public decimal? FCreditOr { get; set; }

        [DisplayName("Cost Center")]
        [Required(ErrorMessage = "The field is required")]
        public string? CostCenterCode { get; set; }
        public string? TransType { get; set; }

        //Lists
        public List<CostCenterViewDTO>? CostCenters { get; set; }
    }
}
