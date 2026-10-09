using OutOfStaff.Domain.Shared;

namespace OutOfStaff.Domain.ApprovalRequests;

public interface IApprovalRequestRepository
{
    Task<ApprovalRequest?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<ApprovalRequest>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<List<ApprovalRequest>> GetAllByStatusAsync(RequestStatus status, CancellationToken cancellationToken = default);
    Task<List<ApprovalRequest>> GetAllByApproverIdAsync(Guid approverId, CancellationToken cancellationToken = default);
    Task AddAsync(ApprovalRequest approvalRequest, CancellationToken cancellationToken = default);
    Task ApproveRequestAsync(ApprovalRequest approvalRequest, CancellationToken cancellationToken = default);
    Task RejectRequestAsync(ApprovalRequest approvalRequest, CancellationToken cancellationToken = default);
}