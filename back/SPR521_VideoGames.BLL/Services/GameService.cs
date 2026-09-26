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
        private readonly DeveloperRepository _developerRepository;
        private readonly GameRepository _gameRepository;
        private readonly FileService _fileService;
        private readonly IMapper _mapper;

        public GameService(GameRepository gameRepository, IMapper mapper, FileService fileService, DeveloperRepository developerRepository)
        {
            _gameRepository = gameRepository;
            _mapper = mapper;
            _fileService = fileService;
            _developerRepository = developerRepository;
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

        public async Task<ResponseDto> GetByIdAsync(int id, CancellationToken ct = default)
        {
            var entity = await _gameRepository.GetByIdAsync(id, ct);

            if(entity == null)
            {
                return ResponseDto.Error($"Гра з id '{id}' не знайдена");
            }

            var dto = _mapper.Map<GameDto>(entity);
            return ResponseDto.Success("Гру отримано", dto);
        }

        public async Task<ResponseDto> CreateAsync(CreateGameDto dto, string imagesFolder, CancellationToken ct = default)
        {
            var developer = await _developerRepository.GetAll()
                .FirstOrDefaultAsync(d => d.Id == dto.DeveloperId, ct);

            if (developer == null)
            {
                return ResponseDto.Error($"Розробник з id '{dto.DeveloperId}' не знайдений");
            }

            var entity = _mapper.Map<Game>(dto);

            // Save image
            if(dto.Image != null)
            {
                entity.Image = await  _fileService.SaveImageAsync(dto.Image, imagesFolder, ct);
            }

            await _gameRepository.CreateAsync(entity, ct);

            return ResponseDto.Success("Гру додано", _mapper.Map<GameDto>(entity));
        }

        public async Task<ResponseDto> DeleteAsync(int id, string imagesFolder, CancellationToken ct = default)
        {
            var entity = await _gameRepository.GetByIdAsync(id, ct);

            if (entity == null)
            {
                return ResponseDto.Error($"Гра з id '{id}' не знайдена");
            }

            if(!string.IsNullOrEmpty(entity.Image))
            {
                _fileService.DeleteFile(Path.Combine(imagesFolder, entity.Image));
            }

            await _gameRepository.DeleteAsync(id, ct);

            return ResponseDto.Success("Гру видалено");
        }

        public async Task<ResponseDto> UpdateAsync(UpdateGameDto dto, string imagesFolder, CancellationToken ct = default)
        {
            var entity = await _gameRepository.GetByIdAsync(dto.Id, ct);

            if (entity == null)
            {
                return ResponseDto.Error($"Гра з id '{dto.Id}' не знайдена");
            }

            var developer = await _developerRepository.GetAll()
                .FirstOrDefaultAsync(d => d.Id == dto.DeveloperId, ct);

            if (developer == null)
            {
                return ResponseDto.Error($"Розробник з id '{dto.DeveloperId}' не знайдений");
            }

            _mapper.Map(dto, entity);

            // Save image
            if(dto.Image != null)
            {
                if(!string.IsNullOrEmpty(entity.Image))
                {
                    _fileService.DeleteFile(Path.Combine(imagesFolder, entity.Image));
                }

                entity.Image = await _fileService.SaveImageAsync(dto.Image, imagesFolder, ct);
            }

            await _gameRepository.UpdateAsync(entity, ct);

            return ResponseDto.Success("Дані про гру оновлено", _mapper.Map<GameDto>(entity));
        }
    }
}
