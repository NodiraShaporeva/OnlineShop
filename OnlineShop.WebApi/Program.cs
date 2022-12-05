using Microsoft.EntityFrameworkCore;
using OnlineShop.Data;
using OnlineShop.Data.Repositories;
using OnlineShop.Domain.RepositoriesInterfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddCors();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();

var dbPath = "myapp.db";

builder.Services.AddDbContext<AppDbContext>
    (options => options.UseSqlite($"Data Source={dbPath}"));

var app = builder.Build();

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

app.MapControllers();

app.Run();