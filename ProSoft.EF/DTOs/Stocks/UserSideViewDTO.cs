using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Stocks
{
    public class UserSideViewDTO
    {

        [DisplayName("Sides")]

        public int SideId { get; set; }
      

        public IEnumerable<SelectListItem> Sides { get; set; } = Enumerable.Empty<SelectListItem>();
        

    }

}
