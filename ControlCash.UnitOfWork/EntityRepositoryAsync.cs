using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ControlCash.UnitOfWork
{
    public class EntityRepositoryAsync<T> : IEntityRepositoryAsync<T> where T : class
    {
        protected internal readonly DbContext _context;

        public EntityRepositoryAsync(DbContext context)
        {
            _context = context;
        }

        public virtual ICollection<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public virtual async Task<ICollection<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public virtual T Get(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public virtual async Task<T> GetAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public virtual T Find(Expression<Func<T, bool>> match)
        {
            return _context.Set<T>().SingleOrDefault(match);
        }

        public virtual async Task<T> FindAsync(Expression<Func<T, bool>> match)
        {
            return await _context.Set<T>().SingleOrDefaultAsync(match);
        }

        public virtual ICollection<T> FindAll(Expression<Func<T, bool>> match)
        {
            return _context.Set<T>().Where(match).ToList();
        }

        public virtual async Task<ICollection<T>> FindAllAsync(Expression<Func<T, bool>> match)
        {
            return await _context.Set<T>().Where(match).ToListAsync();
        }

        public virtual T Add(T t)
        {
            _context.Set<T>().Add(t);
            _context.SaveChanges();
            return t;
        }

        public virtual async Task<T> AddAsync(T t)
        {
            _context.Set<T>().Add(t);
            await _context.SaveChangesAsync();
            return t;
        }

        public virtual T Update(T updated, int key)
        {
            if (updated == null)
                return null;

            T existing = _context.Set<T>().Find(key);
            if (existing != null)
            {
                _context.Entry(existing).CurrentValues.SetValues(updated);
                _context.SaveChanges();
            }
            return existing;
        }

        public virtual async Task<T> UpdateAsync(T updated, int key)
        {
            if (updated == null)
                return null;

            T existing = await _context.Set<T>().FindAsync(key);
            if (existing != null)
            {
                _context.Entry(existing).CurrentValues.SetValues(updated);
                await _context.SaveChangesAsync();
            }
            return existing;
        }

        public virtual void Delete(T t)
        {
            _context.Set<T>().Remove(t);
            _context.SaveChanges();
        }

        public virtual async Task<int> DeleteAsync(T t)
        {
            _context.Set<T>().Remove(t);
            return await _context.SaveChangesAsync();
        }

        public virtual int Count()
        {
            return _context.Set<T>().Count();
        }

        public virtual async Task<int> CountAsync()
        {
            return await _context.Set<T>().CountAsync();
        }
    }
}
