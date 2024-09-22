using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProjectsManagement.CQRS.BlockedUsers.Commands;
using ProjectsManagement.ViewModels;

namespace ProjectsManagement.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BlockedUsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BlockedUsersController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost]
        public async Task<ResultViewModel> BlockUser(int BlockedID)
        {
            var result = await _mediator.Send(new BlockUserCommand(BlockedID));

            if (!result.IsSuccess)
            {
                return ResultViewModel.Faliure(result.Message);
            }

            return ResultViewModel.Sucess(result.Message);
        }

        [HttpDelete]
        public async Task<ResultViewModel> UnblockUser(int BlockedID)
        {
            var result = await _mediator.Send(new UnblockUserCommand(BlockedID));

            if (!result.IsSuccess)
            {
                return ResultViewModel.Faliure(result.Message);
            }

            return ResultViewModel.Sucess(result.Message);
        }
    }
}

