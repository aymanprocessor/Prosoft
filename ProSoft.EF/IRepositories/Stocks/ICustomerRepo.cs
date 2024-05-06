using ProSoft.EF.DTOs.Stocks;
using ProSoft.EF.Models.Stocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.IRepositories.Stocks
{
    public interface ICustomerRepo : IRepository<CustCode, int>
    {
        Task<string> GetNewIdAsync();
        Task<CustCodeEditAddDTO> GetEmptyCustomerAsync();
        Task<CustCodeEditAddDTO> GetCustomerByIdAsync(int id);
    }
}
