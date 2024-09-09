
/*
 * Coded By Ayman Saad @ 3/9/2024
*/
using Microsoft.EntityFrameworkCore;
using ProSoft.EF.DbContext;
using ProSoft.EF.DTOs.Stocks;
using ProSoft.EF.IRepositories;
using ProSoft.EF.IRepositories.Stocks;
using ProSoft.EF.Models.Shared;
using ProSoft.EF.Models.Stocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.Core.Repositories.Stocks
{
    public class StockEmpFlagRepo : Repository<StockEmpFlag, int>, IStockEmpFlagRepo
    {
        private readonly AppDbContext _context;

        public StockEmpFlagRepo(AppDbContext Context) : base(Context)
        {
            _context = Context;
        }

        public IEnumerable<StockEmpFlag> GetStockEmpFlags()
        {
            return _context.StockEmpFlags
                .Include(s => s.Stock)
                .Include(s => s.Branch)
                .Include(s => s.KindStore)

                .AsNoTracking()
                .ToList();
        }
        public StockEmpFlag? GetById(int userId, int stkcod, int branchId, int kId)
        {
            return _context.StockEmpFlags
                .Include(s => s.Stock)
                .Include(s => s.Branch)
                .Include(s => s.KindStore)
                .FirstOrDefault(s => s.Stkcod == stkcod && s.UserId == userId && s.KID == kId && s.BranchId == branchId);
        }

        public async Task Add(StockEmpFlagEditAddDTO model)
        {
            StockEmpFlag newData = new()
            {
                BranchId = model.BranchId,
                KID = model.KID,
                Stkcod = model.Stkcod,
                UserId = model.UserCode

            };
            await _context.StockEmpFlags.AddAsync(newData);
            await _context.SaveChangesAsync();

        }

        public async Task Delete(int userId, int stkcod, int branchId, int kId)
        {
            StockEmpFlag? daleteData = await _context.StockEmpFlags.FindAsync(userId, stkcod, branchId, kId);

            if (daleteData != null)
            {
                _context.StockEmpFlags.Remove(daleteData);
                await _context.SaveChangesAsync();
            }


        }
        public async Task<IGeneralResponse<StockEmpFlag>> Update(int userId, int stkcod, int branchId, int kId, StockEmpFlagEditAddDTO model)
        {
            StockEmpFlag? existingData = await _context.StockEmpFlags.FindAsync(userId, stkcod, branchId, kId);

            if (existingData != null)
            {
                // _context.Entry(existingData).State = EntityState.Detached;

                using (var transaction = await _context.Database.BeginTransactionAsync())
                {
                    try
                    {
                        _context.StockEmpFlags.Remove(existingData);
                        await _context.SaveChangesAsync();


                        existingData.Stkcod = model.Stkcod;
                        existingData.KID = model.KID;

                        await _context.StockEmpFlags.AddAsync(existingData);
                        await _context.SaveChangesAsync();

                        await transaction.CommitAsync();
                        Console.WriteLine("Object Modified Successfully");

                        return new GeneralResponse<StockEmpFlag>();
                    }
                    catch (DbUpdateException ex)
                    {
                        // Check for primary key violation
                        if (ex.InnerException != null && ex.InnerException.Message.Contains("duplicate key"))
                        {
                            // Handle duplicate key error (primary key or unique constraint violation)
                            Console.WriteLine("Duplicate primary key detected.");
                            // You can log this or provide a user-friendly message
                            return new GeneralResponse<StockEmpFlag>(message: "Duplicate primary key detected");

                        }
                        else
                        {
                            // Log other database update exceptions
                            Console.WriteLine("An error occurred while saving the entity: " + ex.Message);
                            return new GeneralResponse<StockEmpFlag>(message: "An error occurred while saving the entity: " + ex.Message);

                        }

                    }
                    catch (Exception ex)
                    {

                        await transaction.RollbackAsync();
                        Console.WriteLine(ex.Message);
                        return new GeneralResponse<StockEmpFlag>(message: "An error occurred while saving the entity: " + ex.Message);


                    }
                }

            }
            return new GeneralResponse<StockEmpFlag>(message: "Object is not found" );

        }

       
    }
}
