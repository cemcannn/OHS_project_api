using MediatR;

namespace OHS_program_api.Application.Features.Commands.AppUser.UploadProfilePhoto
{
    public class UploadProfilePhotoCommandRequest : IRequest<UploadProfilePhotoCommandResponse>
    {
        public string UserId { get; set; }
        public string PhotoBase64 { get; set; }
    }
}
