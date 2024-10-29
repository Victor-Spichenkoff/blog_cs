using blog_c_.Models;

namespace blog_c_.Interfaces;

public interface ICourseRepository
{
    public Course? GetCourseById(long id);
    public ICollection<Course> GetCourses();
}
