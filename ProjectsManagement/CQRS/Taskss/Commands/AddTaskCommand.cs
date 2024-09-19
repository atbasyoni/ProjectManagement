using MediatR;
using ProjectsManagement.CQRS.Projects.Queries;
using ProjectsManagement.CQRS.ProjectUsers.Queries;
using ProjectsManagement.CQRS.Taskss.Queries;
using ProjectsManagement.CQRS.Users.Queries;
using ProjectsManagement.DTOs;
using ProjectsManagement.Helpers;
using ProjectsManagement.Models;
using ProjectsManagement.Repositories.Base;

namespace ProjectsManagement.CQRS.Taskss.Commands
{
    public record AddTaskCommand(TaskCreateDTO TaskCreateDTO) : IRequest<ResultDTO>;

    public record TaskCreateDTO(string Title, 
        string Description, 
        DateTime StartDate, 
        DateTime DueDate, 
        int ProjectID, 
        List<int> UserIDs);

    public class AddTaskCommandHandler : IRequestHandler<AddTaskCommand, ResultDTO>
    {
        IRepository<Tasks> _taskRepository;
        IMediator _mediator;

        public AddTaskCommandHandler(IRepository<Tasks> taskRepository, IMediator mediator)
        {
            _taskRepository = taskRepository;
            _mediator = mediator;
        }

        public async Task<ResultDTO> Handle(AddTaskCommand request, CancellationToken cancellationToken)
        {
            var valdiationResult = await ValidateTask(request.TaskCreateDTO);

            if (!valdiationResult.IsSuccess)
            {
                return valdiationResult;
            }

            var task = request.TaskCreateDTO.MapOne<Tasks>();

            task = await _taskRepository.AddAsync(task);
            await _taskRepository.SaveChangesAsync();

            return ResultDTO.Sucess(task, "Task added successfully!");
        }

        private async Task<ResultDTO> ValidateTask(TaskCreateDTO taskCreateDTO)
        {
            var projectExists = await _mediator.Send(new GetProjectByIdQuery(taskCreateDTO.ProjectID));
            
            if (!projectExists.IsSuccess)
            {
                return projectExists;
            }

            foreach (var userID in taskCreateDTO.UserIDs)
            {
                var usersExistsInProject = await _mediator.Send(new GetProjectUserByIdQuery(taskCreateDTO.ProjectID, userID));

                if (!usersExistsInProject.IsSuccess) 
                {
                    return usersExistsInProject;
                }
            }

            return ResultDTO.Sucess(true);
        }
    }
}
