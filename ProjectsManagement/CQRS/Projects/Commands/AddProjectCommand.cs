using MediatR;
using ProjectsManagement.DTOs;
using ProjectsManagement.Helpers;
using ProjectsManagement.Models;
using ProjectsManagement.Repositories.Base;

namespace ProjectsManagement.CQRS.Projects.Commands
{
    public record AddProjectCommand(ProjectCreateDTO projectCreateDTO) : IRequest<ResultDTO>;

    public record ProjectCreateDTO(string Title, string Description);

    public class AddProjectCommandHandler : IRequestHandler<AddProjectCommand, ResultDTO>
    {
        IRepository<Project> _projectRepository;
        IMediator _mediator;
        UserState _userState;

        public AddProjectCommandHandler(IRepository<Project> projectRepository, IMediator mediator, UserState userState)
        {
            _projectRepository = projectRepository;
            _mediator = mediator;
            _userState = userState;
        }

        public async Task<ResultDTO> Handle(AddProjectCommand request, CancellationToken cancellationToken)
        {
            var project = request.projectCreateDTO.MapOne<Project>();

            project.OwnerID = int.Parse(_userState.ID);

            project = await _projectRepository.AddAsync(project);
            await _projectRepository.SaveChangesAsync();

            return ResultDTO.Sucess(project, "Project added successfully!");
        }
    }
}

