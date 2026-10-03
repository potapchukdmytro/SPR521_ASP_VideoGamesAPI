using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SPR521_VideoGames.BLL.Dtos;
using SPR521_VideoGames.BLL.Dtos.Genre;
using SPR521_VideoGames.DAL.Entities;
using SPR521_VideoGames.DAL.Repositories;

namespace SPR521_VideoGames.BLL.Services
{
    public class GenreService
    {
        private readonly GenreRepository _genreRepository;
        private readonly ILogger<GenreService> _logger;
        private readonly IMapper _mapper;

        public GenreService(GenreRepository genreRepository, IMapper mapper, ILogger<GenreService> logger)
        {
            _genreRepository = genreRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ResponseDto> GetAllAsync(CancellationToken ct = default)
        {
            var entities = await _genreRepository.Genres.ToListAsync(ct);

            var dtos = _mapper.Map<List<GenreDto>>(entities);

            return ResponseDto.Success("Жанри успішно отримано", dtos);
        }

        public async Task<ResponseDto> GetByIdAsync(int id, CancellationToken ct = default)
        {
            var entity = await _genreRepository.GetByIdAsync(id);
            if (entity == null)
            {
                return ResponseDto.Error($"Жанр з id '{id}' не знайдено");
            }
            var dto = _mapper.Map<GenreDto>(entity);
            return ResponseDto.Success("Жанр успішно отримано", dto);
        }

        public async Task<ResponseDto> CreateAsync(CreateGenreDto dto, CancellationToken ct = default)
        {
            var entity = _mapper.Map<Genre>(dto);
            await _genreRepository.CreateAsync(entity, ct);
            return ResponseDto.Success("Жанр успішно створено", _mapper.Map<GenreDto>(entity));
        }

        public async Task<ResponseDto> UpdateAsync(UpdateGenreDto dto, CancellationToken ct = default)
        {
            var entity = await _genreRepository.GetByIdAsync(dto.Id);

            if(entity == null)
            {
                _logger.LogWarning($"Жанр з id '{dto.Id}' не знайдено");
                return ResponseDto.Error($"Жанр з id '{dto.Id}' не знайдено");
            }

            _mapper.Map(dto, entity);
            await _genreRepository.UpdateAsync(entity, ct);
            return ResponseDto.Success("Жанр успішно оновлено", _mapper.Map<GenreDto>(entity));
        }

        public async Task<ResponseDto> DeleteAsync(int id, CancellationToken ct = default)
        {
            var entity = await _genreRepository.GetByIdAsync(id);
            if (entity == null)
            {
                return ResponseDto.Error($"Жанр з id '{id}' не знайдено");
            }
            await _genreRepository.DeleteAsync(entity, ct);
            return ResponseDto.Success("Жанр успішно видалено");
        }
    }
}
