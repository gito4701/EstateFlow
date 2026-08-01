using EstateFlow.Domain.Exceptions;

namespace EstateFlow.Domain.Properties
{
    public class Property
    {
        public PropertyId Id { get; }
        public string Name { get; private set; }
        public string Address { get; private set; }
        public PropertyLifecycleState State { get; private set; }

        protected Property(PropertyId id, string name, string address, PropertyLifecycleState state)
        {
            Id = id;
            Name = name;
            Address = address;
            State = state;
        }

        public static Property Create(PropertyId id)
        {
            return Create(id, "Untitled Property", "Address not provided");
        }

        public static Property Create(PropertyId id, string name, string address)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new InvalidPropertyException("Property Name is required.");
            }

            if (string.IsNullOrWhiteSpace(address))
            {
                throw new InvalidPropertyException("Property Address is required.");
            }

            return new Property(id, name, address, PropertyLifecycleState.Draft);
        }

        public void Activate()
        {
            if (State != PropertyLifecycleState.Draft)
            {
                throw new InvalidPropertyStateException($"Property {Id} cannot transition from {State} to Active.");
            }

            State = PropertyLifecycleState.Active;
        }

        public void Archive()
        {
            if (State == PropertyLifecycleState.Archived)
            {
                throw new InvalidPropertyStateException($"Property {Id} cannot transition from {State} to Archived.");
            }

            State = PropertyLifecycleState.Archived;
        }
    }
}
