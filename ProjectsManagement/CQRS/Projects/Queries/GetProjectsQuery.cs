﻿using MediatR;
using ProjectManagementSystem.Repository.Specification;
using ProjectsManagement.DTOs;
using ProjectsManagement.Enums;
using ProjectsManagement.Helpers;
using ProjectsManagement.Models;
using ProjectsManagement.Repositories.Base;
using ProjectsManagement.Specification.ProjectsSpec;

namespace ProjectsManagement.CQRS.Projects.Queries
{
    public record GetProjectsQuery(SpecParams SpecParams) : IRequest<ResultDTO>;

    public class ProjectDTO
    {
        public string Title { get; set; }
        public ProjectStatus ProjectStatus { get; set; }
        public int NumUsers { get; set; }
        public int NumTasks { get; set; }
        public DateTime CreatedDate { get; set; }
    }

    public class GetProjectsQueryHandler : IRequestHandler<GetProjectsQuery, ResultDTO>
    {
        IRepository<Project> _projectRepository;
        IMediator _mediator;

        public GetProjectsQueryHandler(IRepository<Project> projectRepository, IMediator mediator)
        {
            _projectRepository = projectRepository;
            _mediator = mediator;
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
