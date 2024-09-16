using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Medical.HospitalPatData
{
    public class PriceListRecordsDTO
    {
        public List<PriceListDTO> InsertData { get; set; }
        public List<PriceListDTO> UpdateData { get; set; }
    }
}
