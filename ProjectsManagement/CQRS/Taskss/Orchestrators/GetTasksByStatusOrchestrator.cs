using MediatR;
using ProjectsManagement.CQRS.Taskss.Queries;
using ProjectsManagement.DTOs;

namespace ProjectsManagement.CQRS.Taskss.Orchestrators
{
    public record GetTasksByStatusOrchestrator(int projectID) : IRequest<ResultDTO>;

    public class TaskReturnGroupByStatusDTO()
    {
        public List<TaskDTO> Done { get; set; }
        public List<TaskDTO> InProgress { get; set; }
        public List<TaskDTO> ToDo { get; set; }
    }
    public class GetTasksByStatusOrchestratorHandler : IRequestHandler<GetTasksByStatusOrchestrator, ResultDTO>
    {
        private readonly IMediator _mediator;

        public GetTasksByStatusOrchestratorHandler(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<ResultDTO> Handle(GetTasksByStatusOrchestrator request, CancellationToken cancellationToken)
        {
            var CompletedTasks = await _mediator.Send(new GetCompletedTasksQuery(request.projectID));
            if (!CompletedTasks.IsSuccess) 
            {
                return CompletedTasks;
            }
            var PendingTasks = await _mediator.Send(new GetPendingTasksQuery(request.projectID));
            if (!PendingTasks.IsSuccess)
            {
                return PendingTasks;
            }
            var ToDoTasks = await _mediator.Send(new GetToDoTasksQuery(request.projectID));
            if (!ToDoTasks.IsSuccess)
            {
                return ToDoTasks;
            }

            var TasksReturn = new TaskReturnGroupByStatusDTO()
            {
                Done = CompletedTasks.Data,
                InProgress = PendingTasks.Data,
                ToDo = ToDoTasks.Data
            };
            return ResultDTO.Sucess(TasksReturn);
        }
    }
}
