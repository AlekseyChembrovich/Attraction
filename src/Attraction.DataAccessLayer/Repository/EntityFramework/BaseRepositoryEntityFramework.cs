using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Attraction.DataAccessLayer.Repository.EntityFramework
{
    public class BaseRepositoryEntityFramework<T> : IRepository<T> where T : class, new()
    {
        protected readonly DatabaseContextEntityFramework Context;

        public BaseRepositoryEntityFramework(DatabaseContextEntityFramework context)
        {
            Context = context;
        }

        public int Insert(T entity)
        {
            if (entity is null) return 0;
            Context.Set<T>().Add(entity);
            try
            {
                var count = Context.SaveChanges();
                return count;
            }
            catch (DbUpdateConcurrencyException)
            {

            }

            return 0;
        }

        public int Update(T entity)
        {
            if (entity is null) return 0;
            Context.Entry(entity).State = EntityState.Detached;
            Context.Set<T>().Attach(entity);
            Context.Set<T>().Update(entity);
            try
            {
                var count = Context.SaveChanges();
                return count;
            }
            catch (DbUpdateConcurrencyException)
            {

            }

            return 0;
        }

        public int Delete(T entity)
        {
            if (entity is null) return 0;
            Context.Set<T>().Attach(entity);
            Context.Set<T>().Remove(entity);
            try
            {
                var count = Context.SaveChanges();
                return count;
            }
            catch (DbUpdateConcurrencyException)
            {

            }

            return 0;
        }

        public IEnumerable<T> GetAll() 
        {
            return Context.Set<T>().AsNoTracking().AsEnumerable();
        }

        public T GetById(int id)
        {
            var entity = Context.Set<T>().Find(id);
            if (entity is not null)
            {
                Context.Entry(entity).State = EntityState.Detached;
            }

            return entity;
        }
    }
}
