using EstateFlow.Domain.Exceptions;

namespace EstateFlow.Domain.Exceptions
{
    public class InvalidOwnerException : DomainException
    {
        public InvalidOwnerException(string message) : base(message)
        {
        }
    }
}
