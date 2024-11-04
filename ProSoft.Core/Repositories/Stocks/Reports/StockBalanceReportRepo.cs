using Microsoft.EntityFrameworkCore;
using ProSoft.EF.DbContext;
using ProSoft.EF.DTOs.Stocks.Report.StockBalance;
using ProSoft.EF.IRepositories.Stocks.Reports;
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

        public async Task<List<StockBalanceReportColumnDTO>> GetStockBalanceReportColumns(int BranchId, List<string> StockIds, DateTime FromDate, DateTime ToDate, Filter? filter = null)
        {
            List<StockBalanceReportColumnDTO> stockBalanceReportColumnDTOs = new();
            var tnxDtls = await _context.TransDtls.Where(t =>
            t.DocDate.Value.Date >= FromDate.Date &&
            t.DocDate.Value.Date <= ToDate.Date
            ).ToListAsync();

            foreach (var tnxDtl in tnxDtls)
            {
                StockBalanceReportColumnDTO stockBalanceReportColumnDTO = new();
                foreach (var StockId in StockIds)
                {
                    if (tnxDtl?.StockCode != int.Parse(StockId)) continue;

                    var subItem = await _context.SubItems.FirstOrDefaultAsync(s => s.ItemCode == tnxDtl.ItemMaster);
                    var stock = await _context.Stocks.FirstOrDefaultAsync(s => s.Stkcod == tnxDtl.StockCode);

                    stockBalanceReportColumnDTO.BranchId = BranchId;
                    stockBalanceReportColumnDTO.ItemCode = tnxDtl!.ItemMaster;
                    stockBalanceReportColumnDTO.ItemName = subItem!.SubName;
                    stockBalanceReportColumnDTO.StockName = stock!.Stknam;
                    stockBalanceReportColumnDTO.ItemQty = tnxDtl?.UnitQty ?? 0;
                    stockBalanceReportColumnDTO.ItemPrice = tnxDtl?.Price ?? 0;
                    stockBalanceReportColumnDTO.ItemValue = tnxDtl?.ItemVal ?? 0;
                    stockBalanceReportColumnDTO.SumItemQty += tnxDtl?.UnitQty ?? 0;
                    stockBalanceReportColumnDTO.SumItemValue += tnxDtl?.Price ?? 0;


                }



                stockBalanceReportColumnDTOs.Add(stockBalanceReportColumnDTO);
            }

            return stockBalanceReportColumnDTOs;
        }
    }
}

