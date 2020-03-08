using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Safes.Infrastructure.Interfaces.Repositories
{
    public interface IRepositoryBase<T> : IDisposable
    {
        IEnumerable<T> FindAll();
        IEnumerable<T> FindAllTakeSkip(int? start, int? end);
        IEnumerable<T> FindByCondition(Expression<Func<T, bool>> expression);
        Task<T> FindItemByCondition(Expression<Func<T, bool>> expression);
        void Insert(T entity);
        void InsertRange(List<T> entity);
        void Update(T entity);
        void UpdateRange(IEnumerable<T> entity);
        void Save();
    }
}
