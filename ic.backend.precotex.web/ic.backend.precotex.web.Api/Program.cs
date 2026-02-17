using ic.backend.precotex.web.Api.Controllers.Tintoreria;
using ic.backend.precotex.web.Data.Repositories.Almacen;
using ic.backend.precotex.web.Data.Repositories.CalificacionRollosEnProceso;
using ic.backend.precotex.web.Data.Repositories.CalificacionRollosFinal;
using ic.backend.precotex.web.Data.Repositories.CorteEncogimiento;
using ic.backend.precotex.web.Data.Repositories.DDT;
using ic.backend.precotex.web.Data.Repositories.Implementation.Almacen;
using ic.backend.precotex.web.Data.Repositories.Implementation.CalificacionRollosEnProceso;
using ic.backend.precotex.web.Data.Repositories.Implementation.CalificacionRollosFinal;
using ic.backend.precotex.web.Data.Repositories.Implementation.CorteEncogimiento;
using ic.backend.precotex.web.Data.Repositories.Implementation.DDT;
using ic.backend.precotex.web.Data.Repositories.Implementation.Mantto;
using ic.backend.precotex.web.Data.Repositories.Implementation.Memorandum;
using ic.backend.precotex.web.Data.Repositories.Implementation.OYM;
using ic.backend.precotex.web.Data.Repositories.Implementation.QuejasReclamos;
using ic.backend.precotex.web.Data.Repositories.Implementation.RegistroPartidaParihuela;
using ic.backend.precotex.web.Data.Repositories.Implementation.Tejeduria;
using ic.backend.precotex.web.Data.Repositories.Implementation.Tintoreria;
using ic.backend.precotex.web.Data.Repositories.Mantto;
using ic.backend.precotex.web.Data.Repositories.Memorandum;
using ic.backend.precotex.web.Data.Repositories.OYM;
using ic.backend.precotex.web.Data.Repositories.QuejasReclamos;
using ic.backend.precotex.web.Data.Repositories.RegistroPartidaParihuela;
using ic.backend.precotex.web.Data.Repositories.Tejeduria;
using ic.backend.precotex.web.Data.Repositories.Tintoreria;
using ic.backend.precotex.web.Service.Services.Almacen;
using ic.backend.precotex.web.Service.Services.AzurePowerBI;
using ic.backend.precotex.web.Service.Services.CalificacionrollosEnProceso;
using ic.backend.precotex.web.Service.Services.CalificacionrollosFinal;
using ic.backend.precotex.web.Service.Services.CorteEncogimiento;
using ic.backend.precotex.web.Service.Services.DDT;
using ic.backend.precotex.web.Service.Services.HelpCommon;
using ic.backend.precotex.web.Service.Services.Implementacion.Almacen;
using ic.backend.precotex.web.Service.Services.Implementacion.AzurePowerBI;
using ic.backend.precotex.web.Service.Services.Implementacion.CalificacionRollosEnProceso;
using ic.backend.precotex.web.Service.Services.Implementacion.CalificacionRollosFinal;
using ic.backend.precotex.web.Service.Services.Implementacion.CorteEncogimiento;
using ic.backend.precotex.web.Service.Services.Implementacion.DDT;
using ic.backend.precotex.web.Service.Services.Implementacion.HelpCommon;
using ic.backend.precotex.web.Service.Services.Implementacion.Mantto;
using ic.backend.precotex.web.Service.Services.Implementacion.Memorandum;
using ic.backend.precotex.web.Service.Services.Implementacion.OYM;
using ic.backend.precotex.web.Service.Services.Implementacion.QuejasReclamos;
using ic.backend.precotex.web.Service.Services.Implementacion.RegistroPartidaParihuela;
using ic.backend.precotex.web.Service.Services.Implementacion.Tejeduria;
using ic.backend.precotex.web.Service.Services.Implementacion.Tintoreria;
using ic.backend.precotex.web.Service.Services.Mantto;
using ic.backend.precotex.web.Service.Services.Memorandum;
using ic.backend.precotex.web.Service.Services.OYM;
using ic.backend.precotex.web.Service.Services.QuejasReclamos;
using ic.backend.precotex.web.Service.Services.RegistroPartidaParihuela;
using ic.backend.precotex.web.Service.Services.Tejeduria;
using ic.backend.precotex.web.Service.Services.Tintoreria;
using ic.backend.precotex.web.Service.Services.RetiroRepuestos;
using Microsoft.AspNetCore.Http.Features;
using ic.backend.precotex.web.Service.Services.Implementacion.RetiroRepuestos;
using ic.backend.precotex.web.Data.Repositories.Implementation.RetiroRepuestos;
using ic.backend.precotex.web.Data.Repositories.RetiroRepuestos;
using ic.backend.precotex.web.Service.Services.Implementacion.Laboratorio;
using ic.backend.precotex.web.Data.Repositories.Implementation.Laboratorio;
using ic.backend.precotex.web.Service.Services.Laboratorio;
using ic.backend.precotex.web.Data.Repositories.Laboratorio;
using ic.backend.precotex.web.Service.Services.Implementacion.Login;
using ic.backend.precotex.web.Data.Repositories.Implementation.Login;
using ic.backend.precotex.web.Service.Services.Login;
using ic.backend.precotex.web.Data.Repositories.Login;
using ic.backend.precotex.web.Service.Services.Implementacion.ReporteNC;
using ic.backend.precotex.web.Data.Repositories.Implementation.ReporteNC;
using ic.backend.precotex.web.Service.Services.ReporteNC;
using ic.backend.precotex.web.Data.Repositories.ReporteNC;
using Microsoft.Extensions.FileProviders;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;
using Microsoft.AspNetCore.Builder;
using ic.backend.precotex.web.Service.Services.Implementacion.SolicitudMantenimiento;
using ic.backend.precotex.web.Service.Services.SolicitudMantenimiento;
using ic.backend.precotex.web.Data.Repositories.Implementation.SolicitudMantenimiento;
using ic.backend.precotex.web.Data.Repositories.SolicitudMantenimiento;
using ic.backend.precotex.web.Service.Services.Implementacion.WallyChat;
using ic.backend.precotex.web.Service.Services.WallyChat;
using ic.backend.precotex.web.Service.Services.Implementacion.Cotizaciones;
using ic.backend.precotex.web.Data.Repositories.Implementation.Cotizaciones;
using ic.backend.precotex.web.Data.Repositories.Cotizaciones;
using ic.backend.precotex.web.Service.Services.Cotizaciones;
using Microsoft.AspNetCore.StaticFiles;
using ic.backend.precotex.web.Data.Repositories.SecureNorm;
using ic.backend.precotex.web.Data.Repositories.Implementation.SecureNorm;
using ic.backend.precotex.web.Service.Services.SecureNorm;
using ic.backend.precotex.web.Service.Services.Implementacion.SecureNorm;
using ic.backend.precotex.web.Service.Services.Implementacion.Personas;
using ic.backend.precotex.web.Service.Services.Personas;
using ic.backend.precotex.web.Data.Repositories.Implementation.Personas;
using ic.backend.precotex.web.Data.Repositories.Personas;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "AllAlongAnApp", Version = "v1" });
});


// Configuraci�n de CORS
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

#region INYECTION DEPENDECY

builder.Services.AddHttpClient<TiProcesosTintoreriaController>();

//Inyection Services
builder.Services.AddScoped<ITxBultoHiladoService, TxBultoHiladoService>();
builder.Services.AddScoped<ITxBultoHiladoGrupoService, TxBultoHiladoGrupoService>();
builder.Services.AddScoped<ITxUbicacionService, TxUbicacionService>();
builder.Services.AddScoped<ITmpVisorPermanenciaTelaCrudaService, TmpVisorPermanenciaTelaCrudaService>();
builder.Services.AddScoped<ITiProcesosTintoreriaService, TiProcesosTintoreriaService>();
builder.Services.AddScoped<IPowerBiTokenService, PowerBiTokenService>();
builder.Services.AddScoped<ITxTelaEstructuraTejidoItemsService, TxTelaEstructuraTejidoItemsService>();
builder.Services.AddScoped<ITxCtrolInventarioHiloTejeduriaService, TxCtrolInventarioHiloTejeduriaService>();
builder.Services.AddScoped<ICorteEncogimientoService, SCorteencogimientoService>();
builder.Services.AddScoped<IRegistroPartidaParihuelaService, SRegistroPartidaParihuela>();
builder.Services.AddScoped<IQuejasReclamosService, SQuejasReclamos>();
builder.Services.AddScoped<ICalificacionRollosEnProcesoService, SCalificacionRollosEnProceso>();
builder.Services.AddScoped<ITxUsuarioSedeService, TxUsuarioSedeService>();
builder.Services.AddScoped<ITxProcesoColgadorRegistroService, TxProcesoColgadorRegistroService>();
builder.Services.AddScoped<IHelpCommonService, HelpCommonService>();
builder.Services.AddScoped<ITxUbicacionColgadorService, TxUbicacionColgadorService>();
builder.Services.AddScoped<ICalificacionRollosFinalService, SCalificacionRolloFinal>();
builder.Services.AddScoped<IPartidaQRService, PartidaQRService>();
builder.Services.AddScoped<ITxProcesoMemorandumService, TxProcesoMemorandumService>();
builder.Services.AddScoped<ITxRetiroRepuestosService, TxRetiroRepuestosService>();
builder.Services.AddScoped<ILbColaTrabajoService, LbColaTrabajoService>();
builder.Services.AddScoped<ITxLoginService, TxLoginService>();
builder.Services.AddScoped<ITxReporteNCService, TxReporteNCService>();
builder.Services.AddScoped<ITMSolicitudMantenimientoService, TMSolicitudMantenimientoService>();
builder.Services.AddScoped<IWaliChatService, WaliChatService>();
builder.Services.AddScoped<ITxDesarrolloTelaService, TxDesarrolloTelaService>();
builder.Services.AddScoped<ITjTiempoImproductivoService, TjTiempoImproductivoService>();
builder.Services.AddScoped<ITxCotizacionesService, TxCotizacionesService>();
builder.Services.AddScoped<ITxPersonasService, TxPersonasService>();

builder.Services.AddScoped<ISNNormaService, SNNormaService>();
builder.Services.AddScoped<ISNOrganizacionService, SNOrganizacionService>();

//Inyection Repository
builder.Services.AddScoped<ITxBultoHiladoRepository, TxBultoHiladoRepository>();
builder.Services.AddScoped<ITxBultoHiladoGrupoRepository, TxBultoHiladoGrupoRepository>();
builder.Services.AddScoped<ITxUbicacionRepository, TxUbicacionRepository>();
builder.Services.AddScoped<ITmpVisorPermanenciaTelaCrudaRepository, TmpVisorPermanenciaTelaCrudaRepository>();
builder.Services.AddScoped<ITiProcesosTintoreriaRepository, TiProcesosTintoreriaRepository>();
builder.Services.AddScoped<ITxTelaEstructuraTejidoItemsRepository, TxTelaEstructuraTejidoItemsRepository>();
builder.Services.AddScoped<ITxCtrolInventarioHiloTejeduriaRepository, TxCtrolInventarioHiloTejeduriaRepository>();
builder.Services.AddScoped<ICorteEncogimiento, DCorteEncogimiento>();
builder.Services.AddScoped<IRegistroPartidaParihuela, DRegistroPartidaParihuela>();
builder.Services.AddScoped<IQuejasReclamos, DQuejasReclamos>();
builder.Services.AddScoped<ICalificacionRollosEnProceso, DCalificacionRollosEnProceso>();
builder.Services.AddScoped<ICalificacionRolloFinal, DCalificacionRolloFinal>();
builder.Services.Configure<FormOptions>(options =>
{
options.MultipartBodyLengthLimit = 50 * 1024 * 1024; // 50 MB
});
builder.Services.AddScoped<ITxUsuarioSedeRepository, TxUsuarioSedeRepository>();
builder.Services.AddScoped<ITxProcesoColgadorRegistroRepository, TxProcesoColgadorRegistroRepository>();
builder.Services.AddScoped<ITxUbicacionColgadorRepository, TxUbicacionColgadorRepository>();
builder.Services.AddScoped<IPartidaQRRepository, PartidaQRRepository>();
builder.Services.AddScoped<ITxProcesoMemorandumRepository, TxProcesoMemorandumRepository>();
builder.Services.AddScoped<ITxRetiroRepuestosRepository, TxRetiroRepuestosRepository>();
builder.Services.AddScoped<ILbColaTrabajoRepository, LbColaTrabajoRepository>();
builder.Services.AddScoped<ITxLoginRepository, TxLoginRepository>();
builder.Services.AddScoped<ITxReporteNCRepository, TxReporteNCRepository>();
builder.Services.AddScoped<ITMSolicitudMantenimientoRepository, TMSolicitudMantenimientoRepository>();
builder.Services.AddScoped<ITxDesarrolloTelaRepository, TxDesarrolloTelaRepository>();
builder.Services.AddScoped<ITjTiempoImproductivoRepository, TjTiempoImproductivoRepository>();
builder.Services.AddScoped<ITxCotizacionesRepository, TxCotizacionesRepository>();

builder.Services.AddScoped<ISNNormaRepository, SNNormaRepository>();
builder.Services.AddScoped<ISNOrganizacionRepository, SNOrganizacionRepository>();
builder.Services.AddScoped<ITxPersonasRepository, TxPersonasRepository>();
#endregion




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "BackEnd v1");
    });
}

//app.UseStaticFiles(new StaticFileOptions
//{
//    //FileProvider = new PhysicalFileProvider(@"\\fileserverprx\imagenesretiro$"),
//    FileProvider = new PhysicalFileProvider(@"D:\htdocs\app\foto"),
//    RequestPath = "/imagenes"
//});

// Usa CORS antes de las rutas

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
