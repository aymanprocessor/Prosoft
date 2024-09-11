using ProSoft.EF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs
{
    public  class PriceDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Pricee { get; set; }
        public IEnumerable<Price> Prices { get; set; } = Enumerable.Empty<Price>();
    }
}
