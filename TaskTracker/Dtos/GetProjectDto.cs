using TaskTracker.Models;
using Task = TaskTracker.Models.Task;

namespace TaskTracker.Dtos;

public class GetProjectDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime? CompletionDate { get; set; }
    public ProjectStatus Status { get; set; } = ProjectStatus.NotStarted;
    public int? Priority { get; set; }
    // public List<Task>? Tasks { get; set; }
}