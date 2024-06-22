using API.DTOs;
using API.Entities.Identity;
using AutoMapper;

namespace API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            // Mapping from RegisterDTO to User
            CreateMap<RegisterDTO, User>()
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.MobileNumber))
                .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth.HasValue
                    ? DateTime.SpecifyKind(src.DateOfBirth.Value, DateTimeKind.Utc)
                    : (DateTime?)null));

            // Mapping from User to UserDTO
            CreateMap<User, UserDTO>()
                .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth.ToString("dd-MM-yyyy")))
                .ForMember(dest => dest.LastActive, opt => opt.MapFrom(src => src.LastActive.ToString("dd-MM-yyyy HH:mm:ss")))
                .ForMember(dest => dest.UserRoles, opt => opt.MapFrom(src => src.UserRoles));

            // Mapping from UserRole to UserRoleDTO
            CreateMap<UserRole, UserRoleDTO>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName))
                .ForMember(dest => dest.RoleId, opt => opt.MapFrom(src => src.RoleId))
                .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role.Name));


            // DateTime mappings
            CreateMap<DateTime, DateTime>()
                .ConvertUsing(d => DateTime.SpecifyKind(d, DateTimeKind.Utc));

            CreateMap<DateTime?, DateTime?>()
                .ConvertUsing(d => d.HasValue
                    ? DateTime.SpecifyKind(d.Value, DateTimeKind.Utc)
                    : (DateTime?)null);
        }


    }
}