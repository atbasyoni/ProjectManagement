using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Repository.Specification;
using ProjectsManagement.CQRS.ProjectUsers.Queries;
using ProjectsManagement.CQRS.Taskss.Queries;
using ProjectsManagement.Data;
using ProjectsManagement.DTOs;
using ProjectsManagement.Models;
using ProjectsManagement.Repositories.Base;
using ProjectsManagement.Specification.ProjectsSpec;
using System.Linq;

namespace ProjectsManagement.CQRS.Projects.Queries
{
    public record GetProjectsQuery(SpecParams SpecParams) : IRequest<ResultDTO>;

    public class ProjectDTO
    { public string Title { get; set; }
        public ProjectStatus ProjectStatus { get; set; }
        public int NumUsers { get; set; }
        public int NumTasks { get; set; }
        public DateTime CreatedDate {get;set;}
    }

    public class GetProjectsQueryHandler : IRequestHandler<GetProjectsQuery, ResultDTO>
    {
        private readonly IRepository<Project> _projectRepository;

        public GetProjectsQueryHandler(IRepository<Project> projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<ResultDTO> Handle(GetProjectsQuery request, CancellationToken cancellationToken)
        {
            var spec = new ProjectWithSpecifications(request.SpecParams);

            var projects = await _projectRepository.GetAllWithSpecAsync(spec);

            var projectDTOs = projects.MapOne<IEnumerable<ProjectDTO>>();

            return ResultDTO.Sucess(projectDTOs);
        }
    }
}
