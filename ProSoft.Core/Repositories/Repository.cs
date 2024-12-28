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
            try
            {
                return await _DbSet.ToListAsync();

            }
            catch (Exception ex)
            {

                throw new RepositoryException($"Error retrieving all {typeof(T).Name}s.", ex);
            }
        }

        public async Task<T> GetByIdAsync(Key id)
        {
            try
            {
                return await _DbSet.FindAsync(id);
            }
            catch (Exception ex)
            {
                throw new RepositoryException($"Error retrieving {typeof(T).Name} with ID {id}.", ex);
            }
        }

        public async Task AddAsync(T entity)
        {
            try
            {
                await _DbSet.AddAsync(entity);
            }
            catch (Exception ex)
            {
                throw new RepositoryException($"Error adding new {typeof(T).Name}.", ex);
            }
        }

        public async Task UpdateAsync(T entity)
        {
            try
            {
                _DbSet.Update(entity);
            }
            catch (Exception ex)
            {
                throw new RepositoryException($"Error updating {typeof(T).Name}.", ex);
            }
        }

        public async Task DeleteAsync(T entity)
        {
            try
            {
               
                _DbSet.Remove(entity);
            }
            catch (Exception ex)
            {
               throw new RepositoryException($"Error deleting {typeof(T).Name}.", ex);
            }
        }

        public async Task SaveChangesAsync()
        {
            try
            {
                await _Context.SaveChangesAsync();
            }
            catch (DbUpdateException dbEx)
            {
               //_logger.LogError(dbEx, "An error occurred while updating the database.");
                throw new RepositoryException("An error occurred while saving changes to the database.", dbEx);
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, "An unexpected error occurred while saving changes.");
                throw new RepositoryException("An unexpected error occurred while saving changes.", ex);
            }
        }
    }
}
