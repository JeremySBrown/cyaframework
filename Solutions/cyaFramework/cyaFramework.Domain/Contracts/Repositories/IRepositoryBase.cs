using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace cyaFramework.Domain.Contracts.Repositories
{
    public interface IRepositoryBase<T, TId>
        where T : class, new()
        where TId : struct
    {
        T Find(TId id, params string[] includePaths);
        T Find(Expression<Func<T, bool>> filter, params string[] includePaths);

        IList<T> FindAll(params string[] includePaths);

        IList<T> FindAll(string orderBy, params string[] includePaths);

        IList<T> FindAll(Expression<Func<T, bool>> filter, params string[] includePaths);

        IList<T> FindAll(Expression<Func<T, bool>> filter, string orderBy, params string[] includePaths);

        T Save(T model);

        bool Delete(T model);
        bool Delete(TId id);
    }
}