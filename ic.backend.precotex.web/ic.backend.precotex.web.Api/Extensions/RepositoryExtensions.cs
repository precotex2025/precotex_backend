using ic.backend.precotex.web.Data.Repositories.Administracion.AccesoUsuario;
using ic.backend.precotex.web.Data.Repositories.AgendaTelefonica;
using ic.backend.precotex.web.Data;
using ic.backend.precotex.web.Data.Repositories.Almacen;
using ic.backend.precotex.web.Data.Repositories.CalificacionRollosEnProceso;
using ic.backend.precotex.web.Data.Repositories.CalificacionRollosFinal;
using ic.backend.precotex.web.Data.Repositories.CorteEncogimiento;
using ic.backend.precotex.web.Data.Repositories.Cotizaciones;
using ic.backend.precotex.web.Data.Repositories.DDT;
using ic.backend.precotex.web.Data.Repositories.Implementation.Administracion.AccesoUsuario;
using ic.backend.precotex.web.Data.Repositories.Implementation.AgendaTelefonica;
using ic.backend.precotex.web.Data.Repositories.Implementation.Almacen;
using ic.backend.precotex.web.Data.Repositories.Implementation.CalificacionRollosEnProceso;
using ic.backend.precotex.web.Data.Repositories.Implementation.CalificacionRollosFinal;
using ic.backend.precotex.web.Data.Repositories.Implementation.CorteEncogimiento;
using ic.backend.precotex.web.Data.Repositories.Implementation.Cotizaciones;
using ic.backend.precotex.web.Data.Repositories.Implementation.DDT;
using ic.backend.precotex.web.Data.Repositories.Implementation.Laboratorio;
using ic.backend.precotex.web.Data.Repositories.Implementation.Login;
using ic.backend.precotex.web.Data.Repositories.Implementation.Mantto;
using ic.backend.precotex.web.Data.Repositories.Implementation.Memorandum;
using ic.backend.precotex.web.Data.Repositories.Implementation.OYM;
using ic.backend.precotex.web.Data.Repositories.Implementation.Personas;
using ic.backend.precotex.web.Data.Repositories.Implementation.QuejasReclamos;
using ic.backend.precotex.web.Data.Repositories.Implementation.RegistroPartidaParihuela;
using ic.backend.precotex.web.Data.Repositories.Implementation.ReporteNC;
using ic.backend.precotex.web.Data.Repositories.Implementation.RetiroRepuestos;
using ic.backend.precotex.web.Data.Repositories.Implementation.SecureNorm;
using ic.backend.precotex.web.Data.Repositories.Implementation.SolicitudMantenimiento;
using ic.backend.precotex.web.Data.Repositories.Implementation.Tejeduria;
using ic.backend.precotex.web.Data.Repositories.Implementation.Tintoreria;
using ic.backend.precotex.web.Data.Repositories.Laboratorio;
using ic.backend.precotex.web.Data.Repositories.Login;
using ic.backend.precotex.web.Data.Repositories.Mantto;
using ic.backend.precotex.web.Data.Repositories.Memorandum;
using ic.backend.precotex.web.Data.Repositories.OYM;
using ic.backend.precotex.web.Data.Repositories.Personas;
using ic.backend.precotex.web.Data.Repositories.QuejasReclamos;
using ic.backend.precotex.web.Data.Repositories.RegistroPartidaParihuela;
using ic.backend.precotex.web.Data.Repositories.ReporteNC;
using ic.backend.precotex.web.Data.Repositories.RetiroRepuestos;
using ic.backend.precotex.web.Data.Repositories.SecureNorm;
using ic.backend.precotex.web.Data.Repositories.SolicitudMantenimiento;
using ic.backend.precotex.web.Data.Repositories.Tejeduria;
using ic.backend.precotex.web.Data.Repositories.Tintoreria;

namespace ic.backend.precotex.web.Api.Extensions
{
    public static class RepositoryExtensions
    {
        public static IServiceCollection AddApplicationRepositories(this IServiceCollection services)
        {
            services.AddScoped<ITxBultoHiladoRepository, TxBultoHiladoRepository>();
            services.AddScoped<ITxBultoHiladoGrupoRepository, TxBultoHiladoGrupoRepository>();
            services.AddScoped<ITxUbicacionRepository, TxUbicacionRepository>();
            services.AddScoped<ITmpVisorPermanenciaTelaCrudaRepository, TmpVisorPermanenciaTelaCrudaRepository>();
            services.AddScoped<ITiProcesosTintoreriaRepository, TiProcesosTintoreriaRepository>();
            services.AddScoped<ITxTelaEstructuraTejidoItemsRepository, TxTelaEstructuraTejidoItemsRepository>();
            services.AddScoped<ITxCtrolInventarioHiloTejeduriaRepository, TxCtrolInventarioHiloTejeduriaRepository>();
            services.AddScoped<ICorteEncogimiento, DCorteEncogimiento>();
            services.AddScoped<IRegistroPartidaParihuela, DRegistroPartidaParihuela>();
            services.AddScoped<IQuejasReclamos, DQuejasReclamos>();
            services.AddScoped<ICalificacionRollosEnProceso, DCalificacionRollosEnProceso>();
            services.AddScoped<ICalificacionRolloFinal, DCalificacionRolloFinal>();
            services.AddScoped<ITxUsuarioSedeRepository, TxUsuarioSedeRepository>();
            services.AddScoped<ITxProcesoColgadorRegistroRepository, TxProcesoColgadorRegistroRepository>();
            services.AddScoped<ITxUbicacionColgadorRepository, TxUbicacionColgadorRepository>();
            services.AddScoped<IPartidaQRRepository, PartidaQRRepository>();
            services.AddScoped<ITxProcesoMemorandumRepository, TxProcesoMemorandumRepository>();
            services.AddScoped<ITxRetiroRepuestosRepository, TxRetiroRepuestosRepository>();
            services.AddScoped<ILbColaTrabajoRepository, LbColaTrabajoRepository>();
            services.AddScoped<ITxLoginRepository, TxLoginRepository>();
            services.AddScoped<ITxReporteNCRepository, TxReporteNCRepository>();
            services.AddScoped<ITMSolicitudMantenimientoRepository, TMSolicitudMantenimientoRepository>();
            services.AddScoped<ITxDesarrolloTelaRepository, TxDesarrolloTelaRepository>();
            services.AddScoped<ITjTiempoImproductivoRepository, TjTiempoImproductivoRepository>();
            services.AddScoped<ITxCotizacionesRepository, TxCotizacionesRepository>();
            services.AddScoped<ISNNormaRepository, SNNormaRepository>();
            services.AddScoped<ISNOrganizacionRepository, SNOrganizacionRepository>();
            services.AddScoped<ITxPersonasRepository, TxPersonasRepository>();
            services.AddScoped<ISNSedeRepository, SNSedeRepository>();
            services.AddScoped<ISNProcesoRepository, SNProcesoRepository>();
            services.AddScoped<ISNDocumentosControladosRepository, SNDocumentosControladosRepository>();
            services.AddScoped<ISNPuestoRepository, SNPuestoRepository>();
            services.AddScoped<ISNPermisoRepository, SNPermisoRepository>();
            services.AddScoped<ISNIndicadorRepository, SNIndicadorRepository>();
            services.AddScoped<ISNMejoraRepository, SNMejoraRepository>();
            services.AddScoped<ICnAgendaRepository, CnAgendaRepository>();
            services.AddScoped<IMaeTabRepository, MaeTabRepository>();
            services.AddScoped<IPrimeraPartidaRepository, PrimeraPartidaRepository>();
            services.AddScoped<ILecturaBultosRepository, LecturaBultosRepository>();
            services.AddScoped<ITjSeguimientoSaldoHiloRepository, TjSeguimientoSaldoHiloRepository>();
            services.AddScoped<ITjSolicitudDevolucionAuditoriaRepository, TjSolicitudDevolucionAuditoriaRepository>();
            services.AddScoped<ISNAuditoriaRepository, SNAuditoriaRepository>();
            services.AddScoped<ISNNoConformidadRepository, SNNoConformidadRepository>();
            services.AddScoped<ISNObjetivoRepository, SNObjetivoRepository>();
            services.AddScoped<ISNRiesgoRepository, SNRiesgoRepository>();
            services.AddScoped<ISNReqLegalRepository, SNReqLegalRepository>();
            services.AddScoped<ISNManualRepository, SNManualRepository>();
            services.AddScoped<IAccesoUsuarioRepository, AccesoUsuarioRepository>();

            return services;
        }
    }
}
