using MediatR;
using ProjectManagementSystem.Repository.Specification;
using ProjectManagementSystem.Repository.Specification.ProjectSpecifications;
using ProjectsManagement.CQRS.Projects.Queries;
using ProjectsManagement.Models;
using ProjectsManagement.Repositories.Base;
using ProjectsManagement.Specification.TaskSpec;

namespace ProjectsManagement.CQRS.Taskss.Queries
{
    public record GetCountTaskQuery(SpecParams SpecParams) : IRequest<int>;
    public class GetCountTaskQueryHandler : IRequestHandler<GetCountTaskQuery, int>
    {
        private readonly IRepository<Tasks> _taskRepository;

        public GetCountTaskQueryHandler(IRepository<Tasks> taskRepository)
        {
            _taskRepository = taskRepository;
        }
        public async Task<int> Handle(GetCountTaskQuery request, CancellationToken cancellationToken)
        {
            var taskSpec = new CountTaskWithSpec(request.SpecParams);
            var count = await _taskRepository.GetCountWithSpecAsync(taskSpec);

            return count;
        }
    }
}
