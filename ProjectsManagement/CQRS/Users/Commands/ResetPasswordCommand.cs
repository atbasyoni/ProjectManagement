using MediatR;
using ProjectsManagement.DTOs;
using ProjectsManagement.Helpers;
using ProjectsManagement.Models;
using ProjectsManagement.Repositories.Base;

namespace ProjectsManagement.CQRS.Users.Commands
{
    public record ResetPasswordCommand(ResetPasswordDTO ResetPasswordDTO) : IRequest<ResultDTO>;

    public record ResetPasswordDTO(string Email,string Token, string NewPassword);

    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, ResultDTO>
    {
        private readonly IRepository<PasswordChangeRequest> _passwordChangeRequestRepository;
        private readonly IRepository<User> _userRepository;

        public ResetPasswordCommandHandler(IRepository<PasswordChangeRequest> passwordChangeRequestRepository,
                                           IRepository<User> userRepository)
        {
            _passwordChangeRequestRepository = passwordChangeRequestRepository;
            _userRepository = userRepository;
        }

        public async Task<ResultDTO> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var passwordChangeRequest = await _passwordChangeRequestRepository
                      .FirstAsyncWithTracking(p => p.IsDeleted == false);

            if (passwordChangeRequest is null)
            {
                return ResultDTO.Faliure("Invalid reset token.");
            }

            bool isValidToken = HashHelper.CheckHash(request.ResetPasswordDTO.Token.Trim(), passwordChangeRequest.HashedToken);

            if (!isValidToken)
            {
                return ResultDTO.Faliure("Invalid reset token.");
            }

            if (passwordChangeRequest.Time.AddHours(24) < DateTime.Now)
            {
                return ResultDTO.Faliure("Expired reset token.");
            }
         
            var user = await _userRepository.FirstAsyncWithTracking(u => u.Email == request.ResetPasswordDTO.Email && u.ID==passwordChangeRequest.UserID);
            
            if (user is null)
            {
                return ResultDTO.Faliure("User or email not found.");
            }

            user.PasswordHash = HashHelper.CreateHash(request.ResetPasswordDTO.NewPassword);
            await _userRepository.SaveChangesAsync();

            await _passwordChangeRequestRepository.DeleteAsync(passwordChangeRequest);
            await _passwordChangeRequestRepository.SaveChangesAsync();

            return ResultDTO.Sucess(null, "Password has been reset successfully.");
        }
    }
}
