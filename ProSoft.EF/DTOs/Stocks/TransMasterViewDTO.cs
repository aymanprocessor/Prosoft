﻿using ProSoft.EF.Models.Stocks;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Stocks
{
    public class TransMasterViewDTO
    {
        public int TransMAsterID { get; set; }
        public long? DocNo { get; set; }
        public DateTime? DocDate { get; set; }
        public int? TransType { get; set; }
        public long DocNoFr { get; set; }
        public short StockCode { get; set; }
        public short StockCode2 { get; set; }
        public string PermissionName { get; set; }
        public string SupplierName { get; set; }
        public long? AccTransNo { get; set; }
        public string? SupInvNo { get; set; }
        public DateTime? SupInvDate { get; set; }
        public string StockName { get; set; }
        public string StockName2 { get; set; }
        public string UserName { get; set; }
        public int UserCode { get; set; }
        public int ShowTransPrice { get; set; }
        public string? Flag3 { get; set; }

        public TransDtlWithPriceDTO? TransDtlDTO { get; set; }
        public List<SupCodeViewDTO>? Suppliers { get; set; }

    }
}
