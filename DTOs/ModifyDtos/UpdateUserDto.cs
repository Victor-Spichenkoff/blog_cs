namespace blog_c_.DTOs.ModifyDtos;

public class UpdateUserDto
{
    public long? Id { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
}