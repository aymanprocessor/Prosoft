using ProSoft.EF.DTOs.Stocks;
using ProSoft.EF.DTOs.Stocks.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.IRepositories.Stocks
{
    public interface IReportQuantityClassCardRepo
    {
        Task<ReportQuantityClassCardDTO> GetAllDataAsync(int userCode);
        Task<List<QuantityCardOnlyDTO>> GetQuantityCardOnly(int id, string subItem, DateTime? fromPeriod, DateTime? toPeriod,int branch);
        Task<List<QuantityAndValueCardDTO>> GetQuantityAndValueCard(int id, string subItem, DateTime? fromPeriod, DateTime? toPeriod,int branch);
    }
}
