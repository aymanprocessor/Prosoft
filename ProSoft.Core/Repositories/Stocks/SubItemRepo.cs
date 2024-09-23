using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProSoft.EF.DbContext;
using ProSoft.EF.DTOs.Accounts;
using ProSoft.EF.DTOs.Stocks;
using ProSoft.EF.IRepositories.Stocks;
using ProSoft.EF.Models.Accounts;
using ProSoft.EF.Models.Stocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.Core.Repositories.Stocks
{
    public class SubItemRepo : Repository<SubItem, int>, ISubItemRepo
    {
        private readonly IMapper _mapper;
        public SubItemRepo(AppDbContext Context, IMapper mapper) : base(Context)
        {
            _mapper = mapper;
        }

        public IEnumerable<SubItem> GetAllSubItemByStockId(int stkcod)
        {
            var result = from si in _Context.SubItems
                         join mis in _Context.MainItemStocks
                         on new { si.MainCode, x1 = (int)si.Flag1, x2 = (int)si.BranchId }
                            equals new { mis.MainCode, x1 = mis.Flag1, x2 = mis.BranchId }
                         where mis.Stkcod == stkcod
                         select si;
            return result;
        }
        public async Task<List<SubItemViewDTO>> GetSubItemsByMainIdAsync(int mainId)
        {
            List<SubItem> subItems = await _DbSet.Where(obj => obj.MainId == mainId).ToListAsync();
            var subItemsDTO = _mapper.Map<List<SubItemViewDTO>>(subItems);
            return subItemsDTO;
        }

        public async Task<int> ValidateItemCode(string itemCode)
        {
            // عدد حروف الباركود
            var sysObj2 = await _Context.SystemTables.FirstOrDefaultAsync(obj => obj.SysId == 20);
            var bCode_len = sysObj2.SysValue;
            if(itemCode.Length > bCode_len)
                // 1 => خطأ كود الصنف اكبر من باركود النظام
                // 1 => Error: ItemCode is larger than system barcode
                return 1;
            return 0;
        }

        private async Task<string> GenerateItemCode(MainItem mainItem)
        {
            var ll_flag1 = mainItem.Flag1;
            var batchCounter = mainItem.BatchCounter != null ? mainItem.BatchCounter : 0;
            var ls_code = "";

            // اضافة نوع المخزن للباركود
            var sysObj = await _Context.SystemTables.FirstOrDefaultAsync(obj => obj.SysId == 27);
            var sys_valu = sysObj.SysValue;
            var ll_count_flag = 0;
            if (sys_valu == 1)
            {
                ll_count_flag = ll_flag1.ToString().Length;
            }
            var is_main = mainItem.MainCode;
            var main_name_all = mainItem.MainNameAll;
            // عدد حروف الباركود
            var sysObj2 = await _Context.SystemTables.FirstOrDefaultAsync(obj => obj.SysId == 20);
            var bCode_len = sysObj2.SysValue;
            // كود الصنف اتوماتيك
            var sysObj3 = await _Context.SystemTables.FirstOrDefaultAsync(obj => obj.SysId == 26);
            var sys_value = sysObj3.SysValue;
            var li_sub = await _DbSet.Where(obj => obj.MainId == mainItem.MainId).CountAsync() + 1;
            var is_sub = $"000000000000{li_sub}";
            is_sub = is_sub[^6..];
            var ll_sub_count = is_sub.Length;
            if (sys_value == 1)
            {
                var current_level = mainItem.CurrentLevel;
                var ls_main = string.Empty;
                switch (current_level)
                {
                    case 1:
                        ls_main = is_main.Substring(0, 1);
                        break;
                    case 2:
                        ls_main = is_main.Substring(0, 3);
                        break;
                    case 3:
                        ls_main = is_main.Substring(0, 5);
                        break;
                    case 4:
                        ls_main = is_main.Substring(0, 7);
                        break;
                    case 5:
                        ls_main = is_main.Substring(0, 9);
                        break;
                }
                var ll_main_count = ls_main.Length;
                var ll_count_item = 0;
                if (sys_valu == 1)
                    ll_count_item = ll_count_flag + ll_main_count + 2;
                else
                    ll_count_item = ll_count_flag + ll_main_count + 1;

                var sub_code_len = bCode_len - ll_count_item;
                if (bCode_len > 0)
                {
                    if (ll_count_item > bCode_len)
                    {
                        // Print error 
                        // خطأ كود الصنف اكبر من باركود النظام
                        // return
                    }
                    else
                    {
                        var ls_sub = $"0000000000{li_sub}"[^(int)sub_code_len..];
                        if (sys_valu == 1)
                            ls_code = ll_flag1 + $"-{ls_main}-{ls_sub}";
                        else
                            ls_code = $"{ls_main}-{ls_sub}";
                    }
                }
                else
                {
                    if (sys_valu == 1)
                        ls_code = ll_flag1 + $"-{ls_main}-{li_sub}";
                    else
                        ls_code = $"{ls_main}-{li_sub}";
                }
            }
            return ls_code;
        }

        public async Task<SubItemEditAddDTO> GetNewSubItemAsync(int mainId)
        {
            var subItemDTO = new SubItemEditAddDTO();

            MainItem mainItem = await _Context.MainItems.FindAsync(mainId);

            if (mainItem != null)
            {
                _mapper.Map(mainItem, subItemDTO);
                subItemDTO.MainLevel = mainItem.ParentCode == "1" ? "MainLevel_2" : $"MainLevel_{mainItem.CurrentLevel}";
                subItemDTO.ParentCode = mainItem.ParentCode;
                subItemDTO.MainName = mainItem.MainName;
                subItemDTO.ItemCode = await GenerateItemCode(mainItem);
            }

            List<CostCenter> costCenters = await _Context.CostCenters.Where(obj =>
                (obj.CostFlag == 1 || obj.CostFlag == 10) && obj.CostVisible == 1).ToListAsync();
            subItemDTO.CostCenters = _mapper.Map<List<CostCenterViewDTO>>(costCenters);

            List<SupCode> suppliers = await _Context.SupCodes.ToListAsync();
            subItemDTO.Suppliers = _mapper.Map<List<SupCodeViewDTO>>(suppliers);

            List<StentDes> stentDes = await _Context.StentDess.ToListAsync();
            subItemDTO.StentDess = _mapper.Map<List<StentDesDTO>>(stentDes);

            return subItemDTO;
        }

        public async Task<SubItemEditAddDTO> GetSubItemByIdAsync(int id)
        {
            SubItem subItem = await GetByIdAsync(id);
            MainItem mainItem = await _Context.MainItems.FindAsync(subItem.MainId);
            var subItemDTO = _mapper.Map<SubItemEditAddDTO>(subItem);

            if (mainItem != null)
            {
                subItemDTO.MainLevel = mainItem.ParentCode == "1" ? "MainLevel_2" : $"MainLevel_{mainItem.CurrentLevel}";
                subItemDTO.ParentCode = mainItem.ParentCode;
                subItemDTO.MainName = mainItem.MainName;
            }
            List<CostCenter> costCenters = await _Context.CostCenters.Where(obj =>
                (obj.CostFlag == 1 || obj.CostFlag == 10) && obj.CostVisible == 1).ToListAsync();
            subItemDTO.CostCenters = _mapper.Map<List<CostCenterViewDTO>>(costCenters);

            List<SupCode> suppliers = await _Context.SupCodes.ToListAsync();
            subItemDTO.Suppliers = _mapper.Map<List<SupCodeViewDTO>>(suppliers);

            List<StentDes> stentDes = await _Context.StentDess.ToListAsync();
            subItemDTO.StentDess = _mapper.Map<List<StentDesDTO>>(stentDes);

            return subItemDTO;
        }

        private async Task<bool> IfExistsInStockBalanceAsync(SubItem subItem)
        {
            Stkbalance stockNalance = await _Context.Stkbalances.FirstOrDefaultAsync(obj => obj.ItemCode == subItem.ItemCode
                && obj.Flag1 == subItem.Flag1);
            return stockNalance != null ? true : false;
        }

        private async Task AddStockBalanceAsync(SubItem subItem, int fYear)
        {
            List<MainItemStock> mainItemStockList = await _Context.MainItemStocks.Where(obj => obj.MainCode == subItem.MainCode
                && obj.Flag1 == subItem.Flag1 && obj.BranchId == subItem.BranchId)
                .ToListAsync();
            foreach (var item in mainItemStockList)
            {
                var stockNalance = new Stkbalance();
                stockNalance.Stkcod = (short)item.Stkcod;
                stockNalance.ItemCode = subItem.ItemCode;
                stockNalance.ItemPrice = subItem.ItemPrice;
                stockNalance.FYear = fYear;
                stockNalance.MainCode = item.MainCode;
                stockNalance.Flag1 = item.Flag1;
                stockNalance.BranchId = item.BranchId;
                stockNalance.ItemId = 0;
                stockNalance.BarCode = subItem.ItemCode;
                stockNalance.Ser = int.Parse(subItem.SubCode);

                await _Context.AddAsync(stockNalance);
            }
        }

        public async Task AddSubItemAsync(int mainId, SubItemEditAddDTO subItemDTO, int fYear)
        {
            MainItem mainItem = await _Context.MainItems.FindAsync(mainId);
            var subItem = _mapper.Map<SubItem>(subItemDTO);
            _mapper.Map(mainItem, subItem);
            _mapper.Map(subItemDTO, subItem);

            List<SubItem> otherSubItems = await _DbSet.Where(obj => obj.MainId == mainId).ToListAsync();
            subItem.SubCode = otherSubItems.Count == 0 ? 
                "000001" :
                $"00000{int.Parse(otherSubItems.Max(obj => obj.SubCode)) + 1}"[^6..];

            if (!(await IfExistsInStockBalanceAsync(subItem)))
                await AddStockBalanceAsync(subItem, fYear);
            await AddAsync(subItem);
            await SaveChangesAsync();
        }

        public async Task EditSubItemAsync(int id, SubItemEditAddDTO subItemDTO)
        {
            SubItem subItem = await GetByIdAsync(id);
            _mapper.Map(subItemDTO, subItem);
            subItem.SubId = id;

            await UpdateAsync(subItem);
            await SaveChangesAsync();
        }

        public async Task<int> IfPossibleToDeleteAsync(int subItemID)
        {
            SubItem subItem = await GetByIdAsync(subItemID);
            bool ifStkBalanceExists = (await _Context.Stkbalances.ToListAsync()).Exists(obj => obj.ItemCode == 
                subItem.ItemCode && obj.Flag1 == subItem.Flag1);
            bool ifTransDtlExists = (await _Context.TransDtls.ToListAsync()).Exists(obj => obj.ItemMaster ==
                subItem.ItemCode && obj.Flag1 == subItem.Flag1);
            return ifStkBalanceExists ? 1 : ifTransDtlExists ? 2 : 3;
        }
    }
}
