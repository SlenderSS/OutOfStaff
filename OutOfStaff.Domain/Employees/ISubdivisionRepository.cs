namespace OutOfStaff.Domain.Employees;

public interface ISubdivisionRepository
{
    Task<Subdivision?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<Subdivision>> GetAllAsync(CancellationToken cancellationToken = default);
    void Add(Subdivision subdivision);
    void Update(Subdivision subdivision);
    void Delete(Subdivision subdivision);
}

