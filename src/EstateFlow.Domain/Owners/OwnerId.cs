using System;

namespace EstateFlow.Domain.Owners
{
    public readonly struct OwnerId : IEquatable<OwnerId>
    {
        public Guid Value { get; }

        public OwnerId(Guid value)
        {
            if (value == Guid.Empty) throw new ArgumentException("OwnerId cannot be empty.", nameof(value));
            Value = value;
        }

        public static OwnerId NewId() => new OwnerId(Guid.NewGuid());

        public bool Equals(OwnerId other) => Value.Equals(other.Value);
        public override bool Equals(object? obj) => obj is OwnerId other && Equals(other);
        public override int GetHashCode() => Value.GetHashCode();
        public override string ToString() => Value.ToString();
    }
}
