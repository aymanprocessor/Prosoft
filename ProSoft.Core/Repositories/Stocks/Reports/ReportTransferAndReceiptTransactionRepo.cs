using ProSoft.EF.DbContext;
using ProSoft.EF.DTOs.Stocks.Report.TransferAndReceiptTransactionReport;
using ProSoft.EF.IRepositories.Stocks.Reports;
using ProSoft.EF.Models.Shared;
using ProSoft.EF.Models.Stocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.Core.Repositories.Stocks.Reports
{
    public class ReportTransferAndReceiptTransactionRepo : IReportTransferAndReceiptTransactionRepo
    {
        private readonly AppDbContext _context;

        public ReportTransferAndReceiptTransactionRepo(AppDbContext Context)
        {
            _context = Context;
        }

        public IEnumerable<TransferAndReceiptTransactionReportDTO> GetReport(TransferAndReceiptTransactionReportRequestDTO model)
        {
            // Step 1: Filter TransDtls early to reduce the number of records to process in joins
            var filteredTransDtls = _context.TransDtls
                .Where(transDtl =>
                    transDtl.TransType == model.UniqueType &&
                    transDtl.DocDate >= model.FromDate &&
                    transDtl.DocDate <= model.ToDate)
                .ToList(); // Force evaluation here to minimize further processing in memory

            // Step 2: Perform joins with filtered data to create the desired result set
            var query = filteredTransDtls
                .Join(_context.GeneralCodes,
                    transDtl => transDtl.TransType,
                    generalCode => generalCode.UniqueType,
                    (transDtl, generalCode) => new { transDtl, generalCode })
                .Join(_context.SubItems,
                    combined => new {X1= combined.transDtl.MainCode, X2=combined.transDtl.ItemMaster },
                    subItem => new { X1=subItem.MainCode,X2= subItem.ItemCode },
                    (combined, subItem) => new { combined.transDtl, combined.generalCode, subItem })
                .Join(_context.UnitCodes,
                    combined => combined.transDtl.UnitCode,
                    unitCode => unitCode.Code,
                    (combined, unitCode) => new { combined.transDtl, combined.generalCode, combined.subItem, unitCode })
                .Join(_context.TransMasters,
                    combined => new { combined.transDtl.SerSys, combined.transDtl.TransType, combined.transDtl.StockCode, combined.transDtl.FYear, combined.transDtl.BranchId },
                    transMaster => new { transMaster.SerSys, transMaster.TransType, transMaster.StockCode, transMaster.FYear, transMaster.BranchId },
                    (combined, transMaster) => new { combined.transDtl, combined.generalCode, combined.subItem, combined.unitCode, transMaster })
                .Where(combined =>
                    combined.subItem.BranchId == model.BranchId &&
                    combined.transMaster.StockCode == model.FromStock &&
                    combined.transMaster.StockCode2 == model.ToStock &&
                    combined.unitCode.Flag1 == 1 &&
                    (string.IsNullOrEmpty(model.SearchSubName) || combined. subItem.SubName.Contains(model.SearchSubName))&&
                    (string.IsNullOrEmpty(model.SearchItemMaster) || combined.transDtl.ItemMaster.Contains(model.SearchItemMaster))&&
                    ((string.IsNullOrEmpty(model.SearchFromItemMaster) && string.IsNullOrEmpty(model.SearchToItemMaster ))|| 
                    string.Compare(combined.transDtl.ItemMaster, model.SearchFromItemMaster, StringComparison.Ordinal) >=0 &&
                    string.Compare(combined.transDtl.ItemMaster, model.SearchToItemMaster, StringComparison.Ordinal) <=0 )

                    )
                //filter by model.SearchItemName if not null
                .Select((combined, index) => new TransferAndReceiptTransactionReportDTO
                {
                    SeqCode = index + 1,  // Sequence code starting from 1
                    DocNo = (int)combined.transDtl.DocNo,
                    DocDate = combined.transDtl.DocDate.Value.ToString("dd/MM/yyyy"),
                    UnitQty = (int)combined.transDtl.UnitQty,
                    SubName = combined.subItem.SubName,
                    Price = combined.transDtl.Price,
                    ItemVal = combined.transDtl.ItemVal,
                    ItemMaster = combined.transDtl.ItemMaster,
                    UnitName = combined.unitCode.Names
                })
                .ToList();


            return query;
        }
    }
}
