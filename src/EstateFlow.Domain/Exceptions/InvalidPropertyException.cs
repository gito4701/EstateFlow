using EstateFlow.Domain.Exceptions;

namespace EstateFlow.Domain.Exceptions
{
    public class InvalidPropertyException : DomainException
    {
        public InvalidPropertyException(string message) : base(message)
        {
        }
    }
}
