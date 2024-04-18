using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Stocks
{
    public class StoreTransDTO
    {
        [DisplayName("Code")]
        public int TransId { get; set; }

        [DisplayName("Name")]
        [Required(ErrorMessage = "The field is required")]
        public string TransDesc { get; set; }
    }
}
