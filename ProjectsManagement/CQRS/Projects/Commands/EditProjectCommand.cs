using MediatR;
using ProjectsManagement.DTOs;
using ProjectsManagement.Models;
using ProjectsManagement.Repositories.Base;

namespace ProjectsManagement.CQRS.Projects.Commands
{
    public record EditProjectCommand(int ProjectID, ProjectCreateDTO projectCreateDTO) : IRequest<ResultDTO>;

    public class EditProjectCommandHandler : IRequestHandler<EditProjectCommand, ResultDTO>
    {
        private readonly IRepository<Project> _projectRepository;

        public EditProjectCommandHandler(IRepository<Project> projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<ResultDTO> Handle(EditProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.GetByIDAsync(request.ProjectID);

            if (project is null)
            {
                return ResultDTO.Faliure("Project not found!");
            }

            project.Title = request.projectCreateDTO.Title;
            project.Description = request.projectCreateDTO.Description;

            await _projectRepository.UpdateAsync(project);
            await _projectRepository.SaveChangesAsync();

            return ResultDTO.Sucess(project, "Project updated successfully!");
        }
    }
}
