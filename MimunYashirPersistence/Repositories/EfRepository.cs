using Microsoft.EntityFrameworkCore;
using MimunYashirInfrastructure.Cummon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MimunYashirPersistence.Repositories
{
    public abstract class EfRepository<T> : IAsyncRepository<T>, IDisposable where T : class
    {
        protected readonly Microsoft.EntityFrameworkCore.DbContext _dbContext;
        protected readonly IAppContext _serviceContext;

        protected EfRepository(Microsoft.EntityFrameworkCore.DbContext dbContext, IAppContext serviceContext)
        {
            _dbContext = dbContext;
            _serviceContext = serviceContext;
        }

        public virtual async Task<T> GetByIdAsync(object id)
        {
            var entity = await _dbContext.Set<T>().FindAsync(id).ConfigureAwait(false);
            return entity;
        }

        public async Task<IQueryable<T>> QueryAllAsync()
        {
            return _dbContext.Set<T>()
                .AsQueryable();
        }

        public async Task<IQueryable<T>> QueryAsync(IQuerySpecification<T> spec)
        {
            return ApplySpecification(spec);
        }

        public async Task<IReadOnlyList<T>> ListAllAsync()
        {
            return await _dbContext.Set<T>()
                .AsQueryable().ToListAsync().ConfigureAwait(false);

        }

        public async Task<IReadOnlyList<T>> ListAsync(IQuerySpecification<T> spec)
        {
            return await ApplySpecification(spec)
                .ToListAsync().ConfigureAwait(false);
        }

        public async Task<IReadOnlyList<TResult>> SelectAllAsync<TResult>(Expression<Func<T, TResult>> selectExpression)
        {
            return await _dbContext.Set<T>()
               .AsQueryable().Select(selectExpression).ToListAsync().ConfigureAwait(false);
        }

        public async Task<IReadOnlyList<TResult>> SelectAsync<TResult>(IQuerySpecification<T> spec, Expression<Func<T, TResult>> selectExpression)
        {
            return await ApplySpecification(spec)
                .Select(selectExpression).ToListAsync().ConfigureAwait(false);
        }

        public async Task<T> FirstOrDefaultAsync(IQuerySpecification<T> spec)
        {
            return await ApplySpecification(spec)
                .FirstOrDefaultAsync().ConfigureAwait(false);
        }

        public async Task<int> CountAsync(IQuerySpecification<T> spec)
        {
            return await ApplySpecification(spec)//.Where(x => x.EnvId == _serviceContext.EnvId)
                .CountAsync().ConfigureAwait(false);
        }
        public async Task<TResult> MaxAsync<TResult>(Expression<Func<T, TResult>> maxExperession)
        {
            return await _dbContext.Set<T>().MaxAsync(maxExperession).ConfigureAwait(false);
        }

        public async Task<T> AddAsync(T entity, bool isTransaction = false)
        {
            _dbContext.Set<T>().Add(entity);
            if (!isTransaction)
                await _dbContext.SaveChangesAsync().ConfigureAwait(false);

            return entity;
        }

        public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities, bool isTransaction = false)
        {
            _dbContext.Set<T>().AddRange(entities);

            if (!isTransaction)
                await _dbContext.SaveChangesAsync().ConfigureAwait(false);

            return entities;
        }

        public async Task UpdateAsync(T entity, bool isTransaction = false)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            if (!isTransaction)
            {
                await _dbContext.SaveChangesAsync().ConfigureAwait(false);
            }
        }
        public async Task UpdateAsync<TProperty>(T entity, Expression<Func<T, TProperty>> exceptedPropUpdate, bool isTransaction = false)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.Entry(entity).Property(exceptedPropUpdate).IsModified = false;
            if (!isTransaction)
            {
                await _dbContext.SaveChangesAsync().ConfigureAwait(false);
            }
        }

        public async Task DeleteAsync(T entity, bool isTransaction = false)
        {
            _dbContext.Set<T>().Remove(entity);
            if (!isTransaction)
                await _dbContext.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task DeleteAllAsync(IQuerySpecification<T> spec, bool isTransaction = false)
        {
            _dbContext.Set<T>().RemoveRange(ApplySpecification(spec));
            if (!isTransaction)
                await _dbContext.SaveChangesAsync().ConfigureAwait(false);
        }
        public IQueryable<T> ApplySpecification(IQuerySpecification<T> spec)
        {
            return QuerySpecificationEvaluator<T>.GetQuery(_dbContext.Set<T>().AsQueryable(), spec);
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
