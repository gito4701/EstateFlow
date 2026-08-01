namespace EstateFlow.Domain.Properties
{
    public class Property
    {
        public PropertyId Id { get; }

        protected Property(PropertyId id)
        {
            Id = id;
        }

        public static Property Create(PropertyId id)
        {
            return new Property(id);
        }
    }
}
