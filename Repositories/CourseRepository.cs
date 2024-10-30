using blog_c_.Data;
using blog_c_.Interfaces;
using blog_c_.Models;
using Microsoft.EntityFrameworkCore;

namespace blog_c_.Repositories;

public class CourseRepository(DataContext context) : ICourseRepository
{
    private readonly DataContext _context = context;
    public Course? GetCourseById(long id)
    {
        return _context.Courses
            .AsNoTracking()
            .Where(c => c.Id == id)
            .FirstOrDefault();
    }

    public ICollection<Course> GetCourses()
    {
        return _context.Courses
            .Include(c => c.CoursesUsers)
            .ToList();  
    }

    public ICollection<User>? GetCourseUsers(long courseId)
    {
        return [.. _context.CoursesUsers
                .AsNoTracking()
                .Where(cu => cu.CourseId == courseId)
                .Select(cu => cu.User)
            ];
    }

    public bool CreateCourse(Course course)
    {
        throw new NotImplementedException();
    }
}
