namespace OutOfStaff.Domain.Employees;

public interface IEmployeeRepository
{
    Task<List<Employee>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Employee?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken = default);
    void Add(Employee employee);
    void Update(Employee employee);
    void Delete(Employee employee);
}