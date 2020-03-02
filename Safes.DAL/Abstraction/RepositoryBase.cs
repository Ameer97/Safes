using Microsoft.EntityFrameworkCore;
using Safes.Infrastructure.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Safes.DAL.Abstraction
{
    public class RepositoryBase<TModel, TDbContext> : IRepositoryBase<TModel>
        where TModel : class, new() where TDbContext : DbContext, new()
    {
        private bool _disposed;
        protected TDbContext RepositoryContext { get; set; }

        protected RepositoryBase(TDbContext repositoryContext)
        {
            RepositoryContext = repositoryContext;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public IEnumerable<TModel> FindAll()
        {
            var propertyInfo = typeof(TModel).GetProperty("IsDeleted");
            return RepositoryContext.Set<TModel>().AsNoTracking();
        }

        public IEnumerable<TModel> FindByCondition(Expression<Func<TModel, bool>> expression)
        {
            var propertyInfo = typeof(TModel).GetProperty("IsDeleted");
            return RepositoryContext.Set<TModel>().Where(expression).AsNoTracking();
        }

        public async Task<TModel> FindItemByCondition(Expression<Func<TModel, bool>> expression)
        {
            var propertyInfo = typeof(TModel).GetProperty("IsDeleted");
            return await RepositoryContext.Set<TModel>().AsNoTracking().FirstOrDefaultAsync(expression);
        }

        public void Insert(TModel entity)
        {
            RepositoryContext.Set<TModel>().Add(entity);
            Save();
        }

        public void InsertRange(List<TModel> entity)
        {
            RepositoryContext.Set<TModel>().AddRange(entity);
            Save();
        }

        public void Update(TModel entity)
        {
            RepositoryContext.Set<TModel>().Update(entity);
            Save();
        }
        public void UpdateRange(IEnumerable<TModel> entities)
        {
            RepositoryContext.Set<TModel>().UpdateRange(entities);
            Save();
        }

        public void Save()
        {
            RepositoryContext.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
                if (disposing)
                    RepositoryContext.Dispose();

            _disposed = true;
        }
    }
}
