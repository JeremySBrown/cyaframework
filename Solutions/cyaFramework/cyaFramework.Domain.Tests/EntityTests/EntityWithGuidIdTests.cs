using System;
using cyaFramework.Domain.Tests.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace cyaFramework.Domain.Tests.EntityTests
{
    [TestClass]
    public class EntityWithGuidIdTests
    {
        [TestMethod]
        public void IsNew_ForNewInstance_ReturnsTrue()
        {
            var entity = new EntityWithIdOfGuid();

            Assert.AreEqual(Guid.Empty, entity.Id);
            Assert.IsTrue(entity.IsNew());
        }

        [TestMethod]
        public void IsNew_WithNonDefaultValues_ReturnFalse()
        {
            var entity = new EntityWithIdOfGuid
            {
                Id = Guid.NewGuid()
            };

            Assert.IsFalse(entity.IsNew());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Id_ResetToDefaultValue_ThrowsException()
        {
            var entity = new EntityWithIdOfGuid
            {
                Id = Guid.NewGuid()
            };

            entity.Id = Guid.Empty;
        }

        [TestMethod]
        public void TwoNewInstances_ShouldBeEqual()
        {
            var entity1 = new EntityWithIdOfGuid();
            var entity2 = new EntityWithIdOfGuid();

            Assert.IsTrue(Equals(entity1,entity2));
        }

        [TestMethod]
        public void TwoInstances_WithSameId_ShouldBeEqual()
        {
            var id = Guid.NewGuid();
            var entity1 = new EntityWithIdOfGuid{Id = id};
            var entity2 = new EntityWithIdOfGuid {Id = id};

            Assert.IsTrue(Equals(entity1, entity2));
        }

        [TestMethod]
        public void TwoInstances_WithDifferentIds_Should_NOT_BeEqual()
        {
            var entity1 = new EntityWithIdOfGuid { Id = Guid.NewGuid() };
            var entity2 = new EntityWithIdOfGuid { Id = Guid.NewGuid() };
            
            Assert.IsFalse(Equals(entity1, entity2));
        }
    }
}
