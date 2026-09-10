using System.Collections.Generic;
using System.Threading.Tasks;
using ic.backend.precotex.web.Entity.Entities.Calidad;

namespace ic.backend.precotex.web.Service.Services.Implementacion.Calidad
{
    public interface INoConformidadesService
    {
        Task<List<Dictionary<string, object>>> ListarDatosInformeCalidad(string tipo, string cod = "");
        Task<List<NoConformidades>> MostrarCabecera(string numInforme = "", string fIni = "", string fFin = "", string partida = "");
        Task<List<Dictionary<string, object>>> MostrarPartida(string partida, string tipo = "");
        Task<List<Dictionary<string, object>>> MostrarDetalle(string numInforme = "", string partida = "");
        Task<List<Dictionary<string, object>>> MostrarDetalleMotivo(string numInforme, string partida = "");
        Task<ResponseResultado> GuardarInforme(InformeGuardarRequest req);
        Task<List<Dictionary<string, object>>> ReporteNoConformidad(string fIni = "", string fFin = "");
        Task<List<Dictionary<string, object>>> MostrarEvolutivo();
    }
}
