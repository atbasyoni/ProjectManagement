﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectsManagement.DTOs;
using ProjectsManagement.Enums;
using ProjectsManagement.Models;
using ProjectsManagement.Repositories.Base;

namespace ProjectsManagement.CQRS.Taskss.Queries
{
    
    public record GetToDoTasksQuery(int projectID) : IRequest<ResultDTO>;

    public class GetToDoTasksQueryHandler : IRequestHandler<GetToDoTasksQuery, ResultDTO>
    {
        IRepository<Tasks> _taskRepository;

        public GetToDoTasksQueryHandler(IRepository<Tasks> taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<ResultDTO> Handle(GetToDoTasksQuery request, CancellationToken cancellationToken)
        {
            var tasks = await _taskRepository.GetAll()
                .Where(t => t.ProjectID == request.projectID && t.TaskStatus == TasksStatus.ToDo)
                .Select(task => new TaskDTO()
                {
                    Title = task.Title,
                    TaskStatus = (TaskStatus)task.TaskStatus,
                    UserNames = task.AssignedUsers.Select(au => au.User.UserName).ToList(),
                    ProjectName = task.Project.Title,
                    CreatedDate = task.CreatedDate
                }).ToListAsync();

            if (tasks is null)
            {
                return ResultDTO.Faliure("Failed to retrieve tasks!");
            }

            return ResultDTO.Sucess(tasks);
        }
    }
}
