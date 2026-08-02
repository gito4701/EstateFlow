using EstateFlow.Domain.Exceptions;

namespace EstateFlow.Domain.Exceptions
{
    public class InvalidTenantException : DomainException
    {
        public InvalidTenantException(string message) : base(message)
        {
        }
    }
}
