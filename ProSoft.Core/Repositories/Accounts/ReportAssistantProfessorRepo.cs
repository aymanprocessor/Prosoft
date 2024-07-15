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
    public class ReportAssistantProfessorRepo : IReportAssistantProfessorRepo
    {
        private readonly AppDbContext _Context;
        private readonly IMapper _mapper;
        public ReportAssistantProfessorRepo(AppDbContext Context, IMapper mapper)
        {
            _Context = Context;
            _mapper = mapper;
        }
        public async Task<ReportAssistantProfessorDTO> GetAllDataAsync()
        {
            ReportAssistantProfessorDTO reportReportAssistantProfessorDTO = new ReportAssistantProfessorDTO();
            List<Branch> branches = await _Context.Branchs.ToListAsync();
            List<JournalType> journalTypes = await _Context.JournalTypes.ToListAsync();
            List<CostCenter> costCenters = await _Context.CostCenters.ToListAsync();
            List<AccMainCode> accMainCodes = await _Context.AccMainCodes.Where(main => _Context.AccSubCodes.Any(sub => sub.MainCode == main.MainCode)).ToListAsync();
            List<AccSubCode> accSubCodes = await _Context.AccSubCodes.ToListAsync();
            reportReportAssistantProfessorDTO.branchs = _mapper.Map<List<BranchDTO>>(branches);
            reportReportAssistantProfessorDTO.JournalTypes = _mapper.Map<List<JournalTypeDTO>>(journalTypes);
            reportReportAssistantProfessorDTO.costCenters = _mapper.Map<List<CostCenterViewDTO>>(costCenters);
            reportReportAssistantProfessorDTO.accMainCodes = _mapper.Map<List<AccMainCodeDTO>>(accMainCodes);
            reportReportAssistantProfessorDTO.accSubCodes = _mapper.Map<List<AccSubCodeDTO>>(accSubCodes);

            return reportReportAssistantProfessorDTO;
        }

        public async Task<List<AssistantProfessorAnalyticalDTO>> GetAnalyticalAsync(int branch, int journal, string mainCode, string subCode, int costCode, DateTime? fromPeriod, DateTime? toPeriod)
        {
            List<AssistantProfessorAnalyticalDTO> assistantProfessorAnalyticalDTOs = new List<AssistantProfessorAnalyticalDTO>();

                var accTransDetails = await _Context.AccTransDetails.Where(obj => obj.CoCode == branch &&
                      (journal == 100 || obj.TransType == journal.ToString()) && 
                      obj.MainCode == mainCode && obj.SubCode == subCode &&
                      (costCode == 100 || obj.CostCenterCode == costCode.ToString()) &&
                      (obj.TransDate >= fromPeriod && obj.TransDate <= toPeriod))
                    .ToListAsync();

                //foreach (var obj in accTransDetails)
                //{
                //    var dto = new AssistantProfessorAnalyticalDTO
                //    {
                //        Balance =obj.,
                //        AccountName = await GetAccountName(obj.MainCode, obj.SubCode),
                //        TransDesc = obj.LineDesc,
                //        ValDep = obj.ValDep,
                //        ValCredit = obj.ValCredit,
                //        TransNo = obj.TransNo,
                //        TransDate = obj.TransDate
                //    };
                //    assistantProfessorAnalyticalDTOs.Add(dto);
                //}


                return assistantProfessorAnalyticalDTOs;
        }

        public async Task<List<AssistantProfessorOverAllDTO>> GetOverAllAsync(int branch, int journal, string mainCode, int costCode, DateTime? fromPeriod, DateTime? toPeriod)
        {
            throw new NotImplementedException();
        }
    }
}
