using Microsoft.EntityFrameworkCore;
using SocialMediaPlatform.Server.Data;
using SocialMediaPlatform.Server.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<PostRepository>();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()  // Allow requests from any origin
            .AllowAnyMethod()  // Allow any HTTP method (GET, POST, etc.)
            .AllowAnyHeader(); // Allow any header
    });
});

var app = builder.Build();
app.UseCors("AllowAll");

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    SeedDatabase.Initialize(services);
}


app.UseDefaultFiles();
app.UseStaticFiles();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
