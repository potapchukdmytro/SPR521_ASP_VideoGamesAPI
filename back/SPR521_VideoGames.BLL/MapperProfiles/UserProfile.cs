using AutoMapper;
using SPR521_VideoGames.BLL.Dtos.User;
using SPR521_VideoGames.DAL.Entities;

namespace SPR521_VideoGames.BLL.MapperProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            // User -> UserDto
            CreateMap<User, UserDto>();
        }
    }
}
