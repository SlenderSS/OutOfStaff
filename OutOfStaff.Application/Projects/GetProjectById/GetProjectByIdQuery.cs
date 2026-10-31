using OutOfStaff.Application.Abstractions.Messaging;
using OutOfStaff.Domain.Abstractions;
using OutOfStaff.Domain.Projects;

namespace OutOfStaff.Application.Projects.GetProjectById;

public sealed record GetProjectByIdQuery(Guid Id) : IQuery<Project?>;

public sealed class GetProjectByIdQueryHandler : IQueryHandler<GetProjectByIdQuery, Project?>
{
    private readonly IProjectRepository _projectRepository;

    public GetProjectByIdQueryHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<Result<Project?>> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
    {
        var project = await _projectRepository.GetByIdAsync(request.Id, cancellationToken);
        if(project is null)
            return Result.Failure<Project?>(Error.NotFound(nameof(Project), request.Id));
        
        return Result.Success(project);
    }
}
