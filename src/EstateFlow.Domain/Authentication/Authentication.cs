using System;
using EstateFlow.Domain.Exceptions;

namespace EstateFlow.Domain.Authentications
{
    public class Authentication
    {
        public AuthenticationId Id { get; }
        public string Name { get; private set; }
        public AuthenticationKind Kind { get; private set; }

        protected Authentication(AuthenticationId id, string name, AuthenticationKind kind)
        {
            Id = id;
            Name = name;
            Kind = kind;
        }

        public static Authentication Create(AuthenticationId id, string name, AuthenticationKind kind)
        {
            if (id.Value == Guid.Empty)
            {
                throw new ArgumentException("AuthenticationId cannot be empty.", nameof(id));
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new InvalidAuthenticationException("Authentication Name is required.");
            }

            return new Authentication(id, name, kind);
        }
    }
}
