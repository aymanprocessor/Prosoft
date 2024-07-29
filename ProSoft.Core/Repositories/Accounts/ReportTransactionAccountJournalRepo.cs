using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProSoft.EF.DbContext;
using ProSoft.EF.DTOs.Accounts;
using ProSoft.EF.DTOs.Accounts.Report;
using ProSoft.EF.DTOs.Shared;
using ProSoft.EF.IRepositories.Accounts;
using ProSoft.EF.Models.Accounts;
using ProSoft.EF.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.Core.Repositories.Accounts
{
    public class ReportTransactionAccountJournalRepo : IReportTransactionAccountJournalRepo
    {
        private readonly AppDbContext _Context;
        private readonly IMapper _mapper;
        public ReportTransactionAccountJournalRepo(AppDbContext Context, IMapper mapper)
        {
            _Context = Context;
            _mapper = mapper;
        }

        public async Task<ReportTransactionAccountJournalDTO> GetAllDataAsync()
        {
            ReportTransactionAccountJournalDTO reportTransactionAccountJournalDTO = new ReportTransactionAccountJournalDTO();
            List<Branch> branches = await _Context.Branchs.ToListAsync();
            List<JournalType> journalTypes = await _Context.JournalTypes.ToListAsync();
            List<AccMainCode> accMainCodes = await _Context.AccMainCodes.Where(main => _Context.AccSubCodes.Any(sub => sub.MainCode == main.MainCode)).ToListAsync();
            reportTransactionAccountJournalDTO.branchs = _mapper.Map<List<BranchDTO>>(branches);
            reportTransactionAccountJournalDTO.JournalTypes = _mapper.Map<List<JournalTypeDTO>>(journalTypes);
            reportTransactionAccountJournalDTO.accMainCodes = _mapper.Map<List<AccMainCodeDTO>>(accMainCodes);

            return reportTransactionAccountJournalDTO;
        }

        public async Task<List<TransactionAccountJournalDTO>> GetTransactionAccountJournal(int journal, DateTime? fromPeriod, DateTime? toPeriod, string mainCode, string subCode, int branche)
        {
            List<TransactionAccountJournalDTO> transactionAccountJournalDTOs = new List<TransactionAccountJournalDTO>()
;            var accTransDetails = await _Context.AccTransDetails.Where(obj => (branche == 100 || obj.CoCode == branche) && obj.MainCode == mainCode &&(subCode == null || obj.SubCode == subCode) &&
                  (journal == 100 || obj.TransType == journal.ToString()) && (obj.TransDate >= fromPeriod && obj.TransDate <= toPeriod))
                .ToListAsync();

            foreach (var obj in accTransDetails)
            {
                var dto = new TransactionAccountJournalDTO
                {
                    TransNo = obj.TransNo,
                    TransDate = obj.TransDate,
                    LineDesc = obj.LineDesc,
                    ValDep = obj.ValDep,
                    ValCredit = obj.ValCredit
                };
                transactionAccountJournalDTOs.Add(dto);
            }
        

            return transactionAccountJournalDTOs;
        }
    }
}
