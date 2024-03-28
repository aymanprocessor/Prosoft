using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.IRepositories
{
    public interface IRepository<T, Key> where T : class
    {
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(Key id);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task SaveChangesAsync();
    }
}
