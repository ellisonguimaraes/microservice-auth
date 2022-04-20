using AutoMapper;
using MicroserviceAuth.Domain.DTO.User;
using MicroserviceAuth.Domain.Identity;

namespace MicroserviceAuth.Domain.Mapper;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserResponse>()
            .ForMember(ur => ur.Email, opt => opt.MapFrom(u => u.Email))
            .ForMember(ur => ur.CreatedBy, opt => opt.MapFrom(a => a.CreatedBy))
            .ForMember(ur => ur.CreatedAt, opt => opt.MapFrom(a => a.CreatedAt))
            .ForMember(ur => ur.EditedBy, opt => opt.MapFrom(a => a.EditedBy))
            .ForMember(ur => ur.EditedAt, opt => opt.MapFrom(a => a.EditedAt));
    }
}
