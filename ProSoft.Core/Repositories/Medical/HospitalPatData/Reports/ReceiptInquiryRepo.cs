using System;
using System.Collections.Generic;
using System.Linq;
using ProSoft.EF.DbContext;
using ProSoft.EF.DTOs.Medical.HospitalPatData.Reports;
using ProSoft.EF.IRepositories.Medical.HospitalPatData;
using ProSoft.EF.Models.Medical.HospitalPatData;
using ProSoft.EF.Models.Stocks;

namespace ProSoft.Core.Repositories.Medical.HospitalPatData.Reports;

public class ReceiptInquiryRepo : IReceiptInquiryRepo
{
    private readonly AppDbContext _context;
    public ReceiptInquiryRepo( AppDbContext context)
    {
        _context = context;
    }
    public IEnumerable<ReceiptInquiryReportDTO> GetReceiptInquiryReport(int arg_inv_n, int arg_year, int arg_br)

    { 

        // First query (clinic_trans and related tables)
        var query1 = from ct in _context.ClinicTrans
                     join p in _context.Pats
                         on new { x1 = ct.PatId, x2 = ct.BranchId } equals new { x1 = (int?)p.PatId, x2 = (int?)p.BranchId } // Inner join
                     where ct.ExInvoiceNo == arg_inv_n && ct.ExYear == arg_year && ct.BranchId == arg_br
                     select new
                     {
                         ct.ExDate,
                         ITM_SERV_FLAG = (int?)ct.ItmServFlag,
                         ct.ExInvoiceNo,
                         ct.CompId,
                         ct.ItemMaster,
                         ct.UserCodeModify,
                         ct.UserCodeCreate,
                         CheckIdCancel = (int?)ct.CheckIdCancel,
                         Flag = (int?)ct.Flag,
                         ct.PatAdDate,
                         ct.PatId,
                         p.PatName,
                         Deal = (int?)ct.Deal
                     };

        // Second query (DEPOSIT and related tables)
        var query2 = from d in _context.Deposits
                     join pa in _context.PatAdmissions
                         on new { x1 = d.MasterId, x2 = d.FYear, x3 = d.BranchId, x4 = d.ExchangeType } equals new { x1 = (int?)pa.MasterId, x2 = (int?)pa.ExYear, x3 = (int?)pa.BranchId, x4 = (int?)pa.ExchangeType } // Inner join
                     join p in _context.Pats
                         on new { x1 = pa.PatId, x2 = pa.BranchId } equals new { x1 = (int?)p.PatId, x2 = (decimal?)p.BranchId } // Inner join
                     where d.SafeDocNo == arg_inv_n && d.FYear == arg_year && d.BranchId == arg_br
                     select new
                     {
                         ExDate = d.DpsDate,
                         ITM_SERV_FLAG = (int?)0,
                         ExInvoiceNo = d.SafeDocNo,
                         pa.CompId,
                         ItemMaster = "0",
                         UserCodeModify = d.UserCode,
                         UserCodeCreate = d.UserCode,
                         CheckIdCancel = (int?)1,
                         Flag = (int?)1,
                         PatAdDate = d.DpsDate,
                         pa.PatId,
                         p.PatName,
                         Deal = (int?)pa.Deal
                     };

        var query1List = query1.ToList();
        var query2List = query2.ToList();
        // Combine both queries using UNION ALL
        var finalResult = query1List.Concat(query2List).ToList();

        // Convert the final result to the desired DTO
        var result = finalResult.Select(x => new ReceiptInquiryReportDTO
        {
            
            ExDate = x.ExDate,
            ItmServFlag= x.ITM_SERV_FLAG,
            ExInvoiceNo = x.ExInvoiceNo,
            UserCodeModify = x.UserCodeModify,
            UserCodeCreate = x.UserCodeCreate,
            PatId = x.PatId,
            PatName = x.PatName,
            Deal = x.Deal

        });

        return result;
    }
}