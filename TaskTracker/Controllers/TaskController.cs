using TaskTracker.Dtos;
using TaskTracker.Models;
using TaskTracker.Repositories.TaskRepository;
using Task = TaskTracker.Models.Task;

namespace TaskTracker.Controllers;

// Controller for Task Model
[ApiController]
[Route("api/[controller]")]
public class TaskController : Controller
{
    //Dependency injection
    private readonly ITaskRepository _repository;
    
    public TaskController(ITaskRepository repository)
    {
        _repository = repository;
    }

    // Allows to get all tasks
    [HttpGet]
    public async Task<ActionResult<Response<List<GetTaskDto>>>> Get()
    {
        return Ok(await _repository.GetAllTasks());
    }

    // Allows to get a task
    [HttpGet("{id}")]
    public async Task<ActionResult<Response<GetTaskDto>>> GetById(int id)
    {
        var response = await _repository.GetTaskById(id);

        if(response.Data is null)
        {
            return NotFound(response);
        }
        return Ok(response);
    }

    // Allows to add a task to the list
    [HttpPost]
    public async Task<ActionResult<Response<GetTaskDto>>> Post(InsertTaskDto task)
    {
        return Ok(await _repository.InsertTask(task));
    }

    // Allows to update a task
    [HttpPut]
    public async Task<ActionResult<Response<GetTaskDto>>> Put(UpdateTaskDto task)
    {
        var response = await _repository.UpdateTask(task);

        if(response.Data is null)
        {
            return NotFound(response);
        }
        return Ok(response);
    }

    // Allows to remove a task
    [HttpDelete]
    public async Task<ActionResult<Response<List<GetTaskDto>>>> Delete(int id)
    {
        var response = await _repository.DeleteTask(id);

        if(response.Data is null)
        {
            return NotFound(response);
        }
        return Ok(response);
    }
}