using Demo.Core.Infrastructure.Database;
using Demo.Core.Infrastructure.Persistence;
using Demo.Core.Infrastructure.Repository;

namespace Demo.Core.Application.Behaviours;

public class TransactionalBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public TransactionalBehaviour(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        await _unitOfWork.BeginTransactionAsync();
        var response = await next();
        await _unitOfWork.CommitTransactionAsync();
        return response;
    }
}