using AutoMapper;
using SPR521_VideoGames.BLL.Dtos.Game;
using SPR521_VideoGames.BLL.Dtos.GameDto;
using SPR521_VideoGames.DAL.Entities;

namespace SPR521_VideoGames.BLL.MapperProfiles
{
    public class GameProfile : Profile
    {
        public GameProfile()
        {
            // Game -> GameDto
            CreateMap<Game, GameDto>()
                .ForMember(dest => dest.Developer, opt => opt.MapFrom(src => src.Developer!.Name));

            // CreateGameDto -> Game
            CreateMap<CreateGameDto, Game>();
        }
    }
}
