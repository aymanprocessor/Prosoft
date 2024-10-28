using Microsoft.EntityFrameworkCore;
using ProSoft.Core.Enums;
using ProSoft.EF.DbContext;
using ProSoft.EF.DTOs.Stocks.Report.Transactions;
using ProSoft.EF.IRepositories.Stocks.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.Core.Repositories.Stocks.Reports
{
    public class TransactionReportRepo : ITransactionReportRepo
    {
        private readonly AppDbContext _context;

        public TransactionReportRepo(AppDbContext context)
        {
            this._context = context;
        }

        public async Task<List<TransactionReportDTO>> GetTransactionReport(DateTime FromDate, DateTime ToDate, int StockId, string? MainCode, int? FirstRows, string OrderType = "ASC")
        {
            List<TransactionReportDTO> transactionReportDTOs = new();
            var subItems = await _context.SubItems.ToListAsync();

            foreach (var subItem in subItems)
            {
                TransactionReportDTO transactionReportDTO = new();
                transactionReportDTO.ItemName = subItem.SubName;
                transactionReportDTO.ItemCode = subItem.ItemCode;

                var transTypes = new int[] { 4, 12, 13, 5 };
                var tnxDtls = await _context.TransDtls.Where(t =>
                t.ItemMaster == subItem.ItemCode &&
                t.StockCode == StockId &&
                    transTypes.Contains((int)t.TransType) &&
                    t.DocDate.Value.Date >= FromDate.Date &&
                    t.DocDate.Value.Date <= ToDate.Date &&
                    t.UnitQty > 0).ToListAsync();

                if (tnxDtls.Count == 0) continue;

                if (!string.IsNullOrEmpty(MainCode))
                {
                    tnxDtls = tnxDtls.Where(t => t.MainCode == MainCode).ToList();
                }

                if (FirstRows != null && FirstRows > 0)
                {
                    tnxDtls = tnxDtls.Take((int)FirstRows).ToList();
                }

                transactionReportDTO.ItemQty = (int)tnxDtls.Sum(t => t.UnitQty);
                transactionReportDTOs.Add(transactionReportDTO);
            }
            if(OrderType == "ASC")
            {

            transactionReportDTOs = transactionReportDTOs.OrderBy(t => t.ItemQty).ToList();
            }
            else
            {
            transactionReportDTOs = transactionReportDTOs.OrderByDescending(t => t.ItemQty).ToList();

            }
            return transactionReportDTOs;
        }
    }
}
