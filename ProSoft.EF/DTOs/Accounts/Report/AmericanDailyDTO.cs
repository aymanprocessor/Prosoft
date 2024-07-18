using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Accounts.Report
{
    public class AmericanDailyDTO
    {
        public int? TransNo { get; set; }
        public DateTime? TransDate { get; set; }
        public string? LineDesc { get; set; }
        public string? MainName { get; set; }
        public List<Tuple<string, decimal?, decimal?>> MainCodeValues { get; set; }

        public AmericanDailyDTO()
        {
            MainCodeValues = new List<Tuple<string, decimal?, decimal?>>();
        }
    }
}
