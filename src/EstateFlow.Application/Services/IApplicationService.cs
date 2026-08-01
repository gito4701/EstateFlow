using EstateFlow.Application.Interfaces;

namespace EstateFlow.Application.Services;

public interface IApplicationService<TRequest, TResponse> : IApplicationService
    where TRequest : class
    where TResponse : class
{
    TResponse Handle(TRequest request);
}
