using TaskTracker.Models;
using TaskTracker.Dtos;
using AutoMapper;
using Task = TaskTracker.Models.Task;

namespace TaskTracker.Repositories.ProjectRepository;

// Using repository pattert
public class ProjectRepository : IProjectRepository
{
    //Dependency injection
    private readonly IMapper _mapper;
    private readonly TaskTrackerDbContext _context;
    public ProjectRepository(TaskTrackerDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Response<List<Project>>> GetAllProjects()
    {
        var response = new Response<List<Project>>();
        var allProjects = await _context.Projects
            .Include(x => x.Tasks)
            .ToListAsync();

        // response.Data = allProjects.Select(x => _mapper.Map<GetProjectDto>(x)).ToList();
        response.Data = allProjects;
        return response;
    }

    public async Task<Response<List<GetProjectDto>>> GetAllProjectsQuickView()
    {
        var response = new Response<List<GetProjectDto>>();

        var allProjects = await _context.Projects.ToListAsync();

        response.Data = allProjects.Select(x => _mapper.Map<GetProjectDto>(x)).ToList();
        return response;
    }

    public async Task<Response<Project>> GetProjectById(int id)
    {
        var response = new Response<Project>();

        try
        {
            var singleProject = await _context.Projects
                .Include(x => x.Tasks)
                .FirstOrDefaultAsync(x => x.Id == id);
                
            if(singleProject is null)
            {
                throw new Exception($"Project with Id {id} not found");
            }

            response.Data = singleProject;
        }
        catch(Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;
        }

        return response;
    }

    public async Task<Response<List<GetProjectDto>>> GetProjectsSortedByStartDate()
    {
        var response = new Response<List<GetProjectDto>>();
        var allProjects = await _context.Projects.ToListAsync();

        response.Data = allProjects
            .Select(x => _mapper.Map<GetProjectDto>(x))
            .OrderBy(x => x.StartDate)
            .ToList();

        return response;
    }

    public async Task<Response<List<GetProjectDto>>> GetProjectsSortedByPriority()
    {
        var response = new Response<List<GetProjectDto>>();
        var allProjects = await _context.Projects.ToListAsync();

        response.Data = allProjects
            .Select(x => _mapper.Map<GetProjectDto>(x))
            .OrderBy(x => x.Priority)
            .ToList();

        return response;
    }

    public async Task<Response<List<GetProjectDto>>> GetProjectsFilteredByStatus(ProjectStatus status)
    {
        var response = new Response<List<GetProjectDto>>();
        var allProjects = await _context.Projects
            .Where(x => x.Status == status)
            .ToListAsync();

        response.Data = allProjects.Select(x => _mapper.Map<GetProjectDto>(x)).ToList();
        return response;
    }

    public async Task<Response<List<GetProjectDto>>> GetProjectsStartAtStartDate(DateTime date)
    {
        var response = new Response<List<GetProjectDto>>();
        var allProjects = await _context.Projects
            .Where(x => x.StartDate >= date)
            .ToListAsync();

        response.Data = allProjects.Select(x => _mapper.Map<GetProjectDto>(x)).ToList();
        return response;
    }

    public async Task<Response<List<GetProjectDto>>> GetProjectsEndAtStartDate(DateTime date)
    {
        var response = new Response<List<GetProjectDto>>();
        var allProjects = await _context.Projects
            .Where(x => x.StartDate <= date)
            .ToListAsync();

        response.Data = allProjects.Select(x => _mapper.Map<GetProjectDto>(x)).ToList();
        return response;
    }

    public async Task<Response<List<GetProjectDto>>> GetProjectsStartDateRange(DateTime startDate, DateTime endDate)
    {
        var response = new Response<List<GetProjectDto>>();
        var allProjects = await _context.Projects
            .Where(x => x.StartDate >= startDate && x.StartDate <= endDate)
            .ToListAsync();

        response.Data = allProjects.Select(x => _mapper.Map<GetProjectDto>(x)).ToList();
        return response;
    }

    public async Task<Response<GetProjectDto>> InsertProject(InsertProjectDto project)
    {
        var response = new Response<GetProjectDto>();
        var mappedProject = _mapper.Map<Project>(project);

        _context.Projects.Add(mappedProject);
        await _context.SaveChangesAsync();

        response.Data = _mapper.Map<GetProjectDto>(mappedProject);
        return response;
    }

    public async Task<Response<GetProjectDto>> UpdateProject(UpdateProjectDto project)
    {
        var response = new Response<GetProjectDto>();

        try
        {
            var projectToUpdate = await _context.Projects.FindAsync(project.Id);

            if(projectToUpdate is null)
            {
                throw new Exception($"Project with Id {project.Id} not found");
            }

            _mapper.Map(project, projectToUpdate);
            await _context.SaveChangesAsync();

            response.Data = _mapper.Map<GetProjectDto>(projectToUpdate);
        }
        catch(Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;
        }
        
        return response;
    }

    public async Task<Response<List<GetProjectDto>>> DeleteProject(int id)
    {
        var response = new Response<List<GetProjectDto>>();

        try
        {
            var projectToRemove = await _context.Projects.FindAsync(id);

            if(projectToRemove is null)
            {
                throw new Exception($"Project with Id {id} not found");
            }

            _context.Projects.Remove(projectToRemove);
            await _context.SaveChangesAsync();

            var allProjects = await _context.Projects.ToListAsync();
            response.Data = allProjects.Select(x => _mapper.Map<GetProjectDto>(x)).ToList();
        }
        catch(Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;
        }

        return response;
    }
}