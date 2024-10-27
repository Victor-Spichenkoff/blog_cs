using blog_c_.DTOs;
using blog_c_.Interfaces;
using blog_c_.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace blog_c_.Controllers;

[ApiController]
[Route("/user")]
public class UserController(IUserRepository ur): Controller
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

    [HttpGet("/all/{id}")]
    [ProducesResponseType(typeof (User), 200)]
    [ProducesResponseType(404)]
    public IActionResult GetFullUserData(long id)
    {
        var user = _ur.GetFullUser(id);

        if(user == null)
            return NotFound();

        return Ok(user);
    }


    [HttpGet("/home/{id}")]
    [ProducesResponseType(typeof(CreationUserDto), 200)]
    [ProducesResponseType(404)]
    public IActionResult GetHomeUserData(long id)
    {
        if(id < 1)
            return BadRequest();

        var user = _ur.GetHomeUser(id);

        if (user == null)
            return StatusCode(404, "User inexistente");

        return Ok(user);
    }
}