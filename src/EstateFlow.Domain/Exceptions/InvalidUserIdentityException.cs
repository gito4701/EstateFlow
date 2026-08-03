namespace EstateFlow.Domain.Exceptions
{
    public class InvalidUserIdentityException : DomainException
    {
        public InvalidUserIdentityException(string message) : base(message) { }
    }
}
