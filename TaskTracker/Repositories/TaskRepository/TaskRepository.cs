using TaskTracker.Dtos;
using TaskTracker.Models;
using AutoMapper;
using Task = TaskTracker.Models.Task;

namespace TaskTracker.Repositories.TaskRepository;

// Using repository pattert
public class TaskRepository : ITaskRepository
{
    //Dependency injection
    private readonly IMapper _mapper;
    private readonly TaskTrackerDbContext _context;
    public TaskRepository(TaskTrackerDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Response<List<GetTaskDto>>> GetAllTasks()
    {
        var response = new Response<List<GetTaskDto>>();
        var allTasks = await _context.Tasks.ToListAsync();
        response.Data = allTasks.Select(x => _mapper.Map<GetTaskDto>(x)).ToList();
        return response;        
    }

    public async Task<Response<GetTaskDto>> GetTaskById(int id)
    {
        var response = new Response<GetTaskDto>();

        try
        {
            var singleTask = await _context.Tasks.FindAsync(id);
   
            if(singleTask is null)
            {
                throw new Exception($"Task with Id {id} not found");
            }

            response.Data = _mapper.Map<GetTaskDto>(singleTask);

        }
        catch(Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;
        }

        return response;
    }

    public async Task<Response<GetTaskDto>> InsertTask(InsertTaskDto task)
    {
        var response = new Response<GetTaskDto>();
        var mappedTask = _mapper.Map<Task>(task);

        _context.Tasks.Add(mappedTask);
        await _context.SaveChangesAsync();

        response.Data = _mapper.Map<GetTaskDto>(mappedTask);
        return response;
    }

    public async Task<Response<GetTaskDto>> UpdateTask(UpdateTaskDto task)
    {
        var response = new Response<GetTaskDto>();

        try
        {
            var taskToUpdate = await _context.Tasks.FindAsync(task.Id);

            if(taskToUpdate is null)
            {
                throw new Exception($"Task with Id {task.Id} not found");
            }

            _mapper.Map(task, taskToUpdate);
            await _context.SaveChangesAsync();

            response.Data = _mapper.Map<GetTaskDto>(taskToUpdate);
        }
        catch(Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;
        }
        
        return response;
    }

    public async Task<Response<List<GetTaskDto>>> DeleteTask(int id)
    {
        var response = new Response<List<GetTaskDto>>();

        try
        {
            var taskToRemove = await _context.Tasks.FindAsync(id);

            if(taskToRemove is null)
            {
                throw new Exception($"Task with Id {id} not found");
            }

            _context.Tasks.Remove(taskToRemove);
            await _context.SaveChangesAsync();

            var allTasks = await _context.Tasks.ToListAsync();
            response.Data = allTasks.Select(x => _mapper.Map<GetTaskDto>(x)).ToList();
        }
        catch(Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;
        }

        return response;
    }
}