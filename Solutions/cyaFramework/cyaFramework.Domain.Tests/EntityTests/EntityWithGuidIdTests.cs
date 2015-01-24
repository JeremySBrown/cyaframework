using System;
using System.Diagnostics.CodeAnalysis;
using cyaFramework.Domain.Tests.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace cyaFramework.Domain.Tests.EntityTests
{
    [TestClass]
    [ExcludeFromCodeCoverage]
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
            Assert.AreEqual(entity1.GetHashCode(),entity2.GetHashCode());
        }

        [TestMethod]
        public void TwoInstances_WithSameId_ShouldBeEqual()
        {
            var id = Guid.NewGuid();
            var entity1 = new EntityWithIdOfGuid{Id = id};
            var entity2 = new EntityWithIdOfGuid {Id = id};

            Assert.IsTrue(Equals(entity1, entity2));
            Assert.IsTrue(entity2.Equals(entity1));
            Assert.AreEqual(entity1.GetHashCode(), entity2.GetHashCode());
        }

        [TestMethod]
        public void TwoInstances_WithDifferentIds_Should_NOT_BeEqual()
        {
            var entity1 = new EntityWithIdOfGuid { Id = Guid.NewGuid() };
            var entity2 = new EntityWithIdOfGuid { Id = Guid.NewGuid() };
            
            Assert.IsFalse(Equals(entity1, entity2));
            Assert.IsFalse(entity2.Equals(entity1));
            Assert.AreNotEqual(entity1.GetHashCode(), entity2.GetHashCode());
        }

        [TestMethod]
        public void NonNullInstance_ComparedWillNullInstanse_IsFalse()
        {
            EntityWithIdOfGuid entity1 = new EntityWithIdOfGuid { Id = Guid.NewGuid() };
            EntityWithIdOfGuid entity2 = null;

            Assert.IsFalse(entity1.Equals(entity2));
        }

        [TestMethod]
        public void NonNullInstance_ComparedWillObject_IsFalse()
        {
            EntityWithIdOfGuid entity1 = new EntityWithIdOfGuid { Id = Guid.NewGuid() };
            object entity2 = null;

            Assert.IsFalse(entity1.Equals(entity2));
        }

        [TestMethod]
        public void Compare_InstanceWithTransientID_With_InstanceWithNonTransientID_AreNotEqual()
        {
            var entity1 = new EntityWithIdOfGuid();
            var entity2 = new EntityWithIdOfGuid {Id = Guid.NewGuid()};

            Assert.IsFalse(entity1.Equals(entity2));
        }

        
    }
}
