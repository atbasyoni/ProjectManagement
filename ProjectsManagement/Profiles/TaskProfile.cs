using AutoMapper;
using ProjectsManagement.CQRS.Taskss.Commands;
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
        }
    }
}
