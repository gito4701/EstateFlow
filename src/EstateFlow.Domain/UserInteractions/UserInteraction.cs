using System;
using EstateFlow.Domain.Exceptions;

namespace EstateFlow.Domain.UserInteractions
{
    public class UserInteraction
    {
        public UserInteractionId Id { get; }
        public string Name { get; private set; }
        public UserInteractionKind Kind { get; private set; }

        protected UserInteraction(UserInteractionId id, string name, UserInteractionKind kind)
        {
            Id = id;
            Name = name;
            Kind = kind;
        }

        public static UserInteraction Create(UserInteractionId id, string name, UserInteractionKind kind)
        {
            if (id.Value == Guid.Empty)
            {
                throw new ArgumentException("UserInteractionId cannot be empty.", nameof(id));
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new InvalidUserInteractionException("UserInteraction Name is required.");
            }

            return new UserInteraction(id, name, kind);
        }
    }
}
