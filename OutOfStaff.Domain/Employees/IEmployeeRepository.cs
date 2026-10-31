namespace OutOfStaff.Domain.Employees;

public interface IEmployeeRepository
{
    Task<List<Employee>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Employee?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken = default);
    Task AddAsync(Employee employee, CancellationToken cancellationToken = default);
    Task UpdateAsync(Employee employee, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    Task DeactivateAsync(Guid id, CancellationToken cancellationToken = default);
    Task AssignToProjectAsync(Guid employeeId, Guid projectId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Employee>> GetPaginatedEmployeesAsync(int requestPage, int requestPageSize, CancellationToken cancellationToken);
}