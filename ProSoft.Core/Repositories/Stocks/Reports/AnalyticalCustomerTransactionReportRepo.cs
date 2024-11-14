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

        public async Task<List<AnalyticalCustomerTransactionReportDTO>> GetAnalyticalCustomerTransactionReport(DateTime FromDate, DateTime ToDate, Filter filter)
        {

            List<AnalyticalCustomerTransactionReportDTO> analyticalCustomerTransactionReportDTOs = new();

            var transTypes = new[] { 12 };

            var tnxMstrs = await _context.TransMasters.Where(t =>
            t.DocDate.HasValue &&
            t.DocDate.Value.Date >= FromDate.Date &&
            t.DocDate.Value.Date <= ToDate.Date &&
            transTypes.Contains((int)t.TransType)
            ).ToListAsync();

            if (filter != null && !string.IsNullOrEmpty(filter.CustomerId))
            {
                tnxMstrs = tnxMstrs.Where(t => t.CustNo == filter.CustomerId).ToList();
            }

            
            foreach (var tnxMstr in tnxMstrs)
            {
                AnalyticalCustomerTransactionReportDTO reportDTO = new();
                var customer = await _context.CustCodes.FirstOrDefaultAsync(t => t.CustCode1 == tnxMstr.CustNo);
                if (customer != null) { reportDTO.CustomerName = customer.CustName; }

                reportDTO.InvoiceNo = (decimal)tnxMstr?.DocNo;
                reportDTO.InvoiceDate = tnxMstr?.DocDate?.Date.ToString("yyyy-MM-dd");
                reportDTO.DueDate = tnxMstr?.DueDate?.Date.ToString("yyyy-MM-dd") ?? null;
                reportDTO.SalesValue = (decimal)tnxMstr?.DueValue;
                reportDTO.CashAmount = (decimal)tnxMstr?.CashAmount;
                reportDTO.DueValue = (decimal)tnxMstr.DueValue - (decimal)tnxMstr.CashAmount;

                analyticalCustomerTransactionReportDTOs.Add(reportDTO);
            }
            return analyticalCustomerTransactionReportDTOs;
        }

    }
}
