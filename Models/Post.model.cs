using System.ComponentModel.DataAnnotations;

namespace blog_c_.Models;

public class Post
{
    public int Id { get; set; }
    [Required]
    public string Title { get; set; }
    public string Body { get; set; }
    public int Likes {  get; set; }
}
