using MediatR;
using ProjectsManagement.DTOs;
using ProjectsManagement.Models;
using ProjectsManagement.Repositories.Base;

namespace ProjectsManagement.CQRS.BlockedUsers.Commands
{
    public record UnblockUserCommand(int BlockedID) : IRequest<ResultDTO>;

    public class UnblockUserCommandHandler : IRequestHandler<UnblockUserCommand, ResultDTO>
    {
        private readonly IRepository<BlockedUser> _blockedUserRepository;
        private readonly UserState _userState;

        public UnblockUserCommandHandler(IRepository<BlockedUser> blockedUserRepository, UserState userState)
        {
            _blockedUserRepository = blockedUserRepository;
            _userState = userState;
        }

        public async Task<ResultDTO> Handle(UnblockUserCommand request, CancellationToken cancellationToken)
        {
            var blockerID = int.Parse(_userState.ID);

            var block = await _blockedUserRepository.FirstAsyncWithTracking(b =>
                b.BlockerID == blockerID && b.BlockedID == request.BlockedID);

            if (block is null)
            {
                return ResultDTO.Faliure("No block found for this user.");
            }

            await _blockedUserRepository.DeleteAsync(block);
            await _blockedUserRepository.SaveChangesAsync();

            return ResultDTO.Sucess("User unblocked successfully.");
        }
    }
}
