using MediatR;
using ProjectManagementSystem.Repository.Specification.ProjectSpecifications;
using ProjectManagementSystem.Repository.Specification;
using ProjectsManagement.Models;
using ProjectsManagement.Repositories.Base;

namespace ProjectsManagement.CQRS.Projects.Queries
{
    public record GetProjectsCountQuery(SpecParams SpecParams) : IRequest<int>;

    public class GetProjectsCountQueryHandler : IRequestHandler<GetProjectsCountQuery,int>
    {
        private readonly IRepository<Project> _projectRepo;

        public GetProjectsCountQueryHandler(IRepository<Project> projectRepo)
        {
            _projectRepo = projectRepo;
        }
        public async Task<int> Handle(GetProjectsCountQuery request, CancellationToken cancellationToken)
        {
            var projectSpec = new CountProjectWithSpec(request.SpecParams);

            var count =await _projectRepo.GetCountWithSpecAsync(projectSpec);

            return count ;
        }
    }
}
