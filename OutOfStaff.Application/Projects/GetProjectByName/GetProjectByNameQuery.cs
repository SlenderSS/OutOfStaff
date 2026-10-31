using OutOfStaff.Application.Abstractions.Messaging;
using OutOfStaff.Domain.Abstractions;
using OutOfStaff.Domain.Projects;

namespace OutOfStaff.Application.Projects.GetProjectByName;

public record GetProjectByNameQuery(string Name): IQuery<Project>;

public sealed class GetProjectByNameQueryHandler : IQueryHandler<GetProjectByNameQuery, Project>
{
    private readonly IProjectRepository _projectRepository;

    public GetProjectByNameQueryHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<Result<Project>> Handle(GetProjectByNameQuery request, CancellationToken cancellationToken)
    {
        var project = await _projectRepository.GetByNameAsync(request.Name, cancellationToken);
        
        if (project is null)
            return Result.Failure<Project>(Error.NotFound(nameof(Project), request.Name));

        return Result.Success(project);
    }
}

