using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Medical.HospitalPatData
{
    public class PriceListViewDTO
    {
        public int PLId { get; set; }
        public string PlDesc { get; set; }
        public int? Flag1 { get; set; }
        public DateTime? PLDate { get; set; }
        public int? Year { get; set; }

    }
}
