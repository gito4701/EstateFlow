using System;

namespace EstateFlow.Domain.Authentications
{
    public readonly struct AuthenticationId : IEquatable<AuthenticationId>
    {
        public Guid Value { get; }

        public AuthenticationId(Guid value)
        {
            if (value == Guid.Empty) throw new ArgumentException("AuthenticationId cannot be empty.", nameof(value));
            Value = value;
        }

        public static AuthenticationId NewId() => new AuthenticationId(Guid.NewGuid());

        public bool Equals(AuthenticationId other) => Value.Equals(other.Value);
        public override bool Equals(object? obj) => obj is AuthenticationId other && Equals(other);
        public override int GetHashCode() => Value.GetHashCode();
        public override string ToString() => Value.ToString();
    }
}
