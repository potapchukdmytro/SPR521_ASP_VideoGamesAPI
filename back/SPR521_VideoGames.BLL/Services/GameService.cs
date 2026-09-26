using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SPR521_VideoGames.BLL.Dtos;
using SPR521_VideoGames.BLL.Dtos.Game;
using SPR521_VideoGames.BLL.Dtos.GameDto;
using SPR521_VideoGames.BLL.Dtos.Pagination;
using SPR521_VideoGames.DAL;
using SPR521_VideoGames.DAL.Entities;
using SPR521_VideoGames.DAL.Repositories;

namespace SPR521_VideoGames.BLL.Services
{
    public class GameService
    {
        private readonly AppDbContext _context;
        private readonly GameRepository _gameRepository;
        private readonly IMapper _mapper;

        public GameService(GameRepository gameRepository, AppDbContext context, IMapper mapper)
        {
            _gameRepository = gameRepository;
            _context = context;
            _mapper = mapper;
        }

        public async Task<ResponseDto> GetAllAsync(PaginationRequestDto dto, CancellationToken ct = default)
        {
            int pageSize = dto.PageSize < 1 ? 20 : dto.PageSize;

            int total = await _gameRepository.GetAll().CountAsync();
            int pages = (int)Math.Ceiling((double)total / dto.PageSize);

            int page = dto.Page < 1 || dto.Page > pages ? 1 : dto.Page;

            var entities = await _gameRepository
                .GetAll()
                .Include(g => g.Developer)
                .OrderBy(g => g.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(ct);

            var dtos = _mapper.Map<List<GameDto>>(entities);

            return ResponseDto.Success("Ігри отримано", dtos);
        }

        public async Task<ResponseDto> CreateAsync(CreateGameDto dto, CancellationToken ct = default)
        {
            bool isDeveloper = await _context.Developers
                .AnyAsync(d => d.Id == dto.DeveloperId, ct);

            if(!isDeveloper)
            {
                return ResponseDto.Error($"Розробник з id '{dto.DeveloperId}' не знайдений");
            }

            var entity = _mapper.Map<Game>(dto);

            await _gameRepository.CreateAsync(entity, ct);

            return ResponseDto.Success("Гру додано");
        }
    }
}
