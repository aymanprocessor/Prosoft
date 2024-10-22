using ProSoft.EF.DTOs.Accounts;
using ProSoft.EF.DTOs.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Stocks
{
    public class ReportQuantityClassCardDTO
    {
        [DisplayName("Account (Main)")]
        public string? MainCode { get; set; }
        public int? FYear { get; set; }

        public DateTime FromDate {  get; set; }
        public DateTime ToDate {  get; set; }
        public int BranchId {  get; set; }
        public int StockId {  get; set; }
        [DisplayName("The Item")]
        public string? ItemMaster { get; set; }
        public List<StockViewDTO>? stocks { get; set; }
        public List<SubItemViewDTO>? subItems { get; set; }

    }
}
