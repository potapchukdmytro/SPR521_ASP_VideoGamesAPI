using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SPR521_VideoGames.BLL.Dtos;
using SPR521_VideoGames.BLL.Dtos.Auth;
using SPR521_VideoGames.BLL.Services;
using SPR521_VideoGames.Extensions;

namespace SPR521_VideoGames.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IValidator<LoginDto> _loginValidator;
        private readonly IValidator<RegisterDto> _registerValidator;
        private readonly AuthService _authService;

        public AuthController(AuthService authService, IValidator<LoginDto> loginValidator, IValidator<RegisterDto> registerValidator)
        {
            _authService = authService;
            _loginValidator = loginValidator;
            _registerValidator = registerValidator;
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

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterDto dto, CancellationToken ct = default)
        {
            var validation = await _registerValidator.ValidateAsync(dto);

            if (!validation.IsValid)
            {
                return this.ValidationResponse(validation);
            }

            var response = await _authService.RegisterAsync(dto, ct);
            return this.GetHttpResponse(response);
        }

        [HttpGet("profile")]
        [Authorize]
        public async Task<IActionResult> ProfileAsync(CancellationToken ct = default)
        {
            var authorization = HttpContext.Request.Headers.Authorization.First();

            if (User.Identity == null || !User.Identity.IsAuthenticated || authorization == null)
            {
                return Unauthorized(ResponseDto.Error("Вкажіть токен"));
            }

            string token = new string(authorization.Skip(7).ToArray());

            var response = await _authService.ProfileAsync(token, ct);
            return this.GetHttpResponse(response);
        }
    }
}
