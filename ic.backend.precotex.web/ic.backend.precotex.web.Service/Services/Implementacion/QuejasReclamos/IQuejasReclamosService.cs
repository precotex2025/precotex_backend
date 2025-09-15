using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ic.backend.precotex.web.Entity.Entities.CalificacionRollosEnProceso;
using ic.backend.precotex.web.Entity.Entities.QuejasReclamos;
using ic.backend.precotex.web.Service.common;
using static ic.backend.precotex.web.Entity.Entities.QuejasReclamos.Clientes;

namespace ic.backend.precotex.web.Service.Services.Implementacion.QuejasReclamos 
{ 

    public interface IQuejasReclamosService
    {
        Task<ServiceResponseList<Cliente>?> ObtenerClintes();
        Task<ServiceResponseList<EstadoDto>?> ObtenerEstado();
        Task<ServiceResponseList<UnidadNegocioDto>?> ObtenerUnidadNegocio();
        Task<ServiceResponseList<ResponsableDto>?> ObtenerResponsable();
        Task<ServiceResponseList<MotivoDto>?> ObtenerMotivo();
        Task<ServiceResponseList<ReclamoClienteDto>?> GuardarReclamo(List<ReclamoClienteDto> reclamo, bool isNew);
        Task<ServiceResponseList<FiltroReclamoDto>?> ObtenerReclamos(FiltroReclamoDto filtro);
        Task<ServiceResponseList<ReclamoClienteDto>?> ObtenerDetReclamos(string nroCaso);
        Task<ServiceResponseList<bool>?> EliminarReclamos(string nroCaso);
        Task<ServiceResponseList<string>?> EliminarReclamoDetalle(string id);
        //Nuevos
        Task<ServiceResponseList<ArticuloDto>?> BuscarPorPartida(string partida);
        Task<ServiceResponseList<UnidadNegocioDto>?> ListaUnidadNegocio();

        Task<ServiceResponseList<AreasDto>?> ListaAreasCalidad();
        Task<ServiceResponse<int>> AvanzaEstadoReclamo(int iId);
        Task<ServiceResponse<int>> ProcesoConfirmarReclamo(string sNroCaso, string sNombreArchivoCalidad, string sObservacionCalidad, string sCodAreaResponsableCalidad, string sCod_Usuario);

        Task<ServiceResponseList<ReclamoTipoConsecuenciaDto>?> ListaTipoConsecuencia();
        Task<ServiceResponseList<ReclamoSubTipoDevolucion>?> ListaSubTipoDevolucion(string sCod_Tipo_Consecuencia);
        Task<ServiceResponse<int>> ProcesoCerrarReclamo(string sNroCaso, string sCod_Tipo_Consecuencia, string sCod_SubTipo_Devolucion, string sFlg_NotaCredito, string sObservacion_Comercial_Cierre, string sCod_Usuario);
        Task<ServiceResponseList<ReclamoUsuarioAreaDto>?> ObtieneUsuarioArea(string Cod_Trabajador);
        Task<ServiceResponseList<InformeCalidadDto>?> ObtieneDetalleInformeCalidad(int Id);
        Task<ServiceResponseList<InformeComercialDto>?> ObtieneDetalleInformeComercial(int Id);
        Task<ServiceResponseList<ReclamoClienteEstadoDto>?> ListaEstados();
        Task<ServiceResponseList<ReclamoExportarDto>?> ExportarReclamo(FiltroReclamoDto filtro);
    }
}
