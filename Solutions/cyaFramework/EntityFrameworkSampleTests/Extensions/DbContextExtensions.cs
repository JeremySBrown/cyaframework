using System.Data.Entity;
using System.Linq;
using NSubstitute;

namespace EntityFrameworkSampleTests.Extensions
{
    public static class DbContextExtensions
    {
        public static IDbSet<T> MockDbSet<T>(this IDbSet<T> dbSet, IQueryable<T> data)
            where T : class
        {
            dbSet.Provider.Returns(data.Provider);
            dbSet.Expression.Returns(data.Expression);
            dbSet.ElementType.Returns(data.ElementType);
            dbSet.GetEnumerator().Returns(data.GetEnumerator());

            return dbSet;
        }
    }
}