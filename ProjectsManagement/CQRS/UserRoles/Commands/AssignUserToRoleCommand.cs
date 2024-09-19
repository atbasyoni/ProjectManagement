using MediatR;
using ProjectsManagement.CQRS.Roles.Queries;
using ProjectsManagement.CQRS.TaskUsers.Commands;
using ProjectsManagement.CQRS.UserRoles.Queries;
using ProjectsManagement.CQRS.Users.Queries;
using ProjectsManagement.DTOs;
using ProjectsManagement.Helpers;
using ProjectsManagement.Models;
using ProjectsManagement.Repositories.Base;

namespace ProjectsManagement.CQRS.UserRoles.Commands
{
    public record AssignUserToRoleCommand(UserRoleDTO userRoleDTO) : IRequest<ResultDTO>;

    public record UserRoleDTO(int userID, int roleID);

    public class AssignUserToRoleCommandHandler : IRequestHandler<AssignUserToRoleCommand, ResultDTO>
    {
        IRepository<UserRole> _userRoleRepository;
        IMediator _mediator;

        public AssignUserToRoleCommandHandler(IRepository<UserRole> userRoleRepository, IMediator mediator)
        {
            _userRoleRepository = userRoleRepository;
            _mediator = mediator;
        }

        public async Task<ResultDTO> Handle(AssignUserToRoleCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await ValidateUserRole(request.userRoleDTO);

            if (!validationResult.IsSuccess) 
            {
                return validationResult;
            }

            var userRole = request.userRoleDTO.MapOne<UserRole>();

            userRole = await _userRoleRepository.AddAsync(userRole);
            await _userRoleRepository.SaveChangesAsync();

            return ResultDTO.Sucess(userRole, "User assigned to role successfully!");
        }

        private async Task<ResultDTO> ValidateUserRole(UserRoleDTO userRoleDTO)
        {
            var userExists = await _mediator.Send(new GetUserByIdQuery(userRoleDTO.userID));

            if (!userExists.IsSuccess)
            {
                return userExists;
            }

            var roleExists = await _mediator.Send(new GetRoleByIdQuery(userRoleDTO.roleID));

            if (!roleExists.IsSuccess)
            {
                return roleExists;
            }

            var userAssignedToRole = await _mediator.Send(new GetUserRoleByIdQuery(userRoleDTO.userID, userRoleDTO.roleID));

            if (userAssignedToRole.IsSuccess) 
            {
                return ResultDTO.Faliure("User is already assigned to this role!");
            }

            return ResultDTO.Sucess(true);
        }
    }
}
