using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Stocks
{
    public class SideDTO
    {
        [DisplayName("Side ID")]
        public int SideId { get; set; }

        [DisplayName("Side Name")]
        [Required(ErrorMessage = "The field is required")]
        public string SideDesc { get; set; }
    }
}
