using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Accounts
{
    public class AccStartBalViewDTO
    {
        public int StartBalId { get; set; }
        public string? MainCode { get; set; }
        public string? SubCode { get; set; }
        public string? AccName { get; set; }
        public decimal? FDepOr { get; set; }
        public decimal? FCreditOr { get; set; }
        public string? CostCenterName { get; set; }
        public string? CostCenterCode { get; set; }
    }
}
