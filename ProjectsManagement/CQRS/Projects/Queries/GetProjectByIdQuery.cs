using MediatR;
using ProjectsManagement.DTOs;
using ProjectsManagement.Models;
using ProjectsManagement.Repositories.Base;

namespace ProjectsManagement.CQRS.Projects.Queries
{
    public record GetProjectByIdQuery(int projectID) : IRequest<ResultDTO>;

    public class GetProjectByIdQueryHandler : IRequestHandler<GetProjectByIdQuery, ResultDTO>
    {
        IRepository<Project> _projectRepository;

        public GetProjectByIdQueryHandler(IRepository<Project> projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<ResultDTO> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.GetByIDAsync(request.projectID);
            
            if (project is null)
            {
                return ResultDTO.Faliure("The project isn't found!");
            }

            return ResultDTO.Sucess(project);
        }
    }
}
