using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Kwizzez.DAL.Dtos.Auth;
using Kwizzez.DAL.Dtos.Quizzes;
using Kwizzez.DAL.Dtos.Users;
using Kwizzez.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Kwizzez.DAL.Services.Auth
{
    public interface IAuthService
    {
        Task<AuthDto> RegisterUserAsync(RegisterDto registerUserDto);
        Task<AuthDto> GetTokenAsync(LoginDto loginDto);
        Task<AuthDto> RefreshTokenAsync(RefreshTokenDto refreshTokenDto);
        Task<AuthDto> UpdateUserAsync(UpdateUserDto updateUserDto);
        Task<AuthDto> DeactivateUserAsync(string userId);
        Task<AuthDto> ChangePasswordAsync(UserDto userDto, string currentPassword, string newPassword);
    }
}
