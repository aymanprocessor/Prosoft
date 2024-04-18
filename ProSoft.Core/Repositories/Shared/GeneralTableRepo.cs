using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProSoft.EF.DbContext;
using ProSoft.EF.DTOs.Shared;
using ProSoft.EF.IRepositories.Shared;
using ProSoft.EF.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.Core.Repositories.Shared
{
    public class GeneralTableRepo: Repository<GeneralCode, int>, IGeneralTableRepo
    {
        private readonly IMapper _mapper;
        public GeneralTableRepo(AppDbContext Context, IMapper mapper) : base(Context)
        {
            _mapper = mapper;
        }

        public async Task<int> GetNewIdAsync()
        {
            int newID;
            if (_DbSet.Count() != 0)
            {
                var lastID = await _DbSet.MaxAsync(obj => obj.GId);
                newID = lastID + 1;
            }
            else
                newID = 1;
            return newID;
        }

        public async Task<List<PermissionDefViewDTO>> GetAllPermissionsAsync()
        {
            List<GeneralCode> permissions = await _DbSet.Where(obj => obj.GType == "4")
                .ToListAsync();
            List<PermissionDefViewDTO> permissionsDTO = _mapper.Map<List<PermissionDefViewDTO>>(permissions);
            foreach (var item in permissionsDTO)
            {
                if (item.TransType != 0)
                    item.PermissionDepended = (await _DbSet.FirstOrDefaultAsync(obj =>
                    obj.UniqueType == item.TransType)).GDesc;
                else
                    item.PermissionDepended = "Not Depend On";
            }
            return permissionsDTO;
        }

        public async Task<PermissionDefEditAddDTO> GetEmptyPermissionAsync()
        {
            PermissionDefEditAddDTO permissionDTO = new();
            permissionDTO.GId = await GetNewIdAsync();
            List<GeneralCode> permissions = await _DbSet.Where(obj => obj.GType == "4")
                .ToListAsync();
            permissionDTO.Permissions = _mapper.Map<List<PermissionDefViewDTO>>(permissions);
            return permissionDTO;
        }

        public async Task<PermissionDefEditAddDTO> GetPermissionByIdAsync(int id)
        {
            GeneralCode permission = await GetByIdAsync(id);
            PermissionDefEditAddDTO permissionDTO = _mapper.Map<PermissionDefEditAddDTO>(permission);
            List<GeneralCode> permissions = await _DbSet.Where(obj => obj.GType == "4" && obj.GId != id)
                .ToListAsync();
            permissionDTO.Permissions = _mapper.Map<List<PermissionDefViewDTO>>(permissions);
            return permissionDTO;
        }
    }
}
