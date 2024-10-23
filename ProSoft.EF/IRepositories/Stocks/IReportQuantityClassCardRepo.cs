using ProSoft.EF.DTOs.Stocks;
using ProSoft.EF.DTOs.Stocks.Report.ClassCard;
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
        Task<List<AtTransactionLevelCardDTO>> GetAtLevelTransactionCard(int id, string itemCode, int unitCode, DateTime? fromPeriod, DateTime? toPeriod, int branch);

    }
}
