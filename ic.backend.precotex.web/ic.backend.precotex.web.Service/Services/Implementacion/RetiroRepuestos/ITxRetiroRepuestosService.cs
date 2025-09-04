using ic.backend.precotex.web.Entity.Entities.RetiroRepuestos;
using ic.backend.precotex.web.Service.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Service.Services.Implementacion.RetiroRepuestos
{
    public interface ITxRetiroRepuestosService
    {
        Task<ServiceResponseList<Tx_Retiro_Repuestos>?> ListaRetiros(DateTime FecIni, DateTime FecFin);
        Task<ServiceResponseList<Tx_Retiro_Repuestos>?> ListaRetirosPorNumRequerimiento(int Num_Requerimiento);
        Task<ServiceResponse<int>> RegistrarRequerimiento(Tx_Retiro_Repuestos tx_Retiro_Repuestos);
        Task<ServiceResponse<int>> ActualizarRequerimiento(Tx_Retiro_Repuestos tx_Retiro_Repuestos);
        Task<ServiceResponse<int>> ActualizarRequerimientoPrecintoCierre(Tx_Retiro_Repuestos tx_Retiro_Repuestos);
        Task<ServiceResponseList<Tx_Retiro_Repuestos_Detalle>?> ListaRetiroDetallePorNumRequerimiento(int Num_Requerimiento);
        Task<ServiceResponseList<Tx_Retiro_Repuestos_Detalle>?> ListaRetiroDetallePorNumReqySecuencia(int Num_Requerimiento, int Nro_Secuencia);
        Task<ServiceResponse<int>> RegistrarRequerimientoDetalle(Tx_Retiro_Repuestos_Detalle tx_Retiro_Repuestos_Detalle);
        Task<ServiceResponse<int>> ActualizarRequerimientoDetalle(Tx_Retiro_Repuestos_Detalle tx_Retiro_Repuestos_Detalle);

    }
}
