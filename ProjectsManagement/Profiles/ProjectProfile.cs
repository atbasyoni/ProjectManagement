using AutoMapper;
using ProjectsManagement.CQRS.Projects.Commands;
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
        }
    }
}
