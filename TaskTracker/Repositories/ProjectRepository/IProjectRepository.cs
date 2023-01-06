using TaskTracker.Models;
using TaskTracker.Dtos;

namespace TaskTracker.Repositories.ProjectRepository;

public interface IProjectRepository
{
    Task<Response<List<Project>>> GetAllProjects();
    Task<Response<List<GetProjectDto>>> GetAllProjectsQuickView();
    Task<Response<Project>> GetProjectById(int id);
    Task<Response<List<GetProjectDto>>> GetProjectsSortedByPriority();
    Task<Response<List<GetProjectDto>>> GetProjectsSortedByStartDate();
    Task<Response<List<GetProjectDto>>> GetProjectsFilteredByStatus(ProjectStatus status);
    Task<Response<List<GetProjectDto>>> GetProjectsStartAtStartDate(DateTime date);
    Task<Response<List<GetProjectDto>>> GetProjectsEndAtStartDate(DateTime date);
    Task<Response<List<GetProjectDto>>> GetProjectsStartDateRange(DateTime startDate, DateTime endDate);
    Task<Response<GetProjectDto>> InsertProject(InsertProjectDto project);
    Task<Response<GetProjectDto>> UpdateProject(UpdateProjectDto project);
    Task<Response<List<GetProjectDto>>> DeleteProject(int id);
}