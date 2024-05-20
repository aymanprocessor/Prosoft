using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Treasury
{
    public class AccSafeCashViewDTO
    {
        public int SafeCashId { get; set; }

        public int? DocNo { get; set; }

        public string? SafeName { get; set; }

        public DateTime? DocDate { get; set; }//تاريخ الايصال

        public string? PersonName { get; set; }

        public decimal? ValuePay { get; set; }

    }
}
