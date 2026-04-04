namespace OutOfStaff.Domain.Projects;

public interface IProjectsRepository
{
    Task<List<Project>> GetAllAsync();
    Task<Project> GetAsync(Guid id);
    Task<Project> AddAsync(Project project);
    Task<Project> UpdateAsync(Project project);
    Task DeleteAsync(Project project);
}