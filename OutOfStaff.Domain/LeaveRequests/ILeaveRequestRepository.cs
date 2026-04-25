using OutOfStaff.Domain.Shared;

namespace OutOfStaff.Domain.LeaveRequests;

public interface ILeaveRequestRepository
{
    Task<LeaveRequest?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<LeaveRequest>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<List<LeaveRequest>> GetAllByEmployeeIdAsync(Guid employeeId, CancellationToken cancellationToken = default);
    Task<List<LeaveRequest>> GetAllByStatusAsync(RequestStatus status, CancellationToken cancellationToken = default);
    void Add(LeaveRequest leaveRequest);
    void Update(LeaveRequest leaveRequest);
    void Delete(LeaveRequest leaveRequest);
}