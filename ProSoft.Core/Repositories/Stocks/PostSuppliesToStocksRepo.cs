using Microsoft.EntityFrameworkCore;
using ProSoft.EF.DbContext;
using ProSoft.EF.DTOs.Stocks.TransferSuppliesToStocks;
using ProSoft.EF.IRepositories.Stocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.Core.Repositories.Stocks
{
    public class PostSuppliesToStocksRepo: IPostSuppliesToStocksRepo
    {
        private readonly AppDbContext _context;

        public PostSuppliesToStocksRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<TransferSuppliesToStocksMasterDTO>> GetPatAdmissions(int branchId,int RegionId,DateTime FromDate,DateTime ToDate)
        {
            List<TransferSuppliesToStocksMasterDTO> transferSuppliesToStocksMasterDTOs = new();
            var patAdmissions =  _context.PatAdmissions.Where(p => 
            p.BranchId == branchId &&
            p.PatDateExit.Value.Date >= FromDate.Date &&
            p.PatDateExit.Value.Date <= ToDate.Date &&
            p.SendFr == RegionId &&
            p.PostId == null
            ).ToList();

            foreach (var patAdmission in patAdmissions)
            {
                TransferSuppliesToStocksMasterDTO reportDTO = new();
                var Doctor = await _context.Doctors.FirstOrDefaultAsync(d => d.DrId == patAdmission.DrCode && d.BranchId == branchId);
                var Pat = await _context.Pats.FirstOrDefaultAsync(p => p.PatId == patAdmission.PatId && p.BranchId == branchId);
                var Company = await _context.Companies.FirstOrDefaultAsync(c => c.CompId == patAdmission.CompId && c.BranchId == branchId);
                var ClinicTnx = await _context.ClinicTrans.FirstOrDefaultAsync(c =>
                c.MasterId == patAdmission.MasterId &&
                c.BranchId == branchId &&
                c.ExYear == patAdmission.ExYear &&
                c.Flag == patAdmission.Flag &&
                c.ExchangeType == patAdmission.ExchangeType &&
                c.PipeFlag == null && 
                c.ItmServFlag == 2);

                reportDTO.Date = patAdmission?.PatAdDate?.Date;
                reportDTO.CompanyName = Company?.CompName;
                reportDTO.PatientName = Pat?.PatName;
                reportDTO.DoctorName = Doctor?.DrDesc;
                reportDTO.ExitDate = patAdmission?.PatDateExit?.Date;
                reportDTO.ExitStatus = patAdmission?.PatClose;
                reportDTO.InvoiceTotal = patAdmission?.MainInvTot;
                reportDTO.InvoiceNo = patAdmission?.MainInvNo;




                transferSuppliesToStocksMasterDTOs.Add(reportDTO);

            }

            return transferSuppliesToStocksMasterDTOs;

        }
    }
}
