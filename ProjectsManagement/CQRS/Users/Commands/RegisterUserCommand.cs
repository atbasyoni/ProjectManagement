using MediatR;
using ProjectsManagement.DTOs;
using ProjectsManagement.Helpers;
using ProjectsManagement.Models;
using ProjectsManagement.Repositories.Base;

namespace ProjectsManagement.CQRS.Users.Commands
{
    public record RegisterUserCommand(RegisterRequestDTO registerRequestDTO) : IRequest<ResultDTO>;
 
    public record RegisterRequestDTO(string FirstName, 
        string LastName, 
        string UserName,
        string Email,
        string PhoneNumber,
        string Password,
        string ConfirmPassword,
        string Country);

    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, ResultDTO>
    {
        IRepository<User> _userRepository;
        IMediator _mediator;
        EmailSenderHelper _emailSenderHelper;

        public RegisterUserCommandHandler(IRepository<User> userRepository, IMediator mediator, EmailSenderHelper emailSender)
        {
            _userRepository = userRepository;
            _mediator = mediator;
            _emailSenderHelper = emailSender;
            
        }

        public async Task<ResultDTO> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var result = await _userRepository.FirstAsync(u => u.Email == request.registerRequestDTO.Email);

            if (result is not null)
            {
                return ResultDTO.Faliure("Email is already registered!");
            }

            result = await _userRepository.FirstAsync(user => user.UserName == request.registerRequestDTO.UserName);

            if (result is not null) 
            {
                return ResultDTO.Faliure("Username is alerady registered!");
            }

            // Generate OTP
            var otp = GenerateOTP();

            // Send OTP
            await SendOTPAsync(request.registerRequestDTO.Email, otp);

            var user = request.registerRequestDTO.MapOne<User>();
            user.PasswordHash = CreatePasswordHash(request.registerRequestDTO.Password);
            user.OTP = otp;
            user.OTPExpiration = DateTime.Now.AddMinutes(5);

            user = await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync(); 

            return ResultDTO.Sucess(user, "User registred successfully!");
        }

        private string CreatePasswordHash(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        private string GenerateOTP()
        {
            Random random = new Random();
            return random.Next(100000, 999999).ToString();
        }

        private async Task SendOTPAsync(string email, string otp)
        {
            string subject = "Verify your Account";
            string body = $"Your Verification code is {otp}. It will expire in 5 minutes.";
            await _emailSenderHelper.SendEmailAsync(email, subject, body);

        }
    }
}
