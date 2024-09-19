using MediatR;
using ProjectsManagement.DTOs;
using ProjectsManagement.Models;
using ProjectsManagement.Repositories.Base;

namespace ProjectsManagement.CQRS.Taskss.Queries
{
    public record GetTaskByIdQuery(int taskID) : IRequest<ResultDTO>;

    public class GetTaskByIdQueryHandler : IRequestHandler<GetTaskByIdQuery, ResultDTO>
    {
        IRepository<Tasks> _taskRepository;

        public GetTaskByIdQueryHandler(IRepository<Tasks> taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<ResultDTO> Handle(GetTaskByIdQuery request, CancellationToken cancellationToken)
        {
            var task = await _taskRepository.GetByIDAsync(request.taskID);

            if (task is null)
            {
                return ResultDTO.Faliure("Task isn't found!");
            }

            return ResultDTO.Sucess(task);
        }
    }
}
