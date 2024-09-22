using MediatR;
using ProjectsManagement.DTOs;
using ProjectsManagement.Models;
using ProjectsManagement.Repositories.Base;

namespace ProjectsManagement.CQRS.ProjectUsers.Commands
{
    public record UnassignUserFromProjectCommand(ProjectUserDTO projectUserDTO) : IRequest<ResultDTO>;
    public class UnassignUserFromProjectCommandHandler : IRequestHandler<UnassignUserFromProjectCommand, ResultDTO>
    {
        private readonly IRepository<ProjectUser> _projectUserRepository;
        private readonly IMediator _mediator;

        public UnassignUserFromProjectCommandHandler(IRepository<ProjectUser> projectUserRepository, IMediator mediator)
        {
            _projectUserRepository = projectUserRepository;
            _mediator = mediator;
        }

        public async Task<ResultDTO> Handle(UnassignUserFromProjectCommand request, CancellationToken cancellationToken)
        {
            var projectUser = await _projectUserRepository.FirstAsyncWithTracking(
                pu => pu.ProjectID == request.projectUserDTO.projectID && pu.UserID == request.projectUserDTO.userID);

            if (projectUser == null)
            {
                return ResultDTO.Faliure("User is not assigned to this project!");
            }

            await _projectUserRepository.DeleteAsync(projectUser);
            await _projectUserRepository.SaveChangesAsync();

            return ResultDTO.Sucess(null, "User unassigned from project successfully!");
        }
    }
}
