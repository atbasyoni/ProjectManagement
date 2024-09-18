using MediatR;
using ProjectsManagement.DTOs;
using ProjectsManagement.Helpers;
using ProjectsManagement.Models;
using ProjectsManagement.Repositories.Base;

namespace ProjectsManagement.CQRS.Pictures.Commands
{
    public record AddUserPictureCommand(PictureDTO pictureDTO) : IRequest<ResultDTO>;

    public record PictureDTO(string Title, string URL);

    public class AddUserPictureCommandHandler : IRequestHandler<AddUserPictureCommand, ResultDTO>
    {
        IRepository<Picture> _pictureRepository;
        IMediator _mediator;

        public AddUserPictureCommandHandler(IRepository<Picture> pictureRepository, IMediator mediator)
        {
            _pictureRepository = pictureRepository;
            _mediator = mediator;
        }

        public async Task<ResultDTO> Handle(AddUserPictureCommand request, CancellationToken cancellationToken)
        {
            var picture = request.pictureDTO.MapOne<Picture>();
            
            picture = await _pictureRepository.AddAsync(picture);
            await _pictureRepository.SaveChangesAsync();

            return ResultDTO.Sucess(picture, "User Picture added successfully!");
        }
    }
}
