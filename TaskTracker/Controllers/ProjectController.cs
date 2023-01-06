using TaskTracker.Models;
using TaskTracker.Repositories.ProjectRepository;
using TaskTracker.Dtos;

namespace TaskTracker.Controllers;

// Controller for Project Model
[ApiController]
[Route("api/[controller]")]
public class ProjectController : ControllerBase
{
        //Dependency injection
    private readonly IProjectRepository _repository;

    public ProjectController(IProjectRepository repository)
    {
        _repository = repository;
    }

    // Allows to get all projects with tasks from the database
    [HttpGet("All")]
    public async Task<ActionResult<Response<List<GetProjectDto>>>> Get()
    {
        return Ok(await _repository.GetAllProjects());
    }

    // Allows to get all projects without tasks from the database 
    [HttpGet("QuickView")]
    public async Task<ActionResult<Response<List<GetProjectDto>>>> GetQuickView()
    {
        return Ok(await _repository.GetAllProjectsQuickView());
    }

    // Allows to get a project by id from the database
    [HttpGet("{id}")]
    public async Task<ActionResult<Response<GetProjectDto>>> GetById(int id)
    {
        var response = await _repository.GetProjectById(id);

        if(response.Data is null)
        {
            return NotFound(response);
        }
        return Ok(response);
    }

    // Allows to get a list of projects sorted by StartDate property
    [HttpGet("SortedByStartDate")]
    public async Task<ActionResult<Response<List<GetProjectDto>>>> GetSortedByStartDate()
    {
        return Ok(await _repository.GetProjectsSortedByStartDate());
    }

    // Allows to get a list of projects sorted by Priority property
    [HttpGet("SortedBySPriority")]
    public async Task<ActionResult<Response<List<GetProjectDto>>>> GetSortedByPriority()
    {
        return Ok(await _repository.GetProjectsSortedByPriority());
    }

    // Allows to get a list of projects filtered by Status property
    [HttpGet("FilteredByStatus")]
    public async Task<ActionResult<Response<List<GetProjectDto>>>> GetFilteredByStatus(ProjectStatus status)
    {
        return Ok(await _repository.GetProjectsFilteredByStatus(status));
    }
    
    // Allows to get a list of projects whose StartDate are greater than a specific date
    [HttpGet("StartAtStartDate")]
    public async Task<ActionResult<Response<List<GetProjectDto>>>> GetStartAt(DateTime date)
    {
        return Ok(await _repository.GetProjectsStartAtStartDate(date));
    }

    // Allows to get a list of projects whose StartDate are less than a specific date
    [HttpGet("EndAtStartDate")]
    public async Task<ActionResult<Response<List<GetProjectDto>>>> GetEndAt(DateTime date)
    {
        return Ok(await _repository.GetProjectsEndAtStartDate(date));
    }

    // Allows to get a list of projects whose StartDate are between a specific dates
    [HttpGet("StartDateRange")]
    public async Task<ActionResult<Response<List<GetProjectDto>>>> GetRange(DateTime startDate, DateTime endDate)
    {
        return Ok(await _repository.GetProjectsStartDateRange(startDate, endDate));
    }

    // Allows to add a project to the list
    [HttpPost]
    public async Task<ActionResult<Response<GetProjectDto>>> Post(InsertProjectDto project)
    {
        return Ok(await _repository.InsertProject(project));
    }

    // Allows to update a project
    [HttpPut]
    public async Task<ActionResult<Response<GetProjectDto>>> Put(UpdateProjectDto project)
    {
         var response = await _repository.UpdateProject(project);

         if(response.Data is null)
         {
            return NotFound(response);
         }
         return Ok(response);
    }

    // Allows to remove a project
    [HttpDelete]
    public async Task<ActionResult<Response<List<GetProjectDto>>>> Delete(int id)
    {
        var response = await _repository.DeleteProject(id);

        if(response.Data is null)
        {
            return NotFound(response);
        }
        return Ok(response);
    }
}