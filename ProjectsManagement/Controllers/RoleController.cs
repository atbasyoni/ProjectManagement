using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProjectsManagement.CQRS.Roles.Commands;
using ProjectsManagement.Helpers;
using ProjectsManagement.ViewModels;
using ProjectsManagement.ViewModels.Roles;

namespace ProjectsManagement.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RoleController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ResultViewModel> CreateRole(RoleViewModel roleViewModel)
        {
            var roleDTO = roleViewModel.MapOne<RoleDTO>();

            var resultDTO = await _mediator.Send(new CreateRoleCommand(roleDTO));

            if (!resultDTO.IsSuccess)
            {
                return ResultViewModel.Faliure(resultDTO.Message);
            }

            return ResultViewModel.Sucess(resultDTO.Data, resultDTO.Message);
        }
    }
}
