using System;

namespace EstateFlow.Domain.Users
{
    public readonly struct UserIdentityId : IEquatable<UserIdentityId>
    {
        public Guid Value { get; }

        public UserIdentityId(Guid value)
        {
            if (value == Guid.Empty) throw new ArgumentException("UserIdentityId cannot be empty.", nameof(value));
            Value = value;
        }

        public static UserIdentityId NewId() => new UserIdentityId(Guid.NewGuid());

        public bool Equals(UserIdentityId other) => Value.Equals(other.Value);
        public override bool Equals(object? obj) => obj is UserIdentityId other && Equals(other);
        public override int GetHashCode() => Value.GetHashCode();
        public override string ToString() => Value.ToString();
    }
}
