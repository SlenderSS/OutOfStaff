using Microsoft.EntityFrameworkCore;
using OutOfStaff.Domain.ApprovalRequests;
using OutOfStaff.Domain.Employees;
using OutOfStaff.Domain.LeaveRequests;
using OutOfStaff.Domain.Projects;

namespace OutOfStaff.Application.Abstractions.Data;

public interface IApplicationDbContext : IUnitOfWork
{
    DbSet<Employee> Employees { get; }
    DbSet<LeaveRequest> LeaveRequests { get; }
    DbSet<ApprovalRequest> ApprovalRequests { get; }
    DbSet<Project> Projects { get; }
}