using System;

namespace EstateFlow.Domain.Leases
{
    public readonly struct LeaseId : IEquatable<LeaseId>
    {
        public Guid Value { get; }

        public LeaseId(Guid value)
        {
            if (value == Guid.Empty) throw new ArgumentException("LeaseId cannot be empty.", nameof(value));
            Value = value;
        }

        public static LeaseId NewId() => new LeaseId(Guid.NewGuid());

        public bool Equals(LeaseId other) => Value.Equals(other.Value);
        public override bool Equals(object? obj) => obj is LeaseId other && Equals(other);
        public override int GetHashCode() => Value.GetHashCode();
        public override string ToString() => Value.ToString();
    }
}
