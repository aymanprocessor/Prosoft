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
        Task<string> GetBarCodeAsync(int transMAsterID, int serial, int itemMaster);
        Task InsertItemBatchAsync(TransDtl transDtl);
        // For Showing Trans Price
        Task<TransDtlWithPriceDTO> GetNewTransDtlWithPriceAsync(int transMAsterID);
        Task<TransDtlWithPriceDTO> GetTransDtlWithPriceByIdAsync(int transDtlID);
        Task AddTransDtlWithPriceAsync(TransDtlWithPriceDTO transDtlDTO);
        Task UpdateTransDtlWithPriceAsync(int id, TransDtlWithPriceDTO transDtlDTO);
        // For Not Showing Trans Price
        Task<TransDtlDTO> GetNewTransDtlAsync(int transMAsterID);
        Task<TransDtlDTO> GetTransDtlByIdAsync(int transDtlID);
        Task AddTransDtlAsync(TransDtlDTO transDtlDTO);
        Task UpdateTransDtlAsync(int id, TransDtlDTO transDtlDTO);
    }
}
