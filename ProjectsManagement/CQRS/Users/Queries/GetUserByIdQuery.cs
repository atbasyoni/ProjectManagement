using MediatR;
using ProjectsManagement.DTOs;
using ProjectsManagement.Models;
using ProjectsManagement.Repositories.Base;

namespace ProjectsManagement.CQRS.Users.Queries
{
    public record GetUserByIdQuery(int userID) : IRequest<ResultDTO>;

    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, ResultDTO>
    {
        IRepository<User> _userRepository;

        public GetUserByIdQueryHandler(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ResultDTO> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIDAsync(request.userID);

            if (user is null) 
            {
                return ResultDTO.Faliure("User isn't found!");
            }

            return ResultDTO.Sucess(user);
        }
    }
}
