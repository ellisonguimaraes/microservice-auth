using AutoMapper;
using MicroserviceAuth.Domain.DTO.Application;
using MicroserviceAuth.Domain.DTO.User;
using MicroserviceAuth.Domain.Identity;

namespace MicroserviceAuth.Domain.Mapper;

public class ApplicationProfile : Profile
{
    public ApplicationProfile()
    {
        CreateMap<Application.Application, ApplicationResponse>()
            .ForMember(ar => ar.Id, opt => opt.MapFrom(a => a.Id))
            .ForMember(ar => ar.ApiKey, opt => opt.MapFrom(a => a.ApiKey))
            .ForMember(ar => ar.ApplicationName, opt => opt.MapFrom(a => a.ApplicationName))
            .ForMember(ar => ar.CreatedBy, opt => opt.MapFrom(a => a.CreatedBy))
            .ForMember(ar => ar.CreatedAt, opt => opt.MapFrom(a => a.CreatedAt))
            .ForMember(ar => ar.EditedBy, opt => opt.MapFrom(a => a.EditedBy))
            .ForMember(ar => ar.EditedAt, opt => opt.MapFrom(a => a.EditedAt))
            .ForMember(ar => ar.Users, opt => opt.MapFrom((application, applicationResponse, i, context) => 
            {
                if (application.ApplicationUsers == null)
                    return null;

                var users = application.ApplicationUsers.Select(au => au.User);
                return context.Mapper.Map<IEnumerable<User>, IEnumerable<UserResponse>>(users);
            }));

        CreateMap<ApplicationRequest, Application.Application>()
            .ForMember(ar => ar.Id, opt => opt.MapFrom(a => a.Id))
            .ForMember(ar => ar.ApiKey, opt => opt.MapFrom(a => a.ApiKey))
            .ForMember(ar => ar.ApplicationName, opt => opt.MapFrom(a => a.ApplicationName))
            .ForMember(ar => ar.ApplicationUsers, opt => opt.Ignore())
            .ForMember(ar => ar.CreatedAt, opt => opt.Ignore())
            .ForMember(ar => ar.CreatedBy, opt => opt.Ignore())
            .ForMember(ar => ar.EditedAt, opt => opt.Ignore())
            .ForMember(ar => ar.EditedBy, opt => opt.Ignore());
    }
}
