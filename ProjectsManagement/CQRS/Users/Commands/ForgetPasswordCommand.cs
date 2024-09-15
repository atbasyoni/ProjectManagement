using MediatR;
using ProjectsManagement.DTOs;
using ProjectsManagement.Helpers;
using ProjectsManagement.Models;
using ProjectsManagement.Repositories.Base;
using ProjectsManagement.Specification.UserSpecifications;

namespace ProjectsManagement.CQRS.Users.Commands
{
    public record ForgetPasswordCommand(ForgetPasswordDTO forgetPasswordDTO):IRequest<ResultDTO>;
    public record ForgetPasswordDTO(string Email);
    public class ForgetPasswordCommandHandler : IRequestHandler<ForgetPasswordCommand, ResultDTO>
    {
        private readonly IRepository<User> _userRepository;
        private readonly EmailSenderHelper _emailSender;
        private readonly IRepository<PasswordChangeRequest> _passwordChangeRequestRepository;

        public ForgetPasswordCommandHandler(IRepository<User> userRepository, EmailSenderHelper emailSender,
            IRepository<PasswordChangeRequest> passwordChangeRequestRepository)
        {
            _userRepository = userRepository;
            _emailSender = emailSender;
            _passwordChangeRequestRepository = passwordChangeRequestRepository;
        }
        public async Task<ResultDTO> Handle(ForgetPasswordCommand request, CancellationToken cancellationToken)
        {
            var spec = new UserSpecification(request.forgetPasswordDTO.Email);
            var user = await _userRepository.FirstAsync(spec.Criteria);
            if (user == null)
                return ResultDTO.Faliure("This email can not be found");

            string resetToken = Guid.NewGuid().ToString();
            string hashedToken = BCrypt.Net.BCrypt.HashPassword(resetToken);

            var passwordChangeRequest = new PasswordChangeRequest
            {
                UserID = user.ID,
                HashedToken = hashedToken,
                Time = DateTime.Now
            };

            await _passwordChangeRequestRepository.AddAsync(passwordChangeRequest);
            await _passwordChangeRequestRepository.SaveChangesAsync();

             
            await _emailSender.SendEmailAsync(user.Email, "Password Reset",
                           $"Please reset your password[ {resetToken} ]. This Token will expire in 24 hours.");

            return ResultDTO.Sucess(null, "Password reset link has been sent to your email.");

        }
    }


}
