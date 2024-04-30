using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Stocks
{
    public class SupCodeDTO
    {
        public int Sup { get; set; }
        public string? SupCode1 { get; set; }
        public string? SupName { get; set; }
        public string? SupAdd { get; set; }
        public string? SupPhone1 { get; set; }
        public string? SupPhone2 { get; set; }
        public string? SupFax { get; set; }
        public string? Remarks { get; set; }
        public decimal? ValDept { get; set; }
        public decimal? ValCredit { get; set; }
        public string? SubCode { get; set; }
        public string? MainCode { get; set; }
        public string? Email { get; set; }
        public int? CityCode { get; set; }
        public int? AreaCode { get; set; }
        public int? BranchId { get; set; }
        public string? PersonName { get; set; }
        public short? SupType { get; set; }
        public double? Replcate { get; set; }
        public string? BrReplc { get; set; }
    }
}
