using ProSoft.EF.DTOs.Medical.HospitalPatData;
using ProSoft.EF.Models.Medical.HospitalPatData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.IRepositories.Medical.HospitalPatData
{
    public interface IPriceListRepo : IRepository<PriceList,int>
    {
        Task<List<PriceListViewDTO>> GetAllPriceList();
        Task<int> GetNewIdAsync();
        Task<PriceListEditAddDTO> GetPriceListByIdAsync(int id);
        ////////
        Task AddPriceListAsync(PriceListEditAddDTO priceListDTO);
        Task AddBatchPriceListsAsync(IEnumerable<PriceListEditAddDTO> priceListDTOs);
        Task EditPriceListAsync(int id, PriceListEditAddDTO priceListDTO);
        Task EditPriceListBatchAsync(IEnumerable<PriceListEditAddDTO> priceListDTOs);

    }
}
