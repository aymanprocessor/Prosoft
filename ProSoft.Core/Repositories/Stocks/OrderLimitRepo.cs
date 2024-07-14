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

        public ItmReorder GetNewItemReorderAsnyc(SubItem subItem, DateTime fromDate, DateTime toDate,
            int stockID, int branchID)
        {
            var itemReorder = new ItmReorder();
            itemReorder.ItemCd = subItem.ItemCode;
            itemReorder.StoreId = stockID;
            itemReorder.ReordQty = 0;
            itemReorder.MaxQty = 0;
            itemReorder.MinQty = 0;
            itemReorder.BranchId = branchID;
            itemReorder.FDate = fromDate;
            itemReorder.TDate = toDate;
            return itemReorder;
        }

        public async Task<List<ItmReorderDTO>> GetItemsLimitsAsync(/*int item1, int item2,*/DateTime fromDate, DateTime toDate,
            int stockID, int branchID)
        {
            List<ItmReorder> orderLimits = await _DbSet
                //.Where(obj => int.Parse(obj.ItemCd) >= item1 &&
                //int.Parse(obj.ItemCd) <= item2)
                .Where(obj => obj.StoreId == stockID && obj.BranchId == branchID &&
                obj.FDate >= fromDate && obj.TDate <= toDate)
                .ToListAsync();
            List<SubItem> subItems = await _Context.SubItems.Where(obj => obj.BranchId == branchID/* && obj.SubId > 3031*/
                /*&& int.Parse(obj.ItemCode) >= item1 && int.Parse(obj.ItemCode) <= item2*/)
                .ToListAsync();

            var itemReorderDTOs = new List<ItmReorderDTO>();
            if(orderLimits.Count == 0)
            {
                //if(subItems.Count != 0)
                //{
                //}
                foreach (var item in subItems)
                {
                    //MainItemStock mainItemStock = await _Context.MainItemStocks.FirstOrDefaultAsync(obj =>
                    //    obj.MainCode == item.MainCode && obj.BranchId == branchID && obj.Stkcod == stockID &&
                    //    obj.Flag1 == item.Flag1);
                    //Stock stock = await _Context.Stocks.FindAsync(stockID);

                    ItmReorder itemReorder = GetNewItemReorderAsnyc(item, fromDate, toDate, stockID, branchID);
                    await AddAsync(itemReorder);

                    var itemReorderDTO = _mapper.Map<ItmReorderDTO>(itemReorder);
                    itemReorderDTO.ItemName = item.SubName;
                    itemReorderDTOs.Add(itemReorderDTO);
                }
                await SaveChangesAsync();
            }
            else
            {
                foreach (var item in subItems)
                {
                    ItmReorder itemReorder = await _DbSet.FirstOrDefaultAsync(obj => obj.ItemCd == item.ItemCode &&
                        obj.FDate == fromDate && obj.TDate == toDate);
                    if(itemReorder == null)
                    {
                        ItmReorder newItemReorder = GetNewItemReorderAsnyc(item, fromDate, toDate, stockID, branchID);
                        await AddAsync(newItemReorder);

                        var itemReorderDTO = _mapper.Map<ItmReorderDTO>(newItemReorder);
                        itemReorderDTO.ItemName = item.SubName;
                        itemReorderDTOs.Add(itemReorderDTO);
                    }
                    else
                    {
                        var itemReorderDTO = _mapper.Map<ItmReorderDTO>(itemReorder);
                        itemReorderDTO.ItemName = item.SubName;
                        itemReorderDTOs.Add(itemReorderDTO);
                    }
                }
                await SaveChangesAsync();
            }
            return itemReorderDTOs;
        }

        //public async Task<List<ItmReorderDTO>> GetItemsLimitsAsync(DateTime date1, DateTime date2, int stockID, int branchID)
        //{
        //}
    }
}
