using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectsManagement.DTOs;
using ProjectsManagement.Enums;
using ProjectsManagement.Models;
using ProjectsManagement.Repositories.Base;

namespace ProjectsManagement.CQRS.Reports.Queries
{
    public record GetUserTaskProgressQuery() : IRequest<ResultDTO>;

    public class GetUserTaskProgressQueryHandler : IRequestHandler<GetUserTaskProgressQuery, ResultDTO>
    {
        private readonly IRepository<TaskUser> _taskUserRepository;
        private readonly UserState _userState;

        public GetUserTaskProgressQueryHandler(IRepository<TaskUser> taskUserRepository, UserState userState)
        {
            _taskUserRepository = taskUserRepository;
            _userState = userState;
        }

        public async Task<ResultDTO> Handle(GetUserTaskProgressQuery request, CancellationToken cancellationToken)
        {
            var userID = int.Parse(_userState.ID);
            var userTasks = await _taskUserRepository.GetAll(tu => tu.UserID == userID, ["Task"]).ToListAsync();
            var totalTasks = userTasks.Count();
            var completedTasks = userTasks.Count(tu => tu.Task.TaskStatus == TasksStatus.Done);
            var progress = totalTasks > 0 ? ((double)completedTasks / totalTasks) * 100 : 0;

            return ResultDTO.Sucess(new
            {
                TotalTasks = totalTasks,
                CompletedTasks = completedTasks,
                Progress = progress
            }, "User task progress retrieved successfully!");
        }
    }
}
