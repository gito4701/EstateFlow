using System;
using EstateFlow.Domain.Exceptions;

namespace EstateFlow.Domain.Tenants
{
    public class Tenant
    {
        public TenantId Id { get; }
        public string Name { get; private set; }

        protected Tenant(TenantId id, string name)
        {
            Id = id;
            Name = name;
        }

        public static Tenant Create(TenantId id, string name)
        {
            if (id.Value == Guid.Empty)
            {
                throw new ArgumentException("TenantId cannot be empty.", nameof(id));
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new InvalidTenantException("Tenant Name is required.");
            }

            return new Tenant(id, name);
        }
    }
}
