using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProSoft.EF.DbContext;
using ProSoft.EF.DTOs.Stocks;
using ProSoft.EF.IRepositories.Stocks;
using ProSoft.EF.Models.Shared;
using ProSoft.EF.Models.Stocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ProSoft.Core.Repositories.Stocks
{
    public class OrderLimitRepo : Repository<ItmReorder, int>, IOrderLimitRepo
    {
        private readonly IMapper _mapper;
        public OrderLimitRepo(AppDbContext Context, IMapper mapper) : base(Context)
        {
            _mapper = mapper;
        }

        private ItmReorder GetNewItemReorderAsnyc(SubItem subItem, DateTime fromDate, DateTime toDate,
            int stockID/*, int branchID*/)
        {
            var itemReorder = new ItmReorder();
            itemReorder.ItemCd = subItem.ItemCode;
            itemReorder.StoreId = stockID;
            itemReorder.ReordQty = 0;
            itemReorder.MaxQty = 0;
            itemReorder.MinQty = 0;
            itemReorder.BranchId = subItem.BranchId;
            itemReorder.FDate = fromDate;
            itemReorder.TDate = toDate;
            return itemReorder;
        }

        public async Task<List<ItmReorderViewDTO>> GetItemsLimitsByDatesAsync(DateTime fromDate, DateTime toDate,
            int stockID, int branchID)
        {
            List<ItmReorder> itemReorders = await GetAllAsync();
            List<SubItem> subItemsInBranch = await _Context.SubItems.Where(obj => obj.BranchId == branchID).ToListAsync();
            List<Stock> stocks = await _Context.Stocks.Where(obj => obj.BranchId == branchID).ToListAsync();
            List<MainItemStock> mainItemStocks = await _Context.MainItemStocks.Where(obj =>
                obj.Stkcod == stockID &&
                obj.BranchId == branchID).Distinct().ToListAsync();
            mainItemStocks = mainItemStocks.Where(obj => stocks.Exists(s => s.Stkcod == obj.Stkcod && s.Flag1 == obj.Flag1)).ToList();

            var itemReorderDTOs = new List<ItmReorderViewDTO>();
            foreach (var item in subItemsInBranch)
            {
                if (mainItemStocks.Exists(obj => obj.MainCode == item.MainCode && obj.Flag1 == item.Flag1))
                {
                    ItmReorder itemReorder = await _DbSet.FirstOrDefaultAsync(obj => obj.ItemCd == item.ItemCode &&
                        obj.StoreId == stockID);
                    if (itemReorder != null)
                    {
                        var itemReorderDTO = _mapper.Map<ItmReorderViewDTO>(itemReorder);
                        itemReorderDTO.ItemName = item.SubName;
                        itemReorderDTOs.Add(itemReorderDTO);
                    }
                    else
                    {
                        ItmReorder newItemReorder = GetNewItemReorderAsnyc(item, fromDate, toDate, stockID/*, branchID*/);
                        await AddAsync(newItemReorder);

                        var itemReorderDTO = _mapper.Map<ItmReorderViewDTO>(newItemReorder);
                        itemReorderDTO.ItemName = item.SubName;
                        itemReorderDTOs.Add(itemReorderDTO);
                    }
                }
            }
            await SaveChangesAsync();
            return itemReorderDTOs;
        }

        public async Task<ItmReorderEditDTO> GetItemReorderByIdAsync(int itemReorderId, int branchId)
        {
            ItmReorder itmReorder = await GetByIdAsync(itemReorderId);
            var itmReorderDTO = _mapper.Map<ItmReorderEditDTO>(itmReorder);

            SubItem subItem = await _Context.SubItems.FirstOrDefaultAsync(obj => 
                obj.ItemCode == itmReorder.ItemCd && obj.BranchId == branchId);
            itmReorderDTO.ItemName = subItem?.SubName;

            return itmReorderDTO;
        }

        public async Task EditItemReorderAsync(int itemReorderId, ItmReorderEditDTO orderLimitDTO)
        {
            ItmReorder itmReorder = await GetByIdAsync(itemReorderId);
            _mapper.Map(orderLimitDTO, itmReorder);
            await UpdateAsync(itmReorder);
            await SaveChangesAsync();
        }
    }
}
