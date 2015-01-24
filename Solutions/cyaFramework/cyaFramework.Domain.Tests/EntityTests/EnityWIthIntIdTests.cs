using System;
using System.Diagnostics.CodeAnalysis;
using cyaFramework.Domain.Tests.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace cyaFramework.Domain.Tests.EntityTests
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class EnityWIthIntIdTests
    {
        [TestMethod]
        public void IsNew_ForNewInstance_ReturnsTrue()
        {
            var entity = new EntityWithIdOfInt();

            Assert.AreEqual(0, entity.Id);
            Assert.IsTrue(entity.IsNew());
        }

        [TestMethod]
        public void IsNew_WithNonDefaultValues_ReturnFalse()
        {
            var entity = new EntityWithIdOfInt
            {
                Id = 1
            };

            Assert.IsFalse(entity.IsNew());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Id_ResetToDefaultValue_ThrowsException()
        {
            var entity = new EntityWithIdOfInt
            {
                Id = 1
            };

            entity.Id = 0;
            Assert.Inconclusive();
        }

        [TestMethod]
        public void TwoNewInstances_ShouldBeEqual()
        {
            var entity1 = new EntityWithIdOfInt();
            var entity2 = new EntityWithIdOfInt();

            Assert.IsTrue(Equals(entity1, entity2));
        }

        [TestMethod]
        public void TwoInstances_WithSameId_ShouldBeEqual()
        {
            var entity1 = new EntityWithIdOfInt { Id = 1 };
            var entity2 = new EntityWithIdOfInt { Id = 1 };

            Assert.IsTrue(Equals(entity1, entity2));
            Assert.IsTrue(entity2.Equals(entity1));
            Assert.AreEqual(entity1.GetHashCode(), entity2.GetHashCode());
        }

        [TestMethod]
        public void TwoInstances_WithDifferentIds_Should_NOT_BeEqual()
        {
            var entity1 = new EntityWithIdOfInt { Id = 1 };
            var entity2 = new EntityWithIdOfInt { Id = 2 };

            Assert.IsFalse(Equals(entity1, entity2));
            Assert.IsFalse(entity2.Equals(entity1));
            Assert.AreNotEqual(entity1.GetHashCode(), entity2.GetHashCode());
        }

        [TestMethod]
        public void NonNullInstance_ComparedWillNullInstanse_IsFalse()
        {
            EntityWithIdOfInt entity1 = new EntityWithIdOfInt { Id =1 };
            EntityWithIdOfInt entity2 = null;

            Assert.IsFalse(entity1.Equals(entity2));
        }

        [TestMethod]
        public void NonNullInstance_ComparedWillObject_IsFalse()
        {
            EntityWithIdOfInt entity1 = new EntityWithIdOfInt { Id = 1 };
            object entity2 = null;

            Assert.IsFalse(entity1.Equals(entity2));
        }

        [TestMethod]
        public void Compare_InstanceWithTransientID_With_InstanceWithNonTransientID_AreNotEqual()
        {
            var entity1 = new EntityWithIdOfInt();
            var entity2 = new EntityWithIdOfInt { Id = 1 };

            Assert.IsFalse(entity1.Equals(entity2));
        }

    }
}
