//using Microsoft.AspNetCore.Mvc;
//using OnlineShop.Models;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Data;
using OnlineShop.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddCors();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
//builder.Services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));

var dbPath = "myapp.db";

builder.Services.AddDbContext<AppDbContext>
    (options => options.UseSqlite($"Data Source={dbPath}"));

var app = builder.Build();

app.MapControllers();
/*
app.MapGet("/get_all", async (
        [FromServices] IRepository<IEntity> repo,
        CancellationToken cancellationToken)
    =>
{
    var products = await repo.GetAll(cancellationToken);
    return products;
});

app.MapPost("/add", async (
        [FromBody] Product product,
        IRepository<IEntity> repo,
        CancellationToken cancellationToken)
    =>
{
    await repo.Add(product, cancellationToken);
});

app.MapGet("/get_by_id", async (
        [FromQuery] Guid productId,
        IRepository<IEntity> repo,
        CancellationToken cancellationToken)
    =>
{
    await repo.GetById(productId, cancellationToken);
});

app.MapPut("/update", async (
            [FromBody] Product newProduct,
            IRepository<IEntity> repo,
            CancellationToken cancellationToken)
        =>
    {
        await repo.Update(newProduct, cancellationToken);
    }
);

app.MapDelete("/delete", async (
        [FromBody] Product product,
        IRepository<IEntity> repo,
        CancellationToken cancellationToken)
    =>
{
    await repo.Delete(product, cancellationToken);
});
*/
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

app.UseAuthorization();

app.MapControllers();

app.Run();