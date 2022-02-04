using Microsoft.EntityFrameworkCore;
using RepositoryPattern.Core.Consts;
using RepositoryPattern.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPattern.EF.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected ApplicationDbContext _context;

        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<T>> GetAllAsync() 
            => await _context.Set<T>().ToListAsync();


        public T GetById(int id) 
            => _context.Set<T>().Find(id);


        public async Task<T> GetByIdAsync(int id) 
            => await _context.Set<T>().FindAsync(id);



        public async Task<T> FindAsync(Expression<Func<T, bool>> criteria, string[] includes = null)
        {
            IQueryable<T> query = _context.Set<T>();
            if (includes != null)

                foreach (var include in includes)
                    query = query.Include(include);

            return await query.SingleOrDefaultAsync(criteria);
        }

        public async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> criteria, string[] includes = null)
        {
            IQueryable<T> query = _context.Set<T>();
            if (includes != null)

                foreach (var include in includes)
                    query = query.Include(include);

            return query.Where(criteria).ToList();
        }

        public async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> criteria, int take, int skip)
        {
            return _context.Set<T>().Where(criteria).Skip(skip).Take(take).ToList();
        }

        
        public async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> criteria, int? take, int? skip,
            Expression<Func<T, object>> orderBy = null, string orderByDirection = OrderBy.Ascending)
        {
            IQueryable<T> query = _context.Set<T>().Where(criteria);

            if (take.HasValue)
                query = query.Take(take.Value);

            if (skip.HasValue)
                query = query.Skip(skip.Value);

            if (orderBy != null)
            {
                if(orderByDirection == OrderBy.Ascending)
                    query = query.OrderBy(orderBy);
                else
                    query = query.OrderByDescending(orderBy);
            }
            return await query.ToListAsync();
        }

        public async Task<T> Add(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            _context.SaveChanges();

            return entity;
        }

        public async Task<IEnumerable<T>> AddRange(IEnumerable<T> entities)
        {
            await _context.Set<T>().AddRangeAsync(entities);
            _context.SaveChanges();

            return entities;
        }

    }
}
