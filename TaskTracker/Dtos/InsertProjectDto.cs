using TaskTracker.Models;

namespace TaskTracker.Dtos;

public class InsertProjectDto
{
    public string Name { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime? CompletionDate { get; set; }
    public ProjectStatus Status { get; set; } = ProjectStatus.NotStarted;
    public int? Priority { get; set; }
}