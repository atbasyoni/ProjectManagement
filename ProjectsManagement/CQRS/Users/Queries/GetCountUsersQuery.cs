using MediatR;
using ProjectManagementSystem.Repository.Specification;
using ProjectsManagement.Models;
using ProjectsManagement.Repositories.Base;
using ProjectsManagement.Specification.UserSpecifications;

namespace ProjectsManagement.CQRS.Users.Queries
{
    public record GetCountUsersQuery(SpecParams specParams):IRequest<int>;

    public class GetCountUsersQueryHandler : IRequestHandler<GetCountUsersQuery, int>
    {
        private readonly IRepository<User> _userRepo;

        public GetCountUsersQueryHandler(IRepository<User> userRepo)
        {
            _userRepo = userRepo;
        }
        public async Task<int> Handle(GetCountUsersQuery request, CancellationToken cancellationToken)
        {
            var userSpec = new CountUserWithSpec(request.specParams);
            var count = await _userRepo.GetCountWithSpecAsync(userSpec);

            return count;
        }
    }
}
