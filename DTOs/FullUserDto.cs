using blog_c_.Models;

namespace blog_c_.DTOs;

public class FullUserDto
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public ICollection<Post>? Posts { get; set; }
}
