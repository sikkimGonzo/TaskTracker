namespace TaskTracker.Models;

public class TaskTrackerDbContext : DbContext
{
    public TaskTrackerDbContext(DbContextOptions<TaskTrackerDbContext> options) : base(options){}
    public DbSet<Project> Projects => Set<Project>();
    public DbSet<Models.Task> Tasks => Set<Models.Task>();
}
