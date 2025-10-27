using Microsoft.EntityFrameworkCore;
using sievefilteringinternational;
using Testcontainers.PostgreSql;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApiDocument();
builder.Services.AddScoped<IPostService, PostService>();

var postgresContainer = new PostgreSqlBuilder().Build();
postgresContainer.StartAsync().GetAwaiter().GetResult();

builder.Services.AddDbContext<MyDbContext>((provider, optionsBuilder) =>
{
    optionsBuilder.UseNpgsql(postgresContainer.GetConnectionString());
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var ctx = scope.ServiceProvider.GetRequiredService<MyDbContext>();
    ctx.Database.EnsureCreated();
    ctx.Posts.Add(new Post()
    {
        CommentCount = 12344512,
        LikeCount = 21321387,
        Title = "kim kardahisna post with ton of engangement",
        DateCreated = DateTime.UtcNow,
        Id = 1
    });
    ctx.Posts.Add(new Post()
    {
        CommentCount = 2,
        LikeCount = 1,
        Title = "bobs post of his food which noone cares about",
        DateCreated = DateTime.UtcNow,
        Id = 2
    });
    ctx.SaveChanges();
}

app.MapControllers();
app.UseOpenApi();
app.UseSwaggerUi();

app.Run();
