using MediatR;
using ProjectsManagement.DTOs;
using ProjectsManagement.Models;
using ProjectsManagement.Repositories.Base;

namespace ProjectsManagement.CQRS.Reports.Queries
{
    public record GetProjectsReportQuery : IRequest<ResultDTO>;
    public class GetProjectsReportQueryHandler : IRequestHandler<GetProjectsReportQuery, ResultDTO>
    {
        private readonly IRepository<ProjectUser> _projectUserRepository;
        private readonly UserState _userState;

        public GetProjectsReportQueryHandler(IRepository<ProjectUser> projectUserRepository, UserState userState)
        {
            _projectUserRepository = projectUserRepository;
            _userState = userState;
        }

        public async Task<ResultDTO> Handle(GetProjectsReportQuery request, CancellationToken cancellationToken)
        {
            var userID = int.Parse(_userState.ID);

            var userProjects = await _projectUserRepository.ListAsync(pu => pu.UserID == userID);
            var totalProjects = userProjects.Count;
            return ResultDTO.Sucess(totalProjects, "User assigned projects count retrieved successfully!");
        }

    }
}