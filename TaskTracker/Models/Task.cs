using System.Text.Json.Serialization;

namespace TaskTracker.Models;

public class Task
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public TaskStatus Status { get; set; } = TaskStatus.ToDo;
    public int? Priority { get; set; }
    public int ProjectId { get; set; }
    [JsonIgnore] 
    public Project? Project { get; set; }
}