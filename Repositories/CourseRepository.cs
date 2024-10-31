using blog_c_.Data;
using blog_c_.Erros;
using blog_c_.Interfaces;
using blog_c_.Models;
using Microsoft.EntityFrameworkCore;

namespace blog_c_.Repositories;

public class CourseRepository(DataContext context, IUserRepository ur) : ICourseRepository
{
    private readonly DataContext _context = context;
    private readonly IUserRepository _userRepository = ur;
    public Course? GetCourseById(long id)
    {
        return _context.Courses
            .AsNoTracking()
            .FirstOrDefault(c => c.Id == id);
    }

    public ICollection<Course> GetCourses()
    {
        return _context.Courses
            .Include(c => c.CoursesUsers)
            .ToList();  
    }

    public ICollection<User> GetCourseUsers(long courseId)
    {
        return [.. _context.CoursesUsers
                .AsNoTracking()
                .Where(cu => cu.CourseId == courseId)
                .Select(cu => cu.User)!
            ];
    }

    public bool CreateCourse(Course course)
    {
        _context.Add(course);
        
        return _context.SaveChanges() > 0;
    }


    public bool AddUserToCourse(long courseId, long userId)
    {
        
        
        if (!CourseExists(courseId))
            throw new GenericDbError("Curso não existe");
        
        if(!_userRepository.UserExists(userId))
            throw new GenericDbError("Usuário não existe");

        if(_context.CoursesUsers.Any(cu => cu.CourseId == courseId && cu.UserId == userId))
            throw new GenericDbError("Usuário já está no curso");
            
            
        var coursesUsers = new CourseUser()
        {
            CourseId = courseId,
            UserId = userId
        };

        _context.CoursesUsers.Add(coursesUsers);

        return _context.SaveChanges() > 0;
    }

    public bool CourseExists(long id) => _context.Courses.Any(c => c.Id == id);
}
