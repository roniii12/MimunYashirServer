using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MimunYashirPersistence.Repositories
{
    public interface IAsyncRepository<T> : IAsyncReadOnlyRepository<T>
    {
        Task<T> GetByIdAsync(object id);
        Task<T> AddAsync(T entity, bool isTransaction = false);
        Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities, bool isTransaction = false);
        Task UpdateAsync(T entity, bool isTransaction = false);
        Task UpdateAsync<TProperty>(T entity, Expression<Func<T, TProperty>> exceptedPropUpdate, bool isTransaction = false);
        Task DeleteAsync(T entity, bool isTransaction = false);
        Task DeleteAllAsync(IQuerySpecification<T> spec, bool isTransaction = false);
        Task<IQueryable<T>> QueryAllAsync();
        Task<IQueryable<T>> QueryAsync(IQuerySpecification<T> spec);

        //Task SaveTransactionAsync();
    }
    public interface IAsyncReadOnlyRepository<T>
    {

        Task<IReadOnlyList<T>> ListAllAsync();

        Task<IReadOnlyList<T>> ListAsync(IQuerySpecification<T> spec);
        Task<IReadOnlyList<TResult>> SelectAllAsync<TResult>(Expression<Func<T, TResult>> selectExpression);
        Task<IReadOnlyList<TResult>> SelectAsync<TResult>(IQuerySpecification<T> spec, Expression<Func<T, TResult>> selectExpression);
        Task<T> FirstOrDefaultAsync(IQuerySpecification<T> spec);
        Task<TResult> MaxAsync<TResult>(Expression<Func<T, TResult>> maxExperession);
        Task<int> CountAsync(IQuerySpecification<T> spec);
    }
}
