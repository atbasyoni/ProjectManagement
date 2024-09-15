using MediatR;
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
            var userRole = await _userRoleRepository.FirstAsync(ur => (ur.RoleID == request.userRoleDTO.roleID) &&  (ur.UserID == request.userRoleDTO.userID));

            if (userRole is not null)
            {
                return ResultDTO.Faliure("User is already assigned to this role!");
            }

            userRole = request.userRoleDTO.MapOne<UserRole>();

            userRole = await _userRoleRepository.AddAsync(userRole);
            await _userRoleRepository.SaveChangesAsync();

            return ResultDTO.Sucess(userRole, "User assigned to role successfully!");
        }
    }
}
