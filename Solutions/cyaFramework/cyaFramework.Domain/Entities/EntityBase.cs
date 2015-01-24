using System;
using System.Reflection;
using cyaFramework.Domain.Contracts.Entities;

namespace cyaFramework.Domain.Entities
{
    public abstract class EntityBase<TId> : IEquatable<EntityBase<TId>>, IEntityBase<TId>
    {
        internal TId _id;
        internal TypeInfo _idTypeInfo;

        public virtual TId Id
        {
            get { return _id; }
            set
            {
                string msg = "Entity Id cannot be set to default value once it has been set to a non-default value.";
                if (Equals(value, default(TId))
                    && !IsNew())
                {
                    throw new ArgumentException(msg);
                }

                if (Equals(value, string.Empty)
                    && !IsNew())
                {
                    throw new ArgumentException(msg);
                }
                
                
                _id = value;
            }
        }

        public virtual bool IsNew()
        {
            return Equals(this.Id, default(TId));
        }
        
        public override bool Equals(object otherObject)
        {
            var otherEntity = otherObject as EntityBase<TId>;
            if (otherEntity == null)
            {
                return false;
            }

            // compare two new objects
            bool otherIsTransient = Equals(otherEntity.Id, default(TId));
            bool thisIsTransient = Equals(this.Id, default(TId));
            if (otherIsTransient && thisIsTransient)
            {
                return true;
            }

            return otherEntity.Id.Equals(this.Id);
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }

        public bool Equals(EntityBase<TId> other)
        {
            if (other == null)
            {
                return false;
            }

            return this.Id.Equals(other.Id);
        }

        internal TypeInfo GetIdTypeInfo()
        {
            return _idTypeInfo ?? (_idTypeInfo = typeof(TId).GetTypeInfo());
        }
    }
}