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

        public async Task<List<TotalItemCardQuantityReportDTO>> GetTotalItemCardQuantity(DateTime fromDate, DateTime toDate)
        {
            List<TotalItemCardQuantityReportDTO> totalItemCardQuantityReportDTOs = new();
            var SubItems = await _context.SubItems.ToListAsync();

            foreach (var SubItem in SubItems)
            {
                TotalItemCardQuantityReportDTO totalItemCardQuantityReportDTO = new();
                var stkBal = await _context.Stkbalances.FirstOrDefaultAsync(s => s.ItemCode == SubItem.ItemCode && s.QtyStartDt.Value == new DateTime(_currentUserService.Year, 1, 1));
                totalItemCardQuantityReportDTO.BalanceBefore = (decimal)stkBal?.QtyStart!;
                totalItemCardQuantityReportDTO.ItemValue =  (decimal)stkBal.ItemPrice2!;
                totalItemCardQuantityReportDTO.ItemCode  =  SubItem.ItemCode!;
                totalItemCardQuantityReportDTO.ItemName  =  SubItem.SubName!;
                totalItemCardQuantityReportDTO.ItemValue =  (decimal)stkBal.ItemPrice2!;

                // ----- رصيد ما قبله ----- //
                var startDate = new DateTime(DateTime.Now.Year, 1, 1);

                var txnDtls =  await _context.TransDtls.Where(t =>
                t.DocDate >= startDate &&
                t.DocDate < fromDate
                ).ToListAsync();


                foreach (var Txn in txnDtls)
                {
                    var genCd = await _context.GeneralCodes.FirstOrDefaultAsync(g => g.UniqueType == Txn.TransType);
                    if(genCd != null && genCd.AddSub == 1)
                    {
                        totalItemCardQuantityReportDTO.BalanceBefore += (decimal)Txn.UnitQty!;
                    }
                    else if(genCd != null && genCd.AddSub == 2)
                    {
                        totalItemCardQuantityReportDTO.BalanceBefore -= (decimal)Txn.UnitQty!;

                    }

                }


                
                txnDtls = await _context.TransDtls.Where(t =>
                t.DocDate >= fromDate &&
                t.DocDate <= toDate 
                ).ToListAsync();

                totalItemCardQuantityReportDTO.TotalOfInQuantity = 0;
                totalItemCardQuantityReportDTO.TotalOfOutQuantity = 0;
                foreach (var Txn in txnDtls)
                {
                    
                    var genCd = await _context.GeneralCodes.FirstOrDefaultAsync(g => g.UniqueType == Txn.TransType);
                    if (genCd != null && genCd.AddSub == 1)// ----- اجمالي الوارد ----- //
                    {
                        totalItemCardQuantityReportDTO.TotalOfInQuantity += (decimal)Txn.UnitQty!;
                        totalItemCardQuantityReportDTO.ItemValue += (decimal)Txn.ItemVal!;
                    }
                    else if (genCd != null && genCd.AddSub == 2)// ----- اجمالي المنصرف ----- //
                    {
                        totalItemCardQuantityReportDTO.TotalOfOutQuantity += (decimal)Txn.UnitQty!;
                        totalItemCardQuantityReportDTO.ItemValue -= (decimal)Txn.ItemVal!;


                    }

                }

                totalItemCardQuantityReportDTO.CurrentBalance = totalItemCardQuantityReportDTO.BalanceBefore + totalItemCardQuantityReportDTO.TotalOfInQuantity - totalItemCardQuantityReportDTO.TotalOfOutQuantity;
                totalItemCardQuantityReportDTO.AvgPrice = totalItemCardQuantityReportDTO.ItemValue / totalItemCardQuantityReportDTO.CurrentBalance;

                totalItemCardQuantityReportDTOs.Add(totalItemCardQuantityReportDTO);

            }
            return totalItemCardQuantityReportDTOs;
        }
    }
}
