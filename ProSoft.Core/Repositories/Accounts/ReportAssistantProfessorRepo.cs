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

        public async Task<List<AssistantProfessorAnalyticalDTO>> GetAnalyticalAsync(int branch, int journal, string mainCode, string subCode, int costCode, DateTime? fromPeriod, DateTime? toPeriod,int fYear)
        {
            List<AssistantProfessorAnalyticalDTO> assistantProfessorAnalyticalDTOs = new List<AssistantProfessorAnalyticalDTO>();

            //get first balance
            AccStartBal accStartBal = await _Context.AccStartBals.FirstOrDefaultAsync(obj=> obj.TransType == journal.ToString() && obj.MainCode == mainCode && obj.SubCode == subCode && obj.FYear == fYear && (branch == 100 || obj.CoCode == branch));
                decimal lcBalFirstYear = 0;
                    if (accStartBal==null)
                    {
                        lcBalFirstYear = 0;
                    }
                    else
                     lcBalFirstYear = (decimal)(accStartBal.FCreditOr - accStartBal.FDepOr);
            
            AccTransDetail accTransDetailForFirstbalance = await _Context.AccTransDetails.FirstOrDefaultAsync(obj=>obj.FYear==fYear&& (branch == 100 || obj.CoCode == branch) &&
                      (journal == 100 || obj.TransType == journal.ToString()) && obj.MainCode == mainCode && obj.SubCode == subCode && obj.TransDate < fromPeriod);
              decimal lcBalBeforeDate;
                    if (accTransDetailForFirstbalance == null)
                    {
                        lcBalBeforeDate = 0;
                    }
                    else
                     lcBalBeforeDate = accTransDetailForFirstbalance.ValCredit - accTransDetailForFirstbalance.ValDep ?? 0;
          
              decimal lcBalFirstPeriod = lcBalFirstYear + lcBalBeforeDate;
              AssistantProfessorAnalyticalDTO specificDTO;
                    if (lcBalFirstPeriod<=0)
                    {
                        // Define specific data for the first row
                         specificDTO = new AssistantProfessorAnalyticalDTO
                        {
                            ValDep = lcBalFirstPeriod,
                            ValCredit = 0,
                            TransDesc = "رصيد اول الفترة",
                            CostDesc = "",
                            TransNo = 0,
                            // TransDate = 
                        };
                    }
                    else
                    {
                        // Define specific data for the first row
                         specificDTO = new AssistantProfessorAnalyticalDTO
                        {
                            ValDep = 0,
                            ValCredit = lcBalFirstPeriod,
                            TransDesc = "رصيد اول الفترة",
                            CostDesc = "",
                            TransNo = 0,
                            // TransDate = 
                        };
                    }

            // Prepend the specific DTO to the list
            assistantProfessorAnalyticalDTOs.Insert(0, specificDTO);

            var accTransDetails = await _Context.AccTransDetails.OrderBy(obj=>obj.TransNo).Where(obj =>
                      (branch == 100 || obj.CoCode == branch) && 
                      (journal == 100 || obj.TransType == journal.ToString()) && 
                      obj.MainCode == mainCode && obj.SubCode == subCode &&
                      (costCode == 100 || obj.CostCenterCode == costCode.ToString()) &&
                      (obj.TransDate >= fromPeriod && obj.TransDate <= toPeriod))
                    .ToListAsync();

            foreach (var obj in accTransDetails)
            {
                var costCenterName = await GetCostCenterName(obj.CostCenterCode);

                var dto = new AssistantProfessorAnalyticalDTO
                {
                    ValDep = obj.ValDep,
                    ValCredit = obj.ValCredit,
                    TransDesc = obj.AccName +"/"+obj.LineDesc,
                    CostDesc = costCenterName,
                    TransNo = obj.TransNo,
                    TransDate = obj.TransDate
                };
                assistantProfessorAnalyticalDTOs.Add(dto);
            }

            return assistantProfessorAnalyticalDTOs;
        }

        public async Task<List<AssistantProfessorOverAllDTO>> GetOverAllAsync(int branch, int journal, string mainCode, int costCode, DateTime? fromPeriod, DateTime? toPeriod)
        {
            List<AssistantProfessorOverAllDTO> assistantProfessorOverAllDTOs = new List<AssistantProfessorOverAllDTO>();

            List<AccSubCode> accSubCodes = await _Context.AccSubCodes.Where(obj => obj.MainCode == mainCode).ToListAsync();
            foreach (var sub in accSubCodes)
            {
                var accTransDetails = await _Context.AccTransDetails.OrderBy(obj => obj.TransNo).Where(obj =>
                      (branch == 100 || obj.CoCode == branch) &&
                      (journal == 100 || obj.TransType == journal.ToString()) &&
                      obj.MainCode == mainCode && obj.SubCode == sub.SubCode &&
                      (costCode == 100 || obj.CostCenterCode == costCode.ToString()) &&
                      (obj.TransDate >= fromPeriod && obj.TransDate <= toPeriod))
                    .ToListAsync();

                foreach (var obj in accTransDetails)
                {
                    var accName = await GetAccountName(obj.MainCode, obj.SubCode);

                    // Check if SubCode already exists in the list
                    var existingDto = assistantProfessorOverAllDTOs.FirstOrDefault(dto => dto.SubCode == obj.SubCode);
                    if (existingDto != null)
                    {
                        // Update the existing DTO
                        existingDto.ValDep += obj.ValDep;
                        existingDto.ValCredit += obj.ValCredit;
                        existingDto.TransValDep += obj.ValDep;
                        existingDto.TransValCredit += obj.ValCredit;
                    }
                    else
                    {
                        // Create a new DTO and add to the list
                        var dto = new AssistantProfessorOverAllDTO
                        {
                            SubCode = obj.SubCode,
                            AccName = accName,
                            ValDep = obj.ValDep,
                            ValCredit = obj.ValCredit,
                            TransValDep = obj.ValDep,
                            TransValCredit = obj.ValCredit,
                        };
                        assistantProfessorOverAllDTOs.Add(dto);
                    }
                }
            }
            return assistantProfessorOverAllDTOs;


        }
        private async Task<string> GetCostCenterName(string costCenterCode)
        {
            var costCenter = await _Context.CostCenters
                                           .FirstOrDefaultAsync(cc => cc.CostCode.ToString() == costCenterCode);
            return costCenter?.CostDesc ?? ""; // Return "" if no matching cost center is found
        }
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
