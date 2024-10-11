using System.ComponentModel.DataAnnotations;

namespace blog_c_.Models;

public class User
{
    public long Id { get; set; }
    public required string Name { get; set; }

    [Required]
    public required string Email { get; set; }

    [Required]
    [MinLength(5)]
    public required string Password { get; set; }


    public ICollection<Post>? Posts { get; set; }
}
