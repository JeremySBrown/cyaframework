using System;
using System.Reflection;
using cyaFramework.Domain.Contracts.Entities;

namespace cyaFramework.Domain.Entities
{
    public abstract class EntityBase<TId> : IEquatable<EntityBase<TId>>, IEntityBase<TId>
    {
        internal TId _id;

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
            bool thisIsTransient = Equals(this.Id, default(TId));
            return thisIsTransient ? 0 : this.Id.GetHashCode();
        }

        public virtual bool Equals(EntityBase<TId> other)
        {
            if (other == null)
            {
                return false;
            }

            // compare two new objects
            bool otherIsTransient = Equals(other.Id, default(TId));
            bool thisIsTransient = Equals(this.Id, default(TId));
            if (otherIsTransient && thisIsTransient)
            {
                return true;
            } 
            
            if (thisIsTransient)
            {
                return false;
            }

            return this.Id.Equals(other.Id);
        }
    }
}