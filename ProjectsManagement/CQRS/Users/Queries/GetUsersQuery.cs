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
    public class UsersDTO
    {
        public string UserName { get; set; }
        public UserStatus userStatus { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateTime CreatedDate { get; set; }
    }
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
            var useresDTOs = users.Select(user => new UsersDTO
            {
                UserName = user.UserName,
                userStatus = user.UserStatus,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email
            }).ToList();
            return ResultDTO.Sucess(useresDTOs);

        }
    }

}
