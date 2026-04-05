using MediatR;
using OutOfStaff.Domain.Abstractions;

namespace OutOfStaff.Application.Abstractions.Messaging;


public interface ICommandHandler<TCommand> : IRequestHandler<TCommand, Result>
    where TCommand : ICommand
{
}

public interface ICommandHandler<TCommand, TResponse> : IRequestHandler<TCommand, Result<TResponse>>
    where TCommand : ICommand<TResponse>
{
}