using AutoMapper;
using System.Diagnostics.Metrics;

namespace blog_c_.Helper;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        // Recebe, ´saída
        CreateMap<Pokemon, PokemonDto>();
    }
}