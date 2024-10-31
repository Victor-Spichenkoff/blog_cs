using AutoMapper;
using blog_c_.DTOs;
using blog_c_.DTOs.FilterDtos;
using blog_c_.DTOs.ModifyDtos;
using blog_c_.Models;

namespace blog_c_.Helper;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        // Recebe, saída
        CreateMap<CreationUserDto, User>().ReverseMap();
        CreateMap<CreationUserMessageDto, User>().ReverseMap();
        CreateMap<User, FilterUserDto>().ReverseMap();
        CreateMap<User, FullUserDto>();
        CreateMap<CreationPostDto, Post>().ReverseMap();
        CreateMap<Post, FilterPostDto>().ReverseMap();
        CreateMap<CreationCourseDto, Course>();
    }
}