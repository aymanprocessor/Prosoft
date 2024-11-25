using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Stocks.TransferSuppliesToStocks
{
    public class TransferSuppliesToStocksAJAXReqDTO
    {
       
            public string? companyName { get; set; }
            public DateTime? date { get; set; }
            public string? doctorName { get; set; }
            public DateTime? exitDate { get; set; }
            public int? exitStatus { get; set; }
            public int? invoiceNo { get; set; }
            public decimal? invoiceTotal { get; set; }
            public int? masterId { get; set; }
            public int? patId { get; set; }
            public string? patientName { get; set; }
            public bool? post { get; set; }
        
    }
}
