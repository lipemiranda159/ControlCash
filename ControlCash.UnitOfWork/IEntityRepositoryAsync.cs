using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ControlCash.UnitOfWork
{
    public interface IEntityRepositoryAsync<T> where T : class
    {
        ICollection<T> GetAll();

        Task<ICollection<T>> GetAllAsync();

        T Get(int id);

        Task<T> GetAsync(int id);

        T Find(Expression<Func<T, bool>> match);

        Task<T> FindAsync(Expression<Func<T, bool>> match);
        ICollection<T> FindAll(Expression<Func<T, bool>> match);

        Task<ICollection<T>> FindAllAsync(Expression<Func<T, bool>> match);

        T Add(T t);

        Task<T> AddAsync(T t);

        T Update(T updated, int key);
        Task<T> UpdateAsync(T updated, int key);

        void Delete(T t);


        Task<int> DeleteAsync(T t);

        int Count();

        Task<int> CountAsync();

    }
}
