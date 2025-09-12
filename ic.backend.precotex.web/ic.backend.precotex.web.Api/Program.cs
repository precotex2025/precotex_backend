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

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
        "https://gestion.precotex.com"
        )  // Especifica el origen permitido
        .AllowAnyHeader()                     // Permitir cualquier encabezado
        .AllowAnyMethod();                    // Permitir cualquier m�todo (GET, POST, etc.)

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

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
app.UseSwagger();
app.UseSwaggerUI();
}

// Usa CORS antes de las rutas
app.UseCors("AllowAngularApp");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
