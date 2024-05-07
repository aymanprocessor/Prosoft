using ProSoft.EF.DTOs.Accounts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Medical.HospitalPatData
{
    public class DepositEditAddDTO
    {
        [DisplayName("Code")]
        [Required(ErrorMessage = "The field is required")]
        public int DpsSer { get; set; }

        [DisplayName("Date")]
        [Required(ErrorMessage = "The field is required")]
        public DateTime? DpsDate { get; set; }

        [DisplayName("Payment Type")]
        [Required(ErrorMessage = "The field is required")]
        public int? PostRecipt { get; set; }

        [DisplayName("Payment Method")]
        [Required(ErrorMessage = "The field is required")]
        public int DpsType { get; set; }

        [DisplayName("Value")]
        [Required(ErrorMessage = "The field is required")]
        public decimal DpsVal { get; set; }

        [DisplayName("Note")]
        [Required(ErrorMessage = "The field is required")]
        public string DepositDesc { get; set; }

        [DisplayName("Check No")]
        public string? CheckNo { get; set; }

        [DisplayName("Check Date")]
        public DateTime? CheckDate { get; set; }

        [DisplayName("Bank")]
        public string? BankId { get; set; }

        [DisplayName("User Dependence")]
        [Required(ErrorMessage = "The field is required")]
        public int? PostUser { get; set; }

        [DisplayName("Treasury No")]
        [Required(ErrorMessage = "The field is required")]
        public int? SafeDocNo { get; set; }

        [DisplayName("Voucher No")]
        [Required(ErrorMessage = "The field is required")]
        public int? JorKiedNo { get; set; }

        public int? FYear { get; set; }
        public int? UserCode { get; set; }
        public int? UserModify { get; set; }
        public int? PatId { get; set; }
        public int? MasterId { get; set; }
        public int? BranchId { get; set; }


        //list
        public List<AccSubCodeDTO>? accSubCodes { get; set; }
    }
}
