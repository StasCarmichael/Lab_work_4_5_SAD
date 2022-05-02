using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

namespace UoW.Repository
{
    public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        // узагальнений контекст
        protected DbContext dbContext;
        protected DbSet<TEntity> dbSet;


        // CONSTRUCTORS
        public GenericRepository(DbContext dbContext)
        {
            this.dbContext = dbContext;
            this.dbSet = dbContext.Set<TEntity>();
        }

        // METHODS
        public virtual int Count() { return dbSet.Count(); }

        public virtual IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null,
                                            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null)
        {
            // filter
            IQueryable<TEntity> query = dbSet;
            if (filter != null)
                query = query.Where(filter);

            // ordering
            if (orderBy != null) query = orderBy(query);

            return query;
        }

        public virtual TEntity Get(int id) { return dbSet.Find(id); }


        public virtual void Insert(TEntity entity) { dbSet.Add(entity); }
        public virtual void Update(TEntity item)
        {
            dbContext.Entry(item).State = EntityState.Modified;
        }


        public virtual void Delete(int id)
        {
            // find
            TEntity entityToDelete = dbSet.Find(id);

            // delete finded
            if (entityToDelete == null) throw new InvalidOperationException("There is no records with such id");
            Delete(entityToDelete);
        }
        public virtual void Delete(TEntity entityToDelete)
        {
            if (entityToDelete == null) throw new ArgumentNullException(nameof(entityToDelete));

            if (dbContext.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }
        public virtual void Delete(Expression<Func<TEntity, bool>> predicate)
        {
            if (predicate != null) dbSet.RemoveRange(dbSet.Where(predicate));
            else dbSet.RemoveRange(dbSet);
        }


        public void Save() => dbContext.SaveChanges();


        #region IDispose

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
                if (disposing)
                    dbContext.Dispose();

            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

    }
}
