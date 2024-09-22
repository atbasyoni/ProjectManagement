using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectManagementSystem.Helper;
using ProjectManagementSystem.Repository.Specification;
using ProjectsManagement.CQRS.Projects.Commands;
using ProjectsManagement.CQRS.Projects.Queries;
using ProjectsManagement.CQRS.ProjectUsers.Commands;
using ProjectsManagement.DTOs;
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
        public async Task<ResultViewModel> GetProjects([FromQuery] SpecParams spec)
        {
            var resultDTO = await _mediator.Send(new GetProjectsQuery(spec));
            
            if (!resultDTO.IsSuccess)
            {
                return ResultViewModel.Faliure(resultDTO.Message);
            }
            var projectCount = await _mediator.Send(new GetProjectsCountQuery(spec));
            var paginationResult = new Pagination<ProjectDTO>(spec.PageSize, spec.PageIndex, projectCount, resultDTO.Data);

            return ResultViewModel.Sucess(paginationResult);
        }

        [HttpPost]
        public async Task<ResultViewModel> AssignUserToProject(AssignUserProjectViewModel  assignUserViewModel)
        {
            var  projectUserDTO =  assignUserViewModel.MapOne <ProjectUserDTO>();

            var resultDTO = await _mediator.Send(new AssignUserToProjectCommand(projectUserDTO));

            if (!resultDTO.IsSuccess)
            {
                return ResultViewModel.Faliure(resultDTO.Message);
            }

            return ResultViewModel.Sucess(resultDTO.Message);
        }
        [HttpDelete]
        public async Task<ResultViewModel> UnassignUserFromProject(AssignUserProjectViewModel assignUserViewModel)
        {
            var projectUserDTO = assignUserViewModel.MapOne<ProjectUserDTO>();

            var result = await _mediator.Send(new UnassignUserFromProjectCommand(projectUserDTO));
            if (!result.IsSuccess)
            {
                return ResultViewModel.Faliure(result.Message);
            }
            return ResultViewModel.Sucess(result.Message);
        }
        [HttpPut("EditProject/{projectId}")]
        public async Task<ResultViewModel> EditProjectDetails(int ProjectID, ProjectCreateDTO projectCreateDTO)
        {
            var result = await _mediator.Send(new EditProjectCommand(ProjectID, projectCreateDTO));
            if (!result.IsSuccess)
            {
                return ResultViewModel.Faliure(result.Message);
            }
            return ResultViewModel.Sucess(result.Message);
        }

        [HttpDelete("DeleteProject/{id}")]
        public async Task<ResultViewModel> DeleteProject(int id)
        {
            var result = await _mediator.Send(new DeleteProjectCommand(id));
            if (!result.IsSuccess)
            {
                return ResultViewModel.Faliure(result.Message);
            }
            return ResultViewModel.Sucess(result.Message);
        }
        [HttpPut]
        public async Task<ResultViewModel> ChangeProjectStatus(ProjectStatusViewModel projectStatusViewModel)
        {
            var projectStatusDTO = projectStatusViewModel.MapOne<ProjectStatusDTO>();

            var resultDTO = await _mediator.Send(new ChangeProjectStatusCommand(projectStatusDTO));

            if (!resultDTO.IsSuccess)
            {
                return ResultViewModel.Faliure(resultDTO.Message);
            }

            return ResultViewModel.Sucess(resultDTO.Message);
        }

    }
}
