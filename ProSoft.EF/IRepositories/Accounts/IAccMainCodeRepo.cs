using ProSoft.EF.DTOs.Accounts;
using ProSoft.EF.Models.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.IRepositories.Accounts
{
    public interface IAccMainCodeRepo
    {
        Task<List<AccMainCodeDTO>> GetMainsByLevelAsync(double level);
        Task<AccMainCodeDTO> GetMainByIdAsync(string id);
    }
}
