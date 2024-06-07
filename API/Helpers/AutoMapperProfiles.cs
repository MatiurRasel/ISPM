using API.DTOs;
using API.Entities.Identity;
using AutoMapper;

namespace API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {

            CreateMap<RegisterDTO, User>();
            CreateMap<DateTime, DateTime>()
                .ConvertUsing(d => DateTime.SpecifyKind(d, DateTimeKind.Utc));

            CreateMap<DateTime?, DateTime?>()
                .ConvertUsing(d => d.HasValue
                ? DateTime.SpecifyKind(d.Value, DateTimeKind.Utc) : null);
        }
    }
}