using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Stocks
{
    public class CityCodeDTO
    {
        public string CityCode1 { get; set; }
        public string CityName { get; set; }
        public int Branch { get; set; }
    }
}
