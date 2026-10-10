using Microsoft.AspNetCore.Mvc;
using SPR521_VideoGames.BLL.Dtos;
using SPR521_VideoGames.BLL.Dtos.Auth;
using SPR521_VideoGames.BLL.Services;
using SPR521_VideoGames.BLL.Validators.Auth;
using SPR521_VideoGames.Extensions;

namespace SPR521_VideoGames.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly LoginValidator _loginValidator;
        private readonly AuthService _authService;

        public AuthController(LoginValidator loginValidator, AuthService authService)
        {
            _loginValidator = loginValidator;
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginDto dto, CancellationToken ct = default)
        {
            var validation = _loginValidator.Validate(dto);

            if(!validation.IsValid)
            {
                return this.ValidationResponse(validation);
            }

            var response = await _authService.LoginAsync(dto, ct);
            return this.GetHttpResponse(response);
        }

        [HttpGet("profile")]
        public async Task<IActionResult> ProfileAsync(CancellationToken ct = default)
        {
            var token = HttpContext.Request.Headers.Authorization.FirstOrDefault();

            if(token == null)
            {
                return Unauthorized(ResponseDto.Error("Вкажіть токен"));
            }

            var response = await _authService.ProfileAsync(token, ct);
            return this.GetHttpResponse(response);
        }
    }
}
