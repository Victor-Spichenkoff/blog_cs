﻿using AutoMapper;
using blog_c_.DTOs.FilterDtos;
using blog_c_.DTOs.ModifyDtos;
using blog_c_.Erros;
using blog_c_.Interfaces;
using blog_c_.Models;
using Microsoft.AspNetCore.Mvc;

namespace blog_c_.Controllers;

[ApiController]
[Route("/post")]
public class PostController(IPostRepositoy pr, IUserRepository ur, IMapper m) : Controller
{
    private readonly IPostRepositoy _pr = pr;
    private readonly IUserRepository _userRepository = ur;
    private readonly IMapper _mapper = m;

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


    [HttpGet("/post/{postId}/like")]
    [ProducesResponseType(203)]
    public IActionResult GiveLike(long postId)
    {
        try
        {
            _pr.GiveLike(postId);
            return StatusCode(203);
        }
        catch (GenericDbError error)
        {
            return BadRequest(error.Message);
        }
        catch
        {
            return BadRequest("Erro inesperado");
        }
    }


    [HttpPost]
    [ProducesResponseType(203)]
    public IActionResult CreatePost([FromBody] CreationPostDto post)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var mappedPost = _mapper.Map<Post>(post);

        try
        {
            var success = _pr.CreatePost(mappedPost);

            if (!success)
                return BadRequest("Erro ao criar Post");

            return Created();
        }
        catch (GenericDbError error)
        {
            return BadRequest(error.Message);
        }
        catch
        {
            return BadRequest("Algo deu errado");
        }
    }


    [HttpPatch("{postId}")]
    [ProducesResponseType(typeof(FilterPostDto), 200)]
    public IActionResult UpdatePost(long postId, [FromBody] UpdatePostDto? post)
    {
        if (post is null)
            return BadRequest("Escreva algo");

        try
        {
            var newPost = _pr.UpdatePost(postId, post);

            return Ok(newPost);
        }
        catch (GenericDbError error)
        {
            return BadRequest(error.Message);
        }
        catch(Exception error)
        {
            Console.WriteLine(error + "\n\n\nErro ao atualizar Post");
            return BadRequest("Erro interno!");
        }
    }
}