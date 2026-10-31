using OutOfStaff.Application.Abstractions.Messaging;
using OutOfStaff.Domain.Abstractions;
using OutOfStaff.Domain.Projects;

namespace OutOfStaff.Application.Projects.GetPaginatedProjects;

public sealed record GetPaginatedProjectsQuery(int Page, int PageSize) : IQuery<IReadOnlyList<Project>>;

public sealed class GetPaginatedProjectsQueryHandler : IQueryHandler<GetPaginatedProjectsQuery, IReadOnlyList<Project>>
{
    private readonly IProjectRepository _projectRepository;

    public GetPaginatedProjectsQueryHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<Result<IReadOnlyList<Project>>> Handle(GetPaginatedProjectsQuery request, CancellationToken cancellationToken)
    {
        var paginatedProjects = await _projectRepository.GetPaginatedProjectsAsync(request.Page, request.PageSize, cancellationToken);
        
        return Result.Success( paginatedProjects);
    }
}
