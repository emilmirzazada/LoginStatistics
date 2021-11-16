using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LoginStatistics.Application.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task DeleteAll(string tableName);
        Task<T> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
