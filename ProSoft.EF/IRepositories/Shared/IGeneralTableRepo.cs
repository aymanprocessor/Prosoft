using ProSoft.EF.DTOs.Shared;
using ProSoft.EF.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.IRepositories.Shared
{
    public interface IGeneralTableRepo: IRepository<GeneralCode, int>
    {
        Task<int> GetNewIdAsync();
        Task<List<PermissionDefViewDTO>> GetAllPermissionsAsync(string GType);
        Task<PermissionDefEditAddDTO> GetEmptyPermissionAsync();
        Task<PermissionDefEditAddDTO> GetPermissionByIdAsync(int id);
    }
}
