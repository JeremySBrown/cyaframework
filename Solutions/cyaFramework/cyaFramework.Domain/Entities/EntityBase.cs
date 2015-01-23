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
                string msg = "Entity Id cannot be set to default value. Only during contruction.";
                var typeInfo = typeof(TId).GetTypeInfo();
                if (typeInfo.IsValueType
                    && value.Equals(default(TId)))
                {
                    throw new InvalidOperationException(msg);
                }
                if (typeInfo.IsClass && value == null)
                {
                    throw new InvalidOperationException(msg);
                }
                if (typeInfo.IsClass && value != null && value.ToString() == string.Empty)
                {
                    throw new InvalidOperationException(msg);
                }

                _id = value;
            }
        }

        public virtual bool IsNew()
        {
            var typeInfo = typeof(TId).GetTypeInfo();
            if (typeInfo.IsValueType)
            {
                return this.Id.Equals(default(TId));
            }
            return this.Id == null || this.Id.Equals(default(TId));
        }

        public override bool Equals(object otherObject)
        {
            var entity = otherObject as EntityBase<TId>;
            if (entity != null)
            {
                return this.Equals(entity);
            }

            return base.Equals(otherObject);
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
    }
}