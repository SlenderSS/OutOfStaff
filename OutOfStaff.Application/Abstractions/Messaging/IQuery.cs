using MediatR;
using OutOfStaff.Domain.Abstractions;

namespace OutOfStaff.Application.Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}