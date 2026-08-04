namespace EstateFlow.Domain.Exceptions
{
    public class InvalidAuthenticationException : DomainException
    {
        public InvalidAuthenticationException(string message) : base(message) { }
    }
}
