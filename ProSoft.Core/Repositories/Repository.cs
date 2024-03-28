using Microsoft.EntityFrameworkCore;
using ProSoft.EF.DbContext;
using ProSoft.EF.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.Core.Repositories
{
    public class Repository<T, Key> : IRepository<T, Key> where T : class
    {
        protected readonly AppDbContext _Context;
        protected readonly DbSet<T> _DbSet;
        public Repository(AppDbContext Context)
        {
            _Context = Context;
            _DbSet = _Context.Set<T>();
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _DbSet.ToListAsync();
        }

        public async Task<T> GetByIdAsync(Key id)
        {
            return await _DbSet.FindAsync(id);
        }

        public async Task AddAsync(T entity)
        {
            await _DbSet.AddAsync(entity);
        }

        public async Task UpdateAsync(T entity)
        {
            _DbSet.Update(entity);
        }

        public async Task DeleteAsync(T entity)
        {
            _DbSet.Remove(entity);
        }

        public async Task SaveChangesAsync()
        {
            await _Context.SaveChangesAsync();
        }
    }
}
