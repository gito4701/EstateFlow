using System.Collections.Generic;

namespace EstateFlow.Domain.Common
{
    /// <summary>
    /// Base class for domain entities that use a strongly typed identifier.
    /// </summary>
    /// <typeparam name="TId">The entity identifier type.</typeparam>
    public abstract class Entity<TId>
    {
        /// <summary>
        /// Gets the unique identifier for the entity.
        /// </summary>
        public TId Id { get; protected set; } = default!;

        protected Entity() { }

        protected Entity(TId id)
        {
            Id = id;
        }

        public override bool Equals(object? obj)
        {
            if (obj is not Entity<TId> other) return false;
            return EqualityComparer<TId>.Default.Equals(Id, other.Id);
        }

        public override int GetHashCode()
        {
            return EqualityComparer<TId>.Default.GetHashCode(Id!);
        }
    }
}
