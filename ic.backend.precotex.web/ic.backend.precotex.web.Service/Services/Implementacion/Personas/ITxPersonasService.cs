using ic.backend.precotex.web.Entity.Entities.Personas;
using ic.backend.precotex.web.Service.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Service.Services.Implementacion.Personas
{
    public interface ITxPersonasService
    {
        Task<ServiceResponseList<Tx_Personas>?> ObtenerNombre(string Nro_Dni);
        Task<ServiceResponse<int>> RegistrarDniFoto(Tx_Personas valores);
        Task<ServiceResponse<int>> ActualizarDniFoto(Tx_Personas valores);
        Task<ServiceResponseList<Seg_Camara>?> ObtenerDatosRegistro(string Nro_Dni);
    }
}
