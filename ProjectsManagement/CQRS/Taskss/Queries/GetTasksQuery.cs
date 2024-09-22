using MediatR;
using ProjectManagementSystem.Repository.Specification;
using ProjectsManagement.DTOs;
using ProjectsManagement.Models;
using ProjectsManagement.Repositories.Base;
using ProjectsManagement.Specification.TaskSpec;

namespace ProjectsManagement.CQRS.Taskss.Queries
{
    public record GetTasksQuery(SpecParams specParams) : IRequest<ResultDTO>;

    public record TaskDTO(string Title, TaskStatus TaskStatus, List<string> UserNames, string ProjectName, DateTime CreatedDate);

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
            (
                task.Title,
                (TaskStatus) task.TaskStatus,
                task.AssignedUsers.Select(au => au.User.UserName).ToList(),
                task.Project.Title,
                task.CreatedDate
            )).ToList();


            return ResultDTO.Sucess(taskDTOs);
        }
    }
}
