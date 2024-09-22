using MediatR;
using ProjectsManagement.DTOs;
using ProjectsManagement.Models;
using ProjectsManagement.Repositories.Base;

namespace ProjectsManagement.CQRS.Projects.Commands
{
    public record DeleteProjectCommand(int ProjectID) : IRequest<ResultDTO>;
    public class DeleteProjectCommandHandler : IRequestHandler<DeleteProjectCommand, ResultDTO>
    {
        private readonly IRepository<Project> _projectRepository;
        private readonly UserState _userState;

        public DeleteProjectCommandHandler(IRepository<Project> projectRepository, UserState userState)
        {
            _projectRepository = projectRepository;
            _userState = userState;
        }

        public async Task<ResultDTO> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.FirstAsyncWithTracking(p => p.ID == request.ProjectID);
            if (project == null)
            {
                return ResultDTO.Faliure("Project not found!");
            }

            if (project.OwnerID != int.Parse(_userState.ID))
            {
                return ResultDTO.Faliure("You are not authorized to delete this project!");
            }

            await _projectRepository.DeleteAsync(project);
            await _projectRepository.SaveChangesAsync();

            return ResultDTO.Sucess(null, "Project deleted successfully!");
        }
    }
}
