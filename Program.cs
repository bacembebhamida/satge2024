using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using Microsoft.AspNetCore.Identity;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Configure services
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        // Configure Identity
        builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
        {
            // Configure identity options here
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequireUppercase = true;
            options.Password.RequiredLength = 6;
            options.Password.RequiredUniqueChars = 1;

            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            options.Lockout.MaxFailedAccessAttempts = 5;
            options.Lockout.AllowedForNewUsers = true;

            options.User.RequireUniqueEmail = true;
            options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@";
        })
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultTokenProviders();

        // Add other services
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                //c.RoutePrefix = string.Empty; // Uncomment if you want Swagger UI at the root
            });

            app.UseStaticFiles();
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error"); // Configure error handling for production
        }

        app.UseHttpsRedirection();
        app.UseRouting();

        app.UseAuthentication(); // Ensure authentication is used
        app.UseAuthorization();

        app.MapControllers();
        app.UseDeveloperExceptionPage();
        app.Use(async (context, next) =>
        {
            var routeEndpoint = context.GetEndpoint();
            if (routeEndpoint != null)
            {
                var routePattern = (routeEndpoint as RouteEndpoint)?.RoutePattern?.RawText;
                Console.WriteLine($"Route matched: {routePattern}");
            }
            await next();
        });


        app.Run();
    }
}
