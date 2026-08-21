using ic.backend.precotex.web.Api.Controllers.Tintoreria;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.StaticFiles;
using ic.backend.precotex.web.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

// ========================================
// SERVICES
// ========================================

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "AllAlongAnApp", Version = "v1" });
});

// ========================================
// CORS
// ========================================

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp", policy =>
    {
        /*DESARROLLO*/

        policy.WithOrigins("http://localhost:4200")  // Especifica el origen permitido
              .AllowAnyHeader()                     // Permitir cualquier encabezado
              .AllowAnyMethod();                   // Permitir cualquier m�todo (GET, POST, etc.)

        /*PRODUCCION*/

        //policy.WithOrigins(
        //"http://192.168.1.36",
        //"https://192.168.1.36",
        //"https://gestion.precotex.com",
        //"https://gestion.precotex.com:444"
        //)  // Especifica el origen permitido
        //.AllowAnyHeader()                     // Permitir cualquier encabezado
        //.AllowAnyMethod();                   // Permitir cualquier m�todo (GET, POST, etc.) 

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

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors("AllowAngularApp");

app.UseAuthorization();

app.UseStaticFiles(new StaticFileOptions { 
    ContentTypeProvider = new FileExtensionContentTypeProvider { 
        Mappings = { [".webmanifest"] = "application/manifest.json" } 
    } 
});

app.MapControllers();

app.Run();
