using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectManagementSystem.Helper;
using ProjectManagementSystem.Repository.Specification;
using ProjectsManagement.CQRS.Taskss.Queries;
using ProjectsManagement.CQRS.Users.Commands;
using ProjectsManagement.CQRS.Users.Orchestrators;
using ProjectsManagement.CQRS.Users.Queries;
using ProjectsManagement.DTOs;
using ProjectsManagement.Helpers;
using ProjectsManagement.Models;
using ProjectsManagement.ViewModels;
using ProjectsManagement.ViewModels.Auth;

namespace ProjectsManagement.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ResultViewModel> Register(RegisterRequestViewModel registerRequestViewModel)
        {
            var registerRequestDTO = registerRequestViewModel.MapOne<RegisterRequestDTO>();
            
            var resultDTO = await _mediator.Send(new RegisterUserOrchestrator(registerRequestDTO));

            if (!resultDTO.IsSuccess) 
            {
                return ResultViewModel.Faliure(resultDTO.Message);
            }

            return ResultViewModel.Sucess(resultDTO.Message);
        }

        [HttpPost]
        public async Task<ResultViewModel> Login(LoginRequestViewModel loginRequestViewModel)
        {
            var loginRequestDTO = loginRequestViewModel.MapOne<LoginRequestDTO>();

            var resultDTO = await _mediator.Send(new LoginUserCommand(loginRequestDTO));

            if (!resultDTO.IsSuccess)
            {
                return ResultViewModel.Faliure(resultDTO.Message);
            }

            return ResultViewModel.Sucess(resultDTO.Data, resultDTO.Message);
        }

        [HttpPost]
        public async Task<ResultViewModel> VerifyAccount(string email, string OTP)
        {
            var resultDTO = await _mediator.Send(new VerifyOTPCommand(email,OTP));

            if (!resultDTO.IsSuccess)
            {
                return ResultViewModel.Faliure(resultDTO.Message);
            }

            return ResultViewModel.Sucess(resultDTO.Data, resultDTO.Message);
        }

        [HttpPost]
        public async Task<ResultViewModel> ForgotPassword(ForgetPasswordViewModel forgetPasswordViewModel)
        {
            var forgetPasswordDTO = forgetPasswordViewModel.MapOne<ForgetPasswordDTO>();

            var result = await _mediator.Send(new ForgetPasswordCommand(forgetPasswordDTO));

            if (!result.IsSuccess)
            {
                return ResultViewModel.Faliure(result.Message);
            }

            return ResultViewModel.Sucess(result.Data, result.Message);
        }
        
        [HttpPost]
        public async Task<ResultViewModel> ResetPassword(ResetPasswordViewModel resetPasswordViewModel)
        {
            var resetPasswordDTO = resetPasswordViewModel.MapOne<ResetPasswordDTO>();

            var result = await _mediator.Send(new ResetPasswordCommand(resetPasswordDTO));
            
            if (!result.IsSuccess)
            {
                return ResultViewModel.Faliure(result.Message);
            }

            return ResultViewModel.Sucess(result.Data, result.Message);
        }

        [HttpPut]
        public async Task<ResultViewModel> ChangePassword(ChangePasswordViewModel changePasswordViewModel)
        {
            var changePasswordDTO = changePasswordViewModel.MapOne<ChangePasswordDTO>();

            var result = await _mediator.Send(new ChangePasswordCommand(changePasswordDTO));

            if (!result.IsSuccess)
            {
                return ResultViewModel.Faliure(result.Message);
            }

            return ResultViewModel.Sucess(result.Message);
        }
        [HttpGet]
        public async Task<ResultViewModel> GetUsers([FromQuery] SpecParams spec)
        {
            var resultDTO = await _mediator.Send(new GetUsersQuery(spec));

            if (!resultDTO.IsSuccess)
            {
                return ResultViewModel.Faliure(resultDTO.Message);
            }
            var UserCount = await _mediator.Send(new GetCountUsersQuery(spec));
            var paginationResult = new Pagination<UsersDTO>(spec.PageSize, spec.PageIndex, UserCount, resultDTO.Data);

            return ResultViewModel.Sucess(paginationResult);
        }
    }
}
