using MediatR;
using ProjectsManagement.DTOs;
using ProjectsManagement.Helpers;
using ProjectsManagement.Models;
using ProjectsManagement.Repositories.Base;

namespace ProjectsManagement.CQRS.Taskss.Commands
{
    public record AddTaskCommand(TaskCreateDTO TaskCreateDTO) : IRequest<ResultDTO>;

    public record TaskCreateDTO(string Title, string Description, DateTime StartDate, DateTime DueDate);

    public class AddTaskCommandHandler : IRequestHandler<AddTaskCommand, ResultDTO>
    {
        IRepository<Tasks> _taskRepository;
        IMediator _mediator;

        public AddTaskCommandHandler(IRepository<Tasks> taskRepository, IMediator mediator)
        {
            _taskRepository = taskRepository;
            _mediator = mediator;
        }

        public async Task<ResultDTO> Handle(AddTaskCommand request, CancellationToken cancellationToken)
        {
            var task = request.TaskCreateDTO.MapOne<Tasks>();
            task = await _taskRepository.AddAsync(task);

            return ResultDTO.Sucess(task, "Task added successfully!");
        }
    }
}
