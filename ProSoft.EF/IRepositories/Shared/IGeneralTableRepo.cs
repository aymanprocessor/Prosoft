using Microsoft.AspNetCore.Mvc.Rendering;
using ProSoft.EF.DTOs.Shared;
using ProSoft.EF.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.IRepositories.Shared
{
    public interface IGeneralTableRepo: IRepository<GeneralCode, int>
    {

        // ------------- Ayman Saad ------------- //
        public IEnumerable<SelectListItem> GetAllAsSelectListItem(Expression<Func<GeneralCode, bool>>? predicate = null);
        public IEnumerable<GeneralCode> GetAll(Expression<Func<GeneralCode, bool>>? predicate = null);
        public Task<PermissionDefViewDTO> GetPermissionByUniqueTypeAsync(int id);
        // ------------- Ayman Saad ------------- //
        Task<int> GetNewIdAsync();
        Task<List<PermissionDefViewDTO>> GetPermissionsAsync(string GType);
        Task<PermissionDefEditAddDTO> GetEmptyPermissionAsync();
        Task<PermissionDefEditAddDTO> GetPermissionByIdAsync(int id);
    }
}
