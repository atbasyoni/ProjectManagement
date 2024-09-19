using MediatR;
using ProjectsManagement.DTOs;
using ProjectsManagement.Models;
using ProjectsManagement.Repositories.Base;

namespace ProjectsManagement.CQRS.UserRoles.Queries
{
    public record GetUserRoleByIdQuery(int UserID, int RoleID) : IRequest<ResultDTO>;

    public class GetUserRoleByIdQueryHandler : IRequestHandler<GetUserRoleByIdQuery, ResultDTO>
    {
        IRepository<UserRole> _userRoleRepository;

        public GetUserRoleByIdQueryHandler(IRepository<UserRole> userRoleRepository)
        {
            _userRoleRepository = userRoleRepository;
        }

        public async Task<ResultDTO> Handle(GetUserRoleByIdQuery request, CancellationToken cancellationToken)
        {
            var UserRole = await _userRoleRepository.FirstAsync(tu => tu.UserID == request.UserID && tu.RoleID == request.RoleID);

            if (UserRole is null)
            {
                return ResultDTO.Faliure("UserRole isn't found!");
            }

            return ResultDTO.Sucess(UserRole);
        }
    }
}
