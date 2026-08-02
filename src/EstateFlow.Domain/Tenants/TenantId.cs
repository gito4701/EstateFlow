using System;

namespace EstateFlow.Domain.Tenants
{
    public readonly struct TenantId : IEquatable<TenantId>
    {
        public Guid Value { get; }

        public TenantId(Guid value)
        {
            if (value == Guid.Empty) throw new ArgumentException("TenantId cannot be empty.", nameof(value));
            Value = value;
        }

        public static TenantId NewId() => new TenantId(Guid.NewGuid());

        public bool Equals(TenantId other) => Value.Equals(other.Value);
        public override bool Equals(object? obj) => obj is TenantId other && Equals(other);
        public override int GetHashCode() => Value.GetHashCode();
        public override string ToString() => Value.ToString();
    }
}
