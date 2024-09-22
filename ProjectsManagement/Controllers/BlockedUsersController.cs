using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectsManagement.CQRS.BlockedUsers;
using ProjectsManagement.ViewModels;

namespace ProjectsManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlockedUsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BlockedUsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // Block a user
        [HttpPost("block")]
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

