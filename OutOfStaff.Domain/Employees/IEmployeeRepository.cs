namespace OutOfStaff.Domain.Employees;

public interface IEmployeeRepository
{
    Task<List<Employee>> GetEmployeesAsync();
    Task<Employee> GetEmployeeAsync(Guid id);
    Task<Employee> AddEmployeeAsync(Employee employee);
    Task<Employee> UpdateEmployeeAsync(Employee employee);
    Task DeleteEmployeeAsync(Employee employee);
    
}