
using System;
using System.Security.Claims;
using System.Text;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

using MiniSupermarket.API.Data;

var builder = WebApplication.CreateBuilder(args);

// 1. Cau hinh ket noi SQL Server
var connectionString =
    builder.Configuration.GetConnectionString("DefaultConnection");

if (string.IsNullOrWhiteSpace(connectionString))
{
    throw new InvalidOperationException(
        "Chua cau hinh ConnectionStrings:DefaultConnection trong appsettings.json.");
}

builder.Services.AddDbContext<SupermarketDbContext>(options =>
    options.UseSqlServer(connectionString));

// 2. Doc va kiem tra cau hinh JWT
var jwtSettings = builder.Configuration.GetSection("JwtSettings");

var secret = jwtSettings["Secret"];
var issuer = jwtSettings["Issuer"];
var audience = jwtSettings["Audience"];

if (string.IsNullOrWhiteSpace(secret))
{
    throw new InvalidOperationException(
        "Chua cau hinh JwtSettings:Secret trong appsettings.json.");
}

var secretKey = Encoding.UTF8.GetBytes(secret);

if (secretKey.Length < 32)
{
    throw new InvalidOperationException(
        "JwtSettings:Secret phai co it nhat 32 byte khi ma hoa UTF-8.");
}

if (string.IsNullOrWhiteSpace(issuer))
{
    throw new InvalidOperationException(
        "Chua cau hinh JwtSettings:Issuer trong appsettings.json.");
}

if (string.IsNullOrWhiteSpace(audience))
{
    throw new InvalidOperationException(
        "Chua cau hinh JwtSettings:Audience trong appsettings.json.");
}

// 3. Dang ky JWT Authentication
builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme =
            JwtBearerDefaults.AuthenticationScheme;

        options.DefaultChallengeScheme =
            JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters =
            new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = issuer,

                ValidateAudience = true,
                ValidAudience = audience,

                ValidateLifetime = true,
                RequireExpirationTime = true,
                ClockSkew = TimeSpan.FromMinutes(1),

                ValidateIssuerSigningKey = true,
                IssuerSigningKey =
                    new SymmetricSecurityKey(secretKey),

                NameClaimType = ClaimTypes.Name,
                RoleClaimType = ClaimTypes.Role
            };
    });

// 4. Dang ky Authorization va Controllers
builder.Services.AddAuthorization();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// 5. Cau hinh Swagger ho tro JWT Bearer Token
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "MiniSupermarket.API",
        Version = "v1",
        Description = "API quan ly ban hang sieu thi mini Sakura"
    });

    options.AddSecurityDefinition("Bearer",
        new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Description = "Nhap JWT Token de xac thuc API.",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.Http,
            Scheme = "bearer",
            BearerFormat = "JWT"
        });

    options.AddSecurityRequirement(
        new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                Array.Empty<string>()
            }
        });
});

// 6. Cau hinh CORS cho client
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

var app = builder.Build();

// 7. Bat Swagger trong moi truong Development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

// Thu tu middleware xac thuc va phan quyen
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
