using MediatR;
using ProjectsManagement.CQRS.Taskss.Commands;
using ProjectsManagement.CQRS.TaskUsers.Commands;
using ProjectsManagement.DTOs;

namespace ProjectsManagement.CQRS.Taskss.Orchestrators
{
    public record AddTaskOrchestrator(TaskCreateDTO taskCreateDTO) : IRequest<ResultDTO>;

    public class AddTaskOrchestratorHandler : IRequestHandler<AddTaskOrchestrator, ResultDTO>
    {
        IMediator _mediator;

        public AddTaskOrchestratorHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<ResultDTO> Handle(AddTaskOrchestrator request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new AddTaskCommand(request.taskCreateDTO));

            if (!result.IsSuccess) 
            {
                return result;
            }

            foreach(var userId in request.taskCreateDTO.UserIDs)
            {
                TaskUserDTO taskUserDTO = new() { taskID =  result.Data.ID, userID = userId };
                await _mediator.Send(new AssignUserToTaskCommand(taskUserDTO));
            }

            return result;
        }
    }
}
