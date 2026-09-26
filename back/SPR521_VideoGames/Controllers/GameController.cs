using Microsoft.AspNetCore.Mvc;
using SPR521_VideoGames.BLL.Dtos.Game;
using SPR521_VideoGames.BLL.Dtos.Pagination;
using SPR521_VideoGames.BLL.Services;
using SPR521_VideoGames.Extensions;
using SPR521_VideoGames.Settings;

namespace SPR521_VideoGames.Controllers
{
    [ApiController]
    [Route("api/game")]
    public class GameController : ControllerBase
    {
        private readonly GameService _gameService;
        private readonly string _imagesFolder;

        public GameController(GameService gameService, IWebHostEnvironment webHostEnvironment)
        {
            _gameService = gameService;

            string root = webHostEnvironment.ContentRootPath;
            _imagesFolder = Path.Combine(root, FileSettings.Games);
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
        public async Task<IActionResult> CreateAsync([FromForm] CreateGameDto dto, CancellationToken ct = default)
        {
            var response = await _gameService.CreateAsync(dto, _imagesFolder, ct);
            return this.GetHttpResponse(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromForm] UpdateGameDto dto, CancellationToken ct = default)
        {
            var response = await _gameService.UpdateAsync(dto, _imagesFolder, ct);
            return this.GetHttpResponse(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id, CancellationToken ct = default)
        {
            var response = await _gameService.DeleteAsync(id, _imagesFolder, ct);
            return this.GetHttpResponse(response);
        }
    }
}
