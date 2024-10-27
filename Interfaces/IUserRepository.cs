using blog_c_.DTOs;
using blog_c_.Models;

namespace blog_c_.Interfaces;

public interface IUserRepository
{
    ICollection<User> GetUsers();

    User GetById(long id);

    User GetFullUser(long id);
    CreationUserDto GetHomeUser(long id);
}
