using MediatR;
using ProjectsManagement.DTOs;
using ProjectsManagement.Models;
using ProjectsManagement.Repositories.Base;

namespace ProjectsManagement.CQRS.TaskUsers.Commands
{
    public record UnassignUserFromTaskCommand(TaskUserDTO taskUserDTO) : IRequest<ResultDTO>;
    public class UnassignUserFromTaskCommandHandler : IRequestHandler<UnassignUserFromTaskCommand, ResultDTO>
    {
        private readonly IRepository<TaskUser> _taskUserRepository;
        private readonly IMediator _mediator;

        public UnassignUserFromTaskCommandHandler(IRepository<TaskUser> taskUserRepository, IMediator mediator)
        {
            _taskUserRepository = taskUserRepository;
            _mediator = mediator;
        }

        public async Task<ResultDTO> Handle(UnassignUserFromTaskCommand request, CancellationToken cancellationToken)
        {
            var taskUser = await _taskUserRepository.FirstAsyncWithTracking(tu =>
                tu.TaskID == request.taskUserDTO.taskID && tu.UserID == request.taskUserDTO.userID);

            if (taskUser == null)
            {
                return ResultDTO.Faliure("User is not assigned to this task!");
            }

            await _taskUserRepository.DeleteAsync(taskUser);
            await _taskUserRepository.SaveChangesAsync();

            return ResultDTO.Sucess(null, "User unassigned from task successfully!");
        }
    }
}
