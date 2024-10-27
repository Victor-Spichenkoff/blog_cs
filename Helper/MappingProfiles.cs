using AutoMapper;
using blog_c_.DTOs;
using blog_c_.Models;
using System.Diagnostics.Metrics;

namespace blog_c_.Helper;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        // Recebe, ´saída
        CreateMap<CreationUserDto, User>().ReverseMap();
        CreateMap<User, FullUserDto>();
    }
}