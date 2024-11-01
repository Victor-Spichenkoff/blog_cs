using blog_c_.Data;
using blog_c_.DTOs.ModifyDtos;
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
    
    public Course? UpdateCourse(long courseId, UpdateCourseDto course)
    {
        var dbCourse = _context.Courses
            .FirstOrDefault(c => c.Id == courseId);
            

        if (dbCourse == null)
            throw new GenericDbError("Curso inexistente");

        if (!string.IsNullOrEmpty(course.Name))
            dbCourse.Name = course.Name;        
        
        if (!string.IsNullOrEmpty(course.Description))
            dbCourse.Description = course.Description;

        var success = _context.SaveChanges() > 0;

        var dbStatus = _context.Entry(dbCourse).State; 
        
        if (!success)
            throw new GenericDbError("Nada para atualizar");

        return dbCourse;
    }

    public void DeleteCourse(long courseId)
    {
        var couse = _context.Courses
            .Include(c => c.CoursesUsers)
            .FirstOrDefault(c => c.Id == courseId);
        
        if(couse == null)
            throw new GenericDbError("Curso inexistente");

        
        // apgar relações
        couse.CoursesUsers?.Clear();
        _context.Remove(couse);
        var success = _context.SaveChanges() > 0;
        if (!success)
            throw new GenericDbError("Não foi possível exlcuir");
    }
}
