using MediatR;
using ProjectsManagement.DTOs;
using ProjectsManagement.Helpers;
using ProjectsManagement.Models;
using ProjectsManagement.Repositories.Base;
using ProjectsManagement.Specification.UserSpecifications;

namespace ProjectsManagement.CQRS.Users.Commands
{
    public record ChangePasswordCommand(ChangePasswordDTO forgetPasswordDTO) : IRequest<ResultDTO>;

    public record ChangePasswordDTO(string Email, string CurrentPassword, string NewPassword);

    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, ResultDTO>
    {
        private readonly IRepository<User> _userRepository;

        public ChangePasswordCommandHandler(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ResultDTO> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var spec = new UserSpecification(request.forgetPasswordDTO.Email);

            var user = await _userRepository.FirstAsync(spec.Criteria);

            if (user is null)
            {
                return ResultDTO.Faliure("Email is wrong!");
            }

            if (!HashHelper.CheckPasswordHash(request.forgetPasswordDTO.CurrentPassword, user.PasswordHash))
            {
                return ResultDTO.Faliure("current password is wrong");
            }

            user.PasswordHash = HashHelper.CreatePasswordHash(request.forgetPasswordDTO.NewPassword);

            await _userRepository.UpdateAsync(user);
            await _userRepository.SaveChangesAsync();

            return ResultDTO.Sucess(user, "Your Password changed successfully");
        }
    }
}
