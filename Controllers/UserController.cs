using AutoMapper;
using blog_c_.DTOs.FilterDtos;
using blog_c_.DTOs.ModifyDtos;
using blog_c_.Erros;
using blog_c_.Interfaces;
using blog_c_.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace blog_c_.Controllers;

[ApiController]
[Route("/user")]
public class UserController(IUserRepository ur, IMapper mapper) : Controller
{
    private readonly IUserRepository _ur = ur;

    [HttpGet]
    [ProducesResponseType(typeof(ICollection<User>), 200)]
    [ProducesResponseType(400)]
    public IActionResult GetAllUsers()
    {
        var res = _ur.GetUsers();

        if (res == null)
            return BadRequest();

        return Ok(res);
    }


    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ICollection<User>), 200)]
    [ProducesResponseType(404)]
    public IActionResult GetById(long id)
    {
        var res = _ur.GetById(id);

        if (res == null)
            return NotFound();

        return Ok(res);
    }


    [HttpGet("/all/{id}")]
    [ProducesResponseType(typeof(User), 200)]
    [ProducesResponseType(404)]
    public IActionResult GetFullUserData(long id)
    {
        var user = _ur.GetFullUser(id);

        if (user == null)
            return NotFound();

        return Ok(user);
    }


    [HttpGet("/home/{id}")]
    [ProducesResponseType(typeof(CreationUserDto), 200)]
    [ProducesResponseType(404)]
    public IActionResult GetHomeUserData(long id)
    {
        if (id < 1)
            return BadRequest();

        var user = _ur.GetHomeUser(id);

        if (user == null)
            return StatusCode(404, "User inexistente");

        return Ok(user);
    }

    [HttpPost]
    [ProducesResponseType(200)]
    public IActionResult CreateUser([FromBody] CreationUserMessageDto user)
    {
        if (user == null)
            return BadRequest("Envie um usuário");

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var mappedUser = mapper.Map<User>(user);

            var createdUser = _ur.CreateUser(mappedUser);

            if (createdUser == null)
                return StatusCode(500, "Erro interno");

            var filteredUser = mapper.Map<FilterUserDto>(createdUser);

            return Ok(createdUser);
        }
        catch
        {
            return BadRequest("Email já usado");
        }
    }

    [HttpPatch("{userId}")]
    [ProducesResponseType(typeof(FilterUserDto), 200)]
    [ProducesResponseType(4004)]
    public IActionResult UpdateUser([FromBody] UpdateUserDto? user, long userId)
    {
        if (user == null)
            return BadRequest("Informe algum dado");

        try
        {
            var newUser = _ur.UpdateUser(userId, user);

            return Ok(newUser);
        }
        catch (GenericDbError error)
        {
            return BadRequest(error.Message);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message, "Erro ao update user");

            return BadRequest("Erro interno");
        }
    }

    [HttpDelete("{userId}")]
    [ProducesResponseType(203)]
    public IActionResult DeleteUser(long userId)
    {
        try
        {
            var success = _ur.DeleteUser(userId);

            if (!success)
                return BadRequest("Erro ao delete usuário");
            
            return NoContent();
        }
        catch (GenericDbError error)
        {
            return BadRequest(error.Message);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message, "\n\n\n\nErro ao deletar user");
            return BadRequest("Erro interno");
        }
    }
}