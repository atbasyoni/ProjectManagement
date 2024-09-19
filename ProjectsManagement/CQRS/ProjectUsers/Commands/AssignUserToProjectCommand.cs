using MediatR;
using ProjectsManagement.CQRS.Projects.Queries;
using ProjectsManagement.CQRS.ProjectUsers.Queries;
using ProjectsManagement.CQRS.Taskss.Queries;
using ProjectsManagement.CQRS.TaskUsers.Commands;
using ProjectsManagement.CQRS.Users.Queries;
using ProjectsManagement.DTOs;
using ProjectsManagement.Helpers;
using ProjectsManagement.Models;
using ProjectsManagement.Repositories.Base;

namespace ProjectsManagement.CQRS.ProjectUsers.Commands
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
            var validationResult = await ValidateProjectUser(request.projectUserDTO);

            if (!validationResult.IsSuccess) 
            {
                return validationResult;
            }

            var projectUser = request.projectUserDTO.MapOne<ProjectUser>();

            projectUser = await _projectUserRepository.AddAsync(projectUser);
            await _projectUserRepository.SaveChangesAsync();

            return ResultDTO.Sucess(projectUser, "User assigned to project successfully!");
        }

        private async Task<ResultDTO> ValidateProjectUser(ProjectUserDTO projectUserDTO)
        {
            var userExists = await _mediator.Send(new GetUserByIdQuery(projectUserDTO.userID));

            if (!userExists.IsSuccess)
            {
                return userExists;
            }

            var projectExists = await _mediator.Send(new GetProjectByIdQuery(projectUserDTO.projectID));

            if (!projectExists.IsSuccess)
            {
                return projectExists;
            }

            var userAssignedToProject = await _mediator.Send(new GetProjectUserByIdQuery(projectUserDTO.projectID, projectUserDTO.userID));

            if (userAssignedToProject.IsSuccess)
            {
                return ResultDTO.Faliure("User is already assigned to this project!");
            }

            return ResultDTO.Sucess(true);
        }
    }
}
