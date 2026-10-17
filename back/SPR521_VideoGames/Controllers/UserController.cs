using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SPR521_VideoGames.BLL.Services;
using SPR521_VideoGames.Extensions;

namespace SPR521_VideoGames.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPatch("add-role")]
        public async Task<IActionResult> AddToRoleAsync([FromQuery] int userId, [FromQuery] string roleName, CancellationToken ct = default)
        {
            var response = await _userService.AddToRoleAsync(userId, roleName, ct);
            return this.GetHttpResponse(response);
        }

        [HttpPatch("remove-role")]
        public async Task<IActionResult> RemoveFromRole([FromQuery] int userId, [FromQuery] string roleName, CancellationToken ct = default)
        {
            var response = await _userService.RemoveFromRoleAsync(userId, roleName, ct);
            return this.GetHttpResponse(response);
        }
    }
}
