using System;
using EstateFlow.Domain.Exceptions;

namespace EstateFlow.Domain.Users
{
    public class UserIdentity
    {
        public UserIdentityId Id { get; }
        public string Name { get; private set; }
        public UserIdentityKind Kind { get; private set; }

        protected UserIdentity(UserIdentityId id, string name, UserIdentityKind kind)
        {
            Id = id;
            Name = name;
            Kind = kind;
        }

        public static UserIdentity Create(UserIdentityId id, string name, UserIdentityKind kind)
        {
            if (id.Value == Guid.Empty)
            {
                throw new ArgumentException("UserIdentityId cannot be empty.", nameof(id));
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new InvalidUserIdentityException("UserIdentity Name is required.");
            }

            return new UserIdentity(id, name, kind);
        }
    }
}
