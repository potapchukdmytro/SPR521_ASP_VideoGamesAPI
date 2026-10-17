using AutoMapper;
using SPR521_VideoGames.BLL.Dtos.Auth;
using SPR521_VideoGames.DAL.Entities;

namespace SPR521_VideoGames.BLL.MapperProfiles
{
    public class AuthProfile : Profile
    {
        public AuthProfile()
        {
            // RegisterDto -> User
            CreateMap<RegisterDto, User>()
                .ForMember(dest => dest.NormalizedEmail, opt => opt.MapFrom(src => src.Email.ToUpper()))
                .ForMember(dest => dest.NormalizedUserName, opt => opt.MapFrom(src => src.UserName.ToUpper()))
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());
        }
    }
}
