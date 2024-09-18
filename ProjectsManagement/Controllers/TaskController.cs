using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProjectsManagement.CQRS.Taskss.Commands;
using ProjectsManagement.Helpers;
using ProjectsManagement.ViewModels;
using ProjectsManagement.ViewModels.Taskss;

namespace ProjectsManagement.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TaskController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ResultViewModel> AddTask(TaskCreateViewModel taskCreateViewModel)
        {
            var taskCreateDTO = taskCreateViewModel.MapOne<TaskCreateDTO>();

            var resultDTO = await _mediator.Send(new AddTaskCommand(taskCreateDTO));

            if (!resultDTO.IsSuccess)
            {
                return ResultViewModel.Faliure(resultDTO.Message);
            }

            return ResultViewModel.Sucess(resultDTO.Message);
        }

        [HttpPut]
        public async Task<ResultViewModel> ChangeTaskStatus(TaskStatusViewModel taskStatusViewModel)
        {
            var taskStatusDTO = taskStatusViewModel.MapOne<TaskStatusDTO>();

            var resultDTO = await _mediator.Send(new ChangeTaskStatusCommand(taskStatusDTO));

            if (!resultDTO.IsSuccess)
            {
                return ResultViewModel.Faliure(resultDTO.Message);
            }

            return ResultViewModel.Sucess(resultDTO.Message);
        }
    }
}
