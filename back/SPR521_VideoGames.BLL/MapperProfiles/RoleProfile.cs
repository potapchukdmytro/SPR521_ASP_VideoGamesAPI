using AutoMapper;
using SPR521_VideoGames.BLL.Dtos.Role;
using SPR521_VideoGames.DAL.Entities;

namespace SPR521_VideoGames.BLL.MapperProfiles
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            // Role -> RoleDto
            CreateMap<Role, RoleDto>();

            // CreateRoleDto -> Role
            CreateMap<CreateRoleDto, Role>()
                .ForMember(dest => dest.NormalizedName, opt => opt.MapFrom(src => src.Name.ToUpper()));

            // UpdateRoleDto -> Role
            CreateMap<UpdateRoleDto, Role>()
                .ForMember(dest => dest.NormalizedName, opt => opt.MapFrom(src => src.Name.ToUpper()));
        }
    }
}
