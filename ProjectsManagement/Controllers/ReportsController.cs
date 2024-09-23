using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectsManagement.CQRS.Reports.Queries;
using ProjectsManagement.ViewModels;

namespace ProjectsManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IMediator _mediator;

        ReportsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("user-tasks-report")]
        public async Task<ActionResult<ResultViewModel>> GetUserTaskProgress()
        {
            var resultDTO = await _mediator.Send(new GetUserTaskProgressQuery());

            if (!resultDTO.IsSuccess)
            {
                return ResultViewModel.Faliure(resultDTO.Message);
            }

            return ResultViewModel.Sucess(resultDTO.Data);
        }
        [HttpGet("User-Projects-report")]
        public async Task<ResultViewModel> GetUserProjectsCount()
        {
            var resultDTO = await _mediator.Send(new GetProjectsReportQuery());

            if (!resultDTO.IsSuccess)
            {
                return ResultViewModel.Faliure(resultDTO.Message);
            }

            return ResultViewModel.Sucess(resultDTO.Data, resultDTO.Message);
        }
        [HttpGet("User-Status-report")]
        public async Task<ResultViewModel> GetUserProjectUserStatusCounts()
        {
            var resultDTO = await _mediator.Send(new GetUsersReportQuery());

            if (!resultDTO.IsSuccess)
            {
                return ResultViewModel.Faliure(resultDTO.Message);
            }

            return ResultViewModel.Sucess(resultDTO.Data, resultDTO.Message);
        }
    }
}
