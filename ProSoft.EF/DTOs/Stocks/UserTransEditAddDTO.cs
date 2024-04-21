using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Stocks
{
    public class UserTransEditAddDTO
    {
        [DisplayName("User Code")]
        public int UsrId { get; set; }

        [DisplayName("Permission")]
        [Required(ErrorMessage = "The field is required")]
        public int GId { get; set; }

        [DisplayName("Transaction Type")]
        [Required(ErrorMessage = "The field is required")]
        public string DType { get; set; }

        public List<StoreTransDTO>? TransactionTypes { get; set; }
    }
}
