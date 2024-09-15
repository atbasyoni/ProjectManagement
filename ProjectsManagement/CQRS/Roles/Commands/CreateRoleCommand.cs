using MediatR;
using ProjectsManagement.CQRS.Users.Commands;
using ProjectsManagement.DTOs;
using ProjectsManagement.Helpers;
using ProjectsManagement.Models;
using ProjectsManagement.Repositories.Base;

namespace ProjectsManagement.CQRS.Roles.Commands
{
    public record CreateRoleCommand(RoleDTO roleDTO) : IRequest<ResultDTO>;

    public record RoleDTO(string Name);

    public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, ResultDTO>
    {
        IRepository<Role> _roleRepository;
        IMediator _mediator;

        public CreateRoleCommandHandler(IRepository<Role> roleRepository, IMediator mediator)
        {
            _roleRepository = roleRepository;
            _mediator = mediator;
        }

        public async Task<ResultDTO> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            var result = await _roleRepository.FirstAsync(r => r.Name == request.roleDTO.Name);

            if (result is not null)
            {
                return ResultDTO.Faliure("Role is already exists!");
            }

            var role = request.roleDTO.MapOne<Role>();

            await _roleRepository.AddAsync(role);

            await _roleRepository.SaveChangesAsync();

            return ResultDTO.Sucess(true, "Role created successfully!");
        }
    }
}
