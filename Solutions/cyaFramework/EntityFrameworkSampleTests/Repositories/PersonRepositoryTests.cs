using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.AccessControl;
using EntityFrameworkSample;
using EntityFrameworkSample.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using SampleDomain.Models;

using EntityFrameworkSampleTests.Extensions;

namespace EntityFrameworkSampleTests.Repositories
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class PersonRepositoryTests
    {
        [TestMethod]
        public void Find_WithValidId_Returns_Person()
        {
            var data = GetTestData();
            var mockDbSet = Substitute.For<IDbSet<Person>, DbSet<Person>>().MockDbSet(data);
            mockDbSet.Find(Arg.Any<Guid>()).Returns(data.FirstOrDefault());
            var mockContext = Substitute.For<SampleDomainContext>();
            //mockContext.People.Returns(mockDbSet);
            mockContext.Set<Person>().Returns(mockDbSet);

            var repository = new PersonRepository(mockContext);
            var id = new Guid("00000000-0000-0000-0000-000000000003");
            var model = repository.Find(id);

            Assert.IsNotNull(model);

        }

        [TestMethod]
        public void Find_WithInvalidId_Returns_Null()
        {
            var data = GetTestData();
            var mockDbSet = Substitute.For<IDbSet<Person>, DbSet<Person>>().MockDbSet(data);
            mockDbSet.Find(Arg.Any<Guid>()).Returns((Person)null);
            var mockContext = Substitute.For<SampleDomainContext>();
            mockContext.Set<Person>().Returns(mockDbSet);

            var repository = new PersonRepository(mockContext);
            var id = new Guid("00000000-0000-0000-0000-000000000003");
            var model = repository.Find(id);

            Assert.IsNull(model);
        }

        [TestMethod]
        public void Find_WithCustomFilter_Returns_Model()
        {
            var data = GetTestData();
            var mockDbSet = Substitute.For<IDbSet<Person>, DbSet<Person>>().MockDbSet(data);
            var mockContext = Substitute.For<SampleDomainContext>();
            mockContext.Set<Person>().Returns(mockDbSet);

            var repository = new PersonRepository(mockContext);

            var expectedLastName = "User03";
            var model = repository.Find(p=>p.LastName == expectedLastName);

            Assert.IsNotNull(model);
            Assert.AreEqual(expectedLastName, model.LastName);
        }

        [TestMethod]
        public void FindAll_NoParams_Returns_EntireCollection()
        {
            var data = GetTestData();
            var mockDbSet = Substitute.For<IDbSet<Person>, DbSet<Person>>().MockDbSet(data);
            var mockContext = Substitute.For<SampleDomainContext>();
            mockContext.Set<Person>().Returns(mockDbSet);

            var repository = new PersonRepository(mockContext);


            var result = repository.FindAll();

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count == 5);
        }

        [TestMethod]
        public void FindAll_WithCustom_Returns_EntireCollection()
        {
            var data = GetTestData();
            var mockDbSet = Substitute.For<IDbSet<Person>, DbSet<Person>>().MockDbSet(data);
            var mockContext = Substitute.For<SampleDomainContext>();
            mockContext.Set<Person>().Returns(mockDbSet);

            var repository = new PersonRepository(mockContext);


            var result = repository.FindAll();

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count == 5);
        }

        [TestMethod]
        public void FindAll_WithCustomFilter_Returns_CollectionOfModel_InDescendingOrder()
        {
            var data = GetTestData();
            var mockDbSet = Substitute.For<IDbSet<Person>, DbSet<Person>>().MockDbSet(data);
            var mockContext = Substitute.For<SampleDomainContext>();
            mockContext.Set<Person>().Returns(mockDbSet);

            var repository = new PersonRepository(mockContext);
            
            var result = repository.FindAll("LastName Desc");
            var model = result.FirstOrDefault();
            
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count == 5);
            Assert.AreEqual("User05", model.LastName);
        }

        [TestMethod]
        public void FindAll_WithCustomFilter_Returns_CollectionOfModel()
        {
            var data = GetTestData();
            var mockDbSet = Substitute.For<IDbSet<Person>, DbSet<Person>>().MockDbSet(data);
            var mockContext = Substitute.For<SampleDomainContext>();
            mockContext.Set<Person>().Returns(mockDbSet);

            var repository = new PersonRepository(mockContext);

            var result = repository.FindAll(p=>p.FirstName == "Test");
            

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count == 4);
        }

        private IQueryable<Person> GetTestData()
        {
            return new List<Person>
                   {
                       new Person
                       {
                           Id = new Guid("00000000-0000-0000-0000-000000000001"),
                           FirstName = "Test",
                           LastName = "User01",
                           Email = "test.user01@notarealemail.com"
                       },
                       new Person
                       {
                           Id = new Guid("00000000-0000-0000-0000-000000000002"),
                           FirstName = "Test",
                           LastName = "User02",
                           Email = "test.user02@notarealemail.com"
                       },
                       new Person
                       {
                           Id = new Guid("00000000-0000-0000-0000-000000000003"),
                           FirstName = "Test",
                           LastName = "User03",
                           Email = "test.user01@notarealemail.com"
                       },
                       new Person
                       {
                           Id = new Guid("00000000-0000-0000-0000-000000000003"),
                           FirstName = "OddManOut",
                           LastName = "User04",
                           Email = "test.user04@notarealemail.com"
                       },
                       new Person
                       {
                           Id = new Guid("00000000-0000-0000-0000-000000000001"),
                           FirstName = "Test",
                           LastName = "User05",
                           Email = "test.user05@notarealemail.com"
                       }
                   }.AsQueryable();
        }
    }
}
