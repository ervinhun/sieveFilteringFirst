using Microsoft.EntityFrameworkCore;
using Sieve.Services;
using sievefilteringinternational;
using Testcontainers.PostgreSql;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApiDocument();
builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddScoped<ISieveProcessor, SieveProcessor>();
builder.Services.AddCors();


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
        Title = "kim kardashian post with ton of engagement",
        DateCreated = DateTime.UtcNow,
        Id = 1
    });
    ctx.Posts.Add(new Post()
    {
        CommentCount = 2,
        LikeCount = 1,
        Title = "bobs post of his food which no-one cares about",
        DateCreated = DateTime.UtcNow,
        Id = 2
    });
    ctx.Posts.Add(new Post()
    {
        CommentCount = 20,
        LikeCount = 5,
        Title = "This is the third post, so this is the charming one",
        DateCreated = DateTime.UtcNow,
        Id = 3
    });
    ctx.Posts.Add(new Post()
    {
        CommentCount = 2220,
        LikeCount = 511,
        Title = "This is the fourth post, so this is just here",
        DateCreated = DateTime.UtcNow,
        Id = 4
    });
    ctx.SaveChanges();
}

app.UseCors(config => config.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().SetIsOriginAllowed(a => true));
app.MapControllers();
app.UseOpenApi();
app.UseSwaggerUi();
app.GenerateApiClientsFromOpenApi("/client/src/generated-ts-client.ts").GetAwaiter().GetResult();



app.Run();