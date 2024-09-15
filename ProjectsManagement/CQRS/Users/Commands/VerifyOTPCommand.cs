using MediatR;
using ProjectsManagement.DTOs;
using ProjectsManagement.Models;
using ProjectsManagement.Repositories.Base;

namespace ProjectsManagement.CQRS.Users.Commands
{
    public record VerifyOTPCommand(string Email , string OTP) : IRequest<ResultDTO>;

    public class VerifyOTPCommandHandler : IRequestHandler<VerifyOTPCommand, ResultDTO>
    {
        IRepository<User> _userRepository;
        public VerifyOTPCommandHandler(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<ResultDTO> Handle(VerifyOTPCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.FirstAsyncWithTracking(u => u.Email == request.Email);

            if (user == null || (user.OTPExpiration != null && user.OTPExpiration < DateTime.Now) || (user.OTP != null && user.OTP != request.OTP))
            {
                return ResultDTO.Faliure("Invalid user or Expired Code!");
            }

            user.OTP = null;
            user.OTPExpiration = null;
            user.IsVerified = true;
            await _userRepository.SaveChangesAsync();
            return ResultDTO.Sucess(null, "Your account has been varefied successfully!");
        }
    }
}
