using Microsoft.AspNetCore.Mvc.Rendering;
using ProSoft.EF.Models.Stocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Stocks
{
    public class UsersGroupDTO
    {
        public int G_ID { get; set; }
        public string G_NAME { get; set; }
        public bool FLAG { get; set; }

    }


}
