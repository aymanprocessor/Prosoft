using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using ProSoft.EF.DTOs.Accounts;

namespace ProSoft.EF.DTOs.Treasury
{
    public class UserCashNoEditAddDTO
    {
        [DisplayName("Code")]
        [Required(ErrorMessage = "The field is required")]
        public int UserCashID { get; set; }
        public int UserCode { get; set; }

        [DisplayName("Treasury Name")]
        [Required(ErrorMessage = "The field is required")]
        public int SafeCode { get; set; }
        public int? BranchId { get; set; }

        [DisplayName("Activation")]
        [Required(ErrorMessage = "The field is required")]
        public int? ActdeactFlag { get; set; }
        public int? MCode { get; set; }

        [DisplayName("Sub Safe Account (Main)")]
        [Required(ErrorMessage = "The field is required")]
        public string? MainCode { get; set; }

        [DisplayName("Sub Safe Account (Sub)")]
        [Required(ErrorMessage = "The field is required")]
        public string? SubCode { get; set; }

        [DisplayName("Main Safe Account (Main)")]
        [Required(ErrorMessage = "The field is required")]
        public string? MainCode2 { get; set; }

        [DisplayName("Main Safe Account (Sub)")]
        [Required(ErrorMessage = "The field is required")]
        public string? SubCode2 { get; set; }

        [DisplayName("Posting To Safes")]
        [Required(ErrorMessage = "The field is required")]
        public string? CshUsr { get; set; }

        //Lists
        public List<TreasuryNameViewDTO>? treasuryNames { get; set; }
        public List<AccMainCodeDTO>? MainAccCodes { get; set; }
        public List<AccSubCodeDTO>? SubAccCodes { get; set; }

    }
}
