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
    bool CreateUser(CreationUserDto user);
    bool UserExists(long id);
}
