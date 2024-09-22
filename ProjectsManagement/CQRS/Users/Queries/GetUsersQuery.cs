using MediatR;
using ProjectManagementSystem.Repository.Specification;
using ProjectsManagement.DTOs;
using ProjectsManagement.Enums;
using ProjectsManagement.Models;
using ProjectsManagement.Repositories.Base;
using ProjectsManagement.Specification.UserSpecifications;

namespace ProjectsManagement.CQRS.Users.Queries
{
    public record GetUsersQuery(SpecParams specParams):IRequest<ResultDTO>;
    public record UsersDTO(string UserName, UserStatus UserStatus, string PhoneNumber, string Email, DateTime CreatedDate);

    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, ResultDTO>
    {
        private readonly IRepository<User> _userRepo;

        public GetUsersQueryHandler(IRepository<User> userRepo)
        {
            _userRepo = userRepo;
        }
        public async Task<ResultDTO> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var spec = new UserWithSpecifications(request.specParams);

            var users = await _userRepo.GetAllWithSpecAsync(spec);

            var useresDTOs = users.Select(user => new UsersDTO(user.UserName, user.UserStatus, user.PhoneNumber,user.Email, user.CreatedDate)).ToList();

            return ResultDTO.Sucess(useresDTOs);

        }
    }

}
