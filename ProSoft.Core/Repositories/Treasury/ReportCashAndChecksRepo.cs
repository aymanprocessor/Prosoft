using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProSoft.EF.DbContext;
using ProSoft.EF.DTOs.Accounts;
using ProSoft.EF.DTOs.Auth;
using ProSoft.EF.DTOs.Shared;
using ProSoft.EF.DTOs.Treasury;
using ProSoft.EF.DTOs.Treasury.Report;
using ProSoft.EF.IRepositories.Treasury;
using ProSoft.EF.Models;
using ProSoft.EF.Models.Accounts;
using ProSoft.EF.Models.Shared;
using ProSoft.EF.Models.Treasury;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.Core.Repositories.Treasury
{
    public class ReportCashAndChecksRepo : IReportCashAndChecksRepo
    {
        private readonly AppDbContext  _Context;
        private readonly IMapper _mapper;
        public ReportCashAndChecksRepo(AppDbContext Context, IMapper mapper)
        {
            _Context = Context;
            _mapper = mapper;
        }
        public async Task<ReportCashAndChecksDTO> GetAllDataAsync(int userCode)
        {
            ReportCashAndChecksDTO reportCashAndChecksDTO = new ReportCashAndChecksDTO();

            var safeCode = (await _Context.userCashNos.FirstOrDefaultAsync(obj => obj.UserCashID == userCode)).SafeCode;
            var safeName = (await _Context.SafeNames.FindAsync(safeCode)).SafeNames;


            List<Branch> branches = await _Context.Branchs.ToListAsync();
            List<AppUser> users = await _Context.Users.ToListAsync();

            reportCashAndChecksDTO.branchDTOs = _mapper.Map<List<BranchDTO>>(branches);
            reportCashAndChecksDTO.userDTOs = _mapper.Map<List<UserDTO>>(users);
            reportCashAndChecksDTO.SafeNames = safeName;
            reportCashAndChecksDTO.SafeCode = safeCode;

            return reportCashAndChecksDTO;
        }

        public async Task<List<CashTreasuryDataDTO>> GetCashTreasuryData(int branchId, int userCode, int safeCode, int? fromReceipt, int? toReceipt, DateTime? fromPeriod, DateTime? toPeriod)
        {
            var cashTreasuryDataDTOs = new List<CashTreasuryDataDTO>();
            if (fromReceipt != null && toReceipt != null)
            {
                 cashTreasuryDataDTOs = await _Context.AccSafeCashes.Where(obj=>(obj.DocType=="SFCIN" || obj.DocType == "SFCOT") &&
                      (branchId == 100 || obj.BranchId == branchId) && (userCode == 100 || obj.UserCode == userCode) && obj.SafeCode==safeCode && (obj.DocNo>= fromReceipt && obj.DocNo<= toReceipt))
                    .Select(obj => new CashTreasuryDataDTO()
                      {
                          Expense = obj.DocType == "SFCOT" ? obj.ValuePay : 0 ,
                          Revenue = obj.DocType == "SFCIN" ? (obj.ValuePay + (obj.ProfitTax ?? 0)) : 0 ,
                          DocNo = obj.DocNo,
                          DocDate = obj.DocDate,
                          AccTransNo = obj.AccTransNo,
                          PersonName = obj.PersonName,
                          Commentt = obj.Commentt,
                          CshOrdNum = obj.CshOrdNum,
                      }).ToListAsync();
            }
            else if (fromPeriod != null && toPeriod != null)
            {
                   cashTreasuryDataDTOs = await _Context.AccSafeCashes.Where(obj=>(obj.DocType=="SFCIN" || obj.DocType == "SFCOT") &&
                      (branchId == 100 || obj.BranchId == branchId) && (userCode == 100 || obj.UserCode == userCode) && obj.SafeCode == safeCode && (obj.DocDate>= fromPeriod && obj.DocDate<= toPeriod))
                    .Select(obj => new CashTreasuryDataDTO()
                      {
                          Expense = obj.DocType == "SFCOT" ? obj.ValuePay : 0 ,
                          Revenue = obj.DocType == "SFCIN" ? (obj.ValuePay + (obj.ProfitTax ?? 0)) : 0 ,
                          DocNo = obj.DocNo,
                          DocDate = obj.DocDate,
                          AccTransNo = obj.AccTransNo,
                          PersonName = obj.PersonName,
                          Commentt = obj.Commentt,
                          CshOrdNum = obj.CshOrdNum,
                      }).ToListAsync();
            }

            return cashTreasuryDataDTOs;
        }

        public async Task<List<FollowChecksDataDTO>> GetFollowChecksData(int branchId, int userCode, int safeCode, int? fromReceipt, int? toReceipt, DateTime? fromPeriod, DateTime? toPeriod)
        {
            var followChecksDataDTOs = new List<FollowChecksDataDTO>();
            if (fromReceipt != null && toReceipt != null)
            {
                followChecksDataDTOs = await _Context.AccSafeChecks.Where(obj => (obj.TranType == "SFSIN" || obj.TranType == "SFOUT") &&
                     (branchId == 100 || obj.BranchId == branchId) && (userCode == 100 || obj.UserCode == userCode) && obj.SafeCode == safeCode && (obj.DocNo >= fromReceipt && obj.DocNo <= toReceipt))
                   .Select(obj => new FollowChecksDataDTO()
                   {
                       Credit = obj.TranType == "SFSIN" ? obj.ValuePay : 0,
                       Debit = obj.TranType == "SFOUT" ? obj.ValuePay : 0,
                       DocNo = obj.DocNo,
                       DocDate = obj.DocDate,
                       ChekNo = obj.ChekNo,
                       SattlDate = obj.SattlDate,
                       CheckStatus = obj.CheckStatus,
                       ChekDate = obj.ChekDate,
                       PersonName = obj.PersonName,
                       AccName = obj.AccName,
                   }).ToListAsync();
            }
            else if (fromPeriod != null && toPeriod != null)
            {
                followChecksDataDTOs = await _Context.AccSafeChecks.Where(obj => (obj.TranType == "SFSIN" || obj.TranType == "SFOUT") &&
                   (branchId == 100 || obj.BranchId == branchId) && (userCode == 100 || obj.UserCode == userCode) && obj.SafeCode == safeCode && (obj.DocDate >= fromPeriod && obj.DocDate <= toPeriod))
                 .Select(obj => new FollowChecksDataDTO()
                 {
                     Credit = obj.TranType == "SFSIN" ? obj.ValuePay : 0,
                     Debit = obj.TranType == "SFOUT" ? obj.ValuePay : 0,
                     DocNo = obj.DocNo,
                     DocDate = obj.DocDate,
                     ChekNo = obj.ChekNo,
                     SattlDate = obj.SattlDate,
                     CheckStatus = obj.CheckStatus,
                     ChekDate = obj.ChekDate,
                     PersonName = obj.PersonName,
                     AccName = obj.AccName,
                 }).ToListAsync();
            }

            return followChecksDataDTOs;
        }
    }
}
