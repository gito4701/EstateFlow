using EstateFlow.Domain.Exceptions;

namespace EstateFlow.Domain.Leases
{
    public class Lease
    {
        public LeaseId Id { get; }
        public string Name { get; private set; }

        protected Lease(LeaseId id, string name)
        {
            Id = id;
            Name = name;
        }

        public static Lease Create(LeaseId id, string name)
        {
            if (id.Value == Guid.Empty)
            {
                throw new ArgumentException("LeaseId cannot be empty.", nameof(id));
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new InvalidLeaseException("Lease Name is required.");
            }

            return new Lease(id, name);
        }
    }
}
