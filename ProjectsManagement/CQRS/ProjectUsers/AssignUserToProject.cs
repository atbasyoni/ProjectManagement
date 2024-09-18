using MediatR;
using ProjectsManagement.CQRS.TaskUsers.Commands;
using ProjectsManagement.DTOs;
using ProjectsManagement.Helpers;
using ProjectsManagement.Models;
using ProjectsManagement.Repositories.Base;

namespace ProjectsManagement.CQRS.ProjectUsers
{
    public class AssignUserToProject
    {
        public record AssignUserToProjectCommand(ProjectUserDTO projectUserDTO) : IRequest<ResultDTO>;

        public record ProjectUserDTO(int userID, int projectID);

        public class AssignUserToProjectCommandHandler : IRequestHandler<AssignUserToProjectCommand, ResultDTO>
        {
            IRepository<ProjectUser> _projectUserRepository;
            IMediator _mediator;

            public AssignUserToProjectCommandHandler(IRepository<ProjectUser> projectUserRepository, IMediator mediator)
            {
                _projectUserRepository = projectUserRepository;
                _mediator = mediator;
            }

            public async Task<ResultDTO> Handle(AssignUserToProjectCommand request, CancellationToken cancellationToken)
            {
                var projectUser = await _projectUserRepository.FirstAsync(pu => (pu.ProjectID == request.projectUserDTO.projectID) && (pu.UserID == request.projectUserDTO.userID));

                if (projectUser is not null)
                {
                    return ResultDTO.Faliure("User is already assigned to this project!");
                }

                projectUser = request.projectUserDTO.MapOne<ProjectUser>();

                projectUser = await _projectUserRepository.AddAsync(projectUser);
                await _projectUserRepository.SaveChangesAsync();

                return ResultDTO.Sucess(projectUser, "User assigned to project successfully!");
            }

            public async Task<bool> ValidateUserTask(TaskUserDTO taskUserDTO)
            {
                // Check the user exists
                // Check the project exists
                // Check if the user is already assigned to this project
                return true;
            }
        }
    }
}
