using blog_c_.Configurations;
using blog_c_.Data;
using blog_c_.Interfaces;
using Microsoft.EntityFrameworkCore;
using blog_c_.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// minhas coisas
builder.Services.AddDbContext<DataContext>(options => {
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// mapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// DI
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IPostRepositoy, PostRepository>();
builder.Services.AddScoped<ICourseRepository, CourseRepository>();


// erro do ciclo
builder.Services.AddControllers()
      .AddNewtonsoftJson(o => o.SerializerSettings.ReferenceLoopHandling =
       Newtonsoft.Json.ReferenceLoopHandling.Ignore);
//builder.Services.AddControllers().AddJsonOptions(options =>
//{
//    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
//});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    // coloquei só para dev, erro no 
    app.MigrateDatabase();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


app.Run();
