using System.Text.RegularExpressions;
using Kwizzez.DAL.Dtos.Auth;
using Kwizzez.DAL.Dtos.Responses;
using Kwizzez.DAL.Dtos.Users;
using Kwizzez.DAL.Services.Auth;
using Kwizzez.DAL.Services.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kwizzez.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        private readonly IUsersService _usersService;

        public AuthController(IAuthService authService, IUsersService usersService)
        {
            _authService = authService;
            _usersService = usersService;
        }

        // POST: api/Auth/Signup
        [HttpPost("Signup")]
        public async Task<ApiResponse<AuthDto>> Signup(RegisterDto registerDto)
        {
            var registerResult = await _authService.RegisterUserAsync(registerDto);

            if (!registerResult.IsSucceed)
                return new()
                {
                    Errors = registerResult.Errors
                };
            return new()
            {
                Data = registerResult
            };
        }

        // POST: api/Auth/Login
        [HttpPost("Login")]
        public async Task<ApiResponse<AuthDto>> Login(LoginDto loginDto)
        {
            var loginResult = await _authService.GetTokenAsync(loginDto);

            if (!loginResult.IsSucceed)
                return new()
                {
                    Errors = loginResult.Errors
                };

            return new()
            {
                Data = loginResult
            };
        }

        // POST: api/Auth/Refresh
        [HttpPost("Refresh")]
        public async Task<ApiResponse<AuthDto>> Refresh(RefreshTokenDto refreshTokenDto)
        {
            var refreshResult = await _authService.RefreshTokenAsync(refreshTokenDto);

            if (!refreshResult.IsSucceed)
                return new()
                {
                    Errors = refreshResult.Errors
                };

            return new()
            {
                Data = refreshResult
            };
        }
    }
}