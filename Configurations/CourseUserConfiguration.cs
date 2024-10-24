using blog_c_.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace blog_c_.Configurations;

public class CourseUserConfiguration : IEntityTypeConfiguration<CourseUser>
{
    public void Configure(EntityTypeBuilder<CourseUser> builder)
    {
        //definição da pk composta
        builder.HasKey(s => new { s.UserId, s.CourseId });

        //colocar as relações
        //hasOne WithMany-> 1-n 
        builder.HasOne(cu => cu.User)
            .WithMany(c => c.CoursesUsers)//campo no course que faz a relação
            .HasForeignKey(cu => cu.UserId)//qual a foreing key que aponta para o one
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(cu => cu.Course)
            .WithMany(u => u.CoursesUsers)
            .HasForeignKey(cu => cu.CourseId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
