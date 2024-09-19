using MediatR;
using ProjectsManagement.DTOs;
using ProjectsManagement.Models;
using ProjectsManagement.Repositories.Base;

namespace ProjectsManagement.CQRS.Roles.Queries
{
    public record GetRoleByIdQuery(int roleID) : IRequest<ResultDTO>;

    public class GetRoleByIdQueryHandler : IRequestHandler<GetRoleByIdQuery, ResultDTO>
    {
        IRepository<Role> _roleRepository;

        public GetRoleByIdQueryHandler(IRepository<Role> roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<ResultDTO> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
        {
            var role = await _roleRepository.GetByIDAsync(request.roleID);

            if (role is null)
            {
                return ResultDTO.Faliure("Role isn't found!");
            }

            return ResultDTO.Sucess(role);
        }
    }
}
