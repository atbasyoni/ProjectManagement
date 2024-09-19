using MediatR;
using ProjectsManagement.DTOs;
using ProjectsManagement.Models;
using ProjectsManagement.Repositories.Base;

namespace ProjectsManagement.CQRS.ProjectUsers.Queries
{
    public record GetProjectUserByIdQuery(int projectID, int UserID) : IRequest<ResultDTO>;

    public class GetProjectUserByIdQueryHandler : IRequestHandler<GetProjectUserByIdQuery, ResultDTO>
    {
        IRepository<ProjectUser> _projectUserRepository;

        public GetProjectUserByIdQueryHandler(IRepository<ProjectUser> projectUserRepository)
        {
            _projectUserRepository = projectUserRepository;
        }

        public async Task<ResultDTO> Handle(GetProjectUserByIdQuery request, CancellationToken cancellationToken)
        {
            var projectUser = await _projectUserRepository.FirstAsync(pu => pu.ProjectID == request.projectID && pu.UserID == request.UserID);

            if (projectUser is null)
            {
                return ResultDTO.Faliure("The user isn't assigned to this project!");
            }

            return ResultDTO.Sucess(projectUser);
        }
    }
}
