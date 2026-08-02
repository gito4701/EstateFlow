using System.Collections.Generic;
using System.Linq;

namespace EstateFlow.Domain.Primitives
{
    /// <summary>
    /// Base class for immutable value objects that are compared by their constituent values.
    /// </summary>
    public abstract class ValueObject
    {
        /// <summary>
        /// Gets the components that define the value object's equality semantics.
        /// </summary>
        protected abstract IEnumerable<object?> GetEqualityComponents();

        public override bool Equals(object? obj)
        {
            if (obj is not ValueObject other || GetType() != other.GetType()) return false;
            return GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return GetEqualityComponents().Aggregate(17, (current, component) => current * 23 + (component?.GetHashCode() ?? 0));
            }
        }
    }
}
