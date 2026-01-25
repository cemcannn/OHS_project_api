using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OHS_program_api.Application.Abstractions.Services;
using OHS_program_api.Application.DTOs.User;
using OHS_program_api.Application.Exceptions;
using OHS_program_api.Application.Helpers;
using OHS_program_api.Application.Repositories;
using OHS_program_api.Domain.Entities.Identity;

namespace OHS_program_api.Persistence.Services
{
    public class UserService : IUserService
    {
        private const string SuperAdminRoleName = "SuperAdmin";

        readonly UserManager<Domain.Entities.Identity.AppUser> _userManager;
        readonly IEndpointReadRepository _endpointReadRepository;

        public UserService(UserManager<AppUser> userManager,
            IEndpointReadRepository endpointReadRepository)
        {
            _userManager = userManager;
            _endpointReadRepository = endpointReadRepository;
        }

        public async Task<CreateUserResponse> CreateAsync(CreateUser model)
        {
            IdentityResult result = await _userManager.CreateAsync(new()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = model.UserName,
                Email = model.Email,
                Name = model.Name,
            }, model.Password);

            CreateUserResponse response = new() { Succeeded = result.Succeeded };

            if (result.Succeeded)
                response.Message = "Kullanıcı başarıyla oluşturulmuştur.";
            else
                foreach (var error in result.Errors)
                    response.Message += $"{error.Code} - {error.Description}\n";

            return response;
        }

        public async Task<UpdateUserResponse> UpdateUserAsync(UpdateUser model)
        {
            // Kullanıcının varlığını kontrol edin
            AppUser? _user = await _userManager.FindByIdAsync(model.Id);
            if (_user == null)
            {
                throw new NotFoundUserException();
            }

            // Kullanıcı bilgilerini güncelle
            _user.Id = model.Id;
            _user.UserName = model.UserName;
            _user.Email = model.Email;
            _user.Name = model.Name;

            IdentityResult result = await _userManager.UpdateAsync(_user);

            // Yanıtı oluşturun
            UpdateUserResponse response = new() { Succeeded = result.Succeeded };

            if (result.Succeeded)
                response.Message = "Kullanıcı başarıyla güncellenmiştir.";
            else
                foreach (var error in result.Errors)
                    response.Message += $"{error.Code} - {error.Description}\n";

            return response;
        }


        public async Task UpdateRefreshTokenAsync(string refreshToken, AppUser user, DateTime accessTokenDate, int addOnAccessTokenDate)
        {
            if (user != null)
            {
                user.RefreshToken = refreshToken;
                user.RefreshTokenEndDate = accessTokenDate.AddSeconds(addOnAccessTokenDate);
                await _userManager.UpdateAsync(user);
            }
            else
                throw new NotFoundUserException();
        }

        public async Task UpdatePasswordAsync(string userId, string newPassword)
        {
            AppUser user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new NotFoundUserException();
            }

            var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);

            IdentityResult resetResult = await _userManager.ResetPasswordAsync(user, resetToken, newPassword);
                if (resetResult.Succeeded)
                    await _userManager.UpdateSecurityStampAsync(user);
                else
                    throw new PasswordChangeFailedException();
            
        }

        public async Task<List<ListUser>> GetAllUsersAsync()
        {
            var users = await _userManager.Users
                  .ToListAsync();

            return users.Select(user => new ListUser
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.Name,
                TwoFactorEnabled = user.TwoFactorEnabled,
                UserName = user.UserName

            }).ToList();
        }


        public int TotalUsersCount => _userManager.Users.Count();

        public async Task AssignRoleToUserAsnyc(string userId, string[] roles)
        {
            AppUser user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                await _userManager.RemoveFromRolesAsync(user, userRoles);

                await _userManager.AddToRolesAsync(user, roles);
            }
        }

        public async Task<string[]> GetRolesToUserAsync(string userIdOrName)
        {
            AppUser user = await _userManager.FindByIdAsync(userIdOrName);
            if (user == null)
                user = await _userManager.FindByNameAsync(userIdOrName);

            if (user != null)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                return userRoles.ToArray();
            }
            return new string[] { };
        }

        public async Task<bool> HasRolePermissionToEndpointAsync(string name, string code)
        {
            var userRoles = await GetRolesToUserAsync(name);

            if (!userRoles.Any())
                return false;

            // Super admin, endpoint/rol eşlemesi aranmadan her zaman yetkilidir.
            if (userRoles.Any(r => string.Equals(r, SuperAdminRoleName, StringComparison.OrdinalIgnoreCase)))
                return true;

            Endpoint? endpoint = await _endpointReadRepository.Table
                     .Include(e => e.Roles)
                     .FirstOrDefaultAsync(e => e.Code == code);

            if (endpoint == null)
                return false;

            var hasRole = false;
            var endpointRoles = endpoint.Roles.Select(r => r.Name);

            foreach (var userRole in userRoles)
            {
                foreach (var endpointRole in endpointRoles)
                    if (userRole == endpointRole)
                        return true;
            }

            return false;
        }

        public async Task RemoveUserAsync(string userId)
        {
            AppUser user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                await _userManager.DeleteAsync(user);
            }
            else
            {
                throw new NotFoundUserException();
            }
        }

        public async Task<ListUser> GetUserByIdAsync(string id)
        {
            // Kullanıcıyı ID'ye göre bul
            AppUser? user = await _userManager.FindByIdAsync(id);

            // Kullanıcı bulunamazsa özel durum fırlat
            if (user == null)
                throw new NotFoundUserException();

            // Kullanıcı bilgilerini DTO'ya dönüştür ve döndür
            return new ListUser
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.Name,
                TwoFactorEnabled = user.TwoFactorEnabled,
                UserName = user.UserName
            };
        }
    }
}
