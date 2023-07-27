using Kwizzez.DAL.Dtos.Auth;
using Kwizzez.DAL.Dtos.Responses;
using Kwizzez.DAL.Services.Auth;
using Microsoft.AspNetCore.Mvc;

namespace Kwizzez.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("Signup")]
        public async Task<ApiResponse<AuthDto>> Signup(RegisterDto registerDto)
        {
            var registerResult = await _authService.RegisterUserAsync(registerDto);
            return new()
            {
                Data = registerResult
            };
        }

        [HttpPost("Login")]
        public ApiResponse<AuthDto> Login(LoginDto loginDto)
        {
            return View();
        }
    }
}