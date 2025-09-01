using ic.backend.precotex.web.Data.Repositories.Implementation.Tejeduria;
using ic.backend.precotex.web.Data.Repositories.Tejeduria;
using ic.backend.precotex.web.Entity.Entities;
using ic.backend.precotex.web.Service.common;
using ic.backend.precotex.web.Service.Services.Implementacion.Tejeduria;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ic.backend.precotex.web.Service.Services.Tejeduria
{
    public class TxCtrolInventarioHiloTejeduriaService : ITxCtrolInventarioHiloTejeduriaService
    {
        private readonly ITxCtrolInventarioHiloTejeduriaRepository _txCtrolInventarioHiloTejeduriaRepository;
        public TxCtrolInventarioHiloTejeduriaService(ITxCtrolInventarioHiloTejeduriaRepository txCtrolInventarioHiloTejeduriaRepository)
        {
            _txCtrolInventarioHiloTejeduriaRepository = txCtrolInventarioHiloTejeduriaRepository;
        }
        public async Task<ServiceResponse<int>> CrudCtrolInventarioHiloTejeduria(Tx_Ctrol_Inventario_Hilo_Tejeduria tx_Ctrol_Inventario_Hilo_Tejeduria, string sTipoTransac)
        {
            var result = new ServiceResponse<int>();
            try
            {
                var resultData = await _txCtrolInventarioHiloTejeduriaRepository.CrudCtrolInventarioHiloTejeduria(tx_Ctrol_Inventario_Hilo_Tejeduria, sTipoTransac);
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

        public async Task<ServiceResponseList<Tx_Ctrol_Inventario_Hilo_Tejeduria>?> ObtenerCtrolInventarioHiloTejeduriaByLote(string? Lote)
        {
            var result = new ServiceResponseList<Tx_Ctrol_Inventario_Hilo_Tejeduria>();
            try
            {
                var resultData = await _txCtrolInventarioHiloTejeduriaRepository.ObtenerCtrolInventarioHiloTejeduriaByLote(Lote);
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
    }
}
