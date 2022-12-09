using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Data;
using OnlineShop.Data.Repositories;
using OnlineShop.Domain.RepositoriesInterfaces;
using OnlineShop.Domain.Services;
using OnlineShop.WebApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddCors();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<AccountService>();
builder.Services.Configure<PasswordHasherOptions>(opt => opt.IterationCount = 100_000);
builder.Services.AddSingleton<IPasswordHasherService, Pbkdf2PasswordHasher>();

var dbPath = "myapp.db";

builder.Services.AddDbContext<AppDbContext>
    (options => options.UseSqlite($"Data Source={dbPath}"));

var app = builder.Build();

string hashedPassword = "";
app.MapGet("/hash", (string pwd, IPasswordHasherService hasher) =>
{
   hashedPassword = hasher.HashPassword(pwd);
   return hashedPassword;
});

app.MapGet("/check", Check);

IResult Check(string pwd, IPasswordHasherService hasher, AppDbContext ctx)
    {
        bool result = hasher.VerifyPassword(hashedPassword, pwd);
        if (!result)
        {
            return Results.Unauthorized();
        }
        var account = ctx.Accounts.Where(s => s.PasswordHash==hashedPassword);
        return Results.Ok(account);
    }

app.MapControllers();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(policy =>
{
    policy
        //.WithOrigins("https://localhost:7258")
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowAnyOrigin()
        ;
});
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.Run();