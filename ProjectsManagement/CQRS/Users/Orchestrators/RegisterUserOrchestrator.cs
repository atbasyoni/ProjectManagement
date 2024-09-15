using MediatR;
using ProjectsManagement.CQRS.UserRoles.Commands;
using ProjectsManagement.CQRS.Users.Commands;
using ProjectsManagement.DTOs;
using ProjectsManagement.Models;

namespace ProjectsManagement.CQRS.Users.Orchestrators
{
    public record RegisterUserOrchestrator(RegisterRequestDTO registerRequestDTO) : IRequest<ResultDTO>;

    public class RegisterUserOrchestratorHandler : IRequestHandler<RegisterUserOrchestrator, ResultDTO>
    {
        private readonly IMediator _mediator;
        public RegisterUserOrchestratorHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<ResultDTO> Handle(RegisterUserOrchestrator request, CancellationToken cancellationToken)
        {
            var resultDTO = await _mediator.Send(new RegisterUserCommand(request.registerRequestDTO));

            if (!resultDTO.IsSuccess) 
            {
                return resultDTO;
            }

            UserRoleDTO userRoleDTO = new UserRoleDTO(resultDTO.Data.ID, Role.User);

            await _mediator.Send(new AssignUserToRoleCommand(userRoleDTO));

            return resultDTO;
        }
    }
}
