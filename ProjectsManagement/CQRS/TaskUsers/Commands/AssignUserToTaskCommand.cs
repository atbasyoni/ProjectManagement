using MediatR;
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
        IRepository<Tasks> _taskRepository;
        IRepository<User> _userRepository;
        IMediator _mediator;

        public AssignUserToTaskCommandHandler(IRepository<TaskUser> taskUserRepository, 
            IRepository<Tasks> taskRepository, 
            IRepository<User> userRepository, 
            IMediator mediator)
        {
            _taskUserRepository = taskUserRepository;
            _taskRepository = taskRepository;
            _userRepository = userRepository;
            _mediator = mediator;
        }

        public async Task<ResultDTO> Handle(AssignUserToTaskCommand request, CancellationToken cancellationToken)
        {
            var taskUser = await _taskUserRepository.FirstAsync(tu => (tu.TaskID == request.taskUserDTO.taskID) && (tu.UserID == request.taskUserDTO.userID));

            if (taskUser is not null)
            {
                return ResultDTO.Faliure("User is already assigned to this task!");
            }

            taskUser = request.taskUserDTO.MapOne<TaskUser>();

            taskUser = await _taskUserRepository.AddAsync(taskUser);
            await _taskUserRepository.SaveChangesAsync();

            return ResultDTO.Sucess(taskUser, "User assigned to task successfully!");
        }

        public async Task<bool> ValidateUserTask(TaskUserDTO taskUserDTO)
        {
            // Check the user exists
            // Check the task exists
            // Check the user assigned to the project
            // Check the task realted to that project
            // Check if the user is already assigned to this task
            return true;
        }
    }
}
