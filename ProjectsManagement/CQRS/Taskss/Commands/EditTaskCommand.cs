using MediatR;
using ProjectsManagement.DTOs;
using ProjectsManagement.Enums;
using ProjectsManagement.Models;
using ProjectsManagement.Repositories.Base;

namespace ProjectsManagement.CQRS.Taskss.Commands
{
    public record UpdateTaskCommand(UpdateTaskDTO updateTaskDTO) : IRequest<ResultDTO>;

    public record UpdateTaskDTO(int TaskID, string Title, string Description, DateTime StartDate, DateTime DueDate);
    public class UpdateTaskCommandHandler : IRequestHandler<UpdateTaskCommand, ResultDTO>
    {
        private readonly IRepository<Tasks> _taskRepository;
        private readonly IMediator _mediator;

        public UpdateTaskCommandHandler(IRepository<Tasks> taskRepository, IMediator mediator)
        {
            _taskRepository = taskRepository;
            _mediator = mediator;
        }

        public async Task<ResultDTO> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
        {
            var task = await _taskRepository.FirstAsyncWithTracking(t => t.ID == request.updateTaskDTO.TaskID);

            if (task is null)
            {
                return ResultDTO.Faliure("Task not found!");
            }

            task.Title = request.updateTaskDTO.Title;
            task.Description = request.updateTaskDTO.Description;
            task.StartDate = request.updateTaskDTO.StartDate;
            task.DueDate = request.updateTaskDTO.DueDate;

            await _taskRepository.UpdateAsync(task);
            await _taskRepository.SaveChangesAsync();

            return ResultDTO.Sucess(task, "Task updated successfully!");
        }
    }
}
