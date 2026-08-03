namespace EstateFlow.Domain.Exceptions
{
    public class InvalidUserInteractionException : DomainException
    {
        public InvalidUserInteractionException(string message) : base(message) { }
    }
}
