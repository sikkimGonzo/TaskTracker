using System.Text.Json.Serialization;
using TaskTracker.Models;
using TaskStatus = TaskTracker.Models.TaskStatus;

namespace TaskTracker.Dtos;

public class GetTaskDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public TaskStatus Status { get; set; } = TaskStatus.ToDo;
    public int? Priority { get; set; }
    public int ProjectId { get; set; }
    // public Project? Project { get; set; }
}