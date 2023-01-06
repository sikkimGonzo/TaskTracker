using AutoMapper; 
using TaskTracker.Models;
using TaskTracker.Dtos;
using Task = TaskTracker.Models.Task;

namespace TodoList;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Project, GetProjectDto>();
        CreateMap<InsertProjectDto,Project>();
        CreateMap<UpdateProjectDto, Project>();

        CreateMap<Task, GetTaskDto>();
        CreateMap<InsertTaskDto,Task>();
        CreateMap<UpdateTaskDto, Task>();
    }
}