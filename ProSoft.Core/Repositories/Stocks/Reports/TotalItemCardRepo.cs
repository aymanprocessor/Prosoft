using FastReport.Data;
using Microsoft.EntityFrameworkCore;
using ProSoft.EF.DbContext;
using ProSoft.EF.DTOs.Stocks.Report.TotalItemCards;
using ProSoft.EF.IRepositories.Stocks.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.Core.Repositories.Stocks.Reports
{
    public class TotalItemCardRepo : ITotalItemCardsRepo
    {
        private readonly AppDbContext _context;
        private readonly ICurrentUserService _currentUserService;

        public TotalItemCardRepo(AppDbContext context, ICurrentUserService currentUserService)
        {
            _context = context;
            _currentUserService = currentUserService;
        }

        public async Task<List<TotalItemCardQuantityReportDTO>> GetTotalItemCardQuantity(DateTime fromDate, DateTime toDate,int StockId, string? SearchByCode, string? SearchByName)
        {
            List<TotalItemCardQuantityReportDTO> totalItemCardQuantityReportDTOs = new();
            var subItems = await _context.SubItems.ToListAsync();
            if(!string.IsNullOrEmpty(SearchByName) || !string.IsNullOrEmpty(SearchByCode))
            {
                subItems = subItems.Where(s =>  s.ItemCode.Contains(SearchByCode) ||  s.SubName.Contains(SearchByName)).ToList();
            }
            var startDate = new DateTime(DateTime.Now.Year, 1, 1);

            foreach (var subItem in subItems)
            {
                TotalItemCardQuantityReportDTO reportDto = new();
                var stkBal = await _context.Stkbalances.FirstOrDefaultAsync(s => s.ItemCode == subItem.ItemCode && s.QtyStartDt.Value == new DateTime(_currentUserService.Year, 1, 1) && s.Stkcod == StockId);

               
                reportDto.CarriedBalance = (decimal)(stkBal?.QtyStart ?? 0);
                reportDto.ItemValue = (decimal)(stkBal?.ItemPrice2 ?? 0);
                reportDto.ItemCode = subItem.ItemCode;
                reportDto.ItemName = subItem.SubName;

                // Calculating Balance Before the Date Range
                var txnDtlsBefore = await _context.TransDtls
                                        .Where(t => t.DocDate.Value.Date >= startDate.Date && t.DocDate.Value.Date < fromDate.Date && t.ItemMaster == subItem.ItemCode && t.StockCode == StockId)
                                        .ToListAsync();

                foreach (var txn in txnDtlsBefore)
                {
                    var genCd = await _context.GeneralCodes.FirstOrDefaultAsync(g => g.UniqueType == txn.TransType);
                    if (genCd != null)
                    {
                        reportDto.CarriedBalance += genCd.AddSub == 1 ? (decimal)txn.UnitQty : -(decimal)txn.UnitQty;
                    }
                }

                // Calculating Total In and Out Quantities within the Date Range
                var txnDtlsInRange = await _context.TransDtls
                                         .Where(t => t.DocDate.Value.Date >= fromDate.Date && t.DocDate.Value.Date <= toDate.Date && t.ItemMaster == subItem.ItemCode && t.StockCode == StockId)
                                         .ToListAsync();

                reportDto.TotalOfInQuantity = 0;
                reportDto.TotalOfOutQuantity = 0;

                foreach (var txn in txnDtlsInRange)
                {
                    var genCd = await _context.GeneralCodes.FirstOrDefaultAsync(g => g.UniqueType == txn.TransType);
                    if (genCd != null)
                    {
                        if (genCd.AddSub == 1) // Total In Quantity
                        {
                            reportDto.TotalOfInQuantity += (decimal)txn.UnitQty;
                            reportDto.ItemValue += (decimal)txn.ItemVal;
                        }
                        else if (genCd.AddSub == 2) // Total Out Quantity
                        {
                            reportDto.TotalOfOutQuantity += (decimal)txn.UnitQty;
                            reportDto.ItemValue += (decimal)txn.ItemVal;
                        }
                    }
                }

                // Calculating Current Balance and Average Price
                reportDto.CurrentBalance = reportDto.CarriedBalance + reportDto.TotalOfInQuantity - reportDto.TotalOfOutQuantity;
                reportDto.AvgPrice = reportDto.CurrentBalance != 0 ? reportDto.ItemValue / reportDto.CurrentBalance : 0;

                totalItemCardQuantityReportDTOs.Add(reportDto);
            }
            return totalItemCardQuantityReportDTOs;
        }


        public async Task<List<TotalItemCardPriceReportDTO>> GetTotalItemCardPrice(DateTime fromDate, DateTime toDate, int StockId, string? SearchByCode, string? SearchByName)
        {
            List<TotalItemCardPriceReportDTO> totalItemCardPriceReportDTOs = new();
            var subItems = await _context.SubItems.ToListAsync();
            var startDate = new DateTime(DateTime.Now.Year, 1, 1);

            foreach (var subItem in subItems)
            {
                TotalItemCardPriceReportDTO reportDto = new();
                var stkBal = await _context.Stkbalances.FirstOrDefaultAsync(s => s.ItemCode == subItem.ItemCode && s.QtyStartDt.Value == new DateTime(_currentUserService.Year, 1, 1));

                
                  

                reportDto.TotalCarriedBalanceQty = (decimal)(stkBal?.QtyStart ?? 0);
                reportDto.TotalCarriedBalanceValue = (decimal)(stkBal?.ItemPrice2 ?? 0);
                reportDto.ItemCode = subItem.ItemCode;
                reportDto.ItemName = subItem.SubName;

                // Calculating Balance Before the Date Range
                var txnDtlsBefore = await _context.TransDtls
                                        .Where(t => t.DocDate.Value.Date >= startDate.Date && t.DocDate.Value.Date < fromDate.Date && t.ItemMaster == subItem.ItemCode)
                                        .ToListAsync();

                foreach (var txn in txnDtlsBefore)
                {
                    var genCd = await _context.GeneralCodes.FirstOrDefaultAsync(g => g.UniqueType == txn.TransType);
                    if (genCd != null)
                    {
                        reportDto.TotalCarriedBalanceQty += genCd.AddSub == 1 ? (decimal)txn.UnitQty : -(decimal)txn.UnitQty;
                        reportDto.TotalCarriedBalanceValue += genCd.AddSub == 1 ? (decimal)txn.ItemVal : -(decimal)txn.ItemVal;
                    }
                }

                reportDto.TotalCarriedBalanceAvgPrice = reportDto.TotalCarriedBalanceQty != 0 ? reportDto.TotalCarriedBalanceValue / reportDto.TotalCarriedBalanceQty : 0;

                var txnDtlsInRange = await _context.TransDtls
                                        .Where(t => t.DocDate.Value.Date >= fromDate.Date && t.DocDate.Value.Date <= toDate.Date && t.ItemMaster == subItem.ItemCode)
                                        .ToListAsync();

                reportDto.TotalInQty = 0;
                reportDto.TotalInValue = 0;
                reportDto.TotalInAvgPrice = 0;

                reportDto.TotalOutQty = 0;
                reportDto.TotalOutValue = 0;
                reportDto.TotalOutAvgPrice = 0;

                foreach (var txn in txnDtlsInRange)
                {
                    var genCd = await _context.GeneralCodes.FirstOrDefaultAsync(g => g.UniqueType == txn.TransType);
                    if (genCd != null)
                    {
                        if (genCd.AddSub == 1) // Total In Quantity
                        {
                            reportDto.TotalInQty += (decimal)txn.UnitQty;
                            reportDto.TotalInValue += (decimal)txn.ItemVal;
                        }
                        else if (genCd.AddSub == 2) // Total Out Quantity
                        {
                            reportDto.TotalOutQty += (decimal)txn.UnitQty;
                            reportDto.TotalOutValue += (decimal)txn.ItemVal;
                        }
                    }
                }

                reportDto.TotalInAvgPrice = reportDto.TotalInQty != 0 ? reportDto.TotalInValue / reportDto.TotalInQty : 0;
                reportDto.TotalOutAvgPrice = reportDto.TotalOutQty != 0 ? reportDto.TotalOutValue / reportDto.TotalOutQty : 0;

                reportDto.TotalCurrentBalanceQty = reportDto.TotalCarriedBalanceQty + reportDto.TotalInQty - reportDto.TotalOutQty;
                reportDto.TotalCurrentBalanceValue = reportDto.TotalCarriedBalanceValue + reportDto.TotalInValue - reportDto.TotalOutValue;

                reportDto.TotalCurrentBalanceAvgPrice = reportDto.TotalCurrentBalanceQty != 0 ? reportDto.TotalCurrentBalanceValue / reportDto.TotalCurrentBalanceQty : 0;

                totalItemCardPriceReportDTOs.Add(reportDto);
            }
            return totalItemCardPriceReportDTOs;
        }

        public async Task<List<TotalItemCardDetailReportDTO>> GetTotalItemCardDetail(DateTime fromDate, DateTime toDate, int StockId, string? SearchByCode, string? SearchByName)
        {
            List<TotalItemCardDetailReportDTO> totalItemCardDetailReportDTOs = new();
            var subItems = await _context.SubItems.ToListAsync();
            var startDate = new DateTime(DateTime.Now.Year, 1, 1);

            foreach (var subItem in subItems)
            {
                TotalItemCardDetailReportDTO reportDto = new();
                var stkBal = await _context.Stkbalances.FirstOrDefaultAsync(s => s.ItemCode == subItem.ItemCode && s.QtyStartDt.Value == new DateTime(_currentUserService.Year, 1, 1) && s.Stkcod == StockId);

                
                reportDto.CarriedBalance = (decimal)(stkBal?.QtyStart ?? 0);
                reportDto.ItemCode = subItem.ItemCode;
                reportDto.ItemName = subItem.SubName;

                var txnDtlsBefore = await _context.TransDtls
                                      .Where(t => t.DocDate.Value.Date >= startDate.Date && t.DocDate.Value.Date < fromDate.Date && t.ItemMaster == subItem.ItemCode && t.StockCode == StockId)
                                      .ToListAsync();

                foreach (var txn in txnDtlsBefore)
                {
                    var genCd = await _context.GeneralCodes.FirstOrDefaultAsync(g => g.UniqueType == txn.TransType);
                    if (genCd != null)
                    {
                        reportDto.CarriedBalance += genCd.AddSub == 1 ? (decimal)txn.UnitQty : -(decimal)txn.UnitQty;
                    }
                }

                var txnDtlsInRange = await _context.TransDtls
                                       .Where(t => t.DocDate.Value.Date >= fromDate.Date && t.DocDate.Value.Date <= toDate.Date && t.ItemMaster == subItem.ItemCode && t.StockCode == StockId)
                                       .ToListAsync();


                reportDto.InAdd = 0;
                reportDto.InReturn = 0;
                reportDto.InOther = 0;
                reportDto.OutAdd = 0;
                reportDto.OutReturn = 0;
                reportDto.OutOther = 0;

                foreach (var txn in txnDtlsInRange)
                {
                    var genCd = await _context.GeneralCodes.FirstOrDefaultAsync(g => g.UniqueType == txn.TransType);
                    if (genCd != null)
                    {
                        switch (txn.TransType)
                        {
                            case 2: // الوارد
                                reportDto.InAdd += (decimal)(txn?.UnitQty ?? 0);
                                break;
                            case 16: // مرتجع اضافة
                                reportDto.InReturn += (decimal)(txn?.UnitQty ?? 0);
                                break;
                            case 24: // اخرى اضافة
                                reportDto.InOther += (decimal)(txn?.UnitQty ?? 0);
                                break;
                            case 12: // المنصرف
                                reportDto.OutAdd += (decimal)(txn?.UnitQty ?? 0);
                                break;
                            case 0: // مرتجع بالخصم
                                reportDto.OutReturn += (decimal)(txn?.UnitQty ?? 0);
                                break;
                            case 26: // اخري منصرف 
                            case 6: // اخري منصرف 
                                reportDto.OutOther += (decimal)(txn?.UnitQty ?? 0);
                                break;

                        }
                    }
                }
                reportDto.InTotal = reportDto.InAdd + reportDto.InReturn + reportDto.InOther;
                reportDto.OutTotal = reportDto.OutAdd + reportDto.OutReturn + reportDto.OutOther;
                reportDto.CurrentBalance = reportDto.CarriedBalance + reportDto.InTotal - reportDto.OutTotal;
                totalItemCardDetailReportDTOs.Add(reportDto);

            }
            return totalItemCardDetailReportDTOs;

        }

    }
}
