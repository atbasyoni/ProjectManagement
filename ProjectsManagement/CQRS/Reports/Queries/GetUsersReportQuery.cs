using MediatR;
using ProjectsManagement.DTOs;
using ProjectsManagement.Enums;
using ProjectsManagement.Models;
using ProjectsManagement.Repositories.Base;

namespace ProjectsManagement.CQRS.Reports.Queries
{
    public record GetUsersReportQuery : IRequest<ResultDTO>;
    public class GetUsersReportQueryHandler : IRequestHandler<GetUsersReportQuery, ResultDTO>
    {
        private readonly IRepository<ProjectUser> _projectUserRepository;
        private readonly IRepository<User> _userRepository;
        private readonly UserState _userState;

        public GetUsersReportQueryHandler(
            IRepository<ProjectUser> projectUserRepository,
            IRepository<User> userRepository,
            UserState userState)
        {
            _projectUserRepository = projectUserRepository;
            _userRepository = userRepository;
            _userState = userState;
        }

        public async Task<ResultDTO> Handle(GetUsersReportQuery request, CancellationToken cancellationToken)
        {
            var userID = int.Parse(_userState.ID);
            var userProjects = await _projectUserRepository.ListAsync(pu => pu.UserID == userID);

            var projectIDs = userProjects.Select(up => up.ProjectID).ToList();
 
            var usersInProjects = await _userRepository.ListAsync(u => u.ProjectMemberships.Any(pu => projectIDs.Contains(pu.ProjectID)));

            var activeUsersCount = usersInProjects.Count(u => u.UserStatus == UserStatus.Active);
            var inactiveUsersCount = usersInProjects.Count(u => u.UserStatus == UserStatus.NotActive);
 
            var userStatusCounts = new
            {
                ActiveUsers = activeUsersCount,
                InactiveUsers = inactiveUsersCount
            };

            return ResultDTO.Sucess(userStatusCounts, "Active and Inactive users count retrieved successfully!");
        }
    }
}
