using blog_c_.DTOs.FilterDtos;
using blog_c_.Interfaces;
using blog_c_.Models;
using Microsoft.AspNetCore.Mvc;

namespace blog_c_.Controllers;

[ApiController]
[Route("/post")]
public class PostController(IPostRepositoy pr, IUserRepository ur) : Controller
{
    private readonly IPostRepositoy _pr = pr;
    private readonly IUserRepository _userRepository = ur;

    // FY
    [HttpGet]
    [ProducesResponseType(typeof(ICollection<FilterPostDto>), 200)]
    public IActionResult GetFy([FromQuery] int page)
    {
        var posts = _pr.GetPosts(page);

        if (posts.Count == 0)
            return BadRequest("Isso é tudo pessoal...");

        return Ok(posts);
    }


    [HttpGet("{id}")]
    [ProducesResponseType(typeof(Post), 200)]
    [ProducesResponseType(404)]
    public IActionResult GetOnePost(long id)
    {
        if (id < 0)
            return BadRequest("ID inválido");

        var post = _pr.GetPostById(id);

        if (post == null)
            return NotFound("Post não encontrado");

        return Ok(post);
    }

    [HttpGet("/user/{userId}/posts")]
    [ProducesResponseType(typeof(ICollection<Post>), 200)]
    [ProducesResponseType(404)]
    public IActionResult GetPostsFromUserId(long userId)
    {
        if (!_userRepository.UserExists(userId))
            return NotFound("Usuário inexistente");


        var posts = _pr.GetPostsFromUser(userId);

        if (posts?.Count == 0 || posts == null)
            return NotFound("Usuário não tem posts");

        return Ok(posts);
    }
}
