using ProSoft.EF.DTOs.Stocks;
using ProSoft.EF.Models.Medical.HospitalPatData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.IRepositories.Stocks
{
    public interface IDepartmentsWithSectionsRepo :IRepository<Region,int>
    {
        Task<List<RegionsViewDTO>> GetDepartmentsWithSectionsAll(int id);
        Task<RegionsEditAddDTO> GetEmptyDepartmentsWithSections(int regionCode);
        Task AddDepartmentsWithSectionsAsync(int id,RegionsEditAddDTO regionsEditAddDTO);
        Task<RegionsEditAddDTO> GetdepartmentsWithSectionsByIdAsync(int id);
        Task EditDepartmentsWithSectionsAsync(int id,RegionsEditAddDTO regionsEditAddDTO);
        
    }
}
