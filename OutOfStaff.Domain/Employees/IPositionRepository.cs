namespace OutOfStaff.Domain.Employees;

public interface IPositionRepository
{
    Task<Position?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<Position>> GetAllAsync(CancellationToken cancellationToken = default);
    void Add(Position position);
    void Update(Position position);
    void Delete(Position position);
}

