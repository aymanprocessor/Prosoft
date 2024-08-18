using ProSoft.EF.DTOs.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.IRepositories.Accounts
{
    public interface IAccMainCodeDtlRepo
    {
        Task<List<AccMainCodeDtlDTO>> GetAccMainCodeDtl(string id);
        Task AddAccMainCodeDtlAsync(AccMainCodeDtlDTO accMainCodeDtlDTO);
        Task DeleteAccMainCodeDtlAsync(string secCode, string maincode, int branch);
    }
}
