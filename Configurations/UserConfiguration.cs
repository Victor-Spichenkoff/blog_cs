using blog_c_.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace blog_c_.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        //configs gerais
        builder
            .HasIndex(u => u.Email)
            .IsUnique(true);

        builder.Property(u => u.Email)
            .HasDefaultValue("Nenhum");

        //seeding
        //builder.HasData
        //    (
        //new User
        //{
        //    // precisa passar o id nesse métod. Mesmo se fosse um Guid
        //    // Tem outro que usa AddRange() + SaveChanges()
        //    Id = 1,
        //    Name = "Victor",
        //    Email = "v@gmail.com",
        //    Password = "123456"
        //},
        //        new User
        //        {
        //            Id = 2,
        //            Name = "Testador 1",
        //            Email = "t1@gmail.com",
        //            Password = "123456"
        //        },
        //        new User
        //        {
        //            Id = 3,
        //            Name = "testador 2",
        //            Email = "t2@gmail.com",
        //            Password = "123456"
        //        },
        //        new User
        //        {
        //            Id = 4,
        //            Name = "Testador 3",
        //            Email = "t3@gmail.com",
        //            Password = "123456"
        //        }
        //    );
    }
}
