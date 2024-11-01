using blog_c_.Data;
using blog_c_.Models;

namespace blog_c_.Helper;

public class Seeding(DataContext context)
{
    private DataContext _context = context;

    public void CreateAll()
    {
        SeedingUser();
        SeedingPosts();
        SeedingCourses();
        SeedingCourseUsers();

        var success = _context.SaveChanges() > 0;

        if (success)
            Console.WriteLine("[ SEED ] Tudo criado");
        else 
            Console.WriteLine("[ SEED ] Erro ao criar");
    }

    public void SeedingUser()
    {
        if (_context.Users.Any())//validar se já não tem
            return;

        _context.Users.AddRange
        (
            new User
            {
                // precisa passar o id nesse métod. Mesmo se fosse um Guid
                // Tem outro que usa AddRange() + SaveChanges()
                Id = 1,
                Name = "Victor",
                Email = "v@gmail.com",
                Password = "123456"
            },
            new User
            {
                Id = 2,
                Name = "Testador 1",
                Email = "t1@gmail.com",
                Password = "123456"
            },
            new User
            {
                Id = 3,
                Name = "testador 2",
                Email = "t2@gmail.com",
                Password = "123456"
            },
            new User
            {
                Id = 4,
                Name = "Testador 3",
                Email = "t3@gmail.com",
                Password = "123456"
            }
        );
    }

    public void SeedingPosts()
    {
        if (_context.Posts.Any())
            return;

        _context.Posts.AddRange
            (
                new Post
                {
                    Id = 1,
                    Title = "Olá, Mundo!",
                    Body = "Código default",
                    UserId = 1,
                    User = null
                },
                new Post
                {
                    Id = 2,
                    Title = "Texto 2!",
                    Body = "Código default 2",
                    UserId = 1,
                    User = null,
                },
                new Post
                {
                    Id = 3,
                    Title = "tesstando 3",
                    Body = "Código default 3",
                    UserId = 2,
                    User = null
                }
            );
    }

    public void SeedingCourses()
    {
        if (_context.Courses.Any())
            return;

        _context.Courses.AddRange
            (
                new Course()
                {
                    Name = ".NET",
                    Description = "Aprenda c#"
                },
                new Course()
                {
                    Name = ".JS",
                    Description = "Aprenda JAVASCRIPT"
                },
                new Course()
                {
                    Name = "HTML & CSS",
                    Description = "Curso de HTML5 e CSS3"
                },
                new Course()
                {
                    Name = "Next.JS",
                    Description = "Aprener Next"
                },
                new Course()
                {
                    Name = "Não sei",
                    Description = "tanto faz"
                }
            );
    }

    public void SeedingCourseUsers()
    {
        if(_context.CoursesUsers.Any())
            return;

        _context.CoursesUsers.AddRange
            (
                // alunos c# - eu e 1
                new CourseUser()
                { 
                    UserId = 1,
                    CourseId = 1,
                },
                new CourseUser()
                {
                    UserId = 2,
                    CourseId = 1,
                },
                //js --> eu 2
                new CourseUser()
                {
                    UserId = 1,
                    CourseId = 2,
                },
                new CourseUser()
                {
                    UserId = 3,
                    CourseId = 2,
                },
                // não sei -> eu e 3
                new CourseUser()
                {
                    UserId = 1,
                    CourseId = 5,
                },
                new CourseUser()
                {
                    UserId = 4,
                    CourseId = 5,
                }
            );
    }
}
