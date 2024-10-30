using blog_c_.Interfaces;
using blog_c_.Models;
using Microsoft.AspNetCore.Mvc;

namespace blog_c_.Controllers;

[ApiController]
[Route("course")]
public class CourseController(ICourseRepository cr) : Controller
{
    private readonly ICourseRepository _cr = cr;

    // FY
    [HttpGet]
    [ProducesResponseType(typeof(ICollection<Course>), 200)]
    public IActionResult GetFy()
    {
        var courses = _cr.GetCourses();

        if (courses.Count == 0)
            return BadRequest("Sem cursos...");

        return Ok(courses);
    }


    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ICollection<Course>), 200)]
    [ProducesResponseType(404)]
    public IActionResult GetById(long id)
    {
        var res = _cr.GetCourseById(id);

        if (res == null)
            return NotFound("Curso não encontrado");

        return Ok(res);
    }


    [HttpGet("/course/{courseId:long}/users")]
    [ProducesResponseType(typeof(ICollection<User>), 200)]
    [ProducesResponseType(404)]
    public IActionResult GetUsersFromCourse(long courseId)
    {
        var res = _cr.GetCourseUsers(courseId);

        if (res == null || res.Count == 0)
            return NotFound("Curso não encontrado e/ou curso sem usuários");

        return Ok(res);
    }
}
