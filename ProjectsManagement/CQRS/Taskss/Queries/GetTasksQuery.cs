using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Repository.Specification;
using ProjectsManagement.CQRS.Projects.Queries;
using ProjectsManagement.DTOs;
using ProjectsManagement.Models;
using ProjectsManagement.Repositories.Base;
using ProjectsManagement.Specification.ProjectsSpec;
using ProjectsManagement.Specification.TaskSpec;

namespace ProjectsManagement.CQRS.Taskss.Queries
{
    public record GetTasksQuery(SpecParams specParams) : IRequest<ResultDTO>;
    public class TaskDTO
    {
        public string Title { get; set; }
        public TaskStatus TaskStatus { get; set; }
        public List <string> UserNames { get; set; }
        public string ProjectName { get; set; }
        public DateTime CreatedDate { get; set; }
    }

    public class GetTasksQueryHandler : IRequestHandler<GetTasksQuery, ResultDTO>
    {
        IRepository<Tasks> _taskRepository;

        public GetTasksQueryHandler(IRepository<Tasks> taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<ResultDTO> Handle(GetTasksQuery request, CancellationToken cancellationToken)
        {
            var spec = new TaskWithSpecifications(request.specParams);

            var tasks = await _taskRepository.GetAllWithSpecAsync(spec);

            var taskDTOs = tasks.Select(task => new TaskDTO
            {
                Title = task.Title,
                TaskStatus = (TaskStatus)task.TaskStatus,
                UserNames = task.AssignedUsers.Select(au => au.User.UserName).ToList(),
                ProjectName = task.Project?.Title,
                CreatedDate = task.CreatedDate
            }).ToList();


            return ResultDTO.Sucess(taskDTOs);
        }
    }
}
