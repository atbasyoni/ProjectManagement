using MediatR;
using ProjectsManagement.DTOs;
using ProjectsManagement.Models;
using ProjectsManagement.Repositories.Base;

namespace ProjectsManagement.CQRS.Taskss.Commands
{
    public record DeleteTaskCommand(int TaskID) : IRequest<ResultDTO>;
    public class DeleteTaskCommandHandler : IRequestHandler<DeleteTaskCommand, ResultDTO>
    {
        private readonly IRepository<Tasks> _taskRepository;

        public DeleteTaskCommandHandler(IRepository<Tasks> taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<ResultDTO> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
        {
            var task = await _taskRepository.FirstAsync(t => t.ID == request.TaskID);

            if (task == null)
            {
                return ResultDTO.Faliure("Task not found!");
            }
            await _taskRepository.DeleteAsync(task);
            await _taskRepository.SaveChangesAsync();

            return ResultDTO.Sucess("Task deleted successfully!");
        }
    }
}
