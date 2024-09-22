using MediatR;
using ProjectsManagement.DTOs;
using ProjectsManagement.Enums;
using ProjectsManagement.Models;
using ProjectsManagement.Repositories.Base;

namespace ProjectsManagement.CQRS.Taskss.Commands
{
    public record ChangeTaskStatusCommand(TaskStatusDTO taskStatusDTO) : IRequest<ResultDTO>;

    public record TaskStatusDTO(int taskID, TasksStatus tasksStatus);

    public class ChangeTaskStatusCommandHandler : IRequestHandler<ChangeTaskStatusCommand, ResultDTO>
    {
        IRepository<Tasks> _taskRepository;
        IMediator _mediator;

        public ChangeTaskStatusCommandHandler(IRepository<Tasks> taskRepository, IMediator mediator)
        {
            _taskRepository = taskRepository;
            _mediator = mediator;
        }

        public async Task<ResultDTO> Handle(ChangeTaskStatusCommand request, CancellationToken cancellationToken)
        {
            var task = await _taskRepository.FirstAsync(t => t.ID == request.taskStatusDTO.taskID 
            && t.TaskStatus == request.taskStatusDTO.tasksStatus);

            if (task is null)
            {
                return ResultDTO.Faliure($"Task status is already {request.taskStatusDTO.tasksStatus}!");
            }

            task.TaskStatus = request.taskStatusDTO.tasksStatus;

            await _taskRepository.UpdateAsync(task);
            await _taskRepository.SaveChangesAsync();

            return ResultDTO.Sucess(task, "Task Updated successfully!");
        }
    }
}
