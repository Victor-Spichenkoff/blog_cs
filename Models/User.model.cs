using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace blog_c_.Models;

[Table("Usuario")]  
public class User
{
    [Required(ErrorMessage = "Campo obrigátorio")]
    public long Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public required string Email { get; set; }

    [Required]
    [MinLength(5)]
    public required string Password { get; set; }


    public ICollection<Post>? Posts { get; set; }
    public ICollection<CourseUser>? CoursesUsers { get; set; }
}
