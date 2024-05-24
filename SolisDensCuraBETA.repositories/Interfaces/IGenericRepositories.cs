using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SolisDensCuraBETA.repositories.Interfaces
{
    public interface IGenericRepositories<T> : IDisposable
    {
        Task<IEnumerable<T>> GetAllAsync();
        IEnumerable<T> GetAll(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "");
        T GetById(object id);
        Task<T> GetByIdAsync(object id);
        void Add(T entity);
        Task<T> AddAsync(T entity);
        void Update(T entity);
        Task<T> DeleteAsync(T entity);
        void Delete(T entity);
        int Count(Expression<Func<T, bool>> predicate = null);
        IQueryable<T> Where(Expression<Func<T, bool>> predicate);
        Task SaveChangesAsync();
    }
}
