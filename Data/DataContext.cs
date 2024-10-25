using blog_c_.Configurations;
using blog_c_.Models;
using Microsoft.EntityFrameworkCore;

namespace blog_c_.Data;

public class DataContext: DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }


    //tabelas
    public DbSet<User> Users { get; set; }

    public DbSet<Post> Posts { get; set; }

    public DbSet<Course> Courses { get; set; }
    public DbSet<CourseUser> CoursesUsers { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new PostConfiguration());
        modelBuilder.ApplyConfiguration(new CourseUserConfiguration());
        //base.OnModelCreating(modelBuilder);

    }
}
