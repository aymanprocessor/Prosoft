using ProSoft.EF.DTOs.Stocks;
using ProSoft.EF.Models.Stocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.IRepositories.Stocks
{
    public interface ITransDtlRepo: IRepository<TransDtl, int>
    {
        Task<List<TransDtlWithPriceDTO>> GetPermissionDetailsAsync(int transMasterID);
        Task<TransDtlWithPriceDTO> GetTransDtlWithPriceByIdAsync(int transDtlID);
        Task<TransDtlWithPriceDTO> GetNewTransDtlAsync();
        Task addTransDtlWithPriceAsync(TransDtlWithPriceDTO transDtlDTO);
    }
}
