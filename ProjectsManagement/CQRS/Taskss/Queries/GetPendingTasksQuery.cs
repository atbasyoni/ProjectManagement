using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectsManagement.DTOs;
using ProjectsManagement.Enums;
using ProjectsManagement.Models;
using ProjectsManagement.Repositories.Base;

namespace ProjectsManagement.CQRS.Taskss.Queries
{
    
    public record GetPendingTasksQuery(int projectID) : IRequest<ResultDTO>;

    public class GetPendingTasksQueryHandler : IRequestHandler<GetPendingTasksQuery, ResultDTO>
    {
        IRepository<Tasks> _taskRepository;

        public GetPendingTasksQueryHandler(IRepository<Tasks> taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<ResultDTO> Handle(GetPendingTasksQuery request, CancellationToken cancellationToken)
        {
            var tasks = await _taskRepository.GetAll().Where(t=>t.ProjectID==request.projectID && t.TaskStatus==TasksStatus.InProgres).ToListAsync();

            if (tasks is null)
            {
                return ResultDTO.Faliure("Failed to retrieve tasks!");
            }

            return ResultDTO.Sucess(tasks);
        }
    }
}
