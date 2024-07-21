using ProSoft.EF.DTOs.Accounts;
using ProSoft.EF.DTOs.Accounts.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.IRepositories.Accounts
{
    public interface IReportExpenseAnalysisRepo
    {
        Task<ReportExpenseAnalysisDTO> GetAllDataAsync();
        Task<List<ExpenseAnalysisDTO>> GetExpenseAnalysis(int branch, int journal, string mainCode, int year, int filterBy);
        Task<List<AccTransDetailViewDTO>> GetDetailByMounth(int branch, int journal, string mainCode, int year, int mounth);
    }
}
