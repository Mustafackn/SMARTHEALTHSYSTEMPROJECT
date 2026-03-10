using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace SmartHealthSystem.Application.Contracts
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<bool> Exists(int id);
        IQueryable<T> GetWhere(Expression<Func<T, bool>> predicate);
    }
}
