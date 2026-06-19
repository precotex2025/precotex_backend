using ic.backend.precotex.web.Data.Repositories.Implementation.Tejeduria;
using ic.backend.precotex.web.Entity.Entities.Memorandum;
using ic.backend.precotex.web.Entity.Entities.SolicitudMantenimiento;
using ic.backend.precotex.web.Entity.Entities.Tejeduria;
using ic.backend.precotex.web.Service.common;
using ic.backend.precotex.web.Service.Services.Implementacion.Tejeduria;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ic.backend.precotex.web.Service.Services.Tejeduria
{
    public class TjSeguimientoSaldoHiloService : ITjSeguimientoSaldoHiloService
    {
        private readonly ITjSeguimientoSaldoHiloRepository _tjSeguimientoSaldoHiloRepository;
        public TjSeguimientoSaldoHiloService(ITjSeguimientoSaldoHiloRepository tjSeguimientoSaldoHiloRepository)
        {
            _tjSeguimientoSaldoHiloRepository = tjSeguimientoSaldoHiloRepository;
        }

        public async Task<ServiceResponseList<tj_Muestra_OT_Programada>?> ListaOT_Programada(string Cod_OrdProv, string Cod_HilTel)
        {
            var result = new ServiceResponseList<tj_Muestra_OT_Programada>();
            try
            {
                var resultData = await _tjSeguimientoSaldoHiloRepository.ListaOT_Programada(Cod_OrdProv, Cod_HilTel);
                if (resultData == null || !resultData.Any())
                {
                    result.Success = true;
                    result.Message = "No existe información";
                    return result;
                }

                result.Success = true;
                result.Elements = resultData.ToList();
                result.TotalElements = resultData.ToList().Count();
                return result;
            }
            catch (SqlException sql)
            {
                result.Message = "Error en Servidor: " + sql.Message;
                return result;
            }
            catch (Exception ex)
            {
                result.Message = "Ocurrio una excepción" + ex.Message;
                return result;
            }
        }

        public async Task<ServiceResponseList<tj_Muestra_OT_Terminada>?> ListaOT_Terminada(DateTime Fecha, DateTime Fecha_Fin, string Flg_Pendiente)
        {
            var result = new ServiceResponseList<tj_Muestra_OT_Terminada>();
            try
            {
                var resultData = await _tjSeguimientoSaldoHiloRepository.ListaOT_Terminada(Fecha, Fecha_Fin, Flg_Pendiente);
                if (resultData == null || !resultData.Any())
                {
                    result.Success = true;
                    result.Message = "No existe información";
                    return result;
                }

                result.Success = true;
                result.Elements = resultData.ToList();
                result.TotalElements = resultData.ToList().Count();
                return result;
            }
            catch (SqlException sql)
            {
                result.Message = "Error en Servidor: " + sql.Message;
                return result;
            }
            catch (Exception ex)
            {
                result.Message = "Ocurrio una excepción" + ex.Message;
                return result;
            }
        }

        public async Task<ServiceResponse<int>> Proceso(tj_seguimiento_saldo_hilo_tela tj_Seguimiento_Saldo_Hilo_Tela, string sTipoTransac)
        {
            var result = new ServiceResponse<int>();
            try
            {
                var resultData = await _tjSeguimientoSaldoHiloRepository.Proceso(tj_Seguimiento_Saldo_Hilo_Tela, sTipoTransac);
                if (resultData.Codigo > 0)
                {
                    result.Message = resultData.Mensaje;
                    result.Success = true;
                    result.CodeTransacc = resultData.Codigo;

                    return result;
                }

                result.Message = resultData.Mensaje;
                result.Success = false;
                return result;

            }
            catch (SqlException sql)
            {
                result.Message = "Error en Servidor: " + sql.Message;
                result.Success = false;
                return result;
            }
            catch (Exception ex)
            {
                result.Message = "Ocurrio una excepción" + ex.Message;
                result.Success = false;
                return result;
            }
        }
    }
}
