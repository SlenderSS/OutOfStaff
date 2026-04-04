using OutOfStaff.Domain.Shared;

namespace OutOfStaff.Domain.ApprovalRequests;

public interface IApprovalRequestRepository
{
    
    Task<ApprovalRequest> GetByIdAsync(Guid id);
    Task<IEnumerable<ApprovalRequest>> GetAllAsync();
    Task<IEnumerable<ApprovalRequest>> GetAllByStatusAsync(Status status);
    Task<IEnumerable<ApprovalRequest>> GetAllByEmployeeIdAsync(Guid employeeId);
    Task<ApprovalRequest> AddAsync(ApprovalRequest approvalRequest);
}