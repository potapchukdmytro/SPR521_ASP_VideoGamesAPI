using Microsoft.AspNetCore.Mvc;
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

            return Ok();
        }
    }
}
