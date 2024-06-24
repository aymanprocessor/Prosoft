using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Medical.HospitalPatData
{
    public class DepositViewDTO
    {
        public int DpsSer { get; set; }
        public int? PostRecipt { get; set; }
        public int DpsType { get; set; }
        public decimal DpsVal { get; set; }
        public string DepositDesc { get; set; }
        public int? MasterId { get; set; }
        public int? JorKiedNo { get; set; }
        public int? SafeDocNo { get; set; }





    }
}
