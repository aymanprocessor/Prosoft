using Microsoft.EntityFrameworkCore;
using ProSoft.EF.DbContext;
using ProSoft.EF.DTOs.Stocks.Report.Request_Limit_Items_Report;
using ProSoft.EF.IRepositories.Stocks.Reports;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.Core.Repositories.Stocks.Reports
{
    public class RequestLimitItemsReportRepo: IRequestLimitItemsReportRepo
    {

        private readonly AppDbContext _context;
        private readonly ICurrentUserService _currentUserService;

        public RequestLimitItemsReportRepo(AppDbContext context, ICurrentUserService currentUserService)
        {
            _context = context;
            _currentUserService = currentUserService;
        }

        public async Task<List<RequestLimitItemsReportDTO>> GetRequestLimitItemsReport(int stockId, int BranchId, Filter filter)
        {
            List<RequestLimitItemsReportDTO> requestLimitItemsReportDTOs = new();
            var subItems = await _context.SubItems.Where(s => s.BranchId == BranchId).ToListAsync();

            if (filter != null && (filter.FromCode != null || filter.ToCode != null))
            {
                subItems = subItems.Where(s =>
                string.Compare(s.ItemCode, filter.FromCode, StringComparison.OrdinalIgnoreCase) >= 0 &&
                string.Compare(s.ItemCode, filter.ToCode, StringComparison.OrdinalIgnoreCase) <= 0

                ).ToList();
            }
            foreach (var subItem in subItems)
            {

                RequestLimitItemsReportDTO reportDTO = new();
               // var requestItem = await _context.ItmReorders.FirstOrDefaultAsync(i => i.BranchId == BranchId && i.ItemCd == subItem.ItemCode&& i.StoreId == stockId && i.ReordQty > 0);

                //if (requestItem == null) continue;

                //reportDTO.MaxRequestLimit = (decimal)requestItem.MaxQty;
               // reportDTO.MinRequestLimit = (decimal)requestItem.MinQty;
                reportDTO.RequestLimit = (decimal)subItem.LemitCall;
                reportDTO.ItemName = subItem.SubName;
                reportDTO.ItemCode = subItem.ItemCode;
                


                var stkBal = await _context.Stkbalances.FirstOrDefaultAsync(s =>
                s.ItemCode == subItem.ItemCode &&
                s.FYear == _currentUserService.Year &&
                s.Stkcod == stockId &&
                s.QtyStartDt == new DateTime(_currentUserService.Year, 1, 1)
                );

                if (stkBal != null && stkBal.QtyStart > 0)
                {

                    reportDTO.CurrentBalance += (decimal)stkBal!.QtyStart;
                }

                var tnxDtls = await _context.TransDtls.Where(t =>
                t.ItemMaster == subItem.ItemCode &&
                t.BranchId == BranchId &&
                t.StockCode == stockId &&
                t.DocDate.Value.Date >= new DateTime(_currentUserService.Year, 1, 1).Date &&
                (filter.ToDate != null && t.DocDate.Value.Date <= filter.ToDate.Value.Date)
                ).ToListAsync();

                foreach (var tnxDtl in tnxDtls)
                {
                    var generalCode = await _context.GeneralCodes.FirstOrDefaultAsync(g => g.UniqueType == tnxDtl.TransType);

                    if(generalCode.AddSub == 1)
                    {
                        reportDTO.CurrentBalance += tnxDtl?.UnitQty ?? 0;
                    }else if(generalCode.AddSub == 2)
                    {
                        reportDTO.CurrentBalance -= tnxDtl?.UnitQty ?? 0;

                    }



                }

                if(reportDTO.CurrentBalance < subItem.LemitCall)
                {
                    requestLimitItemsReportDTOs.Add(reportDTO);
                }
            }

            return requestLimitItemsReportDTOs;
        }
    }
}
