namespace blog_c_.DTOs.ModifyDtos;

public class UpdatePostDto
{
    public long? Id { get; set; }
    public string? Title { get; set; }
    public string? Body { get; set; }
}