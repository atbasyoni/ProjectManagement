using MediatR;
using ProjectsManagement.DTOs;
using ProjectsManagement.Enums;
using ProjectsManagement.Models;
using ProjectsManagement.Repositories.Base;

namespace ProjectsManagement.CQRS.Users.Commands
{
    public record ChangeUserStatusCommand(int UserId, UserStatus Status) : IRequest<ResultDTO>;
    public class ChangeUserStatusCommandHandler : IRequestHandler<ChangeUserStatusCommand, ResultDTO>
    {
        private readonly IRepository<User> _userRepository;

        public ChangeUserStatusCommandHandler(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ResultDTO> Handle(ChangeUserStatusCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.FirstAsyncWithTracking(u => u.ID == request.UserId);

            if (user == null)
            {
                return ResultDTO.Faliure("User not found!");
            }

            if (user.UserStatus== request.Status)
            {
                return ResultDTO.Faliure($"User status is already {user.UserStatus}!");
            }
            user.UserStatus = request.Status;
            await _userRepository.UpdateAsync(user);
            await _userRepository.SaveChangesAsync();

            return ResultDTO.Sucess(user, "User status updated successfully!");
        }
    }
}
