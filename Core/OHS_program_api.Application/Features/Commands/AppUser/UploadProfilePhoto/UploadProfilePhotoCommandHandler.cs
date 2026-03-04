using MediatR;
using OHS_program_api.Application.Abstractions.Services;

namespace OHS_program_api.Application.Features.Commands.AppUser.UploadProfilePhoto
{
    public class UploadProfilePhotoCommandHandler : IRequestHandler<UploadProfilePhotoCommandRequest, UploadProfilePhotoCommandResponse>
    {
        readonly IUserService _userService;

        public UploadProfilePhotoCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<UploadProfilePhotoCommandResponse> Handle(UploadProfilePhotoCommandRequest request, CancellationToken cancellationToken)
        {
            await _userService.UploadProfilePhotoAsync(request.UserId, request.PhotoBase64);
            return new() { Succeeded = true, Message = "Profil fotoğrafı başarıyla güncellendi." };
        }
    }
}
