using System;
using cyaFramework.Domain.Tests.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace cyaFramework.Domain.Tests.EntityTests
{
    [TestClass]
    public class EntityGenericIdTests
    {
        [TestMethod]
        public void IsNew_For_Guid_Id_Should_Return_True_For_New()
        {
            var entity = new EntityWithIdOfGuid();

            Assert.AreEqual(Guid.Empty, entity.Id);
            Assert.IsTrue(entity.IsNew());
        }

        [TestMethod]
        public void IsNew_For_Guid_Id_Should_Return_False_For_Non_Default_Ids()
        {
            var entity = new EntityWithIdOfGuid()
                         {
                             Id = Guid.NewGuid()
                         };
            
            Assert.IsFalse(entity.IsNew());
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void For_Guid_Id_Should_Not_Be_Able_To_Set_Id_To_Default()
        {
            var entity = new EntityWithIdOfGuid()
            {
                Id = Guid.NewGuid()
            };

            entity.Id = Guid.Empty;
        }

        [TestMethod]
        public void IsNew_For_Int_Id_Should_Return_True_For_New()
        {
            var entity = new EntityWithIdOfInt();

            Assert.AreEqual(0, entity.Id);
            Assert.IsTrue(entity.IsNew());
        }

        [TestMethod]
        public void IsNew_For_Int_Id_Should_Return_False_For_Non_Default_Ids()
        {
            var entity = new EntityWithIdOfInt()
            {
                Id = 1
            };

            Assert.IsFalse(entity.IsNew());
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void For_Int_Id_Should_Not_Be_Able_To_Set_Id_To_Default()
        {
            var entity = new EntityWithIdOfInt()
            {
                Id = 1
            };

            entity.Id = 0;
        }

        [TestMethod]
        public void IsNew_For_String_Id_Should_Return_True_For_New()
        {
            var entity = new EntityWithIdOfString() ;

            Assert.IsNull(entity.Id);
            Assert.IsTrue(entity.IsNew());
        }

        [TestMethod]
        public void IsNew_For_String_Id_Should_Return_False_For_Non_EmptyStrings()
        {
            var entity = new EntityWithIdOfString()
            {
                Id = "12345"
            };

            Assert.IsFalse(entity.IsNew());
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void For_String_Id_Should_Not_Be_Able_To_Set_Id_To_Null()
        {
            var entity = new EntityWithIdOfString()
            {
                Id = "12345"
            };

            entity.Id = null;
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void For_String_Id_Should_Not_Be_Able_To_Set_Id_To_EmptyString()
        {
            var entity = new EntityWithIdOfString()
            {
                Id = "12345"
            };

            entity.Id = string.Empty;
        }
    }
}
