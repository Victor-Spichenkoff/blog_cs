using System.ComponentModel.DataAnnotations;

namespace blog_c_.DTOs.ModifyDtos;

public class CreationPostDto
{
    [Required(ErrorMessage = "Id de usuário inválido")]
    public long UserId { get; set; }
    
    [Required(ErrorMessage = "Crie um título")]
    public string? Title { get; set; }
    
    
    [Required(ErrorMessage = "Crie algum conteúdo")]
    public string? Body { get; set; }
}