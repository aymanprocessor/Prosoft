using ProSoft.EF.DTOs.Stocks.TransferSuppliesToStocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.IRepositories.Stocks
{
    public interface IPostSuppliesToStocksRepo
    {
        Task<List<TransferSuppliesToStocksMasterDTO>> GetPatAdmissions(int branchId, int RegionId, DateTime FromDate, DateTime ToDate, int? postId);
       Task<List<TransferSuppliesToStocksDetailDTO>> GetClinicTnxs(int branchId, int MasterId, int PatId, int Year);
        Task TransferSuppliesToStocks(List<TransferSuppliesToStocksAJAXReqDTO> Rows,int branchId,int year, int userCode);
        Task CancelTransferSuppliesToStocks(List<TransferSuppliesToStocksAJAXReqDTO> Rows,int branchId,int year, int userCode);

    }
}
