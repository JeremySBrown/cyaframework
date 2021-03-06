﻿using System;
using System.Diagnostics.CodeAnalysis;
using cyaFramework.Domain.Tests.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace cyaFramework.Domain.Tests.EntityTests
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class EntityWithStringIdTests
    {
        [TestMethod]
        public void IsNew_ForNewInstance_ReturnsTrue()
        {
            var entity = new EntityWithIdOfString() ;

            Assert.IsNull(entity.Id);
            Assert.IsTrue(entity.IsNew());
        }

        [TestMethod]
        public void IsNew_WithNonDefaultValues_ReturnFalse()
        {
            var entity = new EntityWithIdOfString
            {
                Id = "12345"
            };

            Assert.IsFalse(entity.IsNew());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Id_ResetToDefaultValue_ThrowsException()
        {
            var entity = new EntityWithIdOfString
            {
                Id = "12345"
            };

            entity.Id = null;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Id_ResetToEmptyString_ThrowsException()
        {
            var entity = new EntityWithIdOfString
            {
                Id = "12345"
            };

            entity.Id = string.Empty;
        }

        [TestMethod]
        public void TwoNewInstances_ShouldBeEqual()
        {
            var entity1 = new EntityWithIdOfString();
            var entity2 = new EntityWithIdOfString();

            Assert.IsTrue(Equals(entity1, entity2));
            Assert.IsTrue(entity2.Equals(entity1));
            Assert.AreEqual(entity1.GetHashCode(), entity2.GetHashCode());
        }

        [TestMethod]
        public void TwoInstances_WithSameId_ShouldBeEqual()
        {
            string id = "123456_ABC";
            var entity1 = new EntityWithIdOfString { Id = id };
            var entity2 = new EntityWithIdOfString { Id = id };

            Assert.IsTrue(Equals(entity1, entity2));
            Assert.IsTrue(entity2.Equals(entity1));
            Assert.AreEqual(entity1.GetHashCode(), entity2.GetHashCode());
        }

        [TestMethod]
        public void TwoInstances_WithDifferentIds_Should_NOT_BeEqual()
        {
            var entity1 = new EntityWithIdOfString { Id = "12345" };
            var entity2 = new EntityWithIdOfString { Id = "ABCDE" };

            Assert.IsFalse(Equals(entity1, entity2));
            Assert.IsFalse(entity2.Equals(entity1));
            Assert.AreNotEqual(entity1.GetHashCode(), entity2.GetHashCode());
        }

        [TestMethod]
        public void NonNullInstance_ComparedWillNullInstanse_IsFalse()
        {
            EntityWithIdOfString entity1 = new EntityWithIdOfString { Id = "12345" };
            EntityWithIdOfString entity2 = null;

            Assert.IsFalse(entity1.Equals(entity2));
        }

        [TestMethod]
        public void NonNullInstance_ComparedWillObject_IsFalse()
        {
            EntityWithIdOfString entity1 = new EntityWithIdOfString { Id = "12345" };
            object entity2 = null;

            Assert.IsFalse(entity1.Equals(entity2));
        }

        [TestMethod]
        public void Compare_InstanceWithTransientID_With_InstanceWithNonTransientID_AreNotEqual()
        {
            var entity1 = new EntityWithIdOfString();
            var entity2 = new EntityWithIdOfString { Id = "12345" };

            Assert.IsFalse(entity1.Equals(entity2));
        }
    }
}
