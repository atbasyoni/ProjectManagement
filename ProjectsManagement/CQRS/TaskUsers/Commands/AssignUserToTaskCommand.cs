using MediatR;
using ProjectsManagement.CQRS.Taskss.Queries;
using ProjectsManagement.CQRS.TaskUsers.Queries;
using ProjectsManagement.CQRS.Users.Queries;
using ProjectsManagement.DTOs;
using ProjectsManagement.Helpers;
using ProjectsManagement.Models;
using ProjectsManagement.Repositories.Base;

namespace ProjectsManagement.CQRS.TaskUsers.Commands
{
    public record AssignUserToTaskCommand(TaskUserDTO taskUserDTO) : IRequest<ResultDTO>;

    public record TaskUserDTO(int taskID, int userID);

    public class AssignUserToTaskCommandHandler : IRequestHandler<AssignUserToTaskCommand, ResultDTO>
    {
        IRepository<TaskUser> _taskUserRepository;
        IMediator _mediator;

        public AssignUserToTaskCommandHandler(IRepository<TaskUser> taskUserRepository, IMediator mediator)
        {
            _taskUserRepository = taskUserRepository;
            _mediator = mediator;
        }

        public async Task<ResultDTO> Handle(AssignUserToTaskCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await ValidateUserTask(request.taskUserDTO);

            if (!validationResult.IsSuccess)
            {
                return validationResult;
            }

            var taskUser = request.taskUserDTO.MapOne<TaskUser>();

            taskUser = await _taskUserRepository.AddAsync(taskUser);
            await _taskUserRepository.SaveChangesAsync();

            return ResultDTO.Sucess(taskUser, "User assigned to task successfully!");
        }

        private async Task<ResultDTO> ValidateUserTask(TaskUserDTO taskUserDTO)
        {
            var userExists = await _mediator.Send(new GetUserByIdQuery(taskUserDTO.userID));

            if (!userExists.IsSuccess)
            {
                return userExists;
            }

            var taskExists = await _mediator.Send(new GetTaskByIdQuery(taskUserDTO.taskID));

            if (!taskExists.IsSuccess)
            {
                return taskExists;
            }

            var userAssignedToTask = await _mediator.Send(new GetTaskUserByIdQuery(taskUserDTO.taskID, taskUserDTO.userID));

            if (userAssignedToTask.IsSuccess)
            {
                return ResultDTO.Faliure("User is already assigned to this task!");
            }

            return ResultDTO.Sucess(true);
        }
    }
}