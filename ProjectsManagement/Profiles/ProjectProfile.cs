using AutoMapper;
using ProjectsManagement.CQRS.Projects.Commands;
using ProjectsManagement.CQRS.Projects.Queries;
using ProjectsManagement.CQRS.ProjectUsers.Commands;
using ProjectsManagement.Models;
using ProjectsManagement.ViewModels.Projects;

namespace ProjectsManagement.Profiles
{
    public class ProjectProfile : Profile
    {
        public ProjectProfile()
        {
            CreateMap<ProjectCreateViewModel, ProjectCreateDTO>().ReverseMap();
            CreateMap<ProjectCreateDTO, Project>().ReverseMap();

            CreateMap<Project, ProjectDTO>()
                .ForMember(dest => dest.NumUsers, opt => opt
                .MapFrom(src => src.ProjectUsers.Select(us => us.User)
                .Count()))
           .ForMember(dest => dest.NumTasks, opt => opt.MapFrom(src => src.Tasks.Count()));

            CreateMap<AssignUserProjectViewModel, ProjectUserDTO>().ReverseMap();
            CreateMap<ProjectUserDTO, ProjectUser>().ReverseMap();
        }
    }
}
