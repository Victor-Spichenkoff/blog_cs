namespace blog_c_.Models;

public class Course
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    //relações
    public ICollection<CourseUser>? CoursesUsers { get; set; }
}
