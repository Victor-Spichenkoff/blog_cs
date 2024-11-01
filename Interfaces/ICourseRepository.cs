using blog_c_.DTOs.ModifyDtos;
using blog_c_.Models;

namespace blog_c_.Interfaces;

public interface ICourseRepository
{
    public Course? GetCourseById(long id);
    public ICollection<Course> GetCourses();
    public ICollection<User>? GetCourseUsers(long id);
    public bool CreateCourse(Course course);
    public bool AddUserToCourse(long courseId, long userId);
    public bool CourseExists(long id);
    public Course? UpdateCourse(long courseId, UpdateCourseDto course);
    public void DeleteCourse(long courseId);
}
