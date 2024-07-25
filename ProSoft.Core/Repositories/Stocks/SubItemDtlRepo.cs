using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProSoft.EF.DbContext;
using ProSoft.EF.DTOs.Stocks;
using ProSoft.EF.IRepositories.Stocks;
using ProSoft.EF.Models.Stocks;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.Core.Repositories.Stocks
{
    public class SubItemDtlRepo : Repository<SubItemDtl, int>, ISubItemDtlRepo
    {
        private readonly IMapper _mapper;
        public SubItemDtlRepo(AppDbContext Context, IMapper mapper) : base(Context)
        {
            _mapper = mapper;
        }

        public async Task<List<SubItemDtlDTO>> GetSubItemDetailsAsync(int subItemID)
        {
            SubItem subItem = await _Context.SubItems.FindAsync(subItemID);
            List<SubItemDtl> subItemDtls = await _DbSet.Where(obj => obj.ItemCode == subItem.ItemCode &&
                obj.MainCode == subItem.MainCode && obj.Flag1 == subItem.Flag1 && obj.BranchId == subItem.BranchId)
                .ToListAsync();
            var subItemDtlsDTO = _mapper.Map<List<SubItemDtlDTO>>(subItemDtls);
            foreach(var item in subItemDtlsDTO)
                item.UnitType = (await _Context.UnitCodes.FindAsync(item.UnitCode)).Names;
            //{
                //item.ItemSizeName = item.ItemName == "1" ? "High" : item.ItemName == "2" ? "Low" : "Canceled";
            //}
            return subItemDtlsDTO;
        }

        public async Task<SubItemDtlDTO> GetNewSubItemDtlAsync(int subItemID)
        {
            var subItemDtlDTO = new SubItemDtlDTO();
            SubItem subItem = await _Context.SubItems.FindAsync(subItemID);
            subItemDtlDTO.SubItemName = subItem.SubName;
            subItemDtlDTO.ItemCode = subItem.ItemCode;

            MainItem mainItem = await _Context.MainItems.FindAsync(subItem.MainId);
            _mapper.Map(mainItem, subItemDtlDTO);
            subItemDtlDTO.MainLevel = subItemDtlDTO.ParentCode == "1" ? "MainLevel_2" : $"MainLevel_{mainItem.CurrentLevel}";
            
            List<UnitCode> unitCodes = await _Context.UnitCodes.ToListAsync();
            subItemDtlDTO.UnitCodes = _mapper.Map<List<UnitCodeDTO>>(unitCodes);
            return subItemDtlDTO;
        }

        public async Task<SubItemDtlDTO> GetSubItemDtlByIdAsync(int subItemDtlID)
        {
            SubItemDtl subItemDtl = await GetByIdAsync(subItemDtlID);

            MainItem mainItem = await _Context.MainItems.FirstOrDefaultAsync(obj => obj.MainCode == subItemDtl.MainCode
                && obj.Flag1 == subItemDtl.Flag1 && obj.BranchId == subItemDtl.BranchId);

            var subItemDtlDTO = _mapper.Map<SubItemDtlDTO>(subItemDtl);
            SubItem subItem = await _Context.SubItems.FirstOrDefaultAsync(obj => obj.MainCode == subItemDtl.MainCode
                && obj.Flag1 == subItemDtl.Flag1 && obj.BranchId == subItemDtl.BranchId
                && obj.ItemCode == subItemDtl.ItemCode);
            subItemDtlDTO.SubItemName = subItem.SubName;
            
            _mapper.Map(mainItem, subItemDtlDTO);
            subItemDtlDTO.MainLevel = subItemDtlDTO.ParentCode == "1" ? "MainLevel_2" : $"MainLevel_{mainItem.CurrentLevel}";

            List<UnitCode> unitCodes = await _Context.UnitCodes.ToListAsync();
            subItemDtlDTO.UnitCodes = _mapper.Map<List<UnitCodeDTO>>(unitCodes);
            return subItemDtlDTO;
        }

        public async Task AddSubItemDtlAsync(int subItemID, SubItemDtlDTO subItemDtlDTO)
        {
            SubItem subItem = await _Context.SubItems.FindAsync(subItemID);
            _mapper.Map(subItem, subItemDtlDTO);

            var newSubItemDtl = _mapper.Map<SubItemDtl>(subItemDtlDTO);
            List<SubItemDtl> subItemDtls = await _DbSet.Where(obj => obj.ItemCode == subItem.ItemCode &&
                obj.MainCode == subItem.MainCode && obj.Flag1 == subItem.Flag1 && obj.BranchId == subItem.BranchId)
                .ToListAsync();
            newSubItemDtl.ItemId = subItemDtls.Count == 0 ? 1 : subItemDtls.Count + 1;

            await AddAsync(newSubItemDtl);
            await SaveChangesAsync();
        }

        public async Task EditSubItemDtlAsync(int subItemDtlID, SubItemDtlDTO subItemDtlDTO)
        {
            SubItemDtl subItemDtl = await GetByIdAsync(subItemDtlID);
            _mapper.Map(subItemDtlDTO, subItemDtl);
            subItemDtl.SubItemDtlId = subItemDtlID;

            await UpdateAsync(subItemDtl);
            await SaveChangesAsync();
        }
    }
}
