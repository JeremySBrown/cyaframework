using System.Data.Entity;
using SampleDomain.Models;

namespace EntityFrameworkSample
{
    public class SampleDomainContext : DbContext
    {
        public SampleDomainContext():base("DefaultConnectionString")
        {
            
        }

        public DbSet<Person> People { get; set; }
    }
}