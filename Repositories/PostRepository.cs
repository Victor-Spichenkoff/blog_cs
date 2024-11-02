using AutoMapper;
using blog_c_.Data;
using blog_c_.DTOs.FilterDtos;
using blog_c_.DTOs.ModifyDtos;
using blog_c_.Erros;
using blog_c_.Helper;
using blog_c_.Interfaces;
using blog_c_.Models;
using Microsoft.EntityFrameworkCore;

namespace blog_c_.Repositories;

public class PostRepository(DataContext ctx, IMapper m, IUserRepository ur) : IPostRepositoy
{
    private readonly IMapper _mapper = m;
    private readonly DataContext _context = ctx;
    private readonly IUserRepository _userRepository = ur;

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
    public PagedList<Post>? GetPosts(int page = 0, int pageSize = 2)
    {
        return PagedList<Post>.ToPagedList
        (
            _context.Posts
                .OrderBy(p => p.Id),
            page,
            pageSize
        );
    }

    // pegar os de 1 user
    public ICollection<Post>? GetPostsFromUser(long userId)
    {
        return
        [
            .. _context.Posts
                .Where(p => p.UserId == userId)
                .Include(p => p.User)
        ];
    }

    public void GiveLike(long id)
    {
        var post = GetPostById(id);
        if (post == null)
            throw new GenericDbError("Post não encontrado");

        post.Likes += 1;
        _context.Posts.Update(post);
        _context.SaveChanges();
    }

    public bool CreatePost(Post post)
    {
        if (!_userRepository.UserExists(post.UserId))
            throw new GenericDbError("Usuário inexistente");

        _context.Posts.Add(post);

        return _context.SaveChanges() > 0;
    }

    public Post? UpdatePost(long postId, UpdatePostDto post)
    {
        var dbPost = _context.Posts
            .Where(p => p.Id == postId)
            .FirstOrDefault();

        if (dbPost == null)
            throw new GenericDbError("Post não encontrado");

        if (!string.IsNullOrEmpty(post.Title))
        {
            if (_context.Posts.Any(p => p.Title == post.Title))
                throw new GenericDbError("Email já usado");

            dbPost.Title = post.Title;
        }

        if (!string.IsNullOrEmpty(dbPost.Body) && post?.Body?.Length > 4)
        {
            dbPost.Body = post.Body;
        }

        var success = _context.SaveChanges() > 0;

        if (!success)
            throw new GenericDbError("Nada para atualizar");

        return dbPost;
    }

    public bool DeletePost(long postId)
    {
        var post = _context.Posts
            .FirstOrDefault(p => p.Id == postId);

        if (post == null)
            throw new GenericDbError("Post não encontrado");

        _context.Remove(post);
        return _context.SaveChanges() > 0;
    }
}