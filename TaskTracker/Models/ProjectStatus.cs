using System.Text.Json.Serialization;

namespace TaskTracker.Models;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ProjectStatus
{
    NotStarted,
    Active,
    Completed
}