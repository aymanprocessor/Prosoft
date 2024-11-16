using Microsoft.EntityFrameworkCore;
using ProSoft.EF.DbContext;
using ProSoft.EF.DTOs.Stocks.Report.Customer_Transaction;
using ProSoft.EF.IRepositories.Stocks.Reports;
using Shared;


namespace ProSoft.Core.Repositories.Stocks.Reports
{
    public class CustomerTransactionReportRepo : ICustomerTransactionReportRepo
    {
        private readonly AppDbContext _context;

        public CustomerTransactionReportRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<CustomerTransactionQuantityReportDTO>> GetCustomerTransactionQuantityReport(CustomerTransactionReportRequestDTO request, Filter filter)
        {
            List<CustomerTransactionQuantityReportDTO> customerTransactionQuantityReportDTOs = new();
            var tnxMstrs = await _context.TransMasters.Where(t =>
            t.StockCode == request.StockId &&
           t.DocDate.HasValue &&
           t.DocDate.Value.Date >= request.FromDate.Date &&
           t.DocDate.Value.Date <= request.ToDate.Date &&
         t.TransType == request.TransType
           ).ToListAsync();

            if (filter != null && !string.IsNullOrEmpty(filter.CustomerId))
            {
                tnxMstrs = tnxMstrs.Where(t => t.CustNo == filter.CustomerId).ToList();
            }




            foreach (var tnxMstr in tnxMstrs)
            {
                var tnxDtl = await _context.TransDtls.FirstOrDefaultAsync(t => t.StockCode == tnxMstr.StockCode &&
                t.DocNo == tnxMstr.DocNo &&
                t.TransType == tnxMstr.TransType &&
                t.DocDate == tnxMstr.DocDate
                    );

                CustomerTransactionQuantityReportDTO reportDTO = new();
                if (tnxDtl != null)
                {
                    var subItem = await _context.SubItems.FirstOrDefaultAsync(t => t.ItemCode == tnxDtl.ItemMaster);
                    var customer = await _context.CustCodes.FirstOrDefaultAsync(c => c.CustCode1 == tnxMstr.CustNo);


                    if (subItem != null)
                    {
                        reportDTO.ItemName = subItem?.SubName ?? string.Empty;
                        reportDTO.ItemCode = subItem?.ItemCode ?? string.Empty;

                    }

                    if (customer != null)
                    {
                        reportDTO.CustomerName = customer?.CustName ?? string.Empty;

                    }
                    reportDTO.TransNo = (decimal)tnxMstr?.DocNo;
                    reportDTO.TransDate = tnxMstr?.DocDate?.Date.ToString("yyyy-MM-dd");
                    reportDTO.Quantity = (decimal)tnxDtl?.UnitQty;


                }
                customerTransactionQuantityReportDTOs.Add(reportDTO);

            }
            return customerTransactionQuantityReportDTOs;
        }

        public async Task<List<CustomerTransactionValueReportDTO>> GetCustomerTransactionValueReport(CustomerTransactionReportRequestDTO request, Filter filter)
        {
            List<CustomerTransactionValueReportDTO> customerTransactionValueReportDTOs = new();
            var tnxMstrs = await _context.TransMasters.Where(t =>
            t.StockCode == request.StockId &&
           t.DocDate.HasValue &&
           t.DocDate.Value.Date >= request.FromDate.Date &&
           t.DocDate.Value.Date <= request.ToDate.Date &&
         t.TransType == request.TransType
           ).ToListAsync();

            if (filter != null && !string.IsNullOrEmpty(filter.CustomerId))
            {
                tnxMstrs = tnxMstrs.Where(t => t.CustNo == filter.CustomerId).ToList();
            }




            foreach (var tnxMstr in tnxMstrs)
            {
                var tnxDtl = await _context.TransDtls.FirstOrDefaultAsync(t => t.StockCode == tnxMstr.StockCode &&
                t.DocNo == tnxMstr.DocNo &&
                t.TransType == tnxMstr.TransType &&
                t.DocDate == tnxMstr.DocDate
                    );

                CustomerTransactionValueReportDTO reportDTO = new();
                if (tnxDtl != null)
                {
                    var subItem = await _context.SubItems.FirstOrDefaultAsync(t => t.ItemCode == tnxDtl.ItemMaster);
                    var customer = await _context.CustCodes.FirstOrDefaultAsync(c => c.CustCode1 == tnxMstr.CustNo);

                    if(subItem != null)
                    {
                        reportDTO.ItemName = subItem?.SubName ?? string.Empty;
                        reportDTO.ItemCode = subItem?.ItemCode ?? string.Empty;

                    }

                    if (customer != null)
                    {
                        reportDTO.CustomerName = customer?.CustName ?? string.Empty;

                    }
                    reportDTO.TransNo = (decimal)tnxMstr?.DocNo;
                    reportDTO.TransDate = tnxMstr?.DocDate?.Date.ToString("yyyy-MM-dd");
                    reportDTO.Quantity = ((decimal)tnxDtl?.UnitQty);
                    reportDTO.Value = (decimal)tnxDtl?.ItemVal;
                    reportDTO.Price = (decimal)tnxDtl?.ItemVal / (decimal)tnxDtl?.UnitQty;


                }
                customerTransactionValueReportDTOs.Add(reportDTO);

            }
            return customerTransactionValueReportDTOs;
        }
    }
}
