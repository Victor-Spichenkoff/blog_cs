using blog_c_.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace blog_c_.Configurations;

public class PostConfiguration : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder
            .HasIndex(p => p.Title)
            .IsUnique();

        //builder.HasData
        //    (
        //new Post()
        //{
        //    Id = 1,
        //    Title = "Olá, Mundo!",
        //    Body = "Código default",
        //    UserId = 1
        //},
        //        new Post()
        //        {
        //            Id = 2,
        //            Title = "Texto 2!",
        //            Body = "Código default 2",
        //            UserId = 1
        //        },
        //        new Post()
        //        {
        //            Id = 3,
        //            Title = "tesstando 3",
        //            Body = "Código default 3",
        //            UserId = 2
        //        }
        //    );
    }
}
