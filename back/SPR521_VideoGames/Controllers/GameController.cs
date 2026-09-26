using Microsoft.AspNetCore.Mvc;
using SPR521_VideoGames.BLL.Dtos.Game;
using SPR521_VideoGames.BLL.Dtos.Pagination;
using SPR521_VideoGames.BLL.Services;
using SPR521_VideoGames.Extensions;

namespace SPR521_VideoGames.Controllers
{
    [ApiController]
    [Route("api/game")]
    public class GameController : ControllerBase
    {
        private readonly GameService _gameService;

        public GameController(GameService gameService)
        {
            _gameService = gameService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery] PaginationRequestDto dto, CancellationToken ct = default)
        {
            var response = await _gameService.GetAllAsync(dto, ct);
            return this.GetHttpResponse(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id, CancellationToken ct = default)
        {
            var response = await _gameService.GetByIdAsync(id, ct);
            return this.GetHttpResponse(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateGameDto dto, CancellationToken ct = default)
        {
            var response = await _gameService.CreateAsync(dto, ct);
            return this.GetHttpResponse(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateGameDto dto, CancellationToken ct = default)
        {
            var response = await _gameService.UpdateAsync(dto, ct);
            return this.GetHttpResponse(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id, CancellationToken ct = default)
        {
            var response = await _gameService.DeleteAsync(id, ct);
            return this.GetHttpResponse(response);
        }
    }
}
