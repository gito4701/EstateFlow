using System;

namespace EstateFlow.Domain.Properties
{
    public readonly struct PropertyId : IEquatable<PropertyId>
    {
        public Guid Value { get; }

        public PropertyId(Guid value)
        {
            if (value == Guid.Empty) throw new ArgumentException("PropertyId cannot be empty.", nameof(value));
            Value = value;
        }

        public static PropertyId NewId() => new PropertyId(Guid.NewGuid());

        public bool Equals(PropertyId other) => Value.Equals(other.Value);
        public override bool Equals(object? obj) => obj is PropertyId other && Equals(other);
        public override int GetHashCode() => Value.GetHashCode();
        public override string ToString() => Value.ToString();
    }
}
