using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectsManagement.CQRS.Projects.Commands;
using ProjectsManagement.CQRS.Projects.Queries;
using ProjectsManagement.Helpers;
using ProjectsManagement.Middleware;
using ProjectsManagement.Models;
using ProjectsManagement.ViewModels;
using ProjectsManagement.ViewModels.Projects;

namespace ProjectsManagement.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProjectController : BaseController
    {
        public ProjectController(ControllereParameters controllereParameters) : base(controllereParameters)
        {

        }

        [Authorize]
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

        [HttpGet]
        public async Task<ResultViewModel> GetProjects(int pageNumber, int pageSize)
        {
            var resultDTO = await _mediator.Send(new GetProjectsQuery(pageNumber, pageSize));
            
            if (!resultDTO.IsSuccess)
            {
                return ResultViewModel.Faliure(resultDTO.Message);
            }

            return ResultViewModel.Sucess(resultDTO.Data);
        }

        [HttpGet]
        [Authorize]
        public async Task<ResultViewModel> SearchProjectByTitle(string title)
        {
            var resultDTO = await _mediator.Send(new SearchProjectByTitleQuery(title));

            if (!resultDTO.IsSuccess)
            {
                return ResultViewModel.Faliure(resultDTO.Message);
            }

            return ResultViewModel.Sucess(resultDTO.Data);
        }
    }
}
