﻿using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using ProSoft.EF.DbContext;
using ProSoft.EF.IRepositories.Stocks;
using ProSoft.EF.Models.Stocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.Core.Repositories.Stocks
{
    public class StockBalanceRepo : Repository<Stkbalance, int>, IStockBalanceRepo
    {
        private readonly AppDbContext _context;

        public StockBalanceRepo(AppDbContext Context) : base(Context)
        {
            _context = Context;
        }

      
        public async Task BulkInsert(IEnumerable<Stkbalance> entities)
        {
            _context.BulkInsert(entities);
        }

        public async Task BulkUpdate(IEnumerable<Stkbalance> entities)
        {
           await _context.BulkUpdateAsync(entities);
        }

        public Task<List<Stkbalance>> GetAllByStockId(int id)
        {
            return _context.Stkbalances.AsSplitQuery()
                .Include(s=>s.MainItem)
                .Include(s=>s.SubItem)
                .Include(s=>s.UnitCodee)
                .Where(s => s.Stkcod == id).ToListAsync();
        }
    }
}
