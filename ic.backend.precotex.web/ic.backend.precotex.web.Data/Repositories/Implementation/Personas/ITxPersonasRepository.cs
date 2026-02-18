using ic.backend.precotex.web.Entity.Entities.Personas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Data.Repositories.Implementation.Personas
{
    public interface ITxPersonasRepository
    {
        Task<IEnumerable<Tx_Personas>?> ObtenerNombre(string Nro_Dni);
        Task<(int Codigo, string Mensaje)> RegistrarDniFoto(Tx_Personas valores);
        Task<(int Codigo, string Mensaje)> ActualizarDniFoto(Tx_Personas valores);
        Task<IEnumerable<Seg_Camara>?> ObtenerDatosRegistro(int Id_Marcacion, string Nro_Dni);
        Task<IEnumerable<Seg_Camara>?> ObtenerMarcación1p1();
    }
}
