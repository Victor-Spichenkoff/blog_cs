using System.ComponentModel.DataAnnotations;

namespace blog_c_.DTOs.ModifyDtos;

public class CreationCourseDto
{
    [Required(ErrorMessage = "Passe um nome para o curso")]
    public string? Name { get; set; }
    
    [Required(ErrorMessage = "Passe uma descrição para o curso")]
    public string? Description { get; set; }
}