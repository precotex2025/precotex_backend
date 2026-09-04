using System;
using System.Collections.Generic;
using ic.backend.precotex.web.Data.Repositories.Calidad;
using ic.backend.precotex.web.Entity.Entities.Calidad;
using Microsoft.Extensions.Logging;

namespace ic.backend.precotex.web.Service.Services.Calidad
{
    public interface INoConformidadesService
    {
        List<Dictionary<string, object>> ListarDatosInformeCalidad(string tipo, string cod = "");
        List<Dictionary<string, object>> MostrarCabecera(string numInforme = "", string fIni = "", string fFin = "", string partida = "");
        List<Dictionary<string, object>> MostrarPartida(string partida, string tipo = "");
        List<Dictionary<string, object>> MostrarDetalle(string numInforme = "", string partida = "");
        List<Dictionary<string, object>> MostrarDetalleMotivo(string numInforme, string partida = "");
        ResponseResultado GuardarInforme(InformeGuardarRequest request);
    }

    public class NoConformidadesService : INoConformidadesService
    {
        private readonly NoConformidadesRepository _repo;
        private readonly ILogger<NoConformidadesService> _logger;

        public NoConformidadesService(NoConformidadesRepository repo, ILogger<NoConformidadesService> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        public List<Dictionary<string, object>> ListarDatosInformeCalidad(string tipo, string cod = "")
        {
            if (string.IsNullOrEmpty(tipo)) throw new ArgumentException("El tipo es obligatorio.");
            return _repo.ListarDatosInformeCalidad(tipo, cod);
        }

        public List<Dictionary<string, object>> MostrarCabecera(string numInforme = "", string fIni = "", string fFin = "", string partida = "")
        {
            return _repo.MostrarCabecera(numInforme, fIni, fFin, partida);
        }

        public List<Dictionary<string, object>> MostrarPartida(string partida, string tipo = "")
        {
            if (string.IsNullOrEmpty(partida)) throw new ArgumentException("La partida es obligatoria.");
            return _repo.MostrarPartida(partida, tipo);
        }

        public List<Dictionary<string, object>> MostrarDetalle(string numInforme = "", string partida = "")
        {
            return _repo.MostrarDetalle(numInforme, partida);
        }

        public List<Dictionary<string, object>> MostrarDetalleMotivo(string numInforme, string partida = "")
        {
            return _repo.MostrarDetalleMotivo(numInforme, partida);
        }

        public ResponseResultado GuardarInforme(InformeGuardarRequest request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            if (string.IsNullOrEmpty(request.Accion)) throw new ArgumentException("La acción es obligatoria.");
            if (string.IsNullOrEmpty(request.Cod_OrdPro)) throw new ArgumentException("La partida es obligatoria.");

            return _repo.GuardarTransaccionCompleta(request);
        }
    }
}
