using blog_c_.DTOs.FilterDtos;
using blog_c_.DTOs.ModifyDtos;
using blog_c_.Helper;
using blog_c_.Models;

namespace blog_c_.Interfaces;

public interface IPostRepositoy
{
    // esse deve ser o limitado (só id e title e likes)
    public PagedList<Post>? GetPosts(int page, int pageSize = 2);

    public Post? GetPostById(long id);

    public ICollection<Post>? GetPostsFromUser(long userId);
    public void GiveLike(long id);
    public bool CreatePost(Post post);
    public Post? UpdatePost(long postId, UpdatePostDto post);
    public bool DeletePost(long postId);
}
