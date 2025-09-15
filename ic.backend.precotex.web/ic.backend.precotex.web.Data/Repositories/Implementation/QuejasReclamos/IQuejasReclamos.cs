using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ic.backend.precotex.web.Entity.Entities.CalificacionRollosEnProceso;
using ic.backend.precotex.web.Entity.Entities.QuejasReclamos;
using static ic.backend.precotex.web.Entity.Entities.QuejasReclamos.Clientes;


namespace ic.backend.precotex.web.Data.Repositories.Implementation.QuejasReclamos
{
    public interface IQuejasReclamos
    {
        Task<IEnumerable<Cliente>?> ObtenerClintes();
        Task<IEnumerable<EstadoDto>?> ObtenerEstado();
        Task<IEnumerable<UnidadNegocioDto>?> ObtenerUnidadNegocio();
        Task<IEnumerable<ResponsableDto>?> ObtenerResponsable();
        Task<IEnumerable<MotivoDto>?> ObtenerMotivo();
        Task<IEnumerable<ReclamoClienteDto>?> GuardarReclamo(List<ReclamoClienteDto> reclamo, bool isNew);
        Task<IEnumerable<FiltroReclamoDto>?> ObtenerReclamos(FiltroReclamoDto filtro);
        Task<IEnumerable<ReclamoClienteDto>?> ObtenerDetReclamos(string nroCaso);
        Task<IEnumerable<bool>?> EliminarReclamos(string nroCaso);
        Task<IEnumerable<string>?> EliminarReclamoDetalle(string id);

        //Nuevos
        Task<IEnumerable<ArticuloDto>?> BuscarPorPartida(string partida);
        Task<IEnumerable<UnidadNegocioDto>?> ListaUnidadNegocio();
        Task<IEnumerable<AreasDto>?> ListaAreasCalidad();
        Task<(int Codigo, string Mensaje)> AvanzaEstadoReclamo(int iId);
        Task<(int Codigo, string Mensaje)> ProcesoConfirmarReclamo(string sNroCaso, string sNombreArchivoCalidad, string sObservacionCalidad, string sCodAreaResponsableCalidad, string sCod_Usuario);

        Task<IEnumerable<ReclamoTipoConsecuenciaDto>?> ListaTipoConsecuencia();
        Task<IEnumerable<ReclamoSubTipoDevolucion>?> ListaSubTipoDevolucion(string sCod_Tipo_Consecuencia);
        Task<(int Codigo, string Mensaje)> ProcesoCerrarReclamo(string sNroCaso, string sCod_Tipo_Consecuencia, string sCod_SubTipo_Devolucion, string sFlg_NotaCredito, string sObservacion_Comercial_Cierre, string sCod_Usuario);
        Task<IEnumerable<ReclamoUsuarioAreaDto>?> ObtieneUsuarioArea(string Cod_Trabajador);
        Task<IEnumerable<InformeCalidadDto>?> ObtieneDetalleInformeCalidad(int Id);
        Task<IEnumerable<InformeComercialDto>?> ObtieneDetalleInformeComercial(int Id);
        Task<IEnumerable<ReclamoClienteEstadoDto>?> ListaEstados();
        Task<IEnumerable<ReclamoExportarDto>?> ExportarReclamo(FiltroReclamoDto filtro);
    }
}
