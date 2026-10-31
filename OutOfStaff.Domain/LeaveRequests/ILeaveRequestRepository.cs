using OutOfStaff.Domain.Shared;

namespace OutOfStaff.Domain.LeaveRequests;

public interface ILeaveRequestRepository
{
    Task<LeaveRequest?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<LeaveRequest>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<List<LeaveRequest>> GetAllByEmployeeIdAsync(Guid employeeId, CancellationToken cancellationToken = default);
    Task<List<LeaveRequest>> GetAllByStatusAsync(RequestStatus status, CancellationToken cancellationToken = default);
    Task AddAsync(LeaveRequest leaveRequest, CancellationToken cancellationToken = default);
    Task UpdateAsync(LeaveRequest leaveRequest, CancellationToken cancellationToken = default);
    Task DeleteAsync(LeaveRequest leaveRequest, CancellationToken cancellationToken = default);
}