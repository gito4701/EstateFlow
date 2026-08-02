using System;
using EstateFlow.Domain.Exceptions;

namespace EstateFlow.Domain.Owners
{
    public class Owner
    {
        public OwnerId Id { get; }
        public string Name { get; private set; }

        protected Owner(OwnerId id, string name)
        {
            Id = id;
            Name = name;
        }

        public static Owner Create(OwnerId id, string name)
        {
            if (id.Value == Guid.Empty)
            {
                throw new ArgumentException("OwnerId cannot be empty.", nameof(id));
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new InvalidOwnerException("Owner Name is required.");
            }

            return new Owner(id, name);
        }
    }
}
