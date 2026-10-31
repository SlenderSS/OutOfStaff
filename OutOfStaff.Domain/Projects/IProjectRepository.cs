namespace OutOfStaff.Domain.Projects;

public interface IProjectRepository
{
    Task<List<Project>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Project>> GetPaginatedProjectsAsync(int page, int pageSize, CancellationToken cancellationToken = default);
    Task<Project?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task AddAsync(Project project, CancellationToken cancellationToken = default);
    Task UpdateAsync(Project project, CancellationToken cancellationToken = default);
    Task DeleteAsync(Project project, CancellationToken cancellationToken = default);
    Task DeactivateProjectAsync(Project project, CancellationToken cancellationToken = default);
    Task<Project> GetByNameAsync(string requestName, CancellationToken cancellationToken = default);
}