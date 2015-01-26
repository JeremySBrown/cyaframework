using System;
using System.Data.Entity;

namespace EntityFrameworkSample.Contracts
{
    public interface IUnitOfWork<TContext>: IDisposable where TContext: DbContext
    {
        int Save();
        TContext Context { get; }
    }
}