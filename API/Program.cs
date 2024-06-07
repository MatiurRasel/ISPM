using API.Data;
using API.Entities.Identity;
using API.Extensions;
using API.Middleware;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddIdentityServices(builder.Configuration);

var connString = "";
if (builder.Environment.IsDevelopment())
    connString = builder.Configuration.GetConnectionString("DefaultConnection");
else
{
    // // Use connection string provided at runtime by FlyIO.
    //         var connUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
    //         // Parse connection URL to connection string for Npgsql
    //         connUrl = connUrl.Replace("postgres://", string.Empty);
    //         var pgUserPass = connUrl.Split("@")[0];
    //         var pgHostPortDb = connUrl.Split("@")[1];
    //         var pgHostPort = pgHostPortDb.Split("/")[0];
    //         var pgDb = pgHostPortDb.Split("/")[1];
    //         var pgUser = pgUserPass.Split(":")[0];
    //         var pgPass = pgUserPass.Split(":")[1];
    //         var pgHost = pgHostPort.Split(":")[0];
    //         var pgPort = pgHostPort.Split(":")[1];

    //         connString = $"Server={pgHost};Port={pgPort};User Id={pgUser};Password={pgPass};Database={pgDb};";
}
builder.Services.AddDbContext<DataContext>(opt =>
{
    opt.UseSqlServer(connString);
});

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();
app.UseCors(builder => builder
.AllowAnyHeader()
.AllowAnyMethod()
.AllowCredentials()
.WithOrigins("https://localhost:4200"));

app.UseAuthentication();
app.UseAuthorization();

app.UseDefaultFiles();
app.UseStaticFiles();

app.MapControllers();
//app.MapFallbackToController("Index","Fallback");

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
try
{
    var context = services.GetRequiredService<DataContext>();
    var userManager = services.GetRequiredService<UserManager<User>>();
    var roleManager = services.GetRequiredService<RoleManager<Role>>();
    await context.Database.MigrateAsync();
    await Seed.SeedUsers(userManager, roleManager);
}
catch (Exception ex)
{
    var logger = services.GetService<ILogger<Program>>();
    logger.LogError(ex, "An Error occurred during migration");

}
app.Run();
