using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectsManagement.CQRS.Taskss.Orchestrators;
using ProjectsManagement.DTOs;
using ProjectsManagement.Enums;
using ProjectsManagement.Models;
using ProjectsManagement.Repositories.Base;

namespace ProjectsManagement.CQRS.Taskss.Queries
{
   
    public record GetAllTasksByStatusQuery(int projectID) : IRequest<ResultDTO>;

    public class GetAllTasksByStatusQueryHandler : IRequestHandler<GetAllTasksByStatusQuery, ResultDTO>
    {
        IRepository<Tasks> _taskRepository;

        public GetAllTasksByStatusQueryHandler(IRepository<Tasks> taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<ResultDTO> Handle(GetAllTasksByStatusQuery request, CancellationToken cancellationToken)
        {
            var tasks = await _taskRepository.GetAll()
                .Where(t => t.ProjectID == request.projectID)
                .Select(task => new TaskDTO()
                {
                    Title = task.Title,
                    TaskStatus = (TaskStatus)task.TaskStatus,
                    UserNames = task.AssignedUsers.Select(au => au.User.UserName).ToList(),
                    ProjectName = task.Project.Title,
                    CreatedDate =  task.CreatedDate
                }).ToListAsync();

            if (tasks is null)
            {
                return ResultDTO.Faliure("Failed to retrieve tasks!");
            }

            var TasksReturn = new TaskReturnGroupByStatusDTO()
            {
                Done = tasks.Where(t=>(TasksStatus)t.TaskStatus == TasksStatus.Done).ToList(),
                InProgress = tasks.Where(t => (TasksStatus)t.TaskStatus == TasksStatus.InProgres).ToList(),
                ToDo = tasks.Where(t => (TasksStatus)t.TaskStatus == TasksStatus.ToDo).ToList()
            };
            return ResultDTO.Sucess(TasksReturn);
        }
    }
}
