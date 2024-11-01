using blog_c_.DTOs.FilterDtos;
using blog_c_.DTOs.ModifyDtos;
using blog_c_.Models;

namespace blog_c_.Interfaces;

public interface IUserRepository
{
    ICollection<User> GetUsers();

    FilterUserDto? GetById(long id);

    User GetFullUser(long id);
    CreationUserDto GetHomeUser(long id);
    User? CreateUser(User user);
    bool UserExists(long id);
    FilterUserDto UpdateUser(long userId, UpdateUserDto user);
    bool DeleteUser(long id);
}
