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
    public class ReportExpenseAnalysisRepo : IReportExpenseAnalysisRepo
    {
        private readonly AppDbContext _Context;
        private readonly IMapper _mapper;
        public ReportExpenseAnalysisRepo(AppDbContext Context, IMapper mapper)
        {
            _Context = Context;
            _mapper = mapper;
        }
        public async Task<ReportExpenseAnalysisDTO> GetAllDataAsync()
        {
            ReportExpenseAnalysisDTO reportExpenseAnalysisDTO = new ReportExpenseAnalysisDTO();
            List<Branch> branches = await _Context.Branchs.ToListAsync();
            List<JournalType> journalTypes = await _Context.JournalTypes.ToListAsync();
            List<AccMainCode> accMainCodes = await _Context.AccMainCodes.Where(main => _Context.AccSubCodes.Any(sub => sub.MainCode == main.MainCode)).ToListAsync();
            reportExpenseAnalysisDTO.branchs = _mapper.Map<List<BranchDTO>>(branches);
            reportExpenseAnalysisDTO.JournalTypes = _mapper.Map<List<JournalTypeDTO>>(journalTypes);
            reportExpenseAnalysisDTO.accMainCodes = _mapper.Map<List<AccMainCodeDTO>>(accMainCodes);

            return reportExpenseAnalysisDTO;
        }
        public async Task<List<ExpenseAnalysisDTO>> GetExpenseAnalysis(int branch, int journal, string mainCode, int year, int filterBy)
        {
            var expenseAnalysis = new ExpenseAnalysisDTO();

            for (int month = 1; month <= 12; month++)
            {
                var details = await GetDetailByMounth(branch, journal, mainCode, year, month);

                if (details.Any())
                {
                    var subCode = details.First().SubCode;
                    expenseAnalysis.SubName = await GetSubNameBySubCode(mainCode, subCode);
                }

                decimal monthlySum = CalculateMonthlySum(details, filterBy);
                SetMonthlySum(expenseAnalysis, month, monthlySum);
            }

            return new List<ExpenseAnalysisDTO> { expenseAnalysis };
        }

        private decimal CalculateMonthlySum(List<AccTransDetailViewDTO> details, int filterBy)
        {
            switch (filterBy)
            {
                case 1:
                    return details.Sum(d => d.ValDep ?? 0);
                case 2:
                    return details.Sum(d => d.ValCredit ?? 0);
                case 3:
                    return Math.Abs(details.Sum(d => d.ValDep ?? 0) - details.Sum(d => d.ValCredit ?? 0));
                default:
                    throw new ArgumentException("Invalid filterBy value");
            }
        }

        private void SetMonthlySum(ExpenseAnalysisDTO expenseAnalysis, int month, decimal monthlySum)
        {
            switch (month)
            {
                case 1: expenseAnalysis.Mon1 = monthlySum; break;
                case 2: expenseAnalysis.Mon2 = monthlySum; break;
                case 3: expenseAnalysis.Mon3 = monthlySum; break;
                case 4: expenseAnalysis.Mon4 = monthlySum; break;
                case 5: expenseAnalysis.Mon5 = monthlySum; break;
                case 6: expenseAnalysis.Mon6 = monthlySum; break;
                case 7: expenseAnalysis.Mon7 = monthlySum; break;
                case 8: expenseAnalysis.Mon8 = monthlySum; break;
                case 9: expenseAnalysis.Mon9 = monthlySum; break;
                case 10: expenseAnalysis.Mon10 = monthlySum; break;
                case 11: expenseAnalysis.Mon11 = monthlySum; break;
                case 12: expenseAnalysis.Mon12 = monthlySum; break;
            }
        }

        public async Task<List<AccTransDetailViewDTO>> GetDetailByMounth(int branch, int journal, string mainCode, int year,int mounth)
        {
            // حساب أول يوم وآخر يوم في الشهر المحدد
            var startDate = new DateTime(year, mounth, 1); // أول يوم في الشهر
            var endDate = new DateTime(year, mounth, DateTime.DaysInMonth(year, mounth)); // آخر يوم في الشهر

            var accTransDetails = await _Context.AccTransDetails
                .Where(obj => (branch == 100 || obj.CoCode == branch) &&
                              (journal == 100 || obj.TransType == journal.ToString()) &&
                              obj.MainCode == mainCode &&
                              obj.TransDate >= startDate && obj.TransDate <= endDate)
                .ToListAsync();
            List<AccTransDetailViewDTO> accTransDetailViewDTOs = _mapper.Map<List<AccTransDetailViewDTO>>(accTransDetails);
            return accTransDetailViewDTOs;
        }
        private async Task<string> GetSubNameBySubCode(string mainCode,string subCode)
        {
            var subName = await _Context.AccSubCodes.Where(obj=>obj.MainCode == mainCode && obj.SubCode == subCode)
                                        .Select(sub => sub.SubName)
                                        .FirstOrDefaultAsync();
            return subName;
        }
    }
}
