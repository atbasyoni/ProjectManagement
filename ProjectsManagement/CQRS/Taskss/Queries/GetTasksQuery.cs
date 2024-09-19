using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectsManagement.DTOs;
using ProjectsManagement.Models;
using ProjectsManagement.Repositories.Base;

namespace ProjectsManagement.CQRS.Taskss.Queries
{
    public record GetTasksQuery(int pageNumber, int pageSize) : IRequest<ResultDTO>;

    public class GetTasksQueryHandler : IRequestHandler<GetTasksQuery, ResultDTO>
    {
        IRepository<Tasks> _taskRepository;

        public GetTasksQueryHandler(IRepository<Tasks> taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<ResultDTO> Handle(GetTasksQuery request, CancellationToken cancellationToken)
        {
            var tasks = await _taskRepository.GetAllPagination(request.pageNumber, request.pageSize).ToListAsync();
            
            if (tasks is null)
            {
                return ResultDTO.Faliure("Failed to retrieve tasks!");
            }

            return ResultDTO.Sucess(tasks);
        }
    }
}
