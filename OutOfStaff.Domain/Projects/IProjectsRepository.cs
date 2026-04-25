namespace OutOfStaff.Domain.Projects;

public interface IProjectsRepository
{
    Task<List<Project>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Project?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    void Add(Project project);
    void Update(Project project);
    void Delete(Project project);
}