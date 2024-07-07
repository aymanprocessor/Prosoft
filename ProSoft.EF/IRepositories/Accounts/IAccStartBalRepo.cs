using ProSoft.EF.DTOs.Accounts;
using ProSoft.EF.Models.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.IRepositories.Accounts
{
    public interface IAccStartBalRepo :IRepository<AccStartBal,int>
    {
        Task<List<AccStartBalViewDTO>> GetAccStartBalAsync(int journalCode, int fYear, int branch);
        Task<AccStartBalEditAddDTO> GetAccStartBalById(int id);
        Task EditAccStartBalAsync(int id, AccStartBalEditAddDTO accStartBalDTO);
    }
}
