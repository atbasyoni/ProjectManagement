using MediatR;
using ProjectsManagement.DTOs;
using ProjectsManagement.Models;
using ProjectsManagement.Repositories.Base;

namespace ProjectsManagement.CQRS.TaskUsers.Queries
{
    public record GetTaskUserByIdQuery(int taskID, int userID) : IRequest<ResultDTO>;

    public class GetTaskUserByIdQueryHandler : IRequestHandler<GetTaskUserByIdQuery, ResultDTO>
    {
        IRepository<TaskUser> _taskUserRepository;

        public GetTaskUserByIdQueryHandler(IRepository<TaskUser> taskUserRepository)
        {
            _taskUserRepository = taskUserRepository;
        }

        public async Task<ResultDTO> Handle(GetTaskUserByIdQuery request, CancellationToken cancellationToken)
        {
            var taskUser = await _taskUserRepository.FirstAsync(tu => tu.UserID == request.userID && tu.TaskID == request.taskID);

            if (taskUser is null) 
            {
                return ResultDTO.Faliure("TaskUser isn't found!");
            }

            return ResultDTO.Sucess(taskUser);
        }
    }
}
