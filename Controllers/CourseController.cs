using AutoMapper;
using blog_c_.DTOs.ModifyDtos;
using blog_c_.Erros;
using blog_c_.Interfaces;
using blog_c_.Models;
using Microsoft.AspNetCore.Mvc;

namespace blog_c_.Controllers;

[ApiController]
[Route("course")]
public class CourseController(ICourseRepository cr, IMapper m) : Controller
{
    private readonly ICourseRepository _cr = cr;
    private readonly IMapper _mapper = m;

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


    [HttpPost]
    [ProducesResponseType(203)]
    public IActionResult CreateCourse([FromBody] CreationCourseDto? course)
    {
        try
        {
            if (course == null)
                return BadRequest("Passe um curso");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var mappedCourse = _mapper.Map<Course>(course);
            
            var success = _cr.CreateCourse(mappedCourse);

            if (!success)
                return StatusCode(500, "Algo deu errado");


            return Created();
        }
        catch (GenericDbError error)
        {
            return StatusCode(500, error.Message);
        }
        catch
        {
            return StatusCode(500, "Erro interno");
        }
    }
    
    
    [HttpPatch("/course/{courseId}/user")]
    [ProducesResponseType(203)]
    public IActionResult AddUserToCourse([FromQuery] long userId, long courseId)
    {
        if (userId == 0 || courseId == 0)
            return BadRequest("Passe um ID válido");

        try
        {
            var success = _cr.AddUserToCourse(courseId, userId);

            if (!success)
                return BadRequest("Não foi possível salvar");

            return NoContent();
        }
        catch (GenericDbError error)
        {
            return BadRequest(error.Message);
        }
        catch
        {
            return BadRequest("Erro interno");
        }
    }


    [HttpPatch("{courseId}")]
    [ProducesResponseType(typeof(ICollection<Course>), 200)]
    public IActionResult UpdateCourse(long courseId, [FromBody] UpdateCourseDto? course)
    {
        if (courseId == 0 || course == null)
            return BadRequest("Mande todas as informações");

        try
        {
            var newCourse = _cr.UpdateCourse(courseId, course);

            return Ok(newCourse);
        }
        catch (GenericDbError error)
        {
            return BadRequest(error.Message);
        }
        catch(Exception error)
        {
            Console.WriteLine(error.Message + "\n\n\nErro ao atualizar curso");
            return BadRequest("Erro interno!");
        }
    }

    [HttpDelete("{courseId}")]
    [ProducesResponseType(204)]
    public IActionResult DeleteCourse(long courseId)
    {
        try
        {
            _cr.DeleteCourse(courseId);
            return NoContent();
        }
        catch (GenericDbError error)
        {
            return BadRequest(error.Message);
        }
        catch (Exception e)
        {
            Console.WriteLine(e + "\n\n\nErro ao remover curso");
            return BadRequest("Erro interno");
        }
    }
}