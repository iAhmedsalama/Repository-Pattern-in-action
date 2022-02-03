using Microsoft.EntityFrameworkCore;
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



        public async Task<T> FindAsync(Expression<Func<T, bool>> match, string[] includes = null)
        {
            IQueryable<T> query = _context.Set<T>();
            if (includes != null)

                foreach (var include in includes)
                    query = query.Include(include);

            return await query.SingleOrDefaultAsync(match);
        }

        public async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> match, string[] includes = null)
        {
            IQueryable<T> query = _context.Set<T>();
            if (includes != null)

                foreach (var include in includes)
                    query = query.Include(include);

            return query.Where(match).ToList();
        }
    }
}
