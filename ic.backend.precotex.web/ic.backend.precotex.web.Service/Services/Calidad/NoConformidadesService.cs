using System.Collections.Generic;
using System.Threading.Tasks;
using ic.backend.precotex.web.Data.Repositories.Implementation.Calidad;
using ic.backend.precotex.web.Entity.Entities.Calidad;
using ic.backend.precotex.web.Service.Services.Implementacion.Calidad;

namespace ic.backend.precotex.web.Service.Services.Calidad
{
    public class NoConformidadesService : INoConformidadesService
    {
        private readonly INoConformidadesRepository _repo;

        public NoConformidadesService(INoConformidadesRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<Dictionary<string, object>>> ListarDatosInformeCalidad(string tipo, string cod = "")
        {
            return await _repo.ListarDatosInformeCalidad(tipo, cod);
        }

        public async Task<List<NoConformidades>> MostrarCabecera(string numInforme = "", string fIni = "", string fFin = "", string partida = "")
        {
            return await _repo.MostrarCabecera(numInforme, fIni, fFin, partida);
        }

        public async Task<List<Dictionary<string, object>>> MostrarPartida(string partida, string tipo = "")
        {
            return await _repo.MostrarPartida(partida, tipo);
        }

        public async Task<List<Dictionary<string, object>>> MostrarDetalle(string numInforme = "", string partida = "")
        {
            return await _repo.MostrarDetalle(numInforme, partida);
        }

        public async Task<List<Dictionary<string, object>>> MostrarDetalleMotivo(string numInforme, string partida = "")
        {
            return await _repo.MostrarDetalleMotivo(numInforme, partida);
        }

        public async Task<ResponseResultado> GuardarInforme(InformeGuardarRequest req)
        {
            return await _repo.GuardarTransaccionCompleta(req);
        }

        public async Task<List<Dictionary<string, object>>> ReporteNoConformidad(string fIni = "", string fFin = "")
        {
            return await _repo.ReporteNoConformidad(fIni, fFin);
        }

        public async Task<List<Dictionary<string, object>>> MostrarEvolutivo()
        {
            return await _repo.MostrarEvolutivo();
        }
    }
}
