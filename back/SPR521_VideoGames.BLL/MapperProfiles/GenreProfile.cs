using AutoMapper;
using SPR521_VideoGames.BLL.Dtos.Genre;
using SPR521_VideoGames.DAL.Entities;

namespace SPR521_VideoGames.BLL.MapperProfiles
{
    public class GenreProfile : Profile
    {
        public GenreProfile()
        {
            // Genre -> GenreDto
            CreateMap<Genre, GenreDto>();

            // CreateGenreDto -> Genre
            CreateMap<CreateGenreDto, Genre>();

            // UpdateGenreDto -> Genre
            CreateMap<UpdateGenreDto, Genre>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}
