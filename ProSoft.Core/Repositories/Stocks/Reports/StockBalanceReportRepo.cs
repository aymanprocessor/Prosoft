using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using ProSoft.EF.DbContext;
using ProSoft.EF.DTOs.Stocks.Report.StockBalance;
using ProSoft.EF.IRepositories.Stocks.Reports;
using ProSoft.EF.Models.Stocks;
using Shared;

namespace ProSoft.Core.Repositories.Stocks.Reports
{
    public class StockBalanceReportRepo : IStockBalanceReportRepo
    {
        private readonly AppDbContext _context;

        public StockBalanceReportRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<StockBalanceReportColumnDTO>> GetStockBalanceReportColumns(int BranchId, List<string> StockIds, DateTime FromDate, DateTime ToDate, int FYear, Filter? filter = null)
        {


            List<StockBalanceReportColumnDTO> stockBalanceReportColumnDTOs = new();
            var subItems = await _context.SubItems.ToListAsync();


            if (filter != null && !string.IsNullOrEmpty(filter.FromCode) && !string.IsNullOrEmpty(filter.ToCode))
            {
                subItems = subItems.Where(s =>
                    string.Compare( s.ItemCode,filter.FromCode, StringComparison.OrdinalIgnoreCase) >= 0 &&
                    string.Compare(s.ItemCode, filter.ToCode, StringComparison.OrdinalIgnoreCase) <= 0).ToList();
            }
            if (filter != null && !string.IsNullOrEmpty(filter.SearchByItemName))
            {
                subItems = subItems.Where(s => s.SubName.Contains(filter.SearchByItemName)).ToList();
            }

            foreach (var subItem in subItems)
            {

                StockBalanceReportColumnDTO stockBalanceReportColumnDTO = new();

                var tnxDtls = await _context.TransDtls.Where(t =>
                t.ItemMaster == subItem.ItemCode &&
                t.DocDate.Value.Date >= FromDate.Date &&
                t.DocDate.Value.Date <= ToDate.Date
                ).ToListAsync();
                foreach (var StockId in StockIds)
                {

                    foreach (var tnxDtl in tnxDtls)
                    {
                        if (tnxDtl?.StockCode != int.Parse(StockId)) continue;

                        var stock = await _context.Stocks.FirstOrDefaultAsync(s => s.Stkcod == tnxDtl.StockCode);
                        var generalCode = await _context.GeneralCodes.FirstOrDefaultAsync(g => g.UniqueType == tnxDtl.TransType);

                        stockBalanceReportColumnDTO.BranchId = BranchId;
                        stockBalanceReportColumnDTO.ItemCode = tnxDtl!.ItemMaster;
                        stockBalanceReportColumnDTO.ItemName = subItem!.SubName;
                        stockBalanceReportColumnDTO.StockName = stock!.Stknam;

                        if (generalCode.AddSub == 1)
                        {

                            stockBalanceReportColumnDTO.ItemQty += tnxDtl?.UnitQty ?? 0;
                            stockBalanceReportColumnDTO.ItemValue += tnxDtl?.ItemVal ?? 0;
                            stockBalanceReportColumnDTO.ItemPrice = (tnxDtl?.ItemVal / tnxDtl?.UnitQty) ?? 0;

                        }
                        else if (generalCode.AddSub == 2)
                        {
                            stockBalanceReportColumnDTO.ItemQty -= tnxDtl?.UnitQty ?? 0;
                            stockBalanceReportColumnDTO.ItemValue -= tnxDtl?.ItemVal ?? 0;
                            stockBalanceReportColumnDTO.ItemPrice = (tnxDtl?.ItemVal / tnxDtl?.UnitQty) ?? 0;
                        }



                    }



                }
                stockBalanceReportColumnDTOs.Add(stockBalanceReportColumnDTO);
            }

            return stockBalanceReportColumnDTOs;
        }

        public async Task<List<StockBalanceReportRowByStockDTO>> GetStockBalanceReportRowByStocks(int BranchId, List<string> StockIds, DateTime FromDate, DateTime ToDate, int FYear, Filter? filter = null)
        {
            List<StockBalanceReportRowByStockDTO> stockBalanceReportRowByStockDTOs = new();

            foreach (var StockId in StockIds)
            {
                int StockIdInt = int.Parse(StockId);

                var stock = await _context.Stocks.FirstOrDefaultAsync(s => s.Stkcod == StockIdInt);

                var subItems = await _context.SubItems.Where(s => s.BranchId == BranchId).ToListAsync();

                foreach (var subItem in subItems)
                {
                    StockBalanceReportRowByStockDTO stockBalanceReportRowByStockDTO = new();
                    stockBalanceReportRowByStockDTO.StockName = stock.Stknam;
                    var StkBal = await _context.Stkbalances
                        .FirstOrDefaultAsync(s => s.ItemCode == subItem.ItemCode && s.FYear == FYear && s.Stkcod == StockIdInt);



                    stockBalanceReportRowByStockDTO.ItemCode = subItem.ItemCode;
                    stockBalanceReportRowByStockDTO.ItemName = subItem.SubName;
                    stockBalanceReportRowByStockDTO.CarriedBalanceItemQty = StkBal?.QtyStart ?? 0;
                    stockBalanceReportRowByStockDTO.CurrentBalanceItemPrice = StkBal?.ItemPrice ?? 0;
                    stockBalanceReportRowByStockDTO.CarriedBalanceItemValue = StkBal?.ItemPrice2 ?? 0;

                    var tnxDtls = await _context.TransDtls.Where(t =>
                        t.ItemMaster == subItem.ItemCode &&
                        t.DocDate.Value.Date >= FromDate.Date &&
                        t.DocDate.Value.Date <= ToDate.Date &&
                        t.BranchId == BranchId &&
                        t.FYear == FYear &&
                        t.StockCode == StockIdInt
                        ).ToListAsync();

                    foreach (var tnxDtl in tnxDtls)
                    {
                        var GeneralCode = await _context.GeneralCodes.FirstOrDefaultAsync(g => g.UniqueType == tnxDtl.TransType);

                        if (GeneralCode != null && GeneralCode.AddSub == 1)
                        {

                            stockBalanceReportRowByStockDTO.InItemQty += tnxDtl?.UnitQty ?? 0;
                            stockBalanceReportRowByStockDTO.InItemPrice += tnxDtl?.Price ?? 0;
                            stockBalanceReportRowByStockDTO.InItemValue += tnxDtl?.ItemVal ?? 0;

                        }
                        else if ((GeneralCode != null && GeneralCode.AddSub == 2))
                        {

                            stockBalanceReportRowByStockDTO.OutItemQty += tnxDtl?.UnitQty ?? 0;
                            stockBalanceReportRowByStockDTO.OutItemPrice += tnxDtl?.Price ?? 0;
                            stockBalanceReportRowByStockDTO.OutItemValue += tnxDtl?.ItemVal ?? 0;

                        }

                    }

                    stockBalanceReportRowByStockDTO.CurrentBalanceItemQty = stockBalanceReportRowByStockDTO.CarriedBalanceItemQty + stockBalanceReportRowByStockDTO.InItemQty - stockBalanceReportRowByStockDTO.OutItemQty;
                    stockBalanceReportRowByStockDTO.CurrentBalanceItemPrice = stockBalanceReportRowByStockDTO.CarriedBalanceItemPrice + stockBalanceReportRowByStockDTO.InItemPrice - stockBalanceReportRowByStockDTO.OutItemPrice;
                    stockBalanceReportRowByStockDTO.CurrentBalanceItemValue = stockBalanceReportRowByStockDTO.CarriedBalanceItemValue + stockBalanceReportRowByStockDTO.InItemValue - stockBalanceReportRowByStockDTO.OutItemValue;

                    stockBalanceReportRowByStockDTOs.Add(stockBalanceReportRowByStockDTO);

                }
            }

            return stockBalanceReportRowByStockDTOs;
        }
        public async Task<List<StockBalanceReportRowByItemDTO>> GetStockBalanceReportRowByItems(int BranchId, List<string> StockIds, DateTime FromDate, DateTime ToDate, int FYear, Filter? filter = null)
        {
            List<StockBalanceReportRowByItemDTO> stockBalanceReportRowByItemDTOs = new();

            foreach (var StockId in StockIds)
            {
                int StockIdInt = int.Parse(StockId);

                var stock = await _context.Stocks.FirstOrDefaultAsync(s => s.Stkcod == StockIdInt);

                var subItems = await _context.SubItems.Where(s => s.BranchId == BranchId).ToListAsync();

                foreach (var subItem in subItems)
                {
                    StockBalanceReportRowByItemDTO stockBalanceReportRowByItemDTO = new();
                    stockBalanceReportRowByItemDTO.StockName = stock.Stknam;
                    var StkBal = await _context.Stkbalances
                        .FirstOrDefaultAsync(s => s.ItemCode == subItem.ItemCode && s.FYear == FYear && s.Stkcod == StockIdInt);



                    stockBalanceReportRowByItemDTO.ItemCode = subItem.ItemCode;
                    stockBalanceReportRowByItemDTO.ItemName = subItem.SubName;
                    stockBalanceReportRowByItemDTO.CarriedBalanceItemQty = StkBal?.QtyStart ?? 0;
                    stockBalanceReportRowByItemDTO.CurrentBalanceItemPrice = StkBal?.ItemPrice ?? 0;
                    stockBalanceReportRowByItemDTO.CarriedBalanceItemValue = StkBal?.ItemPrice2 ?? 0;

                    var tnxDtls = await _context.TransDtls.Where(t =>
                        t.ItemMaster == subItem.ItemCode &&
                        t.DocDate.Value.Date >= FromDate.Date &&
                        t.DocDate.Value.Date <= ToDate.Date &&
                        t.BranchId == BranchId &&
                        t.FYear == FYear &&
                        t.StockCode == StockIdInt
                        ).ToListAsync();

                    foreach (var tnxDtl in tnxDtls)
                    {
                        var GeneralCode = await _context.GeneralCodes.FirstOrDefaultAsync(g => g.UniqueType == tnxDtl.TransType);

                        if (GeneralCode != null && GeneralCode.AddSub == 1)
                        {

                            stockBalanceReportRowByItemDTO.InItemQty += tnxDtl?.UnitQty ?? 0;
                            stockBalanceReportRowByItemDTO.InItemPrice += tnxDtl?.Price ?? 0;
                            stockBalanceReportRowByItemDTO.InItemValue += tnxDtl?.ItemVal ?? 0;

                        }
                        else if ((GeneralCode != null && GeneralCode.AddSub == 2))
                        {

                            stockBalanceReportRowByItemDTO.OutItemQty += tnxDtl?.UnitQty ?? 0;
                            stockBalanceReportRowByItemDTO.OutItemPrice += tnxDtl?.Price ?? 0;
                            stockBalanceReportRowByItemDTO.OutItemValue += tnxDtl?.ItemVal ?? 0;

                        }

                    }

                    stockBalanceReportRowByItemDTO.CurrentBalanceItemQty = stockBalanceReportRowByItemDTO.CarriedBalanceItemQty + stockBalanceReportRowByItemDTO.InItemQty - stockBalanceReportRowByItemDTO.OutItemQty;
                    stockBalanceReportRowByItemDTO.CurrentBalanceItemPrice = stockBalanceReportRowByItemDTO.CarriedBalanceItemPrice + stockBalanceReportRowByItemDTO.InItemPrice - stockBalanceReportRowByItemDTO.OutItemPrice;
                    stockBalanceReportRowByItemDTO.CurrentBalanceItemValue = stockBalanceReportRowByItemDTO.CarriedBalanceItemValue + stockBalanceReportRowByItemDTO.InItemValue - stockBalanceReportRowByItemDTO.OutItemValue;

                    stockBalanceReportRowByItemDTOs.Add(stockBalanceReportRowByItemDTO);

                }
            }

            return stockBalanceReportRowByItemDTOs;

        }


    }
}

