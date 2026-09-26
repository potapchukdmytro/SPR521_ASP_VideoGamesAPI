using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SPR521_VideoGames.BLL.Dtos.Game;
using SPR521_VideoGames.BLL.Dtos.GameDto;
using SPR521_VideoGames.BLL.Dtos.Pagination;
using SPR521_VideoGames.BLL.Services;
using SPR521_VideoGames.DAL.Entities;
using SPR521_VideoGames.DAL.Repositories;
using SPR521_VideoGames.Extensions;

namespace SPR521_VideoGames.Controllers
{
    [ApiController]
    [Route("api/game")]
    public class GameController : ControllerBase
    {
        private readonly GameRepository _gameRepository;
        private readonly GameService _gameService;

        public GameController(GameRepository gameRepository, GameService gameService)
        {
            _gameRepository = gameRepository;
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
            var game = await _gameRepository.GetByIdAsync(id, ct);

            if(game != null)
            {
                return Ok(game);
            }
            else
            {
                return NotFound($"Не вдалося знайти книгу з id '{id}'");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateGameDto dto, CancellationToken ct = default)
        {
            var response = await _gameService.CreateAsync(dto, ct);
            return this.GetHttpResponse(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] Game game, CancellationToken ct = default)
        {
            game.ReleaseDate = game.ReleaseDate.ToUniversalTime();
            await _gameRepository.UpdateAsync(game, ct);

            return Ok("Гру додано");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync([FromBody] Game game, CancellationToken ct = default)
        {
            game.ReleaseDate = game.ReleaseDate.ToUniversalTime();
            await _gameRepository.DeleteAsync(game, ct);

            return Ok("Гру додано");
        }
    }
}
