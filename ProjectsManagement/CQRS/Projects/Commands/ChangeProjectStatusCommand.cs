using MediatR;
using ProjectsManagement.DTOs;
using ProjectsManagement.Enums;
using ProjectsManagement.Models;
using ProjectsManagement.Repositories.Base;

namespace ProjectsManagement.CQRS.Projects.Commands
{
    public record ChangeProjectStatusCommand(ProjectStatusDTO projectStatusDTO) : IRequest<ResultDTO>;

    public record ProjectStatusDTO(int ProjectID, ProjectStatus ProjectStatus);
    public class ChangeProjectStatusCommandHandler : IRequestHandler<ChangeProjectStatusCommand, ResultDTO>
    {
        private readonly IRepository<Project> _projectRepository;
        private readonly IMediator _mediator;

        public ChangeProjectStatusCommandHandler(IRepository<Project> projectRepository, IMediator mediator)
        {
            _projectRepository = projectRepository;
            _mediator = mediator;
        }

        public async Task<ResultDTO> Handle(ChangeProjectStatusCommand request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.FirstAsync(p => p.ID == request.projectStatusDTO.ProjectID
            && p.ProjectStatus != request.projectStatusDTO.ProjectStatus);

            if (project is null)
            {
                return ResultDTO.Faliure($"Project status is already {request.projectStatusDTO.ProjectStatus}!");
            }

            project.ProjectStatus = request.projectStatusDTO.ProjectStatus;

            await _projectRepository.UpdateAsync(project);
            await _projectRepository.SaveChangesAsync();

            return ResultDTO.Sucess(project, "Project status updated successfully!");
        }
    }
}
