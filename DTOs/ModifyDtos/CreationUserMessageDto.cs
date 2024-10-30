using System.ComponentModel.DataAnnotations;


namespace blog_c_.DTOs.ModifyDtos;

public class CreationUserMessageDto
{
    [Required(ErrorMessage = "Name is required")]
    public string? Name { get; set; }

    [Required(ErrorMessage = "Passe um email")]
    [EmailAddress(ErrorMessage = "Informe um email válido")]
    public string? Email { get; set; }

    [MinLength(5, ErrorMessage = "Deve ter mais de 5 caracteres")]
    public string? Password { get; set; }
}