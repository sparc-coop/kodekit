using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace UI_Kit.Storage
{
    public class DbRepository<T> : IRepository<T> where T : class
    {
        protected readonly DbContext context;
        protected readonly DbSet<T> Command;
        public IQueryable<T> Query { get; private set; }

        public DbRepository(DbContext context)
        {
            this.context = context;
            Command = context.Set<T>();
            Query = context.Set<T>();
        }

        //private IQueryable<T> Include(IQueryable<T> source, params string[] path)
        //{
        //    foreach (var item in path)
        //    {
        //        source = source.Include(item);
        //    }

        //    return source;
        //}

        //public IQueryable<T> Include(IRepository<T> repository, params string[] path)
        //{
        //    return Include(repository.Query, path);
        //}

        //public IQueryable<T> Include<T, TProperty>(IRepository<T> repository, params Expression<Func<T, TProperty>>[] navigationPropertyPath) where T : class
        //{
        //    var query = repository.Query;

        //    foreach (var item in navigationPropertyPath)
        //    {
        //        query = query.Include(item);
        //    }

        //    return query;
        //}

        public T Find(object id)
        {
            return Command.Find(id);
        }

        public virtual async Task<T> FindAsync(object id)
        {
            return await Command.FindAsync(id);
        }

        public T Find(Func<T, bool> query)
        {
            return Command.Where(query).FirstOrDefault();
        }

        public virtual async Task<T> FindAsync(Func<T, bool> query)
        {
            return await Command.Where(query).AsQueryable().FirstOrDefaultAsync();
        }

        // Commands
        public T Add(T item)
        {
            Command.Add(item);
            return item;
        }

        public IEnumerable<T> AddRange(IEnumerable<T> entities)
        {
            Command.AddRange(entities);
            return entities;
        }

        public async Task<T> AddAsync(T item)
        {
            Command.Add(item);
            return item;
        }

        public void Update(T item)
        {
            context.Attach(item);
            context.Entry(item).State = EntityState.Modified;
        }

        public async Task UpdateAsync(T item)
        {
            context.Attach(item);
            context.Entry(item).State = EntityState.Modified;
        }

        public void Delete(T item)
        {
            Command.Remove(item);
        }

        public void Execute(object id, Action<T> action)
        {
            var entity = context.Set<T>().Find(id);
            action(entity);
        }

        public void Execute(T entity, Action<T> action)
        {
            action(entity);
            //Commit();
        }

        public virtual async Task ExecuteAsync(object id, Action<T> action)
        {
            var entity = await context.Set<T>().FindAsync(id);
            action(entity);
            //await context.SaveChangesAsync();
        }

        public virtual async Task ExecuteAsync(T entity, Action<T> action)
        {
            action(entity);
            //await context.SaveChangesAsync();
        }

        public void Commit()
        {
            context.SaveChanges();
        }

        public async Task CommitAsync()
        {
            await context.SaveChangesAsync();
        }

        public IQueryable<T> Include(Expression<Func<T, object>> includes)
        {
            return Query.Include(includes).AsQueryable();
        }
    }
}
