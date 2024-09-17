using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectsManagement.CQRS.Projects.Commands;
using ProjectsManagement.Helpers;
using ProjectsManagement.ViewModels;
using ProjectsManagement.ViewModels.Projects;

namespace ProjectsManagement.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProjectController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ResultViewModel> AddProject(ProjectCreateViewModel projectCreateViewModel)
        {
            var projectCreateDTO = projectCreateViewModel.MapOne<ProjectCreateDTO>();

            var resultDTO = await _mediator.Send(new AddProjectCommand(projectCreateDTO));

            if (!resultDTO.IsSuccess)
            {
                return ResultViewModel.Faliure(resultDTO.Message);
            }

            return ResultViewModel.Sucess(resultDTO.Message);
        }
    }
}
