using OutOfStaff.Application.Abstractions.Data;
using OutOfStaff.Application.Abstractions.Messaging;
using OutOfStaff.Domain.Abstractions;
using OutOfStaff.Domain.Projects;

namespace OutOfStaff.Application.Projects.DeactivateProject;

public sealed record DeactivateProjectCommand(Guid ProjectId): ICommand<Guid>;

public class DeactivateProjectCommandHandler : ICommandHandler<DeactivateProjectCommand, Guid>
{
    private readonly IProjectRepository _projectRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeactivateProjectCommandHandler(IProjectRepository projectRepository, IUnitOfWork unitOfWork)
    {
        _projectRepository = projectRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(DeactivateProjectCommand command, CancellationToken cancellationToken)
    {
        var project = await _projectRepository.GetByIdAsync(command.ProjectId, cancellationToken);
        if (project is null)
            return Result.Failure<Guid>(Error.NotFound(nameof(Project), command.ProjectId));

        var result = project.Deactivate();
        if (!result.IsSuccess)
            return Result.Failure<Guid>(result.Error);

        await _projectRepository.DeactivateProjectAsync(project, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(project.Id);
    }
}