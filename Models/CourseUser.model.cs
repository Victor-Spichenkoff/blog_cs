using System.ComponentModel.DataAnnotations;

namespace blog_c_.Models;

public class CourseUser
{
    [Required]
    public long UserId { get; set; }

    [Required]
    public long CourseId { get; set; }

    public User? User { get; set; }
    public Course? Course { get; set; }
}
