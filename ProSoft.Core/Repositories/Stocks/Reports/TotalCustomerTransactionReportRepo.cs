using ProSoft.EF.DbContext;
using ProSoft.EF.DTOs.Stocks.Report.Total_Customer_Transaction;
using ProSoft.EF.IRepositories.Stocks.Reports;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.Core.Repositories.Stocks.Reports
{
    public class TotalCustomerTransactionReportRepo : ITotalCustomerTransactionReportRepo
    {
        private readonly AppDbContext _context;

        public TotalCustomerTransactionReportRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TotalCustomerTransactionReportDTO>> GetTotalCustomerTransactionReport(TotalCustomerTransactionReportRequestDTO request, Filter filter)
        {
            List<TotalCustomerTransactionReportDTO> totalCustomerTransactionReportDTOs = new();
            var transTypes = new[] { 2, 4 };
            var tnxMstrs =  _context.TransMasters.Where(
                t => t.DocDate >= request.FromDate &&
                t.DocDate <= request.ToDate &&
               transTypes.Contains( (int)t.TransType)
                )
                .ToList();

            TotalCustomerTransactionReportDTO reportDTO = new();
            foreach (var tnxMstr in tnxMstrs)
            {
                var generalCode = _context.GeneralCodes.FirstOrDefault(g => g.UniqueType == tnxMstr.TransType);
                if (generalCode.AddSub == 1)
                {
                    reportDTO.NetSales += (decimal)tnxMstr?.TotTransVal;
                    reportDTO.DueAmount += (decimal)tnxMstr?.DueValue;
                }

                else if (generalCode.AddSub == 2)
                {
                    reportDTO.NetSales -= (decimal)tnxMstr?.TotTransVal;
                    reportDTO.DueAmount -= (decimal)tnxMstr?.DueValue;
                }
                reportDTO.InvoiceCount++;

            }
          

           

            reportDTO.PaymentAmount = reportDTO.NetSales - reportDTO.DueAmount;
            totalCustomerTransactionReportDTOs.Add(reportDTO);

            return totalCustomerTransactionReportDTOs;
        }
    }
}
