﻿using OHS_program_api.Application.DTOs.User;
using OHS_program_api.Domain.Entities.Identity;

namespace OHS_program_api.Application.Abstractions.Services
{
    public interface IUserService
    {
        Task<CreateUserResponse> CreateAsync(CreateUser model);
        Task<UpdateUserResponse> UpdateUserAsync(UpdateUser model);
        Task UpdateRefreshTokenAsync(string refreshToken, AppUser user, DateTime accessTokenDate, int addOnAccessTokenDate);
        Task UpdatePasswordAsync(string userId, string newPassword);
        Task<List<ListUser>> GetAllUsersAsync();
        Task<ListUser> GetUserByIdAsync(string id);
        int TotalUsersCount { get; }
        Task AssignRoleToUserAsnyc(string userId, string[] roles);
        Task<string[]> GetRolesToUserAsync(string userIdOrName);
        Task<bool> HasRolePermissionToEndpointAsync(string name, string code);
        Task RemoveUserAsync(string iduserId);
    }
}
