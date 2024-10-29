﻿using blog_c_.DTOs.FilterDtos;
using blog_c_.Models;

namespace blog_c_.Interfaces;

public interface IPostRepositoy
{
    // esse deve ser o limitado (só id e title e likes)
    public ICollection<FilterPostDto> GetPosts(int page, int pageSize = 2);

    public Post? GetPostById(long id);

    public ICollection<Post>? GetPostsFromUser(long userId);
}