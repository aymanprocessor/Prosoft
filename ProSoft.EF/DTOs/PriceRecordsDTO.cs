using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs
{
    public class PriceRecordsDTO
    {
        public List<PriceDTO> InsertData { get; set; }
        public List<PriceDTO> UpdateData { get; set; }
    }
}
