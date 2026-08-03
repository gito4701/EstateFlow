using System;

namespace EstateFlow.Domain.UserInteractions
{
    public readonly struct UserInteractionId : IEquatable<UserInteractionId>
    {
        public Guid Value { get; }

        public UserInteractionId(Guid value)
        {
            if (value == Guid.Empty) throw new ArgumentException("UserInteractionId cannot be empty.", nameof(value));
            Value = value;
        }

        public static UserInteractionId NewId() => new UserInteractionId(Guid.NewGuid());

        public bool Equals(UserInteractionId other) => Value.Equals(other.Value);
        public override bool Equals(object? obj) => obj is UserInteractionId other && Equals(other);
        public override int GetHashCode() => Value.GetHashCode();
        public override string ToString() => Value.ToString();
    }
}
