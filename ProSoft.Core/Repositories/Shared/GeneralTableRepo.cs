using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProSoft.EF.DbContext;
using ProSoft.EF.DTOs.Shared;
using ProSoft.EF.IRepositories.Shared;
using ProSoft.EF.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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


        // ------------- Ayman Saad -------------//
        public IEnumerable<SelectListItem> GetAllAsSelectListItem(Expression<Func<GeneralCode, bool>>? predicate = null)
        {
            var query = _Context.GeneralCodes.AsQueryable();

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            return query.Select(g => new SelectListItem { Text = g.GDesc, Value = g.UniqueType.ToString() }).ToList();

        }

        public IEnumerable<GeneralCode> GetAll(Expression<Func<GeneralCode, bool>>? predicate = null)
        {
            var query = _Context.GeneralCodes.AsQueryable();

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            return query.ToList();
        }

        public async Task<PermissionDefViewDTO> GetPermissionByUniqueTypeAsync(int id)
        {
            GeneralCode? permission = await _Context.GeneralCodes.Where(g => g.UniqueType == id).FirstOrDefaultAsync();
            PermissionDefViewDTO permissionDefViewDTO = new PermissionDefViewDTO();
            if (permission != null) { 
            
            permissionDefViewDTO = _mapper.Map<PermissionDefViewDTO>(permission);
            }
            return permissionDefViewDTO;
           
        }
        // ------------- Ayman Saad -------------//
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

        public async Task<List<PermissionDefViewDTO>> GetPermissionsAsync(string GType)
        {
            List<GeneralCode> permissions = await _DbSet.Where(obj => obj.GType == GType && obj.ShowHide == 1)
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
            List<GeneralCode> permissions = await _DbSet.Where(obj => obj.GType == "4" && obj.ShowHide == 1)
                .ToListAsync();
            permissionDTO.Permissions = _mapper.Map<List<PermissionDefViewDTO>>(permissions);
            return permissionDTO;
        }

        public async Task<PermissionDefEditAddDTO> GetPermissionByIdAsync(int id)
        {
            GeneralCode permission = await GetByIdAsync(id);
            PermissionDefEditAddDTO permissionDTO = _mapper.Map<PermissionDefEditAddDTO>(permission);
            List<GeneralCode> permissions = await _DbSet.Where(obj => obj.GType == "4" &&
                obj.GId != id && obj.ShowHide == 1)
                .ToListAsync();
            permissionDTO.Permissions = _mapper.Map<List<PermissionDefViewDTO>>(permissions);
            return permissionDTO;
        }

        
    }
}
