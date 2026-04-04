using OutOfStaff.Domain.Abstractions;

namespace OutOfStaff.Domain.Projects;

public class ProjectType : Entity
{
    public string? Name { get; set; }
    public string? Description { get; set; }
}