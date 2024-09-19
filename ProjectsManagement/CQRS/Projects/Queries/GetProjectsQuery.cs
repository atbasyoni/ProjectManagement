using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectsManagement.CQRS.ProjectUsers.Queries;
using ProjectsManagement.CQRS.Taskss.Queries;
using ProjectsManagement.Data;
using ProjectsManagement.DTOs;
using ProjectsManagement.Enums;
using ProjectsManagement.Helpers;
using ProjectsManagement.Models;
using ProjectsManagement.Repositories.Base;

namespace ProjectsManagement.CQRS.Projects.Queries
{
    public record GetProjectsQuery(int pageNumber, int pageSize) : IRequest<ResultDTO>;

    public record ProjectDTO(string Title, ProjectStatus ProjectStatus, int NumUsers, int NumTasks, DateTime CreatedDate);

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
            var projects = _projectRepository.GetAllPagination(request.pageNumber, request.pageSize);
            var projectIDs = await projects.Select(p => p.ID).ToListAsync();

            var projectUserCounts = await _mediator.Send(new GetProjectsUsersCountQuery(projectIDs));
            var projectTaskCounts = await _mediator.Send(new GetProjectsTasksCountQuery(projectIDs));

            var projectDTOs = await projects.Select(p => new ProjectDTO(p.Title, p.ProjectStatus, projectUserCounts[p.ID], projectTaskCounts[p.ID], p.CreatedDate)).ToListAsync();

            return ResultDTO.Sucess(projectDTOs);
        }
    }
}
