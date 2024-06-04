using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Accounts
{
    public class AccTransMasterViewDTO
    {
        public int TransId { get; set; }
        public int? TransNo { get; set; }
        public DateTime? TransDate { get; set; }
        public decimal? TotalTrans { get; set; }
        public string? TransDesc { get; set; }
        public string? OkPost { get; set; }
        public int? MCode { get; set; }


    }
}
