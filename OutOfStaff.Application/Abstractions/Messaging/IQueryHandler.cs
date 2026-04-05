using MediatR;
using OutOfStaff.Domain.Abstractions;

namespace OutOfStaff.Application.Abstractions.Messaging;

public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>
{
}
