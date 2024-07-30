using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProSoft.EF.DbContext;
using ProSoft.EF.DTOs.Accounts;
using ProSoft.EF.DTOs.Accounts.Report;
using ProSoft.EF.DTOs.Treasury;
using ProSoft.EF.IRepositories.Accounts;
using ProSoft.EF.Models.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.Core.Repositories.Accounts
{
    public class ReportFromToVoucherRepo : IReportFromToVoucherRepo
    {
        private readonly AppDbContext _Context;
        private readonly IMapper _mapper;
        public ReportFromToVoucherRepo(AppDbContext Context, IMapper mapper)
        {
            _Context = Context;
            _mapper = mapper;
        }
        public async Task<ReportFromToVoucherDTO> GetAllDataAsync()
        {
            ReportFromToVoucherDTO reportFromToVoucherDTO = new ReportFromToVoucherDTO();
            List<JournalType> journalTypes = await _Context.JournalTypes.ToListAsync();
            reportFromToVoucherDTO.JournalTypes = _mapper.Map<List<JournalTypeDTO>>(journalTypes);

            return reportFromToVoucherDTO;
        }

        public async Task<List<FromToVoucherDataDTO>> GetFromToVoucherData(int journal, int? fromReceipt, int? toReceipt, DateTime? fromPeriod, DateTime? toPeriod,int fYear)
        {
            List<FromToVoucherDataDTO> fromToVoucherDataDTOs = new List<FromToVoucherDataDTO>();

                var accTransDetails = await _Context.AccTransDetails.Where(obj => obj.FYear == fYear &&
                      (journal == 100 || obj.TransType == journal.ToString()) &&
                      (fromReceipt == null || fromReceipt == 0 || obj.TransNo >= fromReceipt) &&
                      (toReceipt == null || toReceipt == 0 || obj.TransNo <= toReceipt) &&
                      (fromPeriod == null || obj.TransDate >= fromPeriod) &&
                      (toPeriod == null || obj.TransDate <= toPeriod)).OrderBy(obj => obj.TransNo)
                    .ToListAsync();

                foreach (var obj in accTransDetails)
                {
                    var dto = new FromToVoucherDataDTO
                    {
                        TransNo = obj.TransNo,
                        TransDate = obj.TransDate,
                        AccountName = await GetAccountName(obj.MainCode, obj.SubCode),
                        LineDesc = obj.LineDesc,
                        ValDep = obj.ValDep,
                        ValCredit = obj.ValCredit
                    };
                    fromToVoucherDataDTOs.Add(dto);
                }

            return fromToVoucherDataDTOs;
        }

        //to get Account name
        private async Task<string> GetAccountName(string mainCode, string subCode)
        {
            AccMainCode myMainCode = await _Context.AccMainCodes
                .FirstOrDefaultAsync(obj => obj.MainCode == mainCode);
            string mainName = myMainCode.MainName;

            AccSubCode mySubCode = await _Context.AccSubCodes
                .FirstOrDefaultAsync(obj => obj.SubCode == subCode && obj.MainCode == mainCode);
            string subName = mySubCode! != null ? mySubCode.SubName : string.Empty;

            return subName != "" ? $"{mainName} / {subName}" : mainName;
        }
    }
}
