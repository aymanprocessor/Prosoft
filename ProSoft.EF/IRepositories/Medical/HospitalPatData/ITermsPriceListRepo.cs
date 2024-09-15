using ProSoft.EF.DTOs.Medical.HospitalPatData;
using ProSoft.EF.Models.Medical.HospitalPatData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.IRepositories.Medical.HospitalPatData
{
    public interface ITermsPriceListRepo :IRepository<PriceListDetail,int>
    {
        public Task<List<PriceListDetail>> GetAllPriceListDetails(int id);

        Task<List<TermsPriceListViewDTO>> GetAllTermsPriceList(int id);
        Task<int> GetNewIdSortAsync();
        Task<TermsPriceListEditAddDTO> GetEmptyTermsPriceListAsync();
        Task AddTermPriceListAsync(int id, TermsPriceListEditAddDTO termsPriceListDTO);
        Task<TermsPriceListEditAddDTO> GetTermPriceListByIdAsync(int id);
        Task EditDoctorPercentAsync(int id, TermsPriceListEditAddDTO termsPriceListDTO);
    }
}
