using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProjectManagementSystem.Helper;
using ProjectManagementSystem.Repository.Specification;
using ProjectsManagement.CQRS.Taskss.Commands;
using ProjectsManagement.CQRS.Taskss.Orchestrators;
using ProjectsManagement.CQRS.Taskss.Queries;
using ProjectsManagement.CQRS.TaskUsers.Commands;
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

        [HttpGet]
        public async Task<ResultViewModel> GetTasks([FromQuery] SpecParams spec)
        {
            var resultDTO = await _mediator.Send(new GetTasksQuery(spec));

            if (!resultDTO.IsSuccess)
            {
                return ResultViewModel.Faliure(resultDTO.Message);
            }
            var TaskCount = await _mediator.Send(new GetCountTaskQuery(spec));
            var paginationResult = new Pagination<TaskDTO>(spec.PageSize, spec.PageIndex, TaskCount, resultDTO.Data);

            return ResultViewModel.Sucess(paginationResult);
        }

        [HttpPost]
        public async Task<ResultViewModel> AssignUserToTask(TaskUserViewModel taskUserViewModel)
        {
            var taskUserDTO = taskUserViewModel.MapOne<TaskUserDTO>();

            var resultDTO = await _mediator.Send(new AssignUserToTaskCommand(taskUserDTO));

            if (!resultDTO.IsSuccess)
            {
                return ResultViewModel.Faliure(resultDTO.Message);
            }

            return ResultViewModel.Sucess(resultDTO.Message);
        }

        [HttpDelete]
        public async Task<ResultViewModel> UnassignUserFromTask(TaskUserViewModel taskUserViewModel)
        {
            var taskUserDTO = taskUserViewModel.MapOne<TaskUserDTO>();
            var result = await _mediator.Send(new UnassignUserFromTaskCommand(taskUserDTO));
            if (!result.IsSuccess)
            {
                return ResultViewModel.Faliure(result.Message);
            }
            return ResultViewModel.Sucess(result.Message);
        }

        [HttpPut()]
        public async Task<ResultViewModel> UpdateTask(TaskUpdateViewModel taskUpdateViewModel)
        {
            var taskUpdateDTO = taskUpdateViewModel.MapOne<UpdateTaskDTO>();
            var result = await _mediator.Send(new UpdateTaskCommand(taskUpdateDTO));
            if (!result.IsSuccess)
            {
                return ResultViewModel.Faliure(result.Message);
            }
            return ResultViewModel.Sucess(result.Message);
        }

        [HttpDelete("{taskId}")]
        public async Task<ResultViewModel> DeleteTask(int taskId)
        {
            var result = await _mediator.Send(new DeleteTaskCommand(taskId));
            if (!result.IsSuccess)
            {
                return ResultViewModel.Faliure(result.Message);
            }
            return ResultViewModel.Sucess(result.Message);
        }

        [HttpGet]
        public async Task<ResultViewModel> GetTasksByStatusV1(int projectID)
        {
            var result = await _mediator.Send(new GetTasksByStatusOrchestrator(projectID));
            if (!result.IsSuccess)
            {
                return ResultViewModel.Faliure(result.Message);
            }
            return ResultViewModel.Sucess(result.Message);
        }
        
        [HttpGet]
        public async Task<ResultViewModel> GetTasksByStatusV2(int projectID)
        {
            var result = await _mediator.Send(new GetAllTasksByStatusQuery(projectID));
            if (!result.IsSuccess)
            {
                return ResultViewModel.Faliure(result.Message);
            }
            return ResultViewModel.Sucess(result.Message);
        }
    }

}
