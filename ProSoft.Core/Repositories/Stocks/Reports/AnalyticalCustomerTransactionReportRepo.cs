using Microsoft.EntityFrameworkCore;
using ProSoft.EF.DbContext;
using ProSoft.EF.DTOs.Stocks.Report.Analytical_Customer_Transaction_Report;
using ProSoft.EF.IRepositories.Stocks.Reports;
using Shared;

namespace ProSoft.Core.Repositories.Stocks.Reports
{

    public class AnalyticalCustomerTransactionReportRepo : IAnalyticalCustomerTransactionReportRepo
    {
        private readonly AppDbContext _context;

        public AnalyticalCustomerTransactionReportRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<AnalyticalCustomerTransactionReportDTO>> GetAnalyticalCustomerTransactionReport(DateTime FromDate, DateTime ToDate,Filter filter)
        {

            List<AnalyticalCustomerTransactionReportDTO> analyticalCustomerTransactionReportDTOs = new();

            
            var tnxMstrs = await _context.TransMasters.Where(t =>
            t.DocDate.Value.Date >= FromDate.Date &&
            t.DocDate <= ToDate.Date &&
            t.TransType == 12
            ).ToListAsync();

            if(filter != null && !string.IsNullOrEmpty( filter.CustomerId))
            {
                tnxMstrs = tnxMstrs.Where(t => t.CustNo == filter.CustomerId).ToList();
            }

            AnalyticalCustomerTransactionReportDTO reportDTO = new();
            foreach (var tnxMstr in tnxMstrs)
            {
                var customer = await _context.CustCodes.FirstOrDefaultAsync(t => t.CustCode1 == tnxMstr.CustNo);
                if (customer != null) { reportDTO.CustomerName = customer.CustName; }

                reportDTO.InvoiceNo = (decimal)tnxMstr.DocNo;
                reportDTO.InvoiceDate = (DateTime)tnxMstr.DocDate;
                reportDTO.DueDate = (DateTime)tnxMstr.DueDate;
                reportDTO.SalesValue = (decimal)tnxMstr.DueValue;
                reportDTO.CashAmount = (decimal)tnxMstr.CashAmount;
                reportDTO.DueValue = (decimal)tnxMstr.DueValue - (decimal)tnxMstr.CashAmount;

            analyticalCustomerTransactionReportDTOs.Add(reportDTO);
            }
            return analyticalCustomerTransactionReportDTOs;
        }

    }
}
