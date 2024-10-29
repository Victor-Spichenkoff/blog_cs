using AutoMapper;
using blog_c_.Data;
using blog_c_.DTOs.FilterDtos;
using blog_c_.Interfaces;
using blog_c_.Models;
using Microsoft.EntityFrameworkCore;

namespace blog_c_.Repositories;

public class PostRepository(DataContext ctx, IMapper m) : IPostRepositoy
{
    private readonly IMapper _mapper = m;
    private readonly DataContext _context = ctx;

    // pegar 1
    public Post? GetPostById(long id)
    {
        return _context.Posts
            .AsNoTracking()
            .Where(p => p.Id == id)
            .Include(p => p.User)
            .FirstOrDefault();
    }

    // Pegar fy
    public ICollection<FilterPostDto> GetPosts(int page = 0, int pageSize = 2)
    {
        var currentPagePosts = _context.Posts
            .Skip(pageSize * page)
            .Take(pageSize)
            .ToList();

        return _mapper.Map<ICollection<FilterPostDto>>(currentPagePosts);
    }

    // pegar os de 1 user
    public ICollection<Post>? GetPostsFromUser(long userId)
    {
        return [.. _context.Posts
            .Where(p => p.UserId == userId)
            .Include(p =>p.User)
            ];
    }
}
