using EstateFlow.Domain.Exceptions;

namespace EstateFlow.Domain.Exceptions
{
    public class InvalidLeaseException : DomainException
    {
        public InvalidLeaseException(string message) : base(message)
        {
        }
    }
}
