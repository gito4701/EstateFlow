using EstateFlow.Domain.Exceptions;

namespace EstateFlow.Domain.Exceptions
{
    public class InvalidPropertyStateException : DomainException
    {
        public InvalidPropertyStateException(string message) : base(message)
        {
        }
    }
}
