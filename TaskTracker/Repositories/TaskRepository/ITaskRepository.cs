using TaskTracker.Models;
using TaskTracker.Dtos;
using Task = TaskTracker.Models.Task;

namespace TaskTracker.Repositories.TaskRepository;

public interface ITaskRepository
{
    Task<Response<List<GetTaskDto>>> GetAllTasks();
    Task<Response<GetTaskDto>> GetTaskById(int id);
    Task<Response<GetTaskDto>> InsertTask(InsertTaskDto task);
    Task<Response<GetTaskDto>> UpdateTask(UpdateTaskDto task);
    Task<Response<List<GetTaskDto>>> DeleteTask(int id);   
}