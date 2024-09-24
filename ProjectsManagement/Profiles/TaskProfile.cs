using AutoMapper;
using ProjectsManagement.CQRS.Taskss.Commands;
using ProjectsManagement.CQRS.Taskss.Queries;
using ProjectsManagement.CQRS.TaskUsers.Commands;
using ProjectsManagement.Models;
using ProjectsManagement.ViewModels.Taskss;

namespace ProjectsManagement.Profiles
{
    public class TaskProfile : Profile
    {
        public TaskProfile()
        {
            CreateMap<TaskCreateViewModel, TaskCreateDTO>().ReverseMap();
            CreateMap<TaskCreateDTO, Tasks>().ReverseMap();

            CreateMap<TaskDTO, Tasks>().ReverseMap();
            CreateMap<TaskStatusViewModel, TaskStatusDTO>().ReverseMap();
            CreateMap<TaskStatusDTO, Tasks>().ReverseMap();

            CreateMap<TaskUpdateViewModel, UpdateTaskDTO>().ReverseMap();
            CreateMap<UpdateTaskDTO, Tasks>().ReverseMap();

            CreateMap<TaskUserDTO, TaskUser>().ReverseMap();
        }
    }
}
