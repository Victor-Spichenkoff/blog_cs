using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace blog_c_.Models;

public class Post
{
    [Required]
    public int Id { get; set; }

    [Required(ErrorMessage = "Crie um título")]
    [MaxLength(100, ErrorMessage = "Deve ter menos de 100 caracteres")]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "Crie um corpo")]
    public string Body { get; set; } = String.Empty;
    public int Likes { get; set; } = 0;

    [ForeignKey(nameof(User))]
    public long UserId { get; set; }
    public User User { get; set; }
}
