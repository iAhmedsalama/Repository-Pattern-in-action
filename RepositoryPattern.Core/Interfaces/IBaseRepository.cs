using RepositoryPattern.Core.Consts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPattern.Core.Interfaces
{
    public interface IBaseRepository<T> where T:class
    {
        T GetById(int id);
        Task<T> GetByIdAsync(int id);

        Task<IEnumerable<T>> GetAllAsync();

        Task<T> FindAsync(Expression<Func<T, bool>> criteria, string[] includes = null);

        Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> criteria, string[] includes = null);

        Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> criteria, int take, int skip);

        Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> criteria, int? take, int? skip,
            Expression<Func<T, object>> orderBy = null, string orderByDirection = OrderBy.Ascending);

        Task<T> Add(T entity);

        Task<IEnumerable<T>> AddRange(IEnumerable<T> entities);

        Task<T> Update(T entity);

        void Delete(T entity);

        void DeleteRange(IEnumerable<T> entities);

        void Attach(T entity);

        Task<int> Count();

        Task<int> Count(Expression<Func<T, bool>> criteria);
    }
}
