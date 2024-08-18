using ProSoft.EF.DTOs.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.IRepositories.Accounts
{
    public interface IAccSubCodeEditRepo
    {
        Task<DisplayAccSubCodeEditDTO> GetAllDataAsync();
        Task<List<AccSubCodeEditDTO>> GetAccSubCodeEditAsync(string mainCode);
        Task<AccSubCodeEditDTO> GetAccAccSubCodeEditById(string id, string subFr);
        Task EditAccSubCodeEditAsync(string id,int fYear, int branch, AccSubCodeEditDTO accSubCodeEditDTO);
    }
}
