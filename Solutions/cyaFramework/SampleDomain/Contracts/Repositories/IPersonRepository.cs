using System;
using cyaFramework.Domain.Contracts.Repositories;
using SampleDomain.Models;

namespace SampleDomain.Contracts.Repositories
{
    public interface IPersonRepository : IRepositoryBase<Person,Guid>
    {
         
    }
}