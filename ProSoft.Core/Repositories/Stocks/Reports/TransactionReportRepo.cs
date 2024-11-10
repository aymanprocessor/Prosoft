using Microsoft.EntityFrameworkCore;
using ProSoft.Core.Enums;
using ProSoft.EF.DbContext;
using ProSoft.EF.DTOs.Stocks.Report.Transactions;
using ProSoft.EF.IRepositories.Stocks.Reports;
using Shared;
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

              


                transactionReportDTO.ItemQty = (int)tnxDtls.Sum(t => t.UnitQty);
                transactionReportDTOs.Add(transactionReportDTO);
            }
            if (OrderType == "ASC")
            {

                transactionReportDTOs = transactionReportDTOs.OrderByDescending(t => t.ItemQty).ToList();
            }
            else
            {
                transactionReportDTOs = transactionReportDTOs.OrderBy(t => t.ItemQty).ToList();

            }

            if (FirstRows != null && FirstRows > 0)
            {
                transactionReportDTOs = transactionReportDTOs.Take((int)FirstRows).ToList();
            }
            return transactionReportDTOs;
        }

        public async Task<List<TransactionReportDTO>> GetZeroTransactionReport(DateTime FromDate, DateTime ToDate, int StockId, int FYear, int? FirstRows = null)
        {
            List<TransactionReportDTO> transactionReportDTOs = new();
            var stkBals = await _context.Stkbalances.Where(s => s.FYear == FYear && s.Stkcod == StockId).ToListAsync();

            foreach (var stk in stkBals)
            {
                var transTypes = new int[] { 4, 10, 13 };
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
                        ItemQty = 0
                    });
                }

                if (FirstRows != null && FirstRows > 0)
                {
                    transactionReportDTOs = transactionReportDTOs.Take((int)FirstRows).ToList();
                }
            }
            return transactionReportDTOs;

        }

        public async Task<List<TotalPermitsTransactionReportDTO>> GetTotalPermitsTransactionReport(DateTime FromDate, DateTime ToDate, int BranchId, int transType,int StockId)
        {

            List<TotalPermitsTransactionReportDTO> totalPermitsTransactionReportDTOs = new();
            var tnxMstrs = await _context.TransMasters.Where(t =>
            t.TransType == transType &&
            t.BranchId == BranchId &&
            t.StockCode == StockId &&
            t.DocDate.Value.Date >= FromDate.Date &&
            t.DocDate.Value.Date <= ToDate.Date
            ).ToListAsync();

            foreach (var tnxMstr in tnxMstrs)
            {

                TotalPermitsTransactionReportDTO reportDTO = new();

                reportDTO.DocDate = tnxMstr.DocDate.Value.ToString("yyyy-MM-dd");
                reportDTO.DocNo = tnxMstr.DocNo;
                reportDTO.RefDocNo = tnxMstr.RefDocNo;
                reportDTO.TotTransVal = tnxMstr.TotTransVal ?? 0;
                reportDTO.Descount = tnxMstr.Descount ?? 0;
                reportDTO.NetTransValue = reportDTO.TotTransVal - reportDTO.Descount;

                totalPermitsTransactionReportDTOs.Add(reportDTO);

            }
            return totalPermitsTransactionReportDTOs;
        }

        public async Task<List<SupplierPermitsTransactionReportDTO>> GetSupplierPermitsTransactionReport(DateTime FromDate, DateTime ToDate, int BranchId, int transType, int StockId, Filter? filter)
        {

            List<SupplierPermitsTransactionReportDTO> supplierPermitsTransactionReportDTOs = new();
            var tnxDtls = await _context.TransDtls.Where(t =>
            t.TransType == transType &&
            t.StockCode == StockId &&
            t.BranchId == BranchId &&
            t.DocDate.Value.Date >= FromDate.Date &&
            t.DocDate.Value.Date <= ToDate.Date
            ).OrderBy(t => t.DocDate)
             .ToListAsync();

            if (filter?.SupplierId != null) tnxDtls = tnxDtls.Where(t => t.SubCode == filter.SupplierId).ToList();
            if (!string.IsNullOrEmpty(filter?.SearchByItemCode)) tnxDtls = tnxDtls.Where(t => t.ItemMaster.Contains(filter.SearchByItemCode)).ToList();
            if (!string.IsNullOrEmpty(filter?.SearchByItemName)) tnxDtls = tnxDtls.Where(t => ( _context.SubItems.FirstOrDefault(s=>s.ItemCode == t.ItemMaster)).SubName.Contains(filter.SearchByItemName)).ToList();
            if (!string.IsNullOrEmpty(filter?.FromCode)) tnxDtls = tnxDtls.Where(t => string.Compare(t.ItemMaster, filter.FromCode,StringComparison.Ordinal )>=0 && string.Compare(t.ItemMaster, filter.ToCode, StringComparison.Ordinal) <= 0).ToList();
            if (!string.IsNullOrEmpty(filter?.ToCode)) tnxDtls = tnxDtls.Where(t => t.ItemMaster.Contains(filter.SearchByItemCode)).ToList();
            

            foreach (var tnxDtl in tnxDtls)
            {

                SupplierPermitsTransactionReportDTO reportDTO = new();

                var Suppliers = await _context.SupCodes.FirstOrDefaultAsync(s => s.SupCode1== tnxDtl.SubCode.ToString());
                var SubItem = await _context.SubItems.FirstOrDefaultAsync(s => s.ItemCode == tnxDtl.ItemMaster);
                var Unit = await _context.UnitCodes.FirstOrDefaultAsync(u => u.Code == tnxDtl.UnitCode);

                reportDTO.DocDate = tnxDtl.DocDate.Value.ToString("yyyy-MM-dd");
                reportDTO.DocNo = tnxDtl.DocNo;
                reportDTO.SupplierName = Suppliers?.SupName;
                reportDTO.ItemCode = tnxDtl.ItemMaster;
                reportDTO.ItemName = SubItem?.SubName;
                reportDTO.UnitName = Unit?.Names;
                reportDTO.ItemQty = tnxDtl.UnitQty;
                reportDTO.ItemPrice = tnxDtl.Price;
                reportDTO.ItemValue = tnxDtl.ItemVal;




                supplierPermitsTransactionReportDTOs.Add(reportDTO);

            }

            return supplierPermitsTransactionReportDTOs;
        }


    }
}
