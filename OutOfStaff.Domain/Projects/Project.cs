using System.Security.AccessControl;
using OutOfStaff.Domain.Abstractions;
using OutOfStaff.Domain.Employees;
using OutOfStaff.Domain.Shared;

namespace OutOfStaff.Domain.Projects;

public class Project : Entity
{
    public ProjectType ProjectType { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public Guid ProjectManagerId { get; set; }
    public Employee ProjectManager { get; set; }
    public string Comment { get; set; }
    public Status Status { get; set; }
}