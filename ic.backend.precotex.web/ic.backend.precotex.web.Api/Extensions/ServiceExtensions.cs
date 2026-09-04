using ic.backend.precotex.web.Service.Services.Administracion.AccesoUsuario;
using ic.backend.precotex.web.Service.Services.AgendaTelefonica;
using ic.backend.precotex.web.Service.Services.Almacen;
using ic.backend.precotex.web.Service.Services.AzurePowerBI;
using ic.backend.precotex.web.Service.Services.CalificacionrollosEnProceso;
using ic.backend.precotex.web.Service.Services.CalificacionrollosFinal;
using ic.backend.precotex.web.Service.Services.CorteEncogimiento;
using ic.backend.precotex.web.Service.Services.Cotizaciones;
using ic.backend.precotex.web.Service.Services.DDT;
using ic.backend.precotex.web.Service.Services.HelpCommon;
using ic.backend.precotex.web.Service.Services.Implementacion.Administracion.AccesoUsuario;
using ic.backend.precotex.web.Service.Services.Implementacion.AgendaTelefonica;
using ic.backend.precotex.web.Service.Services.Implementacion.Almacen;
using ic.backend.precotex.web.Service.Services.Implementacion.AzurePowerBI;
using ic.backend.precotex.web.Service.Services.Implementacion.CalificacionRollosEnProceso;
using ic.backend.precotex.web.Service.Services.Implementacion.CalificacionRollosFinal;
using ic.backend.precotex.web.Service.Services.Implementacion.CorteEncogimiento;
using ic.backend.precotex.web.Service.Services.Implementacion.Cotizaciones;
using ic.backend.precotex.web.Service.Services.Implementacion.DDT;
using ic.backend.precotex.web.Service.Services.Implementacion.HelpCommon;
using ic.backend.precotex.web.Service.Services.Implementacion.Laboratorio;
using ic.backend.precotex.web.Service.Services.Implementacion.Login;
using ic.backend.precotex.web.Service.Services.Implementacion.Mantto;
using ic.backend.precotex.web.Service.Services.Implementacion.Memorandum;
using ic.backend.precotex.web.Service.Services.Implementacion.OYM;
using ic.backend.precotex.web.Service.Services.Implementacion.Personas;
using ic.backend.precotex.web.Service.Services.Implementacion.QuejasReclamos;
using ic.backend.precotex.web.Service.Services.Implementacion.RegistroPartidaParihuela;
using ic.backend.precotex.web.Service.Services.Implementacion.ReporteNC;
using ic.backend.precotex.web.Service.Services.Implementacion.RetiroRepuestos;
using ic.backend.precotex.web.Service.Services.Implementacion.SecureNorm;
using ic.backend.precotex.web.Service.Services.Implementacion.SolicitudMantenimiento;
using ic.backend.precotex.web.Service.Services.Implementacion.Tejeduria;
using ic.backend.precotex.web.Service.Services.Implementacion.Tintoreria;
using ic.backend.precotex.web.Service.Services.Implementacion.WallyChat;
using ic.backend.precotex.web.Service.Services.Laboratorio;
using ic.backend.precotex.web.Service.Services.Login;
using ic.backend.precotex.web.Service.Services.Mantto;
using ic.backend.precotex.web.Service.Services.Memorandum;
using ic.backend.precotex.web.Service.Services.OYM;
using ic.backend.precotex.web.Service.Services.Personas;
using ic.backend.precotex.web.Service.Services.QuejasReclamos;
using ic.backend.precotex.web.Service.Services.RegistroPartidaParihuela;
using ic.backend.precotex.web.Service.Services.ReporteNC;
using ic.backend.precotex.web.Service.Services.RetiroRepuestos;
using ic.backend.precotex.web.Service.Services.SecureNorm;
using ic.backend.precotex.web.Service.Services.SolicitudMantenimiento;
using ic.backend.precotex.web.Service.Services.Tejeduria;
using ic.backend.precotex.web.Service.Services.Tintoreria;
using ic.backend.precotex.web.Service.Services.WallyChat;
using ic.backend.precotex.web.Service;

namespace ic.backend.precotex.web.Api.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<ITxBultoHiladoService, TxBultoHiladoService>();
            services.AddScoped<ITxBultoHiladoGrupoService, TxBultoHiladoGrupoService>();
            services.AddScoped<ITxUbicacionService, TxUbicacionService>();
            services.AddScoped<ITmpVisorPermanenciaTelaCrudaService, TmpVisorPermanenciaTelaCrudaService>();
            services.AddScoped<ITiProcesosTintoreriaService, TiProcesosTintoreriaService>();
            services.AddScoped<IPowerBiTokenService, PowerBiTokenService>();
            services.AddScoped<ITxTelaEstructuraTejidoItemsService, TxTelaEstructuraTejidoItemsService>();
            services.AddScoped<ITxCtrolInventarioHiloTejeduriaService, TxCtrolInventarioHiloTejeduriaService>();
            services.AddScoped<ICorteEncogimientoService, SCorteencogimientoService>();
            services.AddScoped<IRegistroPartidaParihuelaService, SRegistroPartidaParihuela>();
            services.AddScoped<IQuejasReclamosService, SQuejasReclamos>();
            services.AddScoped<ICalificacionRollosEnProcesoService, SCalificacionRollosEnProceso>();
            services.AddScoped<ITxUsuarioSedeService, TxUsuarioSedeService>();
            services.AddScoped<ITxProcesoColgadorRegistroService, TxProcesoColgadorRegistroService>();
            services.AddScoped<IHelpCommonService, HelpCommonService>();
            services.AddScoped<ITxUbicacionColgadorService, TxUbicacionColgadorService>();
            services.AddScoped<ICalificacionRollosFinalService, SCalificacionRolloFinal>();
            services.AddScoped<IPartidaQRService, PartidaQRService>();
            services.AddScoped<ITxProcesoMemorandumService, TxProcesoMemorandumService>();
            services.AddScoped<ITxRetiroRepuestosService, TxRetiroRepuestosService>();
            services.AddScoped<ILbColaTrabajoService, LbColaTrabajoService>();
            services.AddScoped<ITxLoginService, TxLoginService>();
            services.AddScoped<ITxReporteNCService, TxReporteNCService>();
            services.AddScoped<ITMSolicitudMantenimientoService, TMSolicitudMantenimientoService>();
            services.AddScoped<IWaliChatService, WaliChatService>();
            services.AddScoped<ITxDesarrolloTelaService, TxDesarrolloTelaService>();
            services.AddScoped<ITjTiempoImproductivoService, TjTiempoImproductivoService>();
            services.AddScoped<ITxCotizacionesService, TxCotizacionesService>();
            services.AddScoped<ITxPersonasService, TxPersonasService>();
            services.AddScoped<ISNNormaService, SNNormaService>();
            services.AddScoped<ISNOrganizacionService, SNOrganizacionService>();
            services.AddScoped<IGenerateImageDinamycService, GenerateImageDinamycService>();
            services.AddScoped<ISNSedeService, SNSedeService>();
            services.AddScoped<ISNProcesoService, SNProcesoService>();
            services.AddScoped<ISNDocumentosControladosService, SNDocumentosControladosService>();
            services.AddScoped<ISNPuestoService, SNPuestoService>();
            services.AddScoped<ISNPermisoService, SNPermisoService>();
            services.AddScoped<ISNIndicadorService, SNIndicadorService>();
            services.AddScoped<ISNMejoraService, SNMejoraService>();
            services.AddScoped<ICnAgendaService, CnAgendaService>();
            services.AddScoped<IMaeTabService, MaeTabService>();
            services.AddScoped<IPrimeraPartidaService, PrimeraPartidaService>();
            services.AddScoped<ILecturaBultosService, LecturaBultosService>();
            services.AddScoped<ITjSeguimientoSaldoHiloService, TjSeguimientoSaldoHiloService>();
            services.AddScoped<ITjSolicitudDevolucionAuditoriaService, TjSolicitudDevolucionAuditoriaService>();
            services.AddScoped<ISNAuditoriaService, SNAuditoriaService>();
            services.AddScoped<ISNNoConformidadService, SNNoConformidadService>();
            services.AddScoped<ISNObjetivoService, SNObjetivoService>();
            services.AddScoped<ISNRiesgoService, SNRiesgoService>();
            
            services.AddScoped<ISNManualService, SNManualService>();
            services.AddScoped<IAccesoUsuarioService, AccesoUsuarioService>();
            services.AddScoped<ISNReqLegalService, SNReqLegalService>();

            return services;
        }

    }
}
