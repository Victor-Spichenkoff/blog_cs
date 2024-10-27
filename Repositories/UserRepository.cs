using AutoMapper;
using blog_c_.Data;
using blog_c_.DTOs;
using blog_c_.Interfaces;
using blog_c_.Models;
using Microsoft.EntityFrameworkCore;

namespace blog_c_.Repositories;

public class UserRepository(DataContext context, IMapper mapper) : IUserRepository
{

    private readonly DataContext _context = context;
    private readonly IMapper _mapper = mapper;

    public User GetById(long id)
    {
        throw new NotImplementedException();
    }

    public User GetFullUser(long id)
    {
        var user = _context.Users
            .Where(p => p.Id == id)
            .Include(p => p.Posts)
            .First();

        //return _mapper.Map<FullUserDto>(user);
        return user;
    }

    public CreationUserDto GetHomeUser(long id)
    {
        var responseUser = _context.Users
            .Where(p => p.Id == id)
            .First();

        return _mapper.Map<CreationUserDto>(responseUser);

    }

    public ICollection<User> GetUsers()
    {
        return [.. _context.Users
            .AsNoTracking() ];// não vai modificar, isso dá mais desempenho
            
    }
}
