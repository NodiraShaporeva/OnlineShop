using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using OnlineShop.Data;
using OnlineShop.Data.Repositories;
using OnlineShop.Domain;
using OnlineShop.Domain.RepositoriesInterfaces;
using OnlineShop.Domain.Services;
using OnlineShop.WebApi.Configurations;
using OnlineShop.WebApi.Middleware;
using OnlineShop.WebApi.Services;
using OnlineShop.WebApi.TokenHelpers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddCors();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddScoped<AccountService>();
builder.Services.Configure<PasswordHasherOptions>(opt => opt.IterationCount = 10_000);
builder.Services.AddSingleton<IPasswordHasherService, Pbkdf2PasswordHasher>();
builder.Services.AddScoped<JwtTokenService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

var dbPath = "myapp.db";
builder.Services.AddDbContext<AppDbContext>
    (options => options.UseSqlite($"Data Source={dbPath}"));

JwtConfig jwtConfig = builder.Configuration
    .GetSection("JwtConfig")
    .Get<JwtConfig>()!;
// value cannot be null
// в противном случае может возникнуть проблема при запуске приложения
// при отсутствии пользовательских секретов с JWT Token
if (jwtConfig == null)
{
    throw new ArgumentNullException(nameof(jwtConfig));
}
builder.Services.AddSingleton(jwtConfig);
builder.Services.AddSingleton(jwtConfig);

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            IssuerSigningKey = new SymmetricSecurityKey(jwtConfig.SigningKeyBytes),
            ValidateIssuerSigningKey = true,
            ValidateLifetime = true,
            RequireExpirationTime = true,
            RequireSignedTokens = true,
          
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidAudiences = new[] { jwtConfig.Audience },
            ValidIssuer = jwtConfig.Issuer
        };
    });
builder.Services.AddAuthorization();


builder.Services.AddHttpLogging(options =>
{
    options.LoggingFields = HttpLoggingFields.RequestHeaders
                            | HttpLoggingFields.ResponseHeaders;
});

builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme() {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

var app = builder.Build();

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

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpLogging();

app.UseCors(policy =>
{
    policy
        //.WithOrigins("https://localhost:7258")
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowAnyOrigin()
        ;
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseMiddleware<PagesTransitionsMiddleware>();

app.Run();