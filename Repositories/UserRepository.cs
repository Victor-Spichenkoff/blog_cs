using AutoMapper;
using blog_c_.Data;
using blog_c_.DTOs.FilterDtos;
using blog_c_.DTOs.ModifyDtos;
using blog_c_.Erros;
using blog_c_.Interfaces;
using blog_c_.Models;
using Microsoft.EntityFrameworkCore;

namespace blog_c_.Repositories;

public class UserRepository(DataContext context, IMapper mapper) : IUserRepository
{
    private readonly DataContext _context = context;
    private readonly IMapper _mapper = mapper;

    public User? CreateUser(User user)
    {
        _context.Add(user);

        _context.SaveChanges();
        return user;
    }

    public FilterUserDto? GetById(long id)
    {
        return _mapper.Map<FilterUserDto>(
            _context.Users
                .Where(p => p.Id == id)
                .FirstOrDefault()
        );
    }

    public User? GetUserFullUserById(long id)
    {
        return _context.Users
            .FirstOrDefault(p => p.Id == id);
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
        return
        [
            .. _context.Users
                .AsNoTracking()
        ]; // não vai modificar, isso dá mais desempenho
    }

    public bool UserExists(long id)
    {
        var res = _context.Users.Where(u => u.Id == id).FirstOrDefault();
        return res != null;
    }

    public FilterUserDto UpdateUser(long userId, UpdateUserDto user)
    {
        var dbUser = GetUserFullUserById(userId);

        // Fica como null se não passar
        if (dbUser == null)
            throw new GenericDbError("Usuário não existe");

        // validações
        if (!string.IsNullOrEmpty(user.Email))
        {
            if (_context.Users.Any(u => u.Email == user.Email))
                throw new GenericDbError("Email já usado");

            dbUser.Email = user.Email;
        }

        if (!string.IsNullOrEmpty(user.Password))
        {
            if (user.Password is { Length: < 5 })
                throw new GenericDbError("Senha deve ter 5 caracteres ou mais");

            dbUser.Password = user.Password;
        }

        // não coloquei validações sobre não poder ser "" ou para ser um email válido
        if(!string.IsNullOrEmpty(user.Name))
            dbUser.Name = user.Name;

        var success = _context.SaveChanges();
        if (success <= 0)
            throw new GenericDbError("Nada para mudar");

        
        return _mapper.Map<FilterUserDto>(dbUser);
    }

    public bool DeleteUser(long id)
    {
        var user = _context.Users.FirstOrDefault(u => u.Id == id);

        if (user == null)
            throw new GenericDbError("Usuário não existe");
        
        _context.Remove(user);
        return _context.SaveChanges() > 0;
    }
}