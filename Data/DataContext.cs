using blog_c_.Models;
using Microsoft.EntityFrameworkCore;

namespace blog_c_.Data;

public class DataContext: DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }


    //tabelas
    public DbSet<User> Users { get; set; }

    public DbSet<Post> Posts { get; set; }
}
