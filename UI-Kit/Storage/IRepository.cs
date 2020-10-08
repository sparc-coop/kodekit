using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace UI_Kit.Storage
{
    public interface IRepository<T>
    {
        IQueryable<T> Query { get; }
        T Find(object id);
        Task<T> FindAsync(object id);
        T Find(Func<T, bool> query);
        Task<T> FindAsync(Func<T, bool> query);
        //IQueryable<T> Include(IRepository<T> repository, params string[] path);
        //IQueryable<T> Include<T, TProperty>(IRepository<T> repository, params Expression<Func<T, TProperty>>[] navigationPropertyPath) where T : class;

        // Commands
        T Add(T item);
        Task<T> AddAsync(T item);
        IEnumerable<T> AddRange(IEnumerable<T> entities);
        void Update(T item);
        Task UpdateAsync(T item);
        void Delete(T item);
        void Execute(object id, Action<T> action);
        void Execute(T entity, Action<T> action);
        Task ExecuteAsync(object id, Action<T> action);
        Task ExecuteAsync(T entity, Action<T> action);
        void Commit();
        Task CommitAsync();
    }
}
