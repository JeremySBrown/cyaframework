using System;
using SampleDomain.Models;

namespace EntityFrameworkSample.Repositories
{
    public class PersonRepository : RepositoryBase<Person, Guid>
    {
        
        public PersonRepository(SampleDomainContext dbContext)
            : base(dbContext)
        {
            
        }
    }
}