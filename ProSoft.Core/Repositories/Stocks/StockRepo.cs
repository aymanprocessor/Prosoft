using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProSoft.EF.DbContext;
using ProSoft.EF.DTOs.Accounts;
using ProSoft.EF.DTOs.Shared;
using ProSoft.EF.DTOs.Stocks;
using ProSoft.EF.IRepositories.Stocks;
using ProSoft.EF.Migrations;
using ProSoft.EF.Models.Accounts;
using ProSoft.EF.Models.Shared;
using ProSoft.EF.Models.Stocks;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.Core.Repositories.Stocks
{
    public class StockRepo : Repository<Stock, int>, IStockRepo
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public StockRepo(AppDbContext Context, IMapper mapper) : base(Context)
        {
            _context = Context;
            _mapper = mapper;
        }


        // ------------------- Coded By Ayman Saad ------------------- //


        public IEnumerable<SelectListItem> GetAllStockAsEnumerable()
        {
            return _context.Stocks.Select(s => new SelectListItem { Value = s.Stkcod.ToString(), Text = s.Stknam })
                .ToList();
        }



        public async Task<List<StockViewDTO>> GetActiveStocksForUserAsync(int userCode)
        {
            List<StockEmp> stockTransList = await _Context.StockEmps
                .Where(obj => obj.UserId == userCode && obj.StockDef == 1).ToListAsync();
            List<Stock> stocksList = await _Context.Stocks.ToListAsync();

            var stocksDTO = new List<StockViewDTO>();
            var isExisted = false;
            foreach (var stock in stocksList)
            {
                foreach (var stockTrans in stockTransList)
                {
                    if (stock.Stkcod == stockTrans.Stkcod)
                    {
                        isExisted = true;
                        break;
                    }
                    else
                        isExisted = false;
                }
                if (isExisted)
                {
                    var stockDTO = new StockViewDTO();
                    stockDTO.Stkcod = stock.Stkcod;
                    stockDTO.Stknam = (await _Context.Stocks
                        .FindAsync(stock.Stkcod)).Stknam;
                    stocksDTO.Add(stockDTO);
                }
            }
            return stocksDTO;
        }


        public async Task<List<Stock>> GetStocksDependOnFromStock(int stockId)
        {
            return await _Context.Stocks.Where(obj => obj.Stkcod != stockId).ToListAsync();
        }
        // ------------------- Coded By Ayman Saad ------------------- //


        public async Task<List<StockViewDTO>> GetAllStocksAsync()
        {
            List<StockViewDTO> stocksDTO = await _Context.Stocks
            .Select(obj => new StockViewDTO()
            {
                Stkcod = obj.Stkcod,
                Stknam = obj.Stknam,
                StockTypeName = obj.Flag1Navigation.KName,
                BranchName = obj.Branch.BranchDesc,
                StockType = (int)obj.StockType,
                StockPurchOnshelf = (int)obj.StockPurchOnshelf,
                CalculusJournal = obj.JornalCodeNavigation.JournalName,
                StkOnOff = (int)obj.StkOnOff
            })
            .ToListAsync() ?? new ();
            return stocksDTO;
        }

        public async Task<int> GetNewIdAsync()
        {
            int newID;
            if (_DbSet.Count() != 0)
            {
                var lastID = await _DbSet.MaxAsync(obj => obj.Stkcod);
                newID = lastID + 1;
            }
            else
                newID = 1;
            return newID;
        }

        public async Task<StockEditAddDTO> GetEmptyStockAsync()
        {
            StockEditAddDTO stockDTO = new ();

            List<Branch> branches = await _Context.Branchs.ToListAsync();
            List<KindStore> stocksTypes = await _Context.KindStores.ToListAsync();
            List<JournalType> calculusJournals = await _Context.JournalTypes.ToListAsync();
            stockDTO.Branches = _mapper.Map<List<BranchDTO>>(branches);
            stockDTO.StocksTypes = _mapper.Map<List<KindStoreDTO>>(stocksTypes);
            stockDTO.CalculusJournals = _mapper.Map<List<JournalTypeDTO>>(calculusJournals);

            return stockDTO;
        }

        public async Task<StockEditAddDTO> GetStockByIdAsync(int id)
        {
            Stock stock = await GetByIdAsync(id);
            StockEditAddDTO stockDTO = _mapper.Map<StockEditAddDTO>(stock);

            List<Branch> branches = await _Context.Branchs.ToListAsync();
            List<KindStore> stocksTypes = await _Context.KindStores.ToListAsync();
            List<JournalType> calculusJournals = await _Context.JournalTypes.ToListAsync();
            stockDTO.Branches = _mapper.Map<List<BranchDTO>>(branches);
            stockDTO.StocksTypes = _mapper.Map<List<KindStoreDTO>>(stocksTypes);
            stockDTO.CalculusJournals = _mapper.Map<List<JournalTypeDTO>>(calculusJournals);

            return stockDTO;
        }

       
    }
}
