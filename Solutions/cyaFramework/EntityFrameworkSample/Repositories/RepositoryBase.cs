using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Expressions;
using cyaFramework.Domain.Contracts.Entities;
using cyaFramework.Domain.Contracts.Repositories;

namespace EntityFrameworkSample.Repositories
{
    public abstract class RepositoryBase<T, TId> : IRepositoryBase<T, TId>, IDisposable
        where T: class, IEntityBase<TId>
    {
        internal DbContext _dbContext;

        protected RepositoryBase(DbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public virtual T Find(TId id, params string[] includePaths)
        {
            return _dbContext.Set<T>().Find(id);
        }

        public virtual T Find(Expression<Func<T, bool>> filter, params string[] includePaths)
        {
            DbQuery<T> query = _dbContext.Set<T>();
            query = includePaths.Aggregate(query, (current, includePath) => current.Include(includePath));
            return query.Where(filter).FirstOrDefault();
        }

        public IQueryable<T> All
        {
            get { return _dbContext.Set<T>(); }
            
        }

        public IQueryable<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _dbContext.Set<T>();
            return includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }

        public virtual IList<T> FindAll(params string[] includePaths)
        {
            return FindAll(null, null, includePaths);
        }

        public virtual IList<T> FindAll(string orderBy, params string[] includePaths)
        {
            return FindAll(null, orderBy, includePaths);
        }

        public virtual IList<T> FindAll(Expression<Func<T, bool>> filter, params string[] includePaths)
        {
            return FindAll(filter, null, includePaths);
        }

        public virtual IList<T> FindAll(Expression<Func<T, bool>> filter, string orderBy, params string[] includePaths)
        {
            DbQuery<T> query = _dbContext.Set<T>();
            query = includePaths.Aggregate(query, (current, includePath) => current.Include(includePath));

            if (filter != null)
            {
                return string.IsNullOrEmpty(orderBy)
                    ? query.Where(filter).ToList()
                    : query.Where(filter).OrderBy(orderBy).ToList();
            }

            return string.IsNullOrEmpty(orderBy)
                           ? query.ToList()
                           : query.OrderBy(orderBy).ToList();
        }

        public virtual T Save(T model)
        {
            model = _dbContext.Set<T>().Add(model);
            foreach (var entry in _dbContext.ChangeTracker.Entries<IEntityBase<TId>>())
            {
                entry.State = entry.Entity.IsNew() ? EntityState.Added : EntityState.Modified;
            }

            _dbContext.SaveChanges();

            return model;
        }

        public virtual bool Delete(T model)
        {
            return Delete(model.Id);
        }

        public virtual bool Delete(TId id)
        {
            var model = _dbContext.Set<T>().Find(id);

            if (model != null)
            {
                foreach (var entry in _dbContext.ChangeTracker.Entries<IEntityBase<TId>>())
                {
                    entry.State = EntityState.Deleted;
                }
                _dbContext.Set<T>().Remove(model);
                return _dbContext.SaveChanges() > 0;
            }

            return false;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // The bulk of the clean-up code is implemented in Dispose(bool)
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dbContext.Dispose();
            }
            
        }
    }
}