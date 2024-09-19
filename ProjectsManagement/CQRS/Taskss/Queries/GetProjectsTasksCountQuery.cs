using MediatR;
using ProjectsManagement.Models;
using ProjectsManagement.Repositories.Base;

namespace ProjectsManagement.CQRS.Taskss.Queries
{
    public record GetProjectsTasksCountQuery(List<int> ProjectIDs) : IRequest<Dictionary<int, int>>;

    public class GetProjectsTasksCountQueryHandler : IRequestHandler<GetProjectsTasksCountQuery, Dictionary<int, int>>
    {
        IRepository<Tasks> _taskRepository;

        public GetProjectsTasksCountQueryHandler(IRepository<Tasks> taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<Dictionary<int, int>> Handle(GetProjectsTasksCountQuery request, CancellationToken cancellationToken)
        {
            var projectTaskCounts = new Dictionary<int, int>();

            foreach (var projectID in request.ProjectIDs)
            {
                var userCount = await _taskRepository.CountAsync(t => t.ProjectID == projectID);
                projectTaskCounts[projectID] = userCount;
            }

            return projectTaskCounts;
        }
    }
}
