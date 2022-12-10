using Microsoft.AspNetCore.HttpLogging;
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
builder.Services.Configure<PasswordHasherOptions>(opt => opt.IterationCount = 10_000);
builder.Services.AddSingleton<IPasswordHasherService, Pbkdf2PasswordHasher>();

var dbPath = "myapp.db";
builder.Services.AddDbContext<AppDbContext>
    (options => options.UseSqlite($"Data Source={dbPath}"));

builder.Services.AddHttpLogging(options =>
{
    options.LoggingFields = HttpLoggingFields.RequestHeaders
                            | HttpLoggingFields.ResponseHeaders
                            | HttpLoggingFields.RequestBody
                            | HttpLoggingFields.ResponseBody;
});

var app = builder.Build();

//app.UseMiddleware<RequestLoggingMiddleware>();

app.Use(async (context, next) =>
{
    var userAgent = context.Request.Headers.UserAgent.ToString();
    if (userAgent.Contains("Edg"))
    {
        await next();
    }
    else
    {
        await context.Response.WriteAsJsonAsync("Only Edge is supported");
    }
});
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseHttpLogging();

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

app.UseAuthorization();
app.MapControllers();

app.Run();