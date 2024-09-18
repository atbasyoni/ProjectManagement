using MediatR;
using ProjectsManagement.DTOs;
using ProjectsManagement.Models;
using ProjectsManagement.Repositories.Base;

namespace ProjectsManagement.CQRS.Projects.Queries
{
    public record SearchProjectByTitleQuery(string title) : IRequest<ResultDTO>;

    public class SearchProjectByTitleQueryHandler : IRequestHandler<SearchProjectByTitleQuery, ResultDTO>
    {
        private readonly IRepository<Project> _projectRepository;

        public SearchProjectByTitleQueryHandler(IRepository<Project> projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public Task<ResultDTO> Handle(SearchProjectByTitleQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
