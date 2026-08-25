using ic.backend.precotex.web.Api.Controllers.Tintoreria;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.StaticFiles;
using ic.backend.precotex.web.Api.Extensions;
using Microsoft.OpenApi.Models;
using ic.backend.precotex.web.Api.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// ========================================
// SERVICES
// ========================================

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "AllAlongAnApp", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Ingresa solo el token JWT, sin el prefijo 'Bearer '."
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
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

// ========================================
// CORS
// ========================================

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp", policy =>
    {
        /*DESARROLLO*/
        /*
        policy.WithOrigins("http://localhost:4200")  // Especifica el origen permitido
              .AllowAnyHeader()                     // Permitir cualquier encabezado
              .AllowAnyMethod();                   // Permitir cualquier m�todo (GET, POST, etc.)
        */

        /*PRODUCCION*/
        
        policy.WithOrigins(
        "http://192.168.1.36",
        "https://192.168.1.36",
        "https://gestion.precotex.com",
        "https://gestion.precotex.com:444"
        )  // Especifica el origen permitido
        .AllowAnyHeader()                     // Permitir cualquier encabezado
        .AllowAnyMethod();                   // Permitir cualquier m�todo (GET, POST, etc.) 
       
    });
});

// ========================================
// HTTP CLIENT
// ========================================

builder.Services.AddHttpClient<TiProcesosTintoreriaController>();

// ========================================
// FORM OPTIONS
// ========================================

builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 50 * 1024 * 1024; // 50 MB
});

// ========================================
// DEPENDENCY INJECTION
// Services + Repositories
// ========================================

builder.Services.AddApplication();

builder.Services.AddSingleton<IJwtTokenService, JwtTokenService>();

// ========================================
// AUTHENTICATION (JWT)
// ========================================

var jwtSection = builder.Configuration.GetSection("Jwt");
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSection["Issuer"],
            ValidAudience = jwtSection["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSection["Key"]!))
        };
    });

builder.Services.AddAuthorization();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "BackEnd v1");
    });
}

// ========================================
// MIDDLEWARE
// ========================================

app.UseHttpsRedirection(); //*****************Este de debe de descomentar para producción

app.UseRouting();

app.UseCors("AllowAngularApp");

app.UseAuthentication();

app.UseAuthorization();

app.UseStaticFiles(new StaticFileOptions { 
    ContentTypeProvider = new FileExtensionContentTypeProvider { 
        Mappings = { [".webmanifest"] = "application/manifest.json" } 
    } 
});

app.MapControllers();

app.Run();
