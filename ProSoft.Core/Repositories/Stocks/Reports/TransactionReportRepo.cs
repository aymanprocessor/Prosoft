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

        public async Task<List<TransactionReportDTO>> GetZeroTransactionReport(DateTime FromDate, DateTime ToDate, int StockId,int FYear, int? FirstRows = null)
        {
            List<TransactionReportDTO> transactionReportDTOs = new();
            var stkBals = await _context.Stkbalances.Where(s => s.FYear== FYear && s.Stkcod == StockId).ToListAsync();

            foreach (var stk in stkBals)
            {
                var  transTypes = new int[]{ 4, 10, 13 };
                var tnxDtls = await _context.TransDtls.Where(t =>

                t.DocDate.Value.Date >= FromDate.Date &&
                t.DocDate.Value.Date <= ToDate.Date &&
                t.StockCode == StockId &&
                transTypes.Contains((int)t.TransType)
                ).ToListAsync();

                // check if stk.ItemCode not in tnxdtls.ItemMaster
                bool stkNotInTransactions = tnxDtls.All(t => t.ItemMaster != stk.ItemCode);

                if (stkNotInTransactions)
                {
                    // Add your logic for the items not in transactions
                    var subItem = await _context.SubItems.FirstOrDefaultAsync(s => s.ItemCode == stk.ItemCode);
                    transactionReportDTOs.Add(new TransactionReportDTO
                    {
                        ItemCode = stk.ItemCode,
                        ItemName = subItem.SubName,
                        ItemQty =0
                    });
                }

                if(FirstRows != null&&FirstRows > 0)
                {
                    transactionReportDTOs = transactionReportDTOs.Take((int)FirstRows).ToList();
                }
            }
            return transactionReportDTOs;

        }
    }
}
