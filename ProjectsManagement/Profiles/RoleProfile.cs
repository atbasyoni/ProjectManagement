using AutoMapper;
using ProjectsManagement.CQRS.Roles.Commands;
using ProjectsManagement.Models;
using ProjectsManagement.ViewModels.Roles;

namespace ProjectsManagement.Profiles
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<RoleViewModel, RoleDTO>().ReverseMap();
            CreateMap<RoleDTO, Role>().ReverseMap();
        }
    }
}
