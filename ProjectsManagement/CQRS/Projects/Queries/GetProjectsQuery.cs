using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectsManagement.DTOs;
using ProjectsManagement.Models;
using ProjectsManagement.Repositories.Base;

namespace ProjectsManagement.CQRS.Projects.Queries
{
    public record GetProjectsQuery(int pageNumber, int pageSize) : IRequest<ResultDTO>;

    public class GetProjectsQueryHandler : IRequestHandler<GetProjectsQuery, ResultDTO>
    {
        private readonly IRepository<Project> _projectRepository;

        public GetProjectsQueryHandler(IRepository<Project> projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<ResultDTO> Handle(GetProjectsQuery request, CancellationToken cancellationToken)
        {
            var projects = await _projectRepository.GetAllPaginationAsync(request.pageNumber, request.pageSize).ToListAsync();

            if (projects is null)
            {
                return ResultDTO.Faliure("Failed to retrieve projects!");
            }

            return ResultDTO.Sucess(projects);
        }
    }
}
