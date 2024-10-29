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
        return [.. _context.Courses
            .Include(c => c.CoursesUsers)
            .ThenInclude(c => c.User)
            ];
    }
}
