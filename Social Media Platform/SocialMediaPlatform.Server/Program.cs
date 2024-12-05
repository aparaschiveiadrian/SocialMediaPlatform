using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SocialMediaPlatform.Server.Data;
using SocialMediaPlatform.Server.Models;
using SocialMediaPlatform.Server.Repository;
using SocialMediaPlatform.Server.Services;

var builder = WebApplication.CreateBuilder(args);
//la post folosim mapper
// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

builder.Services.AddScoped<PostRepository>();
builder.Services.AddScoped<TokenService>();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
}).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = options.DefaultChallengeScheme = options.DefaultForbidScheme =
        options.DefaultScheme = options.DefaultSignInScheme =
            options.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidateAudience = true,
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            System.Text.Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SigningKey"]))
    };
});


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin() // Allow requests from any origin
            .AllowAnyMethod() // Allow any HTTP method (GET, POST, etc.)
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

Console.WriteLine($"Issuer: {builder.Configuration["Jwt:Issuer"]}");
Console.WriteLine($"Audience: {builder.Configuration["Jwt:Audience"]}");
Console.WriteLine($"Key: {builder.Configuration["Jwt:SecurityKey"]}");

app.UseAuthorization();
app.UseAuthentication();


app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();