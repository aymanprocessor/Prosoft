using ProSoft.EF.DTOs.Accounts;
using ProSoft.EF.DTOs.Shared;
using ProSoft.EF.DTOs.Stocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.IRepositories.Stocks
{
    public interface IOpenColseTransactionRepo 
    {
        Task<List<PermissionDefViewDTO>> GetPermissionsAsync();
        Task<string> OpenAsync(int stockCode, int permission, int? fromVoucher, int? toVoucher, DateTime? fromPeriod, DateTime? toPeriodint,int closOrCanc);
        Task<string> CloseAsync(int stockCode, int permission, int? fromVoucher, int? toVoucher, DateTime? fromPeriod, DateTime? toPeriodint,int closOrCanc);
    }
}
