using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectsManagement.Models;
using System.Security.Claims;

namespace ProjectsManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected readonly IMediator _mediator;
        protected readonly UserState _userState;
        public BaseController(ControllereParameters controllereParameters)
        {
            _mediator = controllereParameters.Mediator;
            _userState = controllereParameters.UserState;

            var loggedUser = new HttpContextAccessor().HttpContext.User;

            _userState.Role = loggedUser?.FindFirst("RoleID")?.Value ?? "";
            _userState.ID = loggedUser?.FindFirst("ID")?.Value ?? "";
            _userState.Name = loggedUser?.FindFirst(ClaimTypes.Name)?.Value ?? "";
        }
    }
}
