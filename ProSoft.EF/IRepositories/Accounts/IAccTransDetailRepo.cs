using ProSoft.EF.DTOs.Accounts;
using ProSoft.EF.Models.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.IRepositories.Accounts
{
    public interface IAccTransDetailRepo :IRepository<AccTransDetail,int>
    {
        Task<List<AccTransDetailViewDTO>> GetAccTransDetailAsync(int transId, int journalCode);
        Task<AccTransDetailEditAddDTO> GetEmptyAccTransDetailAsync(int id, int journalCode);

    }
}
