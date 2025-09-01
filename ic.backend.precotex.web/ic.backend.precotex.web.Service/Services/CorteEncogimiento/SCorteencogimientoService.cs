using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ic.backend.precotex.web.Data.Repositories.Implementation.CorteEncogimiento;
using ic.backend.precotex.web.Entity.Entities;
using ic.backend.precotex.web.Service.common;
using ic.backend.precotex.web.Service.Services.Implementacion.CorteEncogimiento;

namespace ic.backend.precotex.web.Service.Services.CorteEncogimiento
{
    public class SCorteencogimientoService : ICorteEncogimientoService
    {
        private readonly ICorteEncogimiento _txICorteEncogimiento;

        public SCorteencogimientoService(ICorteEncogimiento txICorteEncogimiento)
        {
            _txICorteEncogimiento = txICorteEncogimiento;
        }

        public async Task<ServiceResponseList<E_Corte_Encogimiento>?> ListaCorteEncogimiento()
        {
            var result = new ServiceResponseList<E_Corte_Encogimiento>();
            try
            {
                var resultData = await _txICorteEncogimiento.ListaCorteEncogimiento();
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

        public async Task<ServiceResponseList<E_Corte_Encogimiento>?> InsertCorteEncogimiento(string pTipo, string? pCod_Ordtra)
        {
            var result = new ServiceResponseList<E_Corte_Encogimiento>();
            try
            {
                var resultData = await _txICorteEncogimiento.InsertCorteEncogimiento(pTipo, pCod_Ordtra);
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

        public async Task<ServiceResponseList<E_Corte_Encogimiento>?> ListCorteEncogimientoDet(string pTipo, string? pNum_Secuencia, string? pCodPartida, decimal? pAncho_Antes_Lav, decimal? pAlto_Antes_Lav, decimal? pAncho_Despues_Lav, decimal? pAlto_Despues_Lav, decimal? pSesgadura)
        {
            var result = new ServiceResponseList<E_Corte_Encogimiento>();
            try
            {
                var resultData = await _txICorteEncogimiento.ListCorteEncogimientoDet(pTipo, pNum_Secuencia, pCodPartida, pAncho_Antes_Lav, pAlto_Antes_Lav, pAncho_Despues_Lav, pAlto_Despues_Lav, pSesgadura);
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

        public async Task<ServiceResponseList<E_Corte_Encogimiento>?> BuscarCorteEncogimiento(string pCod_Ordtra)
        {
            var result = new ServiceResponseList<E_Corte_Encogimiento>();
            try
            {
                var resultData = await _txICorteEncogimiento.BuscarCorteEncogimiento(pCod_Ordtra);
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

        public async Task<ServiceResponseList<E_Corte_Encogimiento>?> UpdateMedidasTablaMaestra(List<E_Corte_Encogimiento> pData)
        {
            var result = new ServiceResponseList<E_Corte_Encogimiento>();
            try
            {
                var resultData = await _txICorteEncogimiento.UpdateMedidasTablaMaestra(pData);
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


        public async Task<ServiceResponseList<E_Corte_Encogimiento>?> BuscarUsuario(string pUsuario)
        {
            var result = new ServiceResponseList<E_Corte_Encogimiento>();
            try
            {
                var resultData = await _txICorteEncogimiento.BuscarUsuario(pUsuario);
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
