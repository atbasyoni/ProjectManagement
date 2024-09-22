using MediatR;
using ProjectsManagement.DTOs;
using ProjectsManagement.Models;
using ProjectsManagement.Repositories.Base;

namespace ProjectsManagement.CQRS.BlockedUsers
{
    public record BlockUserCommand(int BlockedID) : IRequest<ResultDTO>;

    public class BlockUserCommandHandler : IRequestHandler<BlockUserCommand, ResultDTO>
    {
        private readonly IRepository<BlockedUser> _blockedUserRepository;
        private readonly UserState _userState;

        public BlockUserCommandHandler(IRepository<BlockedUser> blockedUserRepository, UserState userState)
        {
            _blockedUserRepository = blockedUserRepository;
            _userState = userState;
        }

        public async Task<ResultDTO> Handle(BlockUserCommand request, CancellationToken cancellationToken)
        {
            var blockerID = int.Parse(_userState.ID);

            var existingBlock = await _blockedUserRepository.FirstAsync(b =>
                b.BlockerID == blockerID && b.BlockedID == request.BlockedID);

            if (existingBlock != null)
            {
                return ResultDTO.Faliure("User is already blocked.");
            }

            var block = new BlockedUser
            {
                BlockerID = blockerID,
                BlockedID = request.BlockedID,
                CreatedDate = DateTime.Now
            };

            await _blockedUserRepository.AddAsync(block);
            await _blockedUserRepository.SaveChangesAsync();

            return ResultDTO.Sucess("User blocked successfully.");
        }
    }
}
