using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectsManagement.Models;
using ProjectsManagement.Repositories.Base;

namespace ProjectsManagement.CQRS.ProjectUsers.Queries
{
    public record GetProjectsUsersCountQuery(List<int> ProjectIDs) : IRequest<Dictionary<int, int>>;

    public class GetProjectsUsersCountQueryHandler : IRequestHandler<GetProjectsUsersCountQuery, Dictionary<int, int>>
    {
        IRepository<ProjectUser> _projectUserRepository;

        public GetProjectsUsersCountQueryHandler(IRepository<ProjectUser> projectUserRepository)
        {
            _projectUserRepository = projectUserRepository;
        }

        public async Task<Dictionary<int, int>> Handle(GetProjectsUsersCountQuery request, CancellationToken cancellationToken)
        { 
            var projectUserCounts = new Dictionary<int, int>();

            foreach (var projectID in request.ProjectIDs)
            {
                var userCount = await _projectUserRepository.CountAsync(pu => pu.ProjectID == projectID);
                projectUserCounts[projectID] = userCount;
            }

            return projectUserCounts;
        }
    }
}
