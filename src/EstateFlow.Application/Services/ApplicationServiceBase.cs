using EstateFlow.Application.Interfaces;
using EstateFlow.Domain.Exceptions;

namespace EstateFlow.Application.Services;

public abstract class ApplicationServiceBase : IApplicationService
{
    protected TResult ExecuteWithDomainException<TException, TResult>(Func<TResult> action, Func<TException, TResult> errorHandler)
        where TException : DomainException
    {
        try
        {
            return action();
        }
        catch (TException ex)
        {
            return errorHandler(ex);
        }
    }
}
