using AutoMapper;
using blog_c_.DTOs;
using blog_c_.DTOs.FilterDtos;
using blog_c_.DTOs.ModifyDtos;
using blog_c_.Models;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;

namespace blog_c_.Helper;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        // Recebe, saída
        CreateMap<User, FullUserDto>();
        //filtro
        CreateMap<User, FilterUserDto>().ReverseMap();
        CreateMap<Post, FilterPostDto>().ReverseMap();
        // CreateMap<Post, FilterPostDto>().ReverseMap();
        // Criação
        CreateMap<CreationUserDto, User>().ReverseMap();
        CreateMap<CreationUserMessageDto, User>().ReverseMap();
        CreateMap<CreationPostDto, Post>().ReverseMap();
        CreateMap<CreationCourseDto, Course>();
        
        // update
        CreateMap<UpdateUserDto, User>();
        CreateMap<UpdatePostDto, Post>();
        CreateMap<UpdateCourseDto, Course>();
    }
}