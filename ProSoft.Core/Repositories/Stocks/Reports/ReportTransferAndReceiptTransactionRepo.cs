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
            var query = from generalCode in _context.GeneralCodes
                        join transDtl in _context.TransDtls on generalCode.UniqueType equals transDtl.TransType
                        join subItem in _context.SubItems on new { X1 = transDtl.MainCode, X2 = transDtl.ItemMaster } equals new { X1 = subItem.MainCode, X2 = subItem.ItemCode }
                        join unitCode in _context.UnitCodes on transDtl.UnitCode equals unitCode.Code
                        join transMaster in _context.TransMasters on new { transDtl.SerSys, transDtl.TransType, transDtl.StockCode, transDtl.FYear, transDtl.BranchId } equals new { transMaster.SerSys, transMaster.TransType, transMaster.StockCode, transMaster.FYear, transMaster.BranchId }
                        join stockA in _context.Stocks on (int)transMaster.StockCode equals stockA.Stkcod
                        join stockB in _context.Stocks on (int)transMaster.StockCode2 equals stockB.Stkcod
                        where subItem.BranchId == model.BranchId
                            && transMaster.StockCode == model.FromStock
                            && transMaster.StockCode2 == model.ToStock
                            && transDtl.TransType == model.TransType
                            && transDtl.DocDate >= model.FromDate
                            && transDtl.DocDate <= model.ToDate
                            && unitCode.Flag1 == 1
                        select new TransferAndReceiptTransactionReportDTO
                        {
                            DocNo = (int)transDtl.DocNo,
                            DocDate = transDtl.DocDate,
                            UnitQty = transDtl.UnitQty,
                            SubName = subItem.SubName,
                            Price = transDtl.Price,
                            ItemVal = transDtl.ItemVal,
                            ItemMaster = transDtl.ItemMaster,
                            UnitNmae = unitCode.Names
                      
                        };
          
            return query;
        }
    }
}
